using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SenteciaRepeat : Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> lst_sentencias;
        List<string> salida = new List<string>();
        public SenteciaRepeat(Operacion condicion, LinkedList<Instruccion> lst_sentencias)
        {
            this.condicion = condicion;
            this.lst_sentencias = lst_sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            bool ver = false;
            do
            {
                TablaDeSimbolos local = new TablaDeSimbolos();
                local.agregarPadre(tabla);

                foreach (Instruccion instruccion in lst_sentencias)
                {
                    if (instruccion.GetType() == typeof(SentenciasBreak))
                    {
                        return null;
                    }
                    if (instruccion.GetType() == typeof(SentenciasContinue))
                    {
                        continue;
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit) || instruccion.GetType() == typeof(Declaracion))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                    }
                    else
                    {
                        instruccion.Ejecutar(local);
                    }
                }
                ver = (bool)condicion.Ejecutar(tabla);
            } while (ver == false);
            return null;
        }
    }
}
