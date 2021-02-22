using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Modelos
{
    public class Simbolo
    {
        private Tipo tipo;
        private string id;
        private string tipo_asignado;
        private object valor;

        public Simbolo(Tipo tipo, string id)
        {
            this.tipo = tipo;
            this.id = id;
        }

        public Tipo Tipo { get => tipo; set => tipo = value; }
        public string Id { get => id; set => id = value; }
        public string Tipo_asignado { get => tipo_asignado; set => tipo_asignado = value; }
        public object Valor { get => valor; set => valor = value; }
    }
   }

