using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SentenciaFor : Instruccion
    {
        Asignacion inicializacion;
        Operacion condicion;
        LinkedList<Instruccion> lst_Sentencias;
        List<string> salida = new List<string>();

        public SentenciaFor(Asignacion inicializacion, Operacion condicion, LinkedList<Instruccion> lst_Sentencias)
        {
            this.inicializacion = inicializacion;
            this.condicion = condicion;
            this.lst_Sentencias = lst_Sentencias;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {

            TablaDeSimbolos local = new TablaDeSimbolos();
            local.agregarPadre(tabla);
            inicializacion.Ejecutar(tabla);
            Simbolo nuevo = new Simbolo(tabla.getTipo(inicializacion.Id.ToString()), inicializacion.Id.ToString());
            Operacion valor = (Operacion)inicializacion.Valor;
            object bre = null;
            bool entro = false;
            try
            {
                nuevo.Valor = valor.Ejecutar(tabla);
                if (nuevo.Valor == null) nuevo.Valor = inicializacion.Valor;
            }
            catch (Exception)
            {
                nuevo.Valor = valor;
            }
            local.AddLast(nuevo);
            bool incremento;
            Operacion actualizar;
            if ((Double)condicion.Valor > (Double)inicializacion.Valor.Valor)
            {
                actualizar = new Operacion(inicializacion.Id, Tipo.INCREMENETO);
                incremento = true;
            }
            else
            {
                actualizar = new Operacion(inicializacion.Id, Tipo.DECREMENTO);
                incremento = false;
            }
            Double valoraux = (double)local.getValor(inicializacion.Id);
            if (incremento)
            {
                while (valoraux != ((double)condicion.Valor) + 1)
                {
                    for (int i = 0; i < lst_Sentencias.Count; i++)
                    {
                        entro = false;
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(SentenciasBreak) || (string)bre == "Break")
                        {
                            return null;
                        }
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(SentenciasContinue) || (string)bre == "Continue")
                        {
                            i = i + 1;
                            entro = true;
                        }
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Funcion) || lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Procedimiento) || lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Exit) || lst_Sentencias.ElementAt(i).GetType() == typeof(Declaracion))
                        {
                            salida.Add("Semantico" + "No puede venir instruccion de este tipo" + lst_Sentencias.ElementAt(i).ToString());
                        }
                        else if (entro == false)
                        {
                            bre = lst_Sentencias.ElementAt(i).Ejecutar(local);
                        }
                    }
                    actualizar.Ejecutar(local);
                    valoraux = (double)local.getValor(inicializacion.Id);
                }
            }
            else
            {
                while (valoraux != ((double)condicion.Valor) + 1)
                {
                    for (int i = 0; i < lst_Sentencias.Count; i++)
                    {
                        entro = false;
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(SentenciasBreak) || (string)bre == "Break")
                        {
                            return null;
                        }
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(SentenciasContinue) || (string)bre == "Continue")
                        {
                            i = i + 1;
                            entro = true;
                        }
                        if (lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Funcion) || lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Procedimiento) || lst_Sentencias.ElementAt(i).GetType() == typeof(Instruccion_Exit) || lst_Sentencias.ElementAt(i).GetType() == typeof(Declaracion))
                        {
                            salida.Add("Semantico" + "No puede venir instruccion de este tipo" + lst_Sentencias.ElementAt(i).ToString());
                        }
                        else if (entro == false)
                        {
                            bre = lst_Sentencias.ElementAt(i).Ejecutar(local);
                        }
                    }
                    actualizar.Ejecutar(local);
                    valoraux = (double)local.getValor(inicializacion.Id);
                }
            }

            return null;
        }
    }
}
