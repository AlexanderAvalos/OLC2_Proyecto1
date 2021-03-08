using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones.Sentencias
{
    class SentenciaWriteln : Instruccion
    {
        Operacion valor;
        LinkedList<Operacion> lst_operacion;

        public SentenciaWriteln(Operacion valor)
        {
            this.valor = valor;
        }

        public SentenciaWriteln(Operacion valor, LinkedList<Operacion> lst_operacion)
        {
            this.valor = valor;
            this.lst_operacion = lst_operacion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            string impresion = valor.Ejecutar(tabla).ToString();
            string impresion2 = "";
            if (lst_operacion != null)
            {
                foreach (var item in lst_operacion)
                {
                    impresion2 += item.Ejecutar(tabla).ToString();
                }
            }
            Program.consola.AppendText(impresion + impresion2+ '\n');
            return null;
        }
    }
}
