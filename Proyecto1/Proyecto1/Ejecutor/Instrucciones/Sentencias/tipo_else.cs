using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class tipo_else:Instruccion
    {
        LinkedList<Instruccion> lst_else;

        public tipo_else(LinkedList<Instruccion> lst_else)
        {
            this.lst_else = lst_else;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
