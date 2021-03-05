using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.funciones
{
    class Atributo
    {
        LinkedList<string> lst_id;
        Tipo tipo;

        public Atributo(LinkedList<string> lst_id, Tipo tipo)
        {
            this.lst_id = lst_id;
            this.tipo = tipo;
        }
    }
}
