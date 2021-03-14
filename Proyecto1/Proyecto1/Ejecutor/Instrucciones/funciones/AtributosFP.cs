using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.funciones
{
    class AtributosFP
    {
        string id;
        Tipo tipodato;
        Tipo tipo;

        public AtributosFP(string id, Tipo tipodato, Tipo tipo)
        {
            this.id = id;
            this.tipodato = tipodato;
            this.tipo = tipo;
        }

        public string Id { get => id; set => id = value; }
        public Tipo Tipodato { get => tipodato; set => tipodato = value; }
        public Tipo Tipo { get => tipo; set => tipo = value; }
    }
}
