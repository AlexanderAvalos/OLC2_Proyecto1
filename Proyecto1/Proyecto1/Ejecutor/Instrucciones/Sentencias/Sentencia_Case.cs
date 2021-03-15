using Proyecto1.Ejecutor.Analizador.Interfaces;
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
        tipo_else lst_else;
        List<string> salida = new List<string>();

        public Sentencia_Case(Operacion condicion_Principal, LinkedList<Caso> lst_casos)
        {
            this.condicion_Principal = condicion_Principal;
            this.lst_casos = lst_casos;
        }

        public Sentencia_Case(Operacion condicion_Principal, LinkedList<Caso> lst_casos, tipo_else lst_else)
        {
            this.condicion_Principal = condicion_Principal;
            this.lst_casos = lst_casos;
            this.lst_else = lst_else;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            string condicion = condicion_Principal.Ejecutar(tabla).ToString();
            TablaDeSimbolos local_ts = new TablaDeSimbolos();
            local_ts.agregarPadre(tabla);
            Boolean encontrado = false;
            foreach (Caso casos in lst_casos)
            {
                foreach (var condiciones in casos.Lst_condiciones)
                {
                    string condicion2 = condiciones.Ejecutar(tabla).ToString();
                    if (condicion.ToLower().Equals(condicion2.ToLower()) || encontrado)
                    {
                        encontrado = true;
                        if (casos.Lst_sentencias != null)
                        {
                            foreach (Instruccion instrucciones in casos.Lst_sentencias)
                            {
                                if (instrucciones.GetType() == typeof(SentenciasBreak))
                                {
                                    return "Break";
                                }
                                if (instrucciones.GetType() == typeof(SentenciasContinue))
                                {
                                    return "Continue";
                                }
                                if (instrucciones.GetType() == typeof(Instruccion_Exit))
                                {
                                    return instrucciones.Ejecutar(local_ts);
                                }
                                if (instrucciones.GetType() == typeof(Instruccion_Funcion) || instrucciones.GetType() == typeof(Instruccion_Procedimiento) || instrucciones.GetType() == typeof(Instruccion_Exit) || instrucciones.GetType() == typeof(Declaracion))
                                {
                                    salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instrucciones.ToString());
                                }
                                else
                                {
                                    instrucciones.Ejecutar(local_ts);
                                }
                            }

                        }
                        else
                        {

                            if (casos.Sentencia_unica.GetType() == typeof(SentenciasBreak))
                            {
                                return "Break";
                            }
                            if (casos.Sentencia_unica.GetType() == typeof(SentenciasContinue))
                            {
                                return "Continue";
                            }
                            if (casos.Sentencia_unica.GetType() == typeof(Instruccion_Exit))
                            {
                                return casos.Sentencia_unica.Ejecutar(local_ts);
                            }
                            if (casos.Sentencia_unica.GetType() == typeof(Instruccion_Funcion) || casos.Sentencia_unica.GetType() == typeof(Instruccion_Procedimiento) || casos.Sentencia_unica.GetType() == typeof(Instruccion_Exit) || casos.Sentencia_unica.GetType() == typeof(Declaracion))
                            {
                                salida.Add("Semantico" + "No puede venir instruccion de este tipo" + casos.Sentencia_unica.ToString());
                            }
                            else
                            {
                                casos.Sentencia_unica.Ejecutar(local_ts);

                            }
                        }
                        return null;

                    }



                }
            }
            if (lst_else != null)
            {
                foreach (Instruccion instrucciones in lst_else.Lst_else)
                {
                    if (instrucciones.GetType() == typeof(SentenciasBreak))
                    {
                        return "Break";
                    }
                    if (instrucciones.GetType() == typeof(SentenciasContinue))
                    {
                        return "Continue";

                    }
                    if (instrucciones.GetType() == typeof(Instruccion_Exit))
                    {
                        return instrucciones.Ejecutar(local_ts);
                    }
                    if (instrucciones.GetType() == typeof(Instruccion_Funcion) || instrucciones.GetType() == typeof(Instruccion_Procedimiento) || instrucciones.GetType() == typeof(Instruccion_Exit) || instrucciones.GetType() == typeof(Declaracion))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instrucciones.ToString());
                    }
                    else
                    {
                        instrucciones.Ejecutar(local_ts);
                    }
                }
                return null;
            }
            return null;
        }
    }
}
