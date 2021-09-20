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

        /// <summary>
        /// Se crea la estructura del nodo
        /// </summary>
        class Nodo
            {
                public char info;
                public Nodo izq, der;
            }

            /// <summary>
            /// se intancia la raiz de tipo 
            /// </summary>
            /// <remarks>
            /// <c>Insertar</c> es el proceso para insertar un nodo a el arbol binario
            /// </remarks>
            Nodo raiz;

            public ArbolBinario()
            {
                raiz = null;
            }

            public void Insertar(char info)
            {
                Nodo nuevo;
                nuevo = new Nodo();
                nuevo.info = info;
                nuevo.izq = null;
                nuevo.der = null;
                if (raiz == null)
                    raiz = nuevo;
                else
                {
                    Nodo anterior = null, reco;
                    reco = raiz;
                    while (reco != null)
                    {
                        anterior = reco;
                        if (info < reco.info)
                            reco = reco.izq;
                        else
                            reco = reco.der;
                    }
                    if (info < anterior.info)
                        anterior.izq = nuevo;
                    else
                        anterior.der = nuevo;
                }
            }


            private void ImprimirPre(Nodo reco)
            {
                if (reco != null)
                {
                auxiliar += reco.info;
                //Console.Write(reco.info + " ");
                ImprimirPre(reco.izq);
                    ImprimirPre(reco.der);
                }
            }

            public void ImprimirPre()
            {
                ImprimirPre(raiz);

                //Console.WriteLine();
            }
             string auxiliar;
            private void ImprimirEntre(Nodo reco)
            {
                if (reco != null)
                {
                    ImprimirEntre(reco.izq);
                // Console.Write(reco.info + " ");
                    auxiliar += reco.info;
                    ImprimirEntre(reco.der);
                }
            }

            public void ImprimirEntre()
            {
                ImprimirEntre(raiz);
                //Console.WriteLine();
            }


            private void ImprimirPost(Nodo reco)
            {
                if (reco != null)
                {
                    ImprimirPost(reco.izq);
                    ImprimirPost(reco.der);
                auxiliar += reco.info;
                // Console.Write(reco.info + " ");
            }
            }


            public void ImprimirPost()
            {
                ImprimirPost(raiz);
               // Console.WriteLine();
            }

            public string Main(string args)
            {
            if (args!=null)
            {
                
                //abo.Insertar(100);
                //abo.Insertar(50);
                //abo.Insertar(25); 
                //abo.Insertar(75);
                //abo.Insertar(150);
                for (int i = 0; i < args.Length; i++)
                {


                    Insertar(args.ToArray()[i]);
                    //if (i==(args.Length-1)/2)
                    //{
                    //    Insertar(args.ToArray()[i]);
                    //}
                    //if (i%2==0)
                    //{
                    //    Insertar(args.ToArray()[i]);
                    //}
                    //if (i % 2 != 0)
                    //{
                    //    Insertar(args.ToArray()[i]);
                    //}


                }
                //Console.WriteLine("Impresion preorden: ");
                //abo.ImprimirPre();
                // Console.WriteLine("Impresion entreorden: ");
                ImprimirPre();
                
                //Console.WriteLine("Impresion postorden: ");
                //abo.ImprimirPost();
                //Console.ReadKey(); 
            }
            return auxiliar;
        }
        






    }
}
