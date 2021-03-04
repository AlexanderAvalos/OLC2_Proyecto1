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
            else if (tipo_operacion == Tipo.MENOR)
            {
                return (Double)operadorIzq.Ejecutar(tabla) < (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.DIFERENTE)
            {
                return (Double)operadorIzq.Ejecutar(tabla) != (Double)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.IGUAL)
            {
                return (Boolean)operadorIzq.Ejecutar(tabla) == (Boolean)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.AND)
            {
                return (Boolean)operadorIzq.Ejecutar(tabla) && (Boolean)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.OR)
            {
                return (Boolean)operadorIzq.Ejecutar(tabla) || (Boolean)operadorDer.Ejecutar(tabla);
            }
            else if (tipo_operacion == Tipo.ENTERO)
            {
                return int.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.DECIMAL)
            {
                return Double.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.FALSE)
            {
                return Boolean.Parse(valor.ToString());
            }
            else if (tipo_operacion == Tipo.TRUE)
            {
                return Boolean.Parse(valor.ToString());
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
