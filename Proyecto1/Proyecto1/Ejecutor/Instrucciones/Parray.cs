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
        //primer indice
        LinkedList<Operacion> indiciO;
        Tipo indiceT;
        //segundo indice
        LinkedList<Operacion> elementoO;
        Tipo elementoT;
        // Tipo-operaecion
        public Parray(string id, Tipo indiceT, LinkedList<Operacion> elemento)
        {
            this.id = id;
            this.indiceT = indiceT;
            this.elementoO = elemento;
        }

        // Tipo-Tipo
        public Parray(string id, Tipo indice2, Tipo tipo)
        {
            this.id = id;
            this.indiceT = indice2;
            this.elementoT = tipo;
        }
        // Operacion-tipo
        public Parray(string id, LinkedList<Operacion> indicie, Tipo tipo)
        {
            this.id = id;
            this.indiciO = indicie;
            this.elementoT = tipo;
        }
        //Tipo-elemento
        public Parray(string id, LinkedList<Operacion> indicie, LinkedList<Operacion> elemento)
        {
            this.id = id;
            this.indiciO = indicie;
            this.elementoO = elemento;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}