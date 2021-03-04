﻿using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class Sentencia_Case : Instruccion
    {
        Operacion condicion_Principal;
        LinkedList<Caso> lst_casos;

        public Sentencia_Case(Operacion condicion_Principal, LinkedList<Caso> lst_casos)
        {
            this.condicion_Principal = condicion_Principal;
            this.lst_casos = lst_casos;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            return null;
        }
    }
}