﻿using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.Sentencias;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Operacion : Instruccion
    {
        private Tipo tipo_operacion;
        private Operacion operadorIzq;
        private Operacion operadorDer;

        private string id_funciones;
        private LinkedList<Operacion> lst_atributos;
        private Object valor;
        private SentenciaLlamar llamada;

        public object Valor { get => valor; set => valor = value; }
        public Tipo Tipo_operacion { get => tipo_operacion; set => tipo_operacion = value; }
        internal Operacion OperadorIzq { get => operadorIzq; set => operadorIzq = value; }
        internal Operacion OperadorDer { get => operadorDer; set => operadorDer = value; }
        public object Valor1 { get => valor; set => valor = value; }
        public string Id_funciones { get => id_funciones; set => id_funciones = value; }
        internal LinkedList<Operacion> Lst_atributos { get => lst_atributos; set => lst_atributos = value; }

        public Operacion(Tipo tipo_operacion, Operacion operadorIzq)
        {
            this.tipo_operacion = tipo_operacion;
            this.operadorIzq = operadorIzq;
        }

        public Operacion(Tipo tipo_operacion, Operacion operadorIzq, Operacion operadorDer)
        {
            this.tipo_operacion = tipo_operacion;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }

        public Operacion(string valor, Tipo tipo_operacion)
        {
            this.valor = valor;
            this.tipo_operacion = tipo_operacion;
        }
        public Operacion(Double valor, Tipo tipo_operacion)
        {
            this.valor = valor;
            this.tipo_operacion = tipo_operacion;
        }

        public Operacion(Boolean valor, Tipo tipo_operacion)
        {
            this.valor = valor;
            this.tipo_operacion = tipo_operacion;
        }

        public Operacion(string id_funciones, LinkedList<Operacion> lst_atributos, Tipo tipo)
        {
            this.id_funciones = id_funciones;
            this.lst_atributos = lst_atributos;
            this.tipo_operacion = tipo;
        }

        public Operacion(string id_funciones)
        {
            this.id_funciones = id_funciones;
        }

        public Operacion(SentenciaLlamar llamada, Tipo tipo_operacion)
        {
            this.llamada = llamada;
            this.tipo_operacion = tipo_operacion;
        }

        public Object Ejecutar(TablaDeSimbolos tabla)
        {
            if (tipo_operacion == Tipo.DIVISION)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) / Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MULTIPLICACION)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) * Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MAS)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) + Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MENOS)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) - Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MODULO)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) % Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MAYOR)
            {
                return Convert.ToDouble(operadorIzq.Ejecutar(tabla)) > Convert.ToDouble(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MENOR)
            {
                return Convert.ToDouble(operadorIzq.Ejecutar(tabla)) < Convert.ToDouble(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.NEGATIVO) {
                return (Double)OperadorIzq.Ejecutar(tabla) * (-1);
            }
            else if (tipo_operacion == Tipo.MAYORIGUAL)
            {
                return Convert.ToDouble(operadorIzq.Ejecutar(tabla)) >= Convert.ToDouble(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.MENORIGUAL)
            {
                return Convert.ToDouble(operadorIzq.Ejecutar(tabla)) <= Convert.ToDouble(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.DIFERENTE)
            {
                return Convert.ToDouble(operadorIzq.Ejecutar(tabla)) != Convert.ToDouble(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.IGUAL)
            {
                return Convert.ToDecimal(operadorIzq.Ejecutar(tabla)) == Convert.ToDecimal(operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.AND)
            {

                bool ver = ((bool)operadorIzq.Ejecutar(tabla)) && ((bool)operadorDer.Ejecutar(tabla));
                return ((bool)operadorIzq.Ejecutar(tabla)) && ((bool)operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.OR)
            {
                return ((bool)operadorIzq.Ejecutar(tabla)) || ((bool)operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.NOT)
            {
                return !(bool)operadorIzq.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.INCREMENETO)
            {
                Double aux;
                aux = (Double)tabla.getValor(valor.ToString());
                tabla.setValor(valor.ToString(), aux + 1);
                return aux;
            }
            else if (tipo_operacion == Tipo.DECREMENTO)
            {
                Double aux;
                aux = (Double)tabla.getValor(valor.ToString());
                tabla.setValor(valor.ToString(), aux - 1);
                return aux;
            }
            else if (tipo_operacion == Tipo.ENTERO)
            {
                return Double.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.INTEGER)
            {
                return Double.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.DECIMAL)
            {
                return Double.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.REAL)
            {
                return Double.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.FALSE)
            {
                return Convert.ToBoolean(valor);
            }
            else if (tipo_operacion == Tipo.TRUE)
            {
                return Convert.ToBoolean(valor);
            }
            else if (tipo_operacion == Tipo.CADENA)
            {
                return valor.ToString();
            }
            else if (tipo_operacion == Tipo.ID)
            {
                return tabla.getValor(valor.ToString());
            }
            else if (tipo_operacion == Tipo.ID_FUNCION)
            {
                object valor = llamada.Ejecutar(tabla);
                return valor;
            }
            else if (tipo_operacion == Tipo.ID_FUNCIONVALORES)
            {
                object valor = llamada.Ejecutar(tabla);
                return valor;
            }
            else
            {
                return null;
            }

        }

    }
}