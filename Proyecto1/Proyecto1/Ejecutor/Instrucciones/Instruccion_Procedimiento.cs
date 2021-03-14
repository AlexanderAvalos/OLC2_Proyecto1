using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.funciones;
using Proyecto1.Ejecutor.Instrucciones.Sentencias;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Instruccion_Procedimiento : Instruccion
    {
        string id_procedure;
        LinkedList<Atributo> lst_atributos;
        LinkedList<Instruccion> lst_instrucciones;
        List<string> salida = new List<string>();

        public Instruccion_Procedimiento(string id_funcion, LinkedList<Atributo> lst_atributos, LinkedList<Instruccion> lst_instrucciones)
        {
            this.id_procedure = id_funcion;
            this.lst_atributos = lst_atributos;
            this.lst_instrucciones = lst_instrucciones;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            TablaDeSimbolos local = new TablaDeSimbolos();
            local.agregarPadre(tabla);
            //agregamos la variable de la funcion
            Simbolo id_pro = new Simbolo(Tipo.FUNCTION, id_procedure);
            //agregamos variables a nuestra tabla de simbolos local
            if (lst_atributos != null)
            {
                foreach (var item in lst_atributos)
                {
                    foreach (var ids in item.Lst_id)
                    {
                        if (tabla.existeID(ids))
                        {
                            salida.Add("Semantico" + "id ya esta declarada anteriormente" + ids);
                        }
                        else
                        {
                            Simbolo nuevo = new Simbolo(item.Tipo, ids);
                            local.AddLast(nuevo);
                        }
                    }
                }
            }
            LinkedList<AtributosFP> aux = new LinkedList<AtributosFP>();
            if (lst_atributos != null)
            {

                foreach (var item in lst_atributos)
                {
                    foreach (var item2 in item.Lst_id)
                    {
                        AtributosFP nuevo = new AtributosFP(item2, item.Tipodato, item.Tipo);
                        aux.AddLast(nuevo);
                    }

                }
            }

            ListaProcedimientos procedimiento = new ListaProcedimientos(id_procedure, Tipo.PROCEDIMIENTO, local, aux, lst_instrucciones);
            Program.lista_PTemporal.AddLast(procedimiento);

            return null;
        }
    }
}
