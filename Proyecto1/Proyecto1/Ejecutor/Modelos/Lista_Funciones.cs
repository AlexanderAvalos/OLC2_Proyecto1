using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.funciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Modelos
{
    class Lista_Funciones
    {
        String nombre;
        Tipo tipo;
        TablaDeSimbolos local;
        LinkedList<AtributosFP> lst_atributos;
        LinkedList<Instruccion> lst_sentencias;

        public Lista_Funciones(string nombre, Tipo tipo, TablaDeSimbolos local, LinkedList<AtributosFP> lst_atributos, LinkedList<Instruccion> lst_sentencias)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.local = local;
            this.lst_atributos = lst_atributos;
            this.lst_sentencias = lst_sentencias;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public Tipo Tipo { get => tipo; set => tipo = value; }
        public TablaDeSimbolos Local { get => local; set => local = value; }
        public LinkedList<Instruccion> Lst_sentencias { get => lst_sentencias; set => lst_sentencias = value; }
        internal LinkedList<AtributosFP> Lst_atributos { get => lst_atributos; set => lst_atributos = value; }
    }
}
