using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SenteciaRepeat : Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> lst_sentencias;

        public SenteciaRepeat(Operacion condicion, LinkedList<Instruccion> lst_sentencias)
        {
            this.condicion = condicion;
            this.lst_sentencias = lst_sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
