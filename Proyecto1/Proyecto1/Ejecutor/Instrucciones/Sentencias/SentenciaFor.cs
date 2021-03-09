using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

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
                    foreach (Instruccion instruccion in lst_Sentencias)
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
                    actualizar.Ejecutar(local);
                    valoraux = (double)local.getValor(inicializacion.Id);
                }
            }
            else
            {
                while (valoraux != ((double)condicion.Valor - 1))
                {
                    foreach (Instruccion instruccion in lst_Sentencias)
                    {
                        if (instruccion.GetType() == typeof(SentenciasBreak))
                        {
                            break;
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
                    actualizar.Ejecutar(local);
                    valoraux = (double)local.getValor(inicializacion.Id);
                }
            }

            return null;
        }
    }
}
