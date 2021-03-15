using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Irony;
using Irony.Parsing;
using System.Text;
using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using Proyecto1.Ejecutor.Instrucciones;
using Proyecto1.Ejecutor.Instrucciones.Sentencias;
using Proyecto1.Ejecutor.Instrucciones.funciones;
using System.Windows.Forms;

namespace Proyecto1.Ejecutor.Analizador
{
    class Sintactico_ejecutar
    {
        public List<string> salida = new List<string>();
        private int indice_nodo = 0;
        public ParseTreeNode nodoglobal = null;
        public ParseTreeNode Analizar(string entrada, Grammar gramatica)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;
            nodoglobal = raiz;
            if (raiz != null && arbol.ParserMessages.Count == 0)
            {
                LinkedList<Instruccion> arbol_AST = Instrucciones(raiz.ChildNodes.ElementAt(0));
                //graficar_TS(raiz);
                if (arbol_AST != null)
                {
                    Ejecutar(arbol_AST);
                }
            }
            else
            {
                salida = lst_Error(arbol);
            }

            return null;
        }
        //verificar 
        private void Ejecutar(LinkedList<Instruccion> AST)
        {

            if (typeof(Pprogram).IsInstanceOfType(AST.ElementAt(0)))
            {
                TablaDeSimbolos global = new TablaDeSimbolos();
                foreach (Instruccion inst in AST)
                {
                    inst.Ejecutar(global); 
                }
                Program.tablageneral = global;
            }
            else
            {
                Console.WriteLine("No viene la instruccion Program");
            }
        }

