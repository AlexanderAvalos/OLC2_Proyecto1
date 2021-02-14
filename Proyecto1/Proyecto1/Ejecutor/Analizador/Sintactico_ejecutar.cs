using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using System.Linq;
using Proyecto1.Ejecutor.Analizador.Interfaces;

namespace Proyecto1.Ejecutor.Analizador
{
    class Sintactico_ejecutar
    {
        bool global = true;
        bool aux_global = true;
        public List<string> salida = new List<string>();
        List<string> lst_ids = new List<string>();
        public bool validar(string entrada, Grammar gramatica)
        {
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            if (arbol.Root != null) return true;
            else return false;

        }
        public ParseTreeNode Analizar(String entrada, Grammar gramtica)
        {
            LanguageData lenguaje = new LanguageData(gramtica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(entrada);
            ParseTreeNode raiz = arbol.Root;

            if (raiz != null && arbol.ParserMessages.Count == 0)
            {
                LinkedList<Instruccion> arbol_AST = Instrucciones(raiz.ChildNodes.ElementAt(0));
            }

            return null;
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
                LinkedList<Instruccion> lista = Instrucciones(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(Instruccion(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }

            return new LinkedList<Instruccion>();
        }

        private Instruccion Instruccion(ParseTreeNode node)
        {
            string produccion = node.ChildNodes.ElementAt(0).Term.Name;
            switch (produccion)
            {
                case "Pprogram":
                    return null;
                case "Declaracion":
                    return null;
                case "Ptype":
                    return null;
                case "Funcion":
                    return null;

            }
            return null;
        }

    }
}
