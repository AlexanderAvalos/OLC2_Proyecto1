using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Instruccion_Exit : Instruccion
    {
        Operacion valor;

        public Instruccion_Exit(Operacion valor)
        {
            this.valor = valor;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
        
            return valor.Ejecutar(tabla);
        }
    }
}
