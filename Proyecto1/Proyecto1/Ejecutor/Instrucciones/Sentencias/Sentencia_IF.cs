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
        List<string> salida = new List<string>();

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
            TablaDeSimbolos ts_local = new TablaDeSimbolos();
            ts_local.agregarPadre(tabla);
            bool verificar = Convert.ToBoolean(condicion.Ejecutar(tabla));
            if (verificar)
            {
                foreach (Instruccion instruccion in lst_sentencias_if)
                {
                    if (instruccion.GetType() == typeof(SentenciasBreak))
                    {
                        return "Break";
                    }
                    if (instruccion.GetType() == typeof(SentenciasContinue))
                    {
                        return "Continue";
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Exit))
                    {
                        return instruccion.Ejecutar(ts_local);
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit) || instruccion.GetType() == typeof(Declaracion))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                    }
                    else
                    {
                        instruccion.Ejecutar(ts_local);
                    }
                }
                return null;
            }
            else
            {
                if (lst_elif != null)
                {
                    foreach (else_if instruccionelif in lst_elif)
                    {
                        bool ver = (bool)instruccionelif.Condicion.Ejecutar(tabla);
                        if (ver)
                        {
                            foreach (Instruccion instruccion in instruccionelif.Lst_if)
                            {
                                if (instruccion.GetType() == typeof(SentenciasBreak))
                                {
                                    return "Break";
                                }
                                if (instruccion.GetType() == typeof(SentenciasContinue))
                                {
                                    return "Continue";
                                }
                                if (instruccion.GetType() == typeof(Instruccion_Exit))
                                {
                                    return instruccion.Ejecutar(ts_local);
                                }
                                if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit) || instruccion.GetType() == typeof(Declaracion))
                                {
                                    salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                                }
                                else
                                {
                                    instruccion.Ejecutar(ts_local);
                                }
                            }
                            return null;
                        }
                    }
                }
                if (sentencia_else != null)
                {
                    foreach (Instruccion instruccion in sentencia_else.Lst_else)
                    {
                        if (instruccion.GetType() == typeof(SentenciasBreak))
                        {
                            return "Break";
                        }
                        if (instruccion.GetType() == typeof(SentenciasContinue))
                        {
                            return "Continue";
                        }
                        if (instruccion.GetType() == typeof(Instruccion_Exit))
                        {
                            return instruccion.Ejecutar(ts_local);
                        }
                        if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit) || instruccion.GetType() == typeof(Declaracion))
                        {
                            salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                        }
                        else
                        {
                            instruccion.Ejecutar(ts_local);
                        }
                    }
                    return null;
                }
            }
            return null;
        }
    }
}
