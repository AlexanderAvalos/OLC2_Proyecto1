using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.Sentencias;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.funciones
{
    class Sentencias : Instruccion
    {
        LinkedList<Instruccion> lst_sentenciasfuncion;
        List<string> salida = new List<string>();
        public Sentencias(LinkedList<Instruccion> lst_sentenciasfuncion)
        {
            this.lst_sentenciasfuncion = lst_sentenciasfuncion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {

            TablaDeSimbolos local = new TablaDeSimbolos();
            local.agregarPadre(tabla);
            foreach (var instruccion in lst_sentenciasfuncion)
            {
                if (instruccion.GetType() == typeof(SentenciasBreak))
                {
                    return null;
                }
                if (instruccion.GetType() == typeof(SentenciasContinue))
                {
                    continue;
                }
                if (instruccion.GetType() == typeof(Instruccion_Exit))
                {
                    object val = instruccion.Ejecutar(local);
                    return val;
                }
                if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit))
                {
                    salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                }
                else
                {
                    instruccion.Ejecutar(local);
                }
            }

            return null;
        }
    }
}
