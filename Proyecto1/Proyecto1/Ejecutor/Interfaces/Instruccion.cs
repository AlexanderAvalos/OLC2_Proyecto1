using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Analizador.Interfaces
{
    public interface Instruccion
    {
        Object Ejecutar(TablaDeSimbolos tabla);
        Tipo getTipo();
    }
}
