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
        private Instruccion llamada;
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

        public Asignacion(string id, Instruccion llamada)
        {
            this.id = id;
            this.llamada = llamada;
        }

        public string Id { get => id; set => id = value; }
        public string Objeto { get => objeto; set => objeto = value; }
        internal Operacion Valor { get => valor; set => valor = value; }

        public Object Ejecutar(TablaDeSimbolos tabla) {
            if (llamada != null)
            {
                Object nuevo = llamada.Ejecutar(tabla);
                tabla.setValor(id, nuevo);
            }
            else
            {
                tabla.setValor(id, valor.Ejecutar(tabla));
            }
            
            return null;
        }
    }
}
