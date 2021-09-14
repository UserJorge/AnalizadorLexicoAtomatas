using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace AnalizadorLexicoAtomatas.Model
{
    public class AnalizadorLexico
    {
        // En el método de PreLectura está hecho con el proósito de eliminar los espacios en blanco, comentarios en el código
        //y demás infromación innecesaria para poder hacer un programa fuente más cercano a las instrucciones.
       // string espacio = "[' ']";


        List<string> ReservadasFun = new List<string> { "SUM", "SUB", "MLT", "DIV", "MOD","INT","FLT" };

        private string auxiliar;
       
        public string Auxiliar
        {
            get { return auxiliar; }
            set { auxiliar = value; }
        }

    
        //la prelectura borra cada especio que se encuentre para dejar solo sentencias
        public void PreLectura(string codigoFuente)
        {
           
            Auxiliar = "";
            foreach (char item in codigoFuente)
            {
                //se van a borrar los espacios
                if (!Regex.IsMatch(item.ToString(), @"(\s+)"))
                {
                    Auxiliar += item;
                }
              
            }         
        }
        string Extring;
        string LEXC;

        //en este proceso identifica cada uno de los lexemas ya sea SUM o un paréntesis delimitador, etc..
        public List<EstructuraLexica> Identificador()
        {
            List<EstructuraLexica> ListaIdent = new List<EstructuraLexica>();
            
                
           
                
                //se crea un nuevo array con la longitud de la propiedad auxiliar
                //donde reside el codigo producto del metodo prelectura
                char[] array = new char[Auxiliar.Length];
                int[] numero = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //se crea un flujo de datos iterativos en los cuales se analizarán cada uno de los caracteres para ser juntos para formar distintos lexemas
            //ya sean identificadores de funcion, delimitadores, variables, declaraciones, asignaciones, etc..
            for (int i = 0; i < Auxiliar.Length; i++)
            {
                array[i] = Convert.ToChar(Auxiliar[i]);

                if (Regex.IsMatch(array[i].ToString(), @"\b([A-Z]{1,1})\b"))
                {
                    Extring += array[i].ToString();
                }

                //saber si las letras almacenadas representan una palabra reservada
                if (Extring != null && Extring.Length > 2)
                {
                    if (Regex.IsMatch(Extring, @"^(SUM|SUB|DIV|MLT|MOD|INT|FLT)$"))
                    { 

                        switch (Extring)
                        {
                            case "SUM": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "SUM",Definicion="FUNCIÓN SUMA",Sintaxis="SUM(N,N);",Ejemplo="SUM(4,2);"}); break;
                            case "SUB": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "SUB", Definicion = "FUNCIÓN RESTA", Sintaxis = "SUB(N,N);", Ejemplo = "SUB(4,2);" }); break;
                            case "MLT": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "MLT", Definicion = "FUNCIÓN MULTIPLICACIÓN", Sintaxis = "MLT(N,N);", Ejemplo = "MLT(4,2);" }); break;
                            case "DIV": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "DIV", Definicion = "FUNCIÓN DIVISIÓN", Sintaxis = "DIV(N,N);", Ejemplo = "DIV(4,2);" }); break;
                            case "MOD": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "MOD", Definicion = "FUNCIÓN MODULAR", Sintaxis = "MOD(N,N);", Ejemplo = "MOD(4,2);" }); break;
                            case "INT": ListaIdent.Add(new EstructuraLexica { Token = "PRIMITIVO", Lexema = "INT", Definicion = "TIPO PRIMITIVO", Sintaxis = "INT NOM_VAR=N;", Ejemplo = "INT a=5;" }); break;
                            case "FLT": ListaIdent.Add(new EstructuraLexica { Token = "PRIMITIVO", Lexema = "FLT", Definicion = "TIPO PRIMITIVO", Sintaxis = "FLT NOM_VAR=N;", Ejemplo = "FLT a=5;" }); break;
                            default: throw new ArgumentException("Interrupación ninguna coincidencia en palabras reservadas");

                        }
                        Extring = "";
                    }
                    // si no entonces envía una interrupción la cual especifica que no se detectaron 
                    //las palabra reservada como primera mención (esto es importante)
                    else
                    {
                        throw new ArgumentException("Interrupción, ninguna coincidencia en palabras reservadas");
                    }

                }


                
                    /*
                     Comprobar si hay una coincidencia en el paréntesis de apertura en algúna de las declarciones
                                  si es así, entonces se coloca en la lista especificada
                                  si no (else) entonces se coloca una excepción en la cual no se encuentra un paréntesis
                  */
                    if (Regex.IsMatch(array[i].ToString(), @"^(\x28)$"))
                    {
                        LEXC += array[i].ToString();
                        switch (LEXC)
                        {
                            case "(": ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = "(", Definicion = "DELIMITA LAS FUNCIONES", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);" }); break;
                        }
                        LEXC = "";
                    }
                    else if (i == 3 && !Regex.IsMatch(array[i].ToString(), @"^(\x28)$"))
                    {
                        throw new ArgumentException("Error en el parentesis apertura");
                    }


                    /*
                     Si se encuentra un número entero se enceuntra se agrega a la lista con sus especificaciones 
                      si no es así entonces si hay concidencias con una coma se agg a la lista
                     */

                    if (Regex.IsMatch(array[i].ToString(), @"[0-9]{1,1}"))
                    {
                        ListaIdent.Add(new EstructuraLexica { Token = "NÚMERO", Lexema = array[i].ToString(), Definicion = "NÚMERO ENTERO", Sintaxis = "NÚMERO [0-9]", Ejemplo = "0,1,2,3...9" });
                    }
                    if (array[i] == ',')
                    {
                        ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = ",", Definicion = "DELIMITADOR COMA", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(N,N);" });
                    }

                    /*
                     Reconoce el paréntesis de clausura \x29 y lo agg a la lista "ListaIdent"
                     */

                    if (Regex.IsMatch(array[i].ToString(), @"^(\x29)$"))
                    {
                        LEXC += array[i].ToString();
                        switch (LEXC)
                        {
                            case ")": ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = ")", Definicion = "DELIMITA LAS FUNCIONES", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);" }); break;
                        }
                        LEXC = "";

                    }
                    //identificar solamente si no hay un paréntesis de clausura
                    else if (i == Auxiliar.Length - 2 && !Regex.IsMatch(array[Auxiliar.Length - 2].ToString(), @"^(\x29)$"))
                    {
                        throw new ArgumentException("parentesis de clausura");
                    }
                    // se termina la sentencia para las funciones aritméticas
                    if (array[i] == ';')
                    {
                        ListaIdent.Add(new EstructuraLexica { Token = "Ent. Terminal", Lexema = ";", Definicion = "SÍMBOLO TERMINAL", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);" });
                    } 
                }
            
              //al final se regresa 
            return ListaIdent;
            

        }

    }
}
