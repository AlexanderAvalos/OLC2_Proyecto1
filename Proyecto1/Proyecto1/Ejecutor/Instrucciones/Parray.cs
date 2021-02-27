using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Parray : Instruccion
    {
        string id;
        LinkedList<Operacion>  indice;
        Tipo indice2;
        Tipo tipo;

        public Parray(string id, Tipo indice2, Tipo tipo)
        {
            this.id = id;
            this.indice2 = indice2;
            this.tipo = tipo;
        }
        public Parray(string id, LinkedList<Operacion>  indicie, Tipo tipo)
        {
            this.id = id;
            this.indice = indicie;
            this.tipo = tipo;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
