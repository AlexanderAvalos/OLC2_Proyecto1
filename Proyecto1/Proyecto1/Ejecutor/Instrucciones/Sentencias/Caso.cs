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
        Instruccion sentencia_unica;
        LinkedList<Instruccion> lst_sentencias;
       

        public Caso(LinkedList<Operacion> lst_condiciones, LinkedList<Instruccion> lst_sentencias)
        {
            this.lst_condiciones = lst_condiciones;
            this.lst_sentencias = lst_sentencias;
        }

        public Caso(LinkedList<Operacion> lst_condiciones, Instruccion sentencia_unica)
        {
            this.lst_condiciones = lst_condiciones;
            this.sentencia_unica = sentencia_unica;
        }

        public LinkedList<Instruccion> Lst_sentencias { get => lst_sentencias; set => lst_sentencias = value; }
        public Instruccion Sentencia_unica { get => sentencia_unica; set => sentencia_unica = value; }
        internal LinkedList<Operacion> Lst_condiciones { get => lst_condiciones; set => lst_condiciones = value; }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
