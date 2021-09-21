using AnalizadorLexicoAtomatas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace AnalizadorLexicoAtomatas.Model
{
    public class AnalizadorLexico
    {
        //prueba 1
        // En el método de PreLectura está hecho con el proósito de eliminar los espacios en blanco, comentarios en el código
        //y demás infromación innecesaria para poder hacer un programa fuente más cercano a las instrucciones.
        // string espacio = "[' ']";


        List<string> ReservadasFun = new List<string> { "SUM", "SUB", "MLT", "DIV", "MOD", "INT", "FLT" };

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
                //if (!Regex.IsMatch(item.ToString(), @"(\s+|(\x3A{1,1})|(\x3D{1,1}))"))
                //{
                Auxiliar += item;
                //}

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
            int lineas = 0;
            for (int i = 0; i < Auxiliar.Length; i++)
            {
                array[i] = Convert.ToChar(Auxiliar[i]);
               
                //if (Regex.IsMatch(array[i].ToString(), @"\b([a-z]{1,1}|[A-Z]{1,1}|\x3A{1,1}|\x3D{1,1})\b"))
                //{
                if ((Regex.IsMatch(array[i].ToString(), @"\S")))
                {
                    Extring += array[i].ToString();
                }
                if ((Regex.IsMatch(array[i].ToString(), @"\r")))
                {
                    lineas++;
                }

                if (Extring != null&&Extring=="IF")
                {
                   ListaIdent.Add(new EstructuraLexica { Token = "CONDICIONAL", Lexema = "IF", Definicion = "IF Statement", Sintaxis = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF;", Ejemplo = "IF(VAR/NUM==VAR/NUM) THEN FUN(4,5);ENDIF;",Linea=lineas+1 });
                    Extring = "";
                }
                if (Extring != null && Extring == "THEN")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "ENTONCES", Lexema = "THEN", Definicion = "IF Statement", Sintaxis = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF;", Ejemplo = "IF(VAR/NUM==VAR/NUM) THEN FUN(4,5);ENDIF;", Linea = lineas + 1 });
                    Extring = "";
                }
                if (Extring != null && Extring == "ENDIF")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "Termino IF", Lexema = "ENDIF", Definicion = "IF Statement", Sintaxis = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF;", Ejemplo = "IF(VAR/NUM==VAR/NUM) THEN FUN(4,5);ENDIF;", Linea = lineas + 1 });
                    Extring = "";
                }
                if (Extring != null && Extring == "ELSE")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "ELSE", Lexema = "ELSE", Definicion = "IF Statement", Sintaxis = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF;", Ejemplo = "IF(VAR/NUM==VAR/NUM) THEN FUN(4,5);ENDIF;", Linea = lineas + 1 });
                    Extring = "";
                }
                if (Extring != null && Extring == "ENDELSE")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "Termino ELSE", Lexema = "ENDELSE", Definicion = "IF Statement", Sintaxis = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF ELSE SUB(8,4); ENDELSE;", Ejemplo = "IF(VAR/NUM==VAR/NUM) THEN FUN(VAR,5);ENDIF ELSE SUB(8,4); ENDELSE;", Linea = lineas + 1 });
                    Extring = "";
                }

                //saber si las letras almacenadas representan una palabra reservada
                //&&Extring!="THE" && Extring != "END"&&Extring!="ELS"
                if (Extring != null && Extring.Length > 2 && (!Regex.IsMatch(Extring, @"(\d+?\x2E\d+?|F)")&&!Regex.IsMatch(Extring,@"(THE|N)") && !Regex.IsMatch(Extring, @"(END|IF)") && !Regex.IsMatch(Extring, @"(ELS|E)") && !Regex.IsMatch(Extring, @"(END|ELSE)"))||Extring=="INT")
                {
                    if (Regex.IsMatch(Extring, @"^(SUM|SUB|DIV|MLT|MOD|INT|FLT|DOU)$"))
                    { 

                        switch (Extring)
                        {
                            case "SUM": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "SUM", Definicion = "FUNCIÓN SUMA", Sintaxis = "SUM(N,N);", Ejemplo = "SUM(4,2);", Linea = lineas + 1 }); break;
                            case "SUB": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "SUB", Definicion = "FUNCIÓN RESTA", Sintaxis = "SUB(N,N);", Ejemplo = "SUB(4,2);", Linea = lineas + 1 }); break;
                            case "MLT": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "MLT", Definicion = "FUNCIÓN MULTIPLICACIÓN", Sintaxis = "MLT(N,N);", Ejemplo = "MLT(4,2);", Linea = lineas + 1 }); break;
                            case "DIV": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "DIV", Definicion = "FUNCIÓN DIVISIÓN", Sintaxis = "DIV(N,N);", Ejemplo = "DIV(4,2);", Linea = lineas + 1 }); break;
                            case "MOD": ListaIdent.Add(new EstructuraLexica { Token = "RESERVADA", Lexema = "MOD", Definicion = "FUNCIÓN MODULAR", Sintaxis = "MOD(N,N);", Ejemplo = "MOD(4,2);", Linea = lineas + 1 }); break;
                            case "INT": ListaIdent.Add(new EstructuraLexica { Token = "PRIMITIVO", Lexema = "INT", Definicion = "TIPO PRIMITIVO", Sintaxis = "INT NOM_VAR:=N;", Ejemplo = "INT a:=5;", Linea = lineas + 1 }); break;
                            case "FLT": ListaIdent.Add(new EstructuraLexica { Token = "PRIMITIVO", Lexema = "FLT", Definicion = "TIPO PRIMITIVO", Sintaxis = "FLT NOM_VAR:=N;", Ejemplo = "FLT a:=5.3F;", Linea = lineas + 1 }); break;
                            case "DOU": ListaIdent.Add(new EstructuraLexica { Token = "PRIMITIVO", Lexema = "DOU", Definicion = "TIPO PRIMITIVO", Sintaxis = "FLT NOM_VAR:=N;", Ejemplo = "DOU a:=5.63D;", Linea = lineas + 1 }); break;
                            default: throw new ArgumentException("Interrupación ninguna coincidencia en palabras reservadas");
                        }
                        Extring = "";
                    }
                    // si no entonces envía una interrupción la cual especifica que no se detectaron 
                    //las palabras reservadas como primera mención (esto es importante)
                    else
                    {
                        throw new ArgumentException("Interrupción, ninguna coincidencia en palabras reservadas");
                    }

                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring.Length == 1 && Regex.IsMatch(Extring, @"^(a|b|c|d|e|f|g)$"))
                {
                    switch (Extring)
                    {
                        case "a": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "a", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT a:=2.3F;", Linea = lineas + 1 }); break;
                        case "b": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "b", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "INT b:=2;", Linea = lineas + 1 }); break;
                        case "c": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "c", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT c:=10.2F;", Linea = lineas + 1 }); break;
                        case "d": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "d", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "INT d:=3;", Linea = lineas + 1 }); break;
                        case "e": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "e", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT e:=3.5F;", Linea = lineas + 1 }); break;
                        case "f": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "f", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "INT f:=3;", Linea = lineas + 1 }); break;
                        case "g": ListaIdent.Add(new EstructuraLexica { Token = "VAR", Lexema = "g", Definicion = "VARIABLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT g:=3.4F;", Linea = lineas + 1 }); break;
                        default: throw new ArgumentException("Interrupación ninguna coincidencia en palabras reservadas");

                    }
                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring == "<")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "LSD", Lexema = "<", Definicion = "MENOR QUE", Sintaxis = "IF(VAR<5)", Ejemplo = "IF(VAR<4) ", Linea = lineas + 1 });

                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring == ">")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "GDR", Lexema = ">", Definicion = "MAYOR QUE", Sintaxis = "IF(5>a)", Ejemplo = "IF(8>a)", Linea = lineas + 1 });

                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring.Length > 1 && Extring == ":=")
                {
                        ListaIdent.Add(new EstructuraLexica { Token = "ASI", Lexema = ":=", Definicion = "ASIGNACIÓN", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT a:=2.3F;", Linea = lineas + 1 }); 
                   
                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring.Length > 1 && Extring == "==")
                {
                   ListaIdent.Add(new EstructuraLexica { Token = "BOOL EQUAL", Lexema = Extring.ToString(), Definicion = "IGUALDAD LÓGICA", Sintaxis = "IF(a==b)", Ejemplo = "IF(a==5)", Linea = lineas + 1 }); 
                    
                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring.Length > 1 && Extring == "!=")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "DESIGUALDAD", Lexema = Extring.ToString(), Definicion = "DESIGUALDAD LÓGICA", Sintaxis = "IF(a!=b)", Ejemplo = "IF(a!=5)", Linea = lineas + 1 });

                    Extring = "";
                }
                //if (!String.IsNullOrWhiteSpace(Extring) && Regex.IsMatch(Extring, @"(\d)") && Auxiliar.ToArray()[i+1].ToString() !="."&& Regex.IsMatch(Extring, @"(\x2C\d)"))
                //{
                //       ListaIdent.Add(new EstructuraLexica { Token = "NUM", Lexema = Extring.ToString(), Definicion = "NÚMERO", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT a:=2.3F;" });
                //       Extring = "";
                //}
                //(Auxiliar.ToArray()[i + 1].ToString() == "," || Auxiliar.ToArray()[i + 1].ToString() == ")" || Auxiliar.ToArray()[i + 1].ToString() == ";")
                //(Regex.IsMatch(array[i].ToString(), @"[0-9]{1,1}")
                //Dado un numero double se debe especificar una D mayúscula que vaya con las siguientes restricciones
                //1. debe respetar un número de cualquier longitud de números del [0-9] almenos una vez o más dígitos 
                //2. debe colocarse un punto para seguir con la escritura de los decimales
                //3. los números decimales pueden ser cualquier longitud de 1,...,n en los cuales son dígitos del [0-9]
                if (!String.IsNullOrWhiteSpace(Extring) && Regex.IsMatch(Extring, @"(\d+?\x2E\d+?)D"))
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "NUM", Lexema = Extring.ToString(), Definicion = "DOUBLE", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT a:=2.3F;", Linea = lineas + 1 });
                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Regex.IsMatch(Extring, @"(\d+?\x2E\d+?)F")|| (Regex.IsMatch(array[i].ToString(), @"[0-9]{1,1}")&&(Auxiliar.ToArray()[i+1].ToString()==","|| Auxiliar.ToArray()[i + 1].ToString() == ")"|| Auxiliar.ToArray()[i + 1].ToString() == ";")))
                {
                    if (Regex.IsMatch(Extring, @"(\d+?\x2E\d+?)F"))
                    {
                        ListaIdent.Add(new EstructuraLexica { Token = "NUM", Lexema = Extring.ToString(), Definicion = "FLOAT", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "FLT a:=2.3F;", Linea = lineas + 1 });
                    }
                    else
                    {
                        ListaIdent.Add(new EstructuraLexica { Token = "NUM", Lexema = Extring.ToString(), Definicion = "INT", Sintaxis = "TIPO VAR := NUM;", Ejemplo = "INT a:=2;", Linea = lineas + 1 });
                    }
                    
                    Extring = "";
                }
                if (!String.IsNullOrWhiteSpace(Extring) && Extring == "\x2E" && ListaIdent.ToArray()[3].Token == "RESERVADA")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "DOT", Lexema = Extring.ToString(), Definicion = "Conversion", Sintaxis = "TIPO VAR := FUN.TIPO(5,FUN(4,3));", Ejemplo = "FLT a:=MLT.FLT(5.3F,6);", Linea = lineas + 1 });
                    Extring = "";
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
                        case "(": ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = "(", Definicion = "DELIMITA LAS FUNCIONES", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);", Linea = lineas + 1 }); break;
                    }
                    LEXC = "";
                    Extring = "";
                }
                else if (i == 3 && !Regex.IsMatch(array[i].ToString(), @"^(\x28)$") && ListaIdent.ToArray()[0].Lexema != "INT" && ListaIdent.ToArray()[0].Lexema != "FLT" && ListaIdent.ToArray()[0].Lexema != "IF" && ListaIdent.ToArray()[0].Lexema != "DOU")
                {
                    throw new ArgumentException("Error en el parentesis apertura");
                }


                /*
                 Si se encuentra un número entero se enceuntra se agrega a la lista con sus especificaciones 
                  si no es así entonces si hay concidencias con una coma se agg a la lista
                */

                //if (Regex.IsMatch(array[i].ToString(), @"[0-9]{1,1}"))
                //{
                //    ListaIdent.Add(new EstructuraLexica { Token = "NÚMERO", Lexema = array[i].ToString(), Definicion = "NÚMERO ENTERO", Sintaxis = "NÚMERO [0-9]", Ejemplo = "0,1,2,3...9" });
                //    Extring = "";
                //}
                if (Extring == ",")
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = ",", Definicion = "DELIMITADOR COMA", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(N,N);", Linea = lineas + 1 });
                    Extring = "";
                }


                //Reconoce el paréntesis de clausura \x29 y lo agg a la lista "ListaIdent"
                if (Regex.IsMatch(array[i].ToString(), @"^(\x29)$"))
                {
                    LEXC += array[i].ToString();
                    switch (LEXC)
                    {
                        case ")": ListaIdent.Add(new EstructuraLexica { Token = "DELIMITADOR", Lexema = ")", Definicion = "DELIMITA LAS FUNCIONES", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);", Linea = lineas + 1 }); break;
                    }
                    LEXC = "";
                    Extring = "";

                }
                //identificar solamente si no hay un paréntesis de clausura
                else if (i == Auxiliar.Length - 2 && !Regex.IsMatch(array[Auxiliar.Length - 2].ToString(), @"^(\x29)$") && !Regex.IsMatch(Auxiliar, @"(\d{1,1}\x2E\d{1,1})F") && !(ListaIdent.ToArray()[0].Lexema == "INT") && !(ListaIdent.ToArray()[0].Lexema == "IF")&& !(ListaIdent.ToArray()[0].Lexema == "DOU"))
                {
                    throw new ArgumentException("parentesis de clausura");
                }
                // se termina la sentencia para las funciones aritméticas
                if (array[i] == ';')
                {
                    ListaIdent.Add(new EstructuraLexica { Token = "Ent. Terminal", Lexema = ";", Definicion = "SÍMBOLO TERMINAL", Sintaxis = "FUN(N,N);", Ejemplo = "FUN(4,2);", Linea = lineas + 1 });
                    Extring = "";
                }


                //al final se regresa la lista con las modificaciones correspondientes
               


            }
            return ListaIdent;
            //List<Complete> ListaCompleta = new List<Complete>();
            //public List<Complete> RegresarTokens(string CodeSource)
            //{
            //    return ListaCompleta;
            //}


        }
    }
}
