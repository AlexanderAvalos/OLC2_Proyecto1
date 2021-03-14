using Proyecto1.Ejecutor.Analizador.Interfaces;
using Proyecto1.Ejecutor.Instrucciones.funciones;
using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Instrucciones
{
    class Instruccion_Funcion : Instruccion
    {
        string id_funcion;
        LinkedList<Atributo> lst_atributos;
        LinkedList<Instruccion> lst_instrucciones;
        Tipo tipo_funcion;
        List<string> salida = new List<string>();

        public Instruccion_Funcion(string id_funcion, LinkedList<Atributo> lst_atributos, LinkedList<Instruccion> lst_instrucciones, Tipo tipo_funcion)
        {
            this.id_funcion = id_funcion;
            this.lst_atributos = lst_atributos;
            this.lst_instrucciones = lst_instrucciones;
            this.tipo_funcion = tipo_funcion;
        }

        public object Ejecutar(TablaDeSimbolos tabla)
        {
            tabla.AddLast(new Simbolo(tipo_funcion, id_funcion, AsignarValor(tipo_funcion)));
            TablaDeSimbolos local = new TablaDeSimbolos();
            local.agregarPadre(tabla);
            //agregamos la variable de la funcion
            Simbolo id_pro = new Simbolo(tipo_funcion, id_funcion);
            //agregamos variables a nuestra tabla de simbolos local
            local.AddLast(id_pro);
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

            foreach (var item in lst_atributos)
            {
                foreach (var item2 in item.Lst_id)
                {
                    AtributosFP nuevo = new AtributosFP(item2, item.Tipodato, item.Tipo);
                    aux.AddLast(nuevo);
                }
            }
           

            Lista_Funciones funciones = new Lista_Funciones(id_funcion, Tipo.FUNCION , local, aux, lst_instrucciones);
            Program.lista_FTemporal.AddLast(funciones);
            return null;
        }
        private string AsignarValor(Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.INTEGER:
                    return "0";
                case Tipo.REAL:
                    return "0.0";
                case Tipo.BOOLEAN:
                    return "false";
                case Tipo.STRING:
                    return " ";
            }
            return "null";
        }
    }
}
