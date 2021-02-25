using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Ptype : Instruccion
    {

        string id;
        LinkedList  <Instruccion>  lst_instruccion;

        public Ptype(string id, LinkedList<Instruccion> lst_instruccion)
        {
            this.id = id;
            this.lst_instruccion = lst_instruccion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
