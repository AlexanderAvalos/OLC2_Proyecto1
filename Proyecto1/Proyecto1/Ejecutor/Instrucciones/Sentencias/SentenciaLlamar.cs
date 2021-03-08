using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SentenciaLlamar : Instruccion
    {
        string id;
        LinkedList<Operacion> lst_atributos;

        public SentenciaLlamar(string id)
        {
            this.id = id;
        }

        public SentenciaLlamar(string id, LinkedList<Operacion> lst_atributos)
        {
            this.id = id;
            this.lst_atributos = lst_atributos;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
