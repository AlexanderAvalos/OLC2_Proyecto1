using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SentenciaLlamar : Instruccion
    {
        string id;
        LinkedList<Operacion> lst_atributos;
        List<string> salida = new List<string>();

        public SentenciaLlamar(string id)
        {
            this.id = id;
        }

        public SentenciaLlamar(string id, LinkedList<Operacion> lst_atributos)
        {
            this.id = id;
            this.lst_atributos = lst_atributos;
        }

        public string Id { get => id; set => id = value; }
        internal LinkedList<Operacion> Lst_atributos { get => lst_atributos; set => lst_atributos = value; }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            Program.retorno = id;
            Object resultado = null;
            TablaDeSimbolos localts = new TablaDeSimbolos();
            ListaProcedimientos actual_procedimiento = null;
            Lista_Funciones actual_funcion = null;
            if (Program.lista_PTemporal.Count > 0)
            {
                foreach (var procedimiento in Program.lista_PTemporal)
                {
                    if (procedimiento.Nombre == id)
                    {
                        actual_procedimiento = procedimiento;
                        localts.agregarPadre(actual_procedimiento.Local);
                    }
                }
            }
             if (Program.lista_FTemporal.Count > 0)
            {
                foreach (var funcion in Program.lista_FTemporal)
                {
                    if (funcion.Nombre == id)
                    {
                        actual_funcion = funcion;
                        localts.agregarPadre(actual_funcion.Local);
                    }
                }
            }
            if (actual_procedimiento != null)
            {
                for (int i = 0; i < actual_procedimiento.Lst_atributos.Count; i++)
                {
                    if (lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MENOS || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MAS || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MULTIPLICACION || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.DIVISION)
                    {
                        actual_procedimiento.Local.setValor(actual_procedimiento.Lst_atributos.ElementAt(i).Id, lst_atributos.ElementAt(i).Ejecutar(actual_procedimiento.Local));
                    }
                    else if (lst_atributos.ElementAt(i).Tipo_operacion != Tipo.ID)
                    {
                        actual_procedimiento.Local.setValor(actual_procedimiento.Lst_atributos.ElementAt(i).Id, lst_atributos.ElementAt(i).Valor.ToString());
                    }
                    else
                    {
                        actual_procedimiento.Local.setValor(actual_procedimiento.Lst_atributos.ElementAt(i).Id, localts.getValor(lst_atributos.ElementAt(i).Valor.ToString()));
                    }
                    if (actual_procedimiento.Lst_atributos.ElementAt(i).Tipodato == Tipo.REFERENCIA)
                    {
                        tabla.setValor(lst_atributos.ElementAt(i).Valor.ToString(), actual_procedimiento.Local.getValor(actual_procedimiento.Lst_atributos.ElementAt(i).Id));
                    }
                }
                foreach (Instruccion instruccion in actual_procedimiento.Lst_sentencias)
                {
                    if (instruccion.GetType() == typeof(SentenciasBreak))
                    {
                        return null;
                    }
                    if (instruccion.GetType() == typeof(SentenciasContinue))
                    {
                        continue;
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                    }
                    else
                    {
                        instruccion.Ejecutar(actual_procedimiento.Local);
                    }
                }
                for (int i = 0; i < actual_procedimiento.Lst_atributos.Count; i++)
                {
                    if (actual_procedimiento.Lst_atributos.ElementAt(i).Tipodato == Tipo.REFERENCIA)
                    {
                        tabla.setValor(lst_atributos.ElementAt(i).Valor.ToString(), actual_procedimiento.Local.getValor(actual_procedimiento.Lst_atributos.ElementAt(i).Id));
                    }
                }
            }
            else if (actual_funcion != null)
            {

                for (int i = 0; i < actual_funcion.Lst_atributos.Count; i++)
                {
                    if (lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MENOS || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MAS || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.MULTIPLICACION || lst_atributos.ElementAt(i).Tipo_operacion == Tipo.DIVISION)
                    {
                        actual_funcion.Local.setValor(actual_funcion.Lst_atributos.ElementAt(i).Id, lst_atributos.ElementAt(i).Ejecutar(actual_funcion.Local));
                    }
                    else if (lst_atributos.ElementAt(i).Tipo_operacion == Tipo.ID_FUNCIONVALORES)
                    {
                        actual_funcion.Local.setValor(actual_funcion.Lst_atributos.ElementAt(i).Id, lst_atributos.ElementAt(i).Ejecutar(actual_funcion.Local));
                    }
                    else if (lst_atributos.ElementAt(i).Tipo_operacion != Tipo.ID)
                    {
                        actual_funcion.Local.setValor(actual_funcion.Lst_atributos.ElementAt(i).Id, lst_atributos.ElementAt(i).Valor.ToString());
                    }
                    else
                    {
                        actual_funcion.Local.setValor(actual_funcion.Lst_atributos.ElementAt(i).Id, tabla.getValor(lst_atributos.ElementAt(i).Valor.ToString()));
                    }
                    if (actual_funcion.Lst_atributos.ElementAt(i).Tipodato == Tipo.REFERENCIA)
                    {
                        tabla.setValor(lst_atributos.ElementAt(i).Valor.ToString(), actual_funcion.Local.getValor(actual_funcion.Lst_atributos.ElementAt(i).Id));
                    }
                }
                foreach (Instruccion instruccion in actual_funcion.Lst_sentencias)
                {
                    if (instruccion.GetType() == typeof(SentenciasBreak))
                    {
                        return null;
                    }
                    if (instruccion.GetType() == typeof(SentenciasContinue))
                    {
                        continue;
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Exit) && resultado == null)
                    {
                        object val = instruccion.Ejecutar(actual_funcion.Local);
                        actual_funcion.Local.setValor(id, val);
                        return val;
                    }
                    if (instruccion.GetType() == typeof(Instruccion_Funcion) || instruccion.GetType() == typeof(Instruccion_Procedimiento) || instruccion.GetType() == typeof(Instruccion_Exit))
                    {
                        salida.Add("Semantico" + "No puede venir instruccion de este tipo" + instruccion.ToString());
                    }
                    else
                    {
                        resultado = instruccion.Ejecutar(actual_funcion.Local);
                    }
                    if (resultado != null)
                    {
                        return resultado;
                    }
                }
                for (int i = 0; i < actual_funcion.Lst_atributos.Count; i++)
                {
                    if (actual_funcion.Lst_atributos.ElementAt(i).Tipodato == Tipo.REFERENCIA)
                    {
                        tabla.setValor(lst_atributos.ElementAt(i).Valor.ToString(), actual_funcion.Local.getValor(actual_funcion.Lst_atributos.ElementAt(i).Id));
                    }
                }
                if (resultado != null)
                {
                    tabla.setValor(actual_funcion.Nombre, resultado.ToString());
                    return tabla.getValor(actual_funcion.Nombre);
                }
                else
                {
                    tabla.setValor(actual_funcion.Nombre, actual_funcion.Local.getValor(actual_funcion.Nombre));
                    return tabla.getValor(actual_funcion.Nombre);
                }
            }

            return null;
        }
    }
}
