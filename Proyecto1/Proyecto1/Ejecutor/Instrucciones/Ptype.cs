using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Ptype : Instruccion
    {

        string id;
        LinkedList  <Instruccion>  lst_instruccion;
        
        public Ptype(string id, LinkedList<Instruccion> lst_instruccion)
        {
            this.id = id;
            this.lst_instruccion = lst_instruccion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            TablaDeSimbolos tablaaux = new TablaDeSimbolos();
            foreach (var item in lst_instruccion)
            {
                item.Ejecutar(tablaaux);
            }
            if (Program.heap.ContainsKey(id))
            {
                //error
            }
            else {
                Program.heap.Add(id,tablaaux);
            }
            return null;
        }
    }
}
