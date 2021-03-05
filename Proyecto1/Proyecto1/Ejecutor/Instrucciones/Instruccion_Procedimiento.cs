﻿using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.funciones;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Instruccion_Procedimiento : Instruccion
    {
        string id_funcion;
        LinkedList<Atributo> lst_atributos;
        LinkedList<Instruccion> lst_instrucciones;

        public Instruccion_Procedimiento(string id_funcion, LinkedList<Atributo> lst_atributos, LinkedList<Instruccion> lst_instrucciones)
        {
            this.id_funcion = id_funcion;
            this.lst_atributos = lst_atributos;
            this.lst_instrucciones = lst_instrucciones;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}
