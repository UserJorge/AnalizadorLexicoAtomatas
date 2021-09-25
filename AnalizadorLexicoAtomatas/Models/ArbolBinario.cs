using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Models
{
   public class ArbolBinario
    {
        //******Esta clase no se utiliza*******
        
        //Puede ocasinarse un OutOfMemoryException
        public class Nodo
        {
            private IComparable datos;

            private Nodo nodoIzquierdo;

            private Nodo nodoDerecho;
            public IComparable Datos
            {
                get { return datos; }
                set { datos = value; }
            }
            public Nodo(IComparable dato)
            {
                nodoIzquierdo = nodoDerecho = null;
            }
            public Nodo NodoDerecho
            {
                get { return nodoDerecho; }
                set { nodoDerecho = value; }
            }
            public Nodo NodoIzquierdo
            {
                get { return nodoIzquierdo; }
                set { nodoIzquierdo = value; }
            }
            public void Insertar(IComparable valorDeInsercion)
            {
                //vamos a ver si hay que transformarlo a double, float o int según corresponda (como venía declarado)
                //y el segundo paso es el recorrido del arbol si hay un double y
                //un int convertimos el int en double para poder hacer la operación
                if (valorDeInsercion.CompareTo(datos)<0)
                {
                    if (nodoIzquierdo==null)
                    {
                        nodoIzquierdo = new Nodo(valorDeInsercion);
                    }
                    else
                    {
                        nodoIzquierdo.Insertar(valorDeInsercion);
                    }
                } 
                else if (valorDeInsercion.CompareTo(datos) > 0)
                {
                    if (nodoIzquierdo == null)
                    {
                        nodoDerecho = new Nodo(valorDeInsercion);
                    }
                    else
                    {
                        nodoDerecho.Insertar(valorDeInsercion);
                    }
                }


            }
        }
        public class Arbol
        {
            private Nodo raiz;
            public Arbol()
            {
                raiz = null;
            }
            public void InsertarNodo(IComparable numero)
            {
                if (raiz == null)
                {
                    raiz = new Nodo(numero);
                }
                else 
                    raiz.Insertar(numero);

            }
            public void RecorridoPreorden()
            {
                AyudantePreorden(raiz);
            }
            public void AyudantePreorden(Nodo nodo)
            {
                if (nodo==null) 
                {
                    return;
                }
                //return nodo.Datos;
                //console write datos del nodo actual
                Imprimir(nodo);
                AyudantePreorden(nodo.NodoIzquierdo);
                AyudantePreorden(nodo.NodoDerecho);
            }
            public void RecorridoInorden()
            {
                AyudanteInorden(raiz);
            }
            public void AyudanteInorden(Nodo nodo)
            {
                if (nodo == null)
                {
                    return;
                }
               
                AyudantePreorden(nodo.NodoIzquierdo);
                //console write datos del nodo actual
                Imprimir(nodo);
                AyudantePreorden(nodo.NodoDerecho);
            }
            public void RecorridoPostorden()
            {
                AyudantePostorden(raiz);
            }
            public void AyudantePostorden(Nodo nodo)
            {
                if (nodo == null)
                {
                    return;
                }

                AyudantePreorden(nodo.NodoIzquierdo);     
                AyudantePreorden(nodo.NodoDerecho);
                Imprimir(nodo);
                //console write datos del nodo actual
            }
            public IComparable Imprimir(Nodo nodo)
            {
                return nodo.Datos;
            }
        }
        
     
    }
}
