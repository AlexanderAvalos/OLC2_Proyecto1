using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Irony;
using Irony.Parsing;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using System.Linq;
using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using Proyecto1.Ejecutor.Instrucciones;

namespace Proyecto1.Ejecutor.Analizador
{
    class Sintactico_ejecutar
    {
        bool global = true;
        bool aux_global = true;
        public List<string> salida = new List<string>();
        List<string> lst_ids = new List<string>();
  
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
            else {
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
                case "Declaracion":
                    return DECLARACION(node.ChildNodes.ElementAt(0));
                case "Ptype":
                    return null;
                case "Funcion":
                    return null;
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

            if (nodo.ChildNodes.Count == 5)
            {
                string nombre = nodo.ChildNodes.ElementAt(0).Term.Name;
                if (nombre.ToLower() == "const")
                {

                }
                else if (nombre.ToLower() == "var")
                {

                }
            }

            return null;
        }
        private Instruccion PTYPE(ParseTreeNode nodo)
        {
            return null;
        }
        private Instruccion FUNCION(ParseTreeNode nodo)
        {
            return null;
        }


    }
}