        private List<string> lst_Error(ParseTree arbol)
        {

            List<string> salida = new List<string>();
            foreach (LogMessage item in arbol.ParserMessages)
            {
                if (item.Message.ToString().Contains("Invalid character"))
                {
                    salida.Add("Error Lexico en: " + item.Location + " " + item.Message.ToString());
                }
                else
                {
                    salida.Add("Error Sintactico en: " + item.Location + " " + item.Message.ToString());
                }
            }


            return salida;
        }
        private LinkedList<Instruccion> Instrucciones(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = Instrucciones(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(Instruccion(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(Instruccion(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return null;
        }

        private Instruccion Instruccion(ParseTreeNode node)
        {
            string produccion = node.ChildNodes.ElementAt(0).Term.Name;
            switch (produccion)
            {
                case "Pprogram":
                    return PPROGRAM(node.ChildNodes.ElementAt(0));
                case "PDeclaracion":
                    return DECLARACION(node.ChildNodes.ElementAt(0));
                case "Ptype":
                    return PTYPE(node.ChildNodes.ElementAt(0));
                case "PBegin":
                    return PBEGIN(node.ChildNodes.ElementAt(0));
                case "PFunciones":
                    return PFUNCIONES(node.ChildNodes.ElementAt(0));
                case "PProcedure":
                    return PPROCEDURE(node.ChildNodes.ElementAt(0));
                case "PInstruccionNativa":
                    return COMANDOSNATIVOS(node.ChildNodes.ElementAt(0));

            }
            return null;
        }
        private Instruccion LLAMARMETODO(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 4)
            {
                string porduccion = nodo.ChildNodes.ElementAt(2).Term.Name;
                if (porduccion == "Valores")
                {
                    return new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), VALORES(nodo.ChildNodes.ElementAt(2)));
                }
                else { return new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()); }

            }
            else if (nodo.ChildNodes.Count == 3)
            {
                return new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString());
            }
            else
            {
                return new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), VALORES(nodo.ChildNodes.ElementAt(2)));
            }
        }

        private Instruccion COMANDOSNATIVOS(ParseTreeNode node)
        {
            string produccion = node.ChildNodes.ElementAt(0).Term.Name;
            switch (produccion)
            {
                case "graficar_ts":
                    return new GraficarTS();
                case "exit":
                    return new Instruccion_Exit(POPERACION(node.ChildNodes.ElementAt(2)));
                case "SentenciaWrite":
                    return SENTENCIAWRITE(node.ChildNodes.ElementAt(0));
                case "SentenciaWriteln":
                    return SENTENCIAWRITELN(node.ChildNodes.ElementAt(0));


            }
            return null;
        }


        private Instruccion SENTENCIAWRITE(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return new SentenciaWrite(POPERACION(nodo.ChildNodes.ElementAt(2)), VALORES(nodo.ChildNodes.ElementAt(4)));
            }
            else
            {
                return new SentenciaWrite(POPERACION(nodo.ChildNodes.ElementAt(2)));
            }
        }

        private LinkedList<Operacion> VALORES(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                LinkedList<Operacion> lista = VALORES(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(POPERACION(nodo.ChildNodes.ElementAt(2)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Operacion> lista = new LinkedList<Operacion>();
                lista.AddLast(POPERACION(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return null;
        }



        private Instruccion SENTENCIAWRITELN(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return new SentenciaWriteln(POPERACION(nodo.ChildNodes.ElementAt(2)), VALORES(nodo.ChildNodes.ElementAt(4)));
            }
            else
            {
                return new SentenciaWriteln(POPERACION(nodo.ChildNodes.ElementAt(2)));
            }
        }

        private Instruccion PBEGIN(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 4)
            {
                return new PBegin(SENTENCIAS(nodo.ChildNodes.ElementAt(1)));
            }
            else
            {
                return null;
            }
        }

        private Instruccion PPROCEDURE(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Instruccion_Procedimiento(nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString(), PROCEDIMIENTOS2(nodo.ChildNodes.ElementAt(2)), PROCEDIMIENTOSAUX(nodo.ChildNodes.ElementAt(2)));
            }


            return null;

        }

        private LinkedList<Atributo> PROCEDIMIENTOS2(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return ATRIBUTOS(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return null;
            }
        }
        private LinkedList<Instruccion> PROCEDIMIENTOSAUX(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(4));
            }
            else if (nodo.ChildNodes.Count == 2)
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(3));
            }
        }
        private Instruccion PFUNCIONES(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Instruccion_Funcion(nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString(), FUNCIONES2(nodo.ChildNodes.ElementAt(2)), FUNCIONAUX(nodo.ChildNodes.ElementAt(2)), ObetenerTipo(nodo.ChildNodes.ElementAt(2)));
            }


            return null;

        }
        private LinkedList<Instruccion> FUNCIONAUX(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(6));
            }
            else if (nodo.ChildNodes.Count == 4)
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(3));
            }
            else
            {
                return PINSTRUCCIONES(nodo.ChildNodes.ElementAt(5));
            }
        }

        private LinkedList<Instruccion> PINSTRUCCIONES(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(DECLARACION(nodo.ChildNodes.ElementAt(0)));
                lista.AddLast(PINSTRUCCIONAUX(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {

                return PINSTRUCCION(nodo.ChildNodes.ElementAt(0));
            }
            return null;
        }
        private Instruccion PINSTRUCCIONAUX(ParseTreeNode nodo)
        {
            return new Sentencias(SENTENCIAS(nodo.ChildNodes.ElementAt(1)));
        }
        private LinkedList<Instruccion> PINSTRUCCION(ParseTreeNode node)
        {
            return SENTENCIAS(node.ChildNodes.ElementAt(1));
        }


        private LinkedList<Atributo> FUNCIONES2(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return ATRIBUTOS(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return null;
            }
        }

        private LinkedList<Atributo> ATRIBUTOS(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                LinkedList<Atributo> lista = ATRIBUTOS(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(PATRIBUTO(nodo.ChildNodes.ElementAt(2)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Atributo> lista = new LinkedList<Atributo>();
                lista.AddLast(PATRIBUTO(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return null;
        }

        private Atributo PATRIBUTO(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                return new Atributo(PIDS(nodo.ChildNodes.ElementAt(0)), Tipo.VALOR, PATRIBUTOS2(nodo.ChildNodes.ElementAt(1)));

            }
            else if (nodo.ChildNodes.Count == 3)
            {
                return new Atributo(PIDS(nodo.ChildNodes.ElementAt(1)), Tipo.REFERENCIA, PATRIBUTOS2(nodo.ChildNodes.ElementAt(2)));
            }

            return null;
        }
        private Tipo PATRIBUTOS2(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                return BuscarTipo(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return Tipo.SINTIPO;
            }
        }
        private Tipo ObetenerTipo(ParseTreeNode nodo)
        {

            if (nodo.ChildNodes.Count == 7)
            {
                return BuscarTipo(nodo.ChildNodes.ElementAt(4));
            }
            else if (nodo.ChildNodes.Count == 4)
            {
                return BuscarTipo(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return BuscarTipo(nodo.ChildNodes.ElementAt(3));
            }


        }

        private LinkedList<Instruccion> SENTENCIAS(ParseTreeNode nodo)
        {

            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = SENTENCIAS(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(SENTENCIA(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(SENTENCIA(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return null;
        }

        private Instruccion SENTENCIA(ParseTreeNode node)
        {
            string produccion = node.ChildNodes.ElementAt(0).Term.Name;
            switch (produccion)
            {
                case "PAsignacion":
                    return SENTENCIAS_ASIGNACION(node.ChildNodes.ElementAt(0));
                case "SentenciaIf":
                    return SENTENCIAIF(node.ChildNodes.ElementAt(0));
                case "SentenciaSwitch":
                    return SENTENCIASW(node.ChildNodes.ElementAt(0));
                case "SentenciaWhile":
                    return SENTENCIASWHILE(node.ChildNodes.ElementAt(0));
                case "SentenciaFor":
                    return SENTENCIAFOR(node.ChildNodes.ElementAt(0));
                case "SentenciaRepeat":
                    return SENTENCIASREPEAT(node.ChildNodes.ElementAt(0));
                case "SentenciasBreak":
                    return new SentenciasBreak();
                case "SentenciaContinue":
                    return new SentenciasContinue();
                case "PInstruccionNativa":
                    return COMANDOSNATIVOS(node.ChildNodes.ElementAt(0));
                case "CallMetodo":
                    return LLAMARMETODO(node.ChildNodes.ElementAt(0));
            }
            return null;
        }

        private Instruccion SENTENCIASREPEAT(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return new SenteciaRepeat(POPERACION(nodo.ChildNodes.ElementAt(3)), SENTENCIAS(nodo.ChildNodes.ElementAt(1)));
            }
            else
            {
                return new SenteciaRepeat(POPERACION(nodo.ChildNodes.ElementAt(6)), SENTENCIAS(nodo.ChildNodes.ElementAt(2)));
            }
        }


        private Instruccion SENTENCIAFOR(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 6)
            {
                return new SentenciaFor(INICIALIZACION(nodo.ChildNodes.ElementAt(1)), POPERACION(nodo.ChildNodes.ElementAt(3)), SENTENCIAS(nodo.ChildNodes.ElementAt(5)));
            }
            else
            {
                return new SentenciaFor(INICIALIZACION(nodo.ChildNodes.ElementAt(1)), POPERACION(nodo.ChildNodes.ElementAt(3)), SENTENCIAS(nodo.ChildNodes.ElementAt(6)));
            }
        }

        private Asignacion INICIALIZACION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Asignacion(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(2)));
            }
            return null;
        }

        private Instruccion SENTENCIASWHILE(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return new SentenciaWhile(POPERACION(nodo.ChildNodes.ElementAt(1)), SENTENCIAS(nodo.ChildNodes.ElementAt(4)));
            }
            return null;
        }

        private Instruccion SENTENCIASW(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 8)
            {
                return new Sentencia_Case(POPERACION(nodo.ChildNodes.ElementAt(2)), PCASOS(nodo.ChildNodes.ElementAt(5)));
            }
            else if (nodo.ChildNodes.Count == 13)
            {
                return new Sentencia_Case(POPERACION(nodo.ChildNodes.ElementAt(2)), PCASOS(nodo.ChildNodes.ElementAt(5)), new tipo_else(SENTENCIAS(nodo.ChildNodes.ElementAt(8))));
            }
            return null;
        }

        private LinkedList<Caso> PCASOS(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Caso> lista = PCASOS(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(PCASO(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Caso> lista = new LinkedList<Caso>();
                lista.AddLast(PCASO(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return null;
        }

        private Caso PCASO(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Caso(CASOS(nodo.ChildNodes.ElementAt(0)), SENTENCIA(nodo.ChildNodes.ElementAt(2)));
            }
            else if (nodo.ChildNodes.Count == 6)
            {
                return new Caso(CASOS(nodo.ChildNodes.ElementAt(0)), SENTENCIAS(nodo.ChildNodes.ElementAt(3)));
            }
            return null;
        }

        private LinkedList<Operacion> CASOS(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                LinkedList<Operacion> lista = CASOS(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(POPERACION(nodo.ChildNodes.ElementAt(2)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Operacion> lista = new LinkedList<Operacion>();
                lista.AddLast(POPERACION(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
            return null;
        }
        private Instruccion SENTENCIAIF(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 7)
            {
                return new Sentencia_IF(PCONDICION(nodo.ChildNodes.ElementAt(1)), SENTENCIAS(nodo.ChildNodes.ElementAt(4)));
            }
            else if (nodo.ChildNodes.Count == 12)
            {
                return new Sentencia_IF(PCONDICION(nodo.ChildNodes.ElementAt(1)), SENTENCIAS(nodo.ChildNodes.ElementAt(4)), new tipo_else(SENTENCIAS(nodo.ChildNodes.ElementAt(9))));
            }
            else if (nodo.ChildNodes.Count == 8)
            {
                return new Sentencia_IF(PCONDICION(nodo.ChildNodes.ElementAt(1)), SENTENCIAS(nodo.ChildNodes.ElementAt(4)), ELSE_IF(nodo.ChildNodes.ElementAt(7)));
            }
            else if (nodo.ChildNodes.Count == 13)
            {
                return new Sentencia_IF(PCONDICION(nodo.ChildNodes.ElementAt(1)), SENTENCIAS(nodo.ChildNodes.ElementAt(4)), new tipo_else(SENTENCIAS(nodo.ChildNodes.ElementAt(10))), ELSE_IF(nodo.ChildNodes.ElementAt(7)));
            }
            return null;
        }
        private Operacion PCONDICION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return POPERACION(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return POPERACION(nodo.ChildNodes.ElementAt(0));
            }
        }

        private LinkedList<else_if> ELSE_IF(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<else_if> lista = ELSE_IF(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(ELIF(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<else_if> lista = new LinkedList<else_if>();
                lista.AddLast(ELIF(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
            return null;
        }

        private else_if ELIF(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 8)
            {
                return new else_if(PCONDICION(nodo.ChildNodes.ElementAt(2)), SENTENCIAS(nodo.ChildNodes.ElementAt(5)));
            }
            return null;
        }

        private Instruccion SENTENCIAS_ASIGNACION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                Instruccion asignacion = ASIGNACIONAUX(nodo.ChildNodes.ElementAt(1), nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString());
                return asignacion;
            }
            return null;
        }
        private Instruccion ASIGNACIONAUX(ParseTreeNode nodo, string id)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Asignacion(id, OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(1)));
            }
            else if (nodo.ChildNodes.Count == 2)
            {
                return new Asignacion(id, LLAMARMETODO(nodo.ChildNodes.ElementAt(1)));
            }
            else
            {
                return new Asignacion(id, nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString(), OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(3)));
            }
        }

        private Instruccion PPROGRAM(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return new Pprogram();
            }
            return null;
        }

        private Instruccion DECLARACION(ParseTreeNode nodo)
        {
            LinkedList<Instruccion> lst_declaracione = new LinkedList<Instruccion>();
            if (nodo.ChildNodes.Count == 5)
            {
                string nombre = nodo.ChildNodes.ElementAt(0).Term.Name;
                if (nombre.ToLower() == "var")
                {
                    Tipo obtener = BuscarTipo(nodo.ChildNodes.ElementAt(3));
                    string produccion = nodo.ChildNodes.ElementAt(1).Term.Name;
                    string produccion_Declaraciones = nodo.ChildNodes.ElementAt(4).Term.Name;
                    Operacion valor = null;
                    if (produccion_Declaraciones == "Declaraciones")
                    {
                        valor = DECLARACIONES(nodo.ChildNodes.ElementAt(4));
                    }
                    if (produccion == "Pids")
                    {
                        return new Declaracion(PIDS(nodo.ChildNodes.ElementAt(1)), obtener, valor);
                    }

                }
                else
                {
                    Tipo obtener = BuscarTipo(nodo.ChildNodes.ElementAt(3));
                    string produccion = nodo.ChildNodes.ElementAt(1).Term.Name;
                    string produccion_Declaraciones = nodo.ChildNodes.ElementAt(4).Term.Name;
                    Operacion valor = null;
                    if (produccion_Declaraciones == "Declaraciones")
                    {
                        valor = DECLARACIONES(nodo.ChildNodes.ElementAt(4));
                    }
                    return new Declaracion(PIDS(nodo.ChildNodes.ElementAt(1)), obtener, valor);
                }

            }
            else if (nodo.ChildNodes.Count == 4)
            {
                Tipo obtener = BuscarTipo(nodo.ChildNodes.ElementAt(2));
                string produccion = nodo.ChildNodes.ElementAt(0).Term.Name;
                string produccion_Declaraciones = nodo.ChildNodes.ElementAt(3).Term.Name;
                Operacion valor = null;
                if (produccion_Declaraciones == "Declaraciones")
                {
                    valor = DECLARACIONES(nodo.ChildNodes.ElementAt(3));
                }
                if (produccion == "Pids")
                {
                    return new Declaracion(PIDS(nodo.ChildNodes.ElementAt(0)), obtener, valor);
                }
            }

            return null;
        }
        private Operacion DECLARACIONES(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                return OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(1));
            }
            return null;
        }

        private Operacion OPERACION_RELACIONAL(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                string operador = nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString();
                switch (operador)
                {
                    case ">":
                        return new Operacion(Tipo.MAYOR, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "<":
                        return new Operacion(Tipo.MENOR, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case ">=":
                        return new Operacion(Tipo.MAYORIGUAL, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "<=":
                        return new Operacion(Tipo.MENORIGUAL, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "<>":
                        return new Operacion(Tipo.DIFERENTE, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "=":
                        return new Operacion(Tipo.IGUAL, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                }
            }
            return OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0));
        }

        private Operacion OPERACION_NUMERICA(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                string op = nodo.ChildNodes.ElementAt(1).Term.Name;
                if (op != "Operacion" && nodo.ChildNodes.ElementAt(0).Term.Name.ToLower() != "id")
                {
                    string operador = nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString();
                    switch (operador)
                    {
                        case "*":
                            return new Operacion(Tipo.MULTIPLICACION, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                        case "/":
                            return new Operacion(Tipo.DIVISION, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                        case "+":
                            return new Operacion(Tipo.MAS, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                        case "-":
                            return new Operacion(Tipo.MENOS, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                        case "%":
                            return new Operacion(Tipo.MODULO, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                        default:
                            return null;
                    }
                }
                else
                {
                    string operador = nodo.ChildNodes.ElementAt(1).Term.Name;
                    if (operador == "Operacion")
                    {
                        return POPERACION(nodo.ChildNodes.ElementAt(1));
                    }
                    else
                    {
                        return new Operacion(new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.ID_FUNCION);
                    }
                }
            }
            else if (nodo.ChildNodes.Count == 2)
            {
                return new Operacion(Tipo.NEGATIVO, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(1)));
            }
            else if (nodo.ChildNodes.Count == 4)
            {
                return new Operacion(new SentenciaLlamar(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), VALORES(nodo.ChildNodes.ElementAt(2))), Tipo.ID_FUNCIONVALORES);
            }
            else
            {
                return PVALOR(nodo.ChildNodes.ElementAt(0));
            }
        }

        private Operacion POPERACION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                string operador = nodo.ChildNodes.ElementAt(1).Term.Name;
                switch (operador.ToLower())
                {
                    case "and":
                        return new Operacion(Tipo.AND, POPERACION(nodo.ChildNodes.ElementAt(0)), POPERACION(nodo.ChildNodes.ElementAt(2)));
                    case "or":
                        return new Operacion(Tipo.OR, POPERACION(nodo.ChildNodes.ElementAt(0)), POPERACION(nodo.ChildNodes.ElementAt(2)));
                }
                return null;
            }
            else if (nodo.ChildNodes.Count == 2)
            {
                return new Operacion(Tipo.NOT, POPERACION(nodo.ChildNodes.ElementAt(1)));
            }
            else
            {
                return OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(0));
            }
        }

        private Operacion PVALOR(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 1)
            {
                string produccion = nodo.ChildNodes.ElementAt(0).Term.Name;
                switch (produccion.ToLower())
                {
                    case "numero":
                        return new Operacion(Double.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.ENTERO);
                    case "cadena":
                        return new Operacion(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), Tipo.CADENA);
                    case "decimal":
                        return new Operacion(Double.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.DECIMAL);
                    case "true":
                        return new Operacion(Boolean.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.TRUE);
                    case "false":
                        return new Operacion(Boolean.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.FALSE);
                    case "id":
                        return new Operacion(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), Tipo.ID);
                }
            }
            return null;
        }

        private Instruccion PTYPE(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                return POBJETO(nodo.ChildNodes.ElementAt(1));
            }
            else
            {
                return null;
            }
        }

        private Instruccion POBJETO(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 4)
            {
                string produccion = nodo.ChildNodes.ElementAt(0).Term.Name;
                if (produccion == "Decla")
                {
                    string id_objeto = PDECLA(nodo.ChildNodes.ElementAt(0));
                    LinkedList<Instruccion> lst_Declaraciones = DECLARACIONES2(nodo.ChildNodes.ElementAt(1));
                    return new Ptype(id_objeto, lst_Declaraciones);
                }
            }
            else if (nodo.ChildNodes.Count == 5)
            {
                bool verificar = verificarTipoIndice(nodo.ChildNodes.ElementAt(3));
                if (verificar)
                {
                    if (verificarTipoElemento(nodo.ChildNodes.ElementAt(3)))
                    {
                        return new Parray(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), PARRAYTIPO(nodo.ChildNodes.ElementAt(3)), PARRAYTIPOT(nodo.ChildNodes.ElementAt(3)));
                    }
                    else
                    {
                        return new Parray(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), PARRAYTIPO(nodo.ChildNodes.ElementAt(3)), PARRAYOPERACIONT(nodo.ChildNodes.ElementAt(3)));
                    }

                }
                else
                {
                    if (verificarTipoElemento(nodo.ChildNodes.ElementAt(3)))
                    {
                        return new Parray(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), PARRAYOPERACION(nodo.ChildNodes.ElementAt(3)), PARRAYTIPOT(nodo.ChildNodes.ElementAt(3)));
                    }
                    else
                    {
                        return new Parray(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString(), PARRAYOPERACION(nodo.ChildNodes.ElementAt(3)), PARRAYOPERACIONT(nodo.ChildNodes.ElementAt(3)));
                    }
                }

            }

            return null;
        }

        //indice
        private LinkedList<Operacion> PARRAYOPERACION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return PINDICE(nodo.ChildNodes.ElementAt(1));
            }
            else if (nodo.ChildNodes.Count == 6)
            {
                return PINDICE(nodo.ChildNodes.ElementAt(2));
            }
            return null;
        }
        private Tipo PARRAYTIPO(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return PINDICET(nodo.ChildNodes.ElementAt(1));
            }
            else if (nodo.ChildNodes.Count == 6)
            {
                return PINDICET(nodo.ChildNodes.ElementAt(2));
            }
            return Tipo.NOENCONTRADO;
        }
        //elemento
        private LinkedList<Operacion> PARRAYOPERACIONT(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return PINDICE(nodo.ChildNodes.ElementAt(4));
            }
            else if (nodo.ChildNodes.Count == 6)
            {
                return PINDICE(nodo.ChildNodes.ElementAt(5));
            }
            return null;
        }
        private Tipo PARRAYTIPOT(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                return PINDICET(nodo.ChildNodes.ElementAt(4));
            }
            else if (nodo.ChildNodes.Count == 6)
            {
                return PINDICET(nodo.ChildNodes.ElementAt(5));
            }
            return Tipo.NOENCONTRADO;
        }


        private LinkedList<Operacion> PINDICE(ParseTreeNode nodo)
        {
            LinkedList<Operacion> lst_indices = new LinkedList<Operacion>();
            if (nodo.ChildNodes.Count == 4)
            {
                lst_indices.AddLast(OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(0)));
                lst_indices.AddLast(OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(3)));
                return lst_indices;
            }
            return null;
        }
        private Tipo PINDICET(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 1)
            {
                return BuscarTipo(nodo.ChildNodes.ElementAt(0));
            }
            return Tipo.NOENCONTRADO;
        }


        private string PDECLA(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 4)
            {
                return nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString();
            }
            return "";
        }

        private LinkedList<Instruccion> DECLARACIONES2(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = DECLARACIONES2(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(DECLARACION(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(DECLARACION(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
            return null;
        }

        private Tipo BuscarTipo(ParseTreeNode nodo)
        {
            string tipo = nodo.ChildNodes.ElementAt(0).Term.Name;
            switch (tipo.ToLower())
            {
                case "integer":
                    return Tipo.INTEGER;
                case "real":
                    return Tipo.REAL;
                case "boolean":
                    return Tipo.BOOLEAN;
                case "string":
                    return Tipo.STRING;
                case "id":
                    return Tipo.ID;
                case "object":
                    return Tipo.OBJECT;
            }
            return Tipo.NOENCONTRADO;
        }
        private LinkedList<string> PIDS(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                LinkedList<string> lst = PIDS(nodo.ChildNodes.ElementAt(0));
                lst.AddLast(nodo.ChildNodes.ElementAt(2).Token.ValueString.ToString());
                return lst;
            }
            else
            {
                LinkedList<string> lst = new LinkedList<string>();
                lst.AddLast(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString());
                return lst;

            }
        }
        private bool verificarTipoIndice(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                string produccion = nodo.ChildNodes.ElementAt(1).Term.Name;
                if (produccion == "Pindice")
                {
                    return verificartipo(nodo.ChildNodes.ElementAt(1));
                }
                else
                {
                    return false;
                }

            }
            else if (nodo.ChildNodes.Count == 6)
            {
                string produccion = nodo.ChildNodes.ElementAt(2).Term.Name;
                if (produccion == "Pindice")
                {
                    return verificartipo(nodo.ChildNodes.ElementAt(2));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private bool verificarTipoElemento(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 5)
            {
                string produccion = nodo.ChildNodes.ElementAt(4).Term.Name;
                if (produccion == "Pindice")
                {
                    return verificartipo(nodo.ChildNodes.ElementAt(4));
                }
                else
                {
                    return false;
                }

            }
            else if (nodo.ChildNodes.Count == 6)
            {
                string produccion = nodo.ChildNodes.ElementAt(5).Term.Name;
                if (produccion == "Pindice")
                {
                    return verificartipo(nodo.ChildNodes.ElementAt(5));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool verificartipo(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void graficar_TS(ParseTreeNode raiz)
        {
            String arbolAST = "digraph {\n";
            arbolAST += arbol(indice_nodo, raiz);
            arbolAST += "}";
            Graficar graficar = new Graficar();
            graficar.generarimagen(arbolAST);
            indice_nodo = 0;
        }

        private String arbol(int indice, ParseTreeNode actual)
        {
            String grafica = "nodo" + indice + "[label=\"" + actual.ToString() + "\", fillcolor = \" red \" ];\n";
            indice_nodo++;
            for (int i = 0; i < actual.ChildNodes.Count; i++)
            {
                grafica += "nodo" + indice + " -> nodo" + indice_nodo + ";\n";
                grafica += arbol(indice_nodo, actual.ChildNodes.ElementAt(i));
            }
            return grafica;
        }

    }
}
