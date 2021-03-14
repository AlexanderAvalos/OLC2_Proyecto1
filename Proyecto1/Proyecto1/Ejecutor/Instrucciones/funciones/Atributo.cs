using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.funciones
{
    class Atributo
    {
        LinkedList<string> lst_id;
        Tipo tipodato;
        Tipo tipo;

        public Atributo(LinkedList<string> lst_id, Tipo tipo)
        {
            this.lst_id = lst_id;
            this.tipo = tipo;
        }

        public Atributo(LinkedList<string> lst_id, Tipo tipodato, Tipo tipo)
        {
            this.lst_id = lst_id;
            this.tipodato = tipodato;
            this.tipo = tipo;
        }

        public LinkedList<string> Lst_id { get => lst_id; set => lst_id = value; }
        public Tipo Tipo { get => tipo; set => tipo = value; }
        public Tipo Tipodato { get => tipodato; set => tipodato = value; }
    }
}
