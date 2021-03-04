using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SentenciaFor : Instruccion
    {
        Asignacion inicializacion;
        Operacion condicion;
        LinkedList<Instruccion> lst_Sentencias;

        public SentenciaFor(Asignacion inicializacion, Operacion condicion, LinkedList<Instruccion> lst_Sentencias)
        {
            this.inicializacion = inicializacion;
            this.condicion = condicion;
            this.lst_Sentencias = lst_Sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {

            return null;
        }
    }
}
