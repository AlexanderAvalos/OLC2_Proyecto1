using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class Sentencia_IF : Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> lst_sentencias_if;
        tipo_else sentencia_else;
        LinkedList<else_if> lst_elif;
        
        public Sentencia_IF(Operacion condicion, LinkedList<Instruccion> lst_sentencias_if)
        {
            this.condicion = condicion;
            this.lst_sentencias_if = lst_sentencias_if;
        }

        public Sentencia_IF(Operacion condicion, LinkedList<Instruccion> lst_sentencias_if, tipo_else sentencia_else)
        {
            this.condicion = condicion;
            this.lst_sentencias_if = lst_sentencias_if;
            this.sentencia_else = sentencia_else;
        }

        public Sentencia_IF(Operacion condicion, LinkedList<Instruccion> lst_sentencias_if, LinkedList<else_if> lst_elif)
        {
            this.condicion = condicion;
            this.lst_sentencias_if = lst_sentencias_if;
            this.lst_elif = lst_elif;
        }

        public Sentencia_IF(Operacion condicion, LinkedList<Instruccion> lst_sentencias_if, tipo_else sentencia_else, LinkedList<else_if> lst_elif)
        {
            this.condicion = condicion;
            this.lst_sentencias_if = lst_sentencias_if;
            this.sentencia_else = sentencia_else;
            this.lst_elif = lst_elif;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
