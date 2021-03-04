using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class Caso : Instruccion
    {
        LinkedList<Operacion> lst_condiciones;
        LinkedList<Instruccion> lst_sentencias;

        public Caso(LinkedList<Operacion> lst_condiciones, LinkedList<Instruccion> lst_sentencias)
        {
            this.lst_condiciones = lst_condiciones;
            this.lst_sentencias = lst_sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
