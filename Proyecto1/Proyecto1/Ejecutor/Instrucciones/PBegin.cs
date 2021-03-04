using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class PBegin : Instruccion
    {
        LinkedList<Instruccion> lst_sentencias;

        public PBegin(LinkedList<Instruccion> lst_sentencias)
        {
            this.lst_sentencias = lst_sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
