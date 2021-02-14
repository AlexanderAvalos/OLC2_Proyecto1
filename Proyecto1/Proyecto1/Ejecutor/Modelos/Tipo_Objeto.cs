using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Modelos
{
    class Tipo_Objeto
    {
        string nombre;
        object valor;
        string padre;
        int tipo;

        public Tipo_Objeto(string nombre, object valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }

        public int Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public object Valor { get => valor; set => valor = value; }
        public string Padre { get => padre; set => padre = value; }
    }
}
