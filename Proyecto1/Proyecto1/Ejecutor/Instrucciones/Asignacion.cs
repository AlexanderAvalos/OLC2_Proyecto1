using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Asignacion : Instruccion
    {
        string id;
        string objeto;
        private Operacion valor;

        public Asignacion(string id, Operacion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public Asignacion(string id, string objeto, Operacion valor)
        {
            this.id = id;
            this.objeto = objeto;
            this.valor = valor;
        }

        public Object Ejecutar(TablaDeSimbolos tabla) {

            tabla.setValor(id,valor.Ejecutar(tabla));
            return null;
        }
    }
}
