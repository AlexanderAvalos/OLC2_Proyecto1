using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SenteciaRepeat : Instruccion
    {
        bool entro = false;
        object bre = null;
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
                for (int i = 0; i < lst_sentencias.Count; i++)
                {
                    entro = false;
                    if (lst_sentencias.ElementAt(i).GetType() == typeof(SentenciasBreak) || (string)bre == "Break")
                    {
                        return null;
                    }
                    if (lst_sentencias.ElementAt(i).GetType() == typeof(SentenciasContinue) || (string)bre == "Continue")
                    {
                        i = i + 1;
                        entro = true;
                    }
                    if (lst_sentencias.ElementAt(i).GetType() == typeof(Instruccion_Funcion) || lst_sentencias.ElementAt(i).GetType() == typeof(Instruccion_Procedimiento) || lst_sentencias.ElementAt(i).GetType() == typeof(Instruccion_Exit) || lst_sentencias.ElementAt(i).GetType() == typeof(Declaracion))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + lst_sentencias.ElementAt(i).ToString());
                    }
                    else if (entro == false)
                    {
                        bre = lst_sentencias.ElementAt(i).Ejecutar(local);
                    }
                }
                //foreach (Instruccion instruccion in lst_sentencias)
                //{
                //    if (instruccion.GetType() == typeof(SentenciasBreak))
                //    {
                //        return null;
                //    }
                //    if (instruccion.GetType() == typeof(SentenciasContinue))
                //    {
                //        continue;
                //    }
                //    if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit) || instruccion.GetType() == typeof(Declaracion))
                //    {
                //        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                //    }
                //    else
                //    {
                //        instruccion.Ejecutar(local);
                //    }
                //}
                ver = (bool)condicion.Ejecutar(tabla);
            } while (ver == false);
            return null;
        }
    }
}
