using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Asginacion : Instruccion
    {
        string id;
        private Operacion valor;

        public Asginacion(string id, Operacion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public Object Ejecutar(TablaDeSimbolos tabla) {
            tabla.setValor(id,valor.Ejecutar(tabla));
            return null;
        }
    }
}
