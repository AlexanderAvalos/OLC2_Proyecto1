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

namespace Proyecto1.Ejecutor.Analizador
{
    class Sintactico_ejecutar
    {
        public List<string> salida = new List<string>();

        public ParseTreeNode Analizar(string entrada, Grammar gramatica)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;

            if (raiz != null && arbol.ParserMessages.Count == 0)
            {
                LinkedList<Instruccion> arbol_AST = Instrucciones(raiz.ChildNodes.ElementAt(0));
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
            }
            return null;
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
                    return new Asignacion(nodo.ChildNodes.ElementAt(1).Token.ValueString.ToString(), OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(3)));
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
                string operador = nodo.ChildNodes.ElementAt(1).ToString();
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
                    default:
                        return null;
                }
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                return OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0));
            }
            else
            {
                return null;
            }
        }

        private Operacion OPERACION_NUMERICA(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                string operador = nodo.ChildNodes.ElementAt(1).ToString();
                switch (operador)
                {
                    case "+":
                        return new Operacion(Tipo.MAS, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "-":
                        return new Operacion(Tipo.MENOS, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "*":
                        return new Operacion(Tipo.MULTIPLICACION, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "/":
                        return new Operacion(Tipo.DIVISION, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "%":
                        return new Operacion(Tipo.MODULO, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    default:
                        string produccion = nodo.ChildNodes.ElementAt(1).Term.Name;
                        if (produccion == "Operacion")
                        {
                            return POPERACION(nodo.ChildNodes.ElementAt(1));
                        }
                        else
                        {
                            return null;
                        }
                }
            }
            else if (nodo.ChildNodes.Count == 1)
            {
                return PVALOR(nodo.ChildNodes.ElementAt(0));
            }
            return null;
        }

        private Operacion POPERACION(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 3)
            {
                string operador = nodo.ChildNodes.ElementAt(1).ToString();
                switch (operador)
                {
                    case "AND":
                        return new Operacion(Tipo.AND, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "OR":
                        return new Operacion(Tipo.OR, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    case "NOT":
                        return new Operacion(Tipo.NOT, OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(0)), OPERACION_NUMERICA(nodo.ChildNodes.ElementAt(2)));
                    default:
                        return OPERACION_RELACIONAL(nodo.ChildNodes.ElementAt(0));
                }
            }
            return null;
        }

        private Operacion PVALOR(ParseTreeNode nodo)
        {
            if (nodo.ChildNodes.Count == 1)
            {
                string produccion = nodo.ChildNodes.ElementAt(0).Term.Name;
                switch (produccion)
                {
                    case "Numero":
                        return new Operacion(Double.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.ENTERO);
                    case "Decimal":
                        return new Operacion(Double.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.DECIMAL);
                    case "true":
                        return new Operacion(Boolean.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.TRUE);
                    case "false":
                        return new Operacion(Boolean.Parse(nodo.ChildNodes.ElementAt(0).Token.ValueString.ToString()), Tipo.FALSE);

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
        private string generarGrafica()
        {

            return "";
        }

    }
}
