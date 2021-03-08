using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class else_if : Instruccion
    {
        private Operacion condicion;
        LinkedList<Instruccion> lst_if;

        public else_if(Operacion condicion, LinkedList<Instruccion> lst_if)
        {
            this.condicion = condicion;
            this.lst_if = lst_if;
        }

        public Operacion Condicion { get => condicion; set => condicion = value; }
        public LinkedList<Instruccion> Lst_if { get => lst_if; set => lst_if = value; }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
