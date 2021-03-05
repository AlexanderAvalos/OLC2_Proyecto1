using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.funciones
{
    class Sentencias : Instruccion
    {
        LinkedList<Instruccion> lst_sentenciasfuncion;

        public Sentencias(LinkedList<Instruccion> lst_sentenciasfuncion)
        {
            this.lst_sentenciasfuncion = lst_sentenciasfuncion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
