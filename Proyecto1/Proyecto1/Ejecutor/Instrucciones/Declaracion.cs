using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Declaracion : Instruccion
    {
        private LinkedList<string> id;
        private Tipo tipo;

        public Declaracion(LinkedList<string> id, Tipo tipo)
        {
            this.id = id;
            this.tipo = tipo;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            foreach (var item in id)
            {
                tabla.AddLast(new Simbolo(tipo, item));
            }
            return null;
        }
    }
}
