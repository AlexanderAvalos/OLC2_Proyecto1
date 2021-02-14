using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Ejecutor.Modelos
{
    public class TablaDeSimbolos : LinkedList<Simbolo>
    {
        TablaDeSimbolos padre = null;
        public TablaDeSimbolos() : base()
        {

        }
        public Object getValor(string id)
        {
            return getValor(id, this);
        }
        private Object getValor(string id, TablaDeSimbolos nodo)
        {
            foreach (Simbolo s in nodo)
            {
                if (s.Id.ToLower().Equals(id.ToLower()))
                {
                    return s.Valor;
                }
            }
            if (nodo != null) return getValor(id, nodo.padre);
            return null;
        }

        public Tipo getTipo(string id)
        {
            return getTipo(id, this);
        }

        private Tipo getTipo(string id, TablaDeSimbolos nodo)
        {

            foreach (Simbolo s in nodo)
            {
                if (s.Id.ToLower().Equals(id.ToLower()))
                {
                    return s.Tipo;
                }
            }
            if (nodo != null) return getTipo(id, nodo.padre);
            return Tipo.NOENCONTRADO;
        }

        public string tipoAsignado(string id)
        {
            return tipoAsignado(id, this);
        }

        private string tipoAsignado(string id, TablaDeSimbolos nodo)
        {
            foreach (Simbolo s in nodo)
            {
                if (s.Id.ToLower().Equals(id.ToLower()))
                {
                    return s.Tipo_asignado;
                }
            }
            if (nodo.padre != null) return tipoAsignado(id, nodo);
            else
                return "No Hay ninguna coincidencia";
        }

        public bool setValorAtributo(string id, object valor, Stack<string> atributos)
        {
            object val = getValor(id);
            return setValorAtributo(val, valor, atributos);
        }

        public bool setValorAtributo(object valor2, object valor, Stack<string> atributos)
        {
            List<Tipo_Objeto> val = (List<Tipo_Objeto>)valor2;
            string atributo = atributos.Pop();
            foreach (Tipo_Objeto s in val)
            {
                if (s.Nombre.ToLower().Equals(atributo))
                {
                    if (atributos.Count != 0)
                    {
                        object val3 = s.Valor;
                        return setValorAtributo(val3, valor, atributos);
                    }
                    else
                    {
                        s.Valor = valor;
                        return true;
                    }
                }
            }
            return false;
        }

        public object getValorAtributo(string id, Stack<string> atributos)
        {
            object valor = getValor(id);
            return getValorAtributo(valor, atributos);
        }
        public object getValorAtributo(object valor, Stack<string> atributos)
        {
            List<Tipo_Objeto> val = (List<Tipo_Objeto>)valor;
            string atributo = atributos.Pop();
            foreach (Tipo_Objeto s in val)
            {
                if (s.Nombre.ToLower().Equals(atributo))
                {
                    if (atributos.Count != 0)
                    {
                        object val3 = s.Valor;
                        return getValorAtributo(val3, atributos);
                    }
                    else
                    {
                        return s.Valor;
                    }
                }
            }
            return null;
        }

        public bool existeID(string id)
        {
            return existeID(id, this);
        }

        public bool existeID(string id, TablaDeSimbolos nodo)
        {
            foreach (Simbolo s in nodo)
            {
                if (s.Id.ToLower().Equals(id.ToLower()))
                {
                    return true;
                }
            }
            if (nodo.padre != null) return existeID(id, nodo.padre);
            else
                return false;
        }

        public bool existID_AA(string id) {
            foreach (Simbolo item in this) 
            {
                if (item.Id.ToLower().Equals(id.ToLower())) return true;
            }
            return false;
        }

        public void agregarPadre(TablaDeSimbolos ts) {
            this.padre = ts;
        }

        public void setValor(string id,Object valor) {
            setValor(id,valor,this);
        }

        public void setValor(string id,object valor,TablaDeSimbolos nodo) {

            foreach (Simbolo s in nodo) {
                if (s.Id.ToLower().Equals(id.ToLower())) {
                    if (s.Tipo == Tipo.ENTERO) {
                        s.Valor = Convert.ToDouble(valor);
                    } else if (s.Tipo == Tipo.DECIMAL) {
                        s.Valor = Convert.ToDouble(valor);

                    } else if (s.Tipo == Tipo.CADENA) {
                        s.Valor = valor.ToString();
                    }
                    return;
                }
            }
            if (nodo.padre != null) setValor(id,valor,nodo.padre);

        }
    }
}

