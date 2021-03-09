using Proyecto1.Ejecutor.Analizador.Interfaces;
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
        private Object valor;
        private string id_funciones;
        private LinkedList<Operacion> lst_atributos;

        public object Valor { get => valor; set => valor = value; }

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

        public Operacion(string id_funciones, LinkedList<Operacion> lst_atributos)
        {
            this.id_funciones = id_funciones;
            this.lst_atributos = lst_atributos;
        }

        public Operacion(string id_funciones)
        {
            this.id_funciones = id_funciones;
        }

        public Object Ejecutar(TablaDeSimbolos tabla)
        {
            if (tipo_operacion == Tipo.MAS)
            {

                return (Double)operadorIzq.Ejecutar(tabla) + (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MENOS)
            {
                return (Double)operadorIzq.Ejecutar(tabla) - (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MULTIPLICACION)
            {
                return (Double)operadorIzq.Ejecutar(tabla) * (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.DIVISION)
            {
                return (Double)operadorIzq.Ejecutar(tabla) / (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MODULO)
            {
                return (Double)operadorIzq.Ejecutar(tabla) % (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MAYOR)
            {
                return (Double)operadorIzq.Ejecutar(tabla) > (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MENOR)
            {
                return (Double)operadorIzq.Ejecutar(tabla) < (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MAYORIGUAL)
            {
                return (Double)operadorIzq.Ejecutar(tabla) >= (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.MENORIGUAL)
            {
                return (Double)operadorIzq.Ejecutar(tabla) <= (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.DIFERENTE)
            {
                return (Double)operadorIzq.Ejecutar(tabla) != (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.IGUAL)
            {
                return ((Double)operadorIzq.Ejecutar(tabla) == (Double)operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.AND)
            {
                return ((bool)operadorIzq.Ejecutar(tabla)) && ((bool)operadorDer.Ejecutar(tabla));
            }
            else if (tipo_operacion == Tipo.OR)
            {
                return ((bool)operadorIzq.Ejecutar(tabla)) || ((bool)operadorDer.Ejecutar(tabla));
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
            else if (tipo_operacion == Tipo.DECIMAL)
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
            else
            {
                return null;
            }

        }

    }
}
