using AnalizadorLexicoAtomatas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Models
{
    //******Esta clase no se utiliza*******
    public class GrammerSintactico
    {

        /*
       1. Funciones aritméticas
       2. Asignación 
       3. Conversion de tipo
       4. Declaración de variables
       5. Estructura de control de flujo condicional (sentencia if)  
       */

        /*
        Sentencia de declaración
        Sentencias ejecutables
        Funciones y procedimientos 
        Identificación de variables
        Etiquetas
        Constentes
        Conversiones y equivalencias de tipo
        Sobrecarga de operadores y funciones
         */
        //private GrammerSintactico sintactico; 

        //public GrammerSintactico Sintactico
        //{
        //    get { return sintactico; }
        //    set { sintactico = value; }
        //}

        private List<string> palabraFUN=new List<string>();

        public List<string> PalabraFUN
        {
            get { return palabraFUN; }
            set { palabraFUN = value; }
        }
        private List<string> palabraASI = new List<string>();

        public List<string> PalabraASI
        {
            get { return palabraASI; }
            set { palabraASI = value; }
        }
        private List<string> palabraASIFUN = new List<string>();

        public List<string> PalabraASIFUN
        {
            get { return palabraASIFUN; }
            set { palabraASIFUN = value; }
        }
        private List<string> palabraCONV = new List<string>();

        public List<string> PalabraCONV
        {
            get { return palabraCONV; }
            set { palabraCONV = value; }
        }
        private List<string> palabraDECL = new List<string>();

        public List<string> PalabraDECL
        {
            get { return palabraDECL; }
            set { palabraDECL = value; }
        }

        private List<string> palabraIF = new List<string>();

        public List<string> PalabraIF
        {
            get { return palabraIF; }
            set { palabraIF = value; }
        }
        ArbolBinario binario = new ArbolBinario();
        ArbolBinario.Arbol arbolBinario = new ArbolBinario.Arbol();

        string palabraArbol;
        public void CrearArbol()
        {
            //Por precedencia en la contrucción del árbol binario (de menor a mayor) en recorrido descendente 
            //1.Declaración de variable simple
            for (int i = 0; i < PalabraFUN.Count(); i++)
            {
                palabraArbol += PalabraFUN.ToArray()[i];
                //función simple
                if (Regex.IsMatch(palabraArbol,@"(SUM|SUB|MLT|DIV|MOD)\("))
                {
                    switch (palabraArbol)
                    {
                        case "SUM":arbolBinario.InsertarNodo(palabraArbol) ;break;
                        case "SUB": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "MLT": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "DIV": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "MOD": arbolBinario.InsertarNodo(palabraArbol); break;
                        
                        default:
                            break;
                    }
                    palabraArbol = "";
                }
                if (Regex.IsMatch(palabraArbol, @"\d+\.?\d*(D?|F?)\,"))
                {
                    switch (palabraArbol)
                    {
                        case "SUM": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "SUB": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "MLT": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "DIV": arbolBinario.InsertarNodo(palabraArbol); break;
                        case "MOD": arbolBinario.InsertarNodo(palabraArbol); break;

                        default:
                            break;
                    }
                    palabraArbol = "";
                }
                palabraArbol = "";
            }
           
            
           
        }





        string palabra;
        List<EstructuraLexica> lexemasSecuencia;
        ArbolBinario.Arbol arbol = new ArbolBinario.Arbol();
        public void SequenceCheck(List<EstructuraLexica> secuenciaTokens)
        {
            lexemasSecuencia = new List<EstructuraLexica>();
            //verificar una función aritmética
            Regex mFUN = new Regex(@"^[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;$");
            //*verificar una asignación simple
            Regex mASI = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=\d+\.?\d*(D?|F?)\;$");
            //*Verificar la asignación de una función
            Regex mASIFUN = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=[A-Z]{3,3}\((\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;$");
            //verificar la conversión de tipos
            Regex mCONV = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=[A-Z]{3,3}\.[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;$");
            //*declaración de una variable simple
            Regex mDECL = new Regex(@"^[A-Z]{3,3}\s?[a-z]\;$");
            //estructura de control de flojo condicional
            Regex mIF = new Regex(@"IF\(([a-z]+|\d+\.?\d*(D?|F?))\=\=([a-z]+|\d+\.?\d*(D?|F?))\)\r\nTHEN\r\n[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;\r\nENDIF\r\nELSE\r\n[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;\r\nENDELSE\;");
            //verificar que una función aritmetica con valores estáticos es correcta en su forma gramatical-sintáctica.
            for (int i = 0; i < secuenciaTokens.Count(); i++)
            {
                //cuando llegue a un punto y coma se salta el for porque ya termino de analizar la sentencia
                if (secuenciaTokens.ToArray()[i].Linea == 1)
                {
                    palabra += secuenciaTokens.ToArray()[i].Lexema;
                    lexemasSecuencia.Add(secuenciaTokens.ToArray()[i]);
                }
            }
            //ir convirtiendo el flujo de strings a codigo C# para hacerlo comprensible a la máquina
            //después lo guardamos en un archivo .cs para que pueda ser utilizado e incorporado para arrojar un resultado de lo que se planteó
            switch (palabra)
            {
                case var vv when mFUN.IsMatch(palabra):
                    PalabraFUN.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                case var vv when mASI.IsMatch(palabra):
                    PalabraASI.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                case var vv when mASIFUN.IsMatch(palabra):
                    PalabraASIFUN.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                case var vv when mCONV.IsMatch(palabra):
                    PalabraCONV.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                case var vv when mDECL.IsMatch(palabra):
                    PalabraDECL.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                case var vv when mIF.IsMatch(palabra):
                    PalabraIF.Add(palabra);
                    palabra = "";
                    lexemasSecuencia.Clear();
                    break;
                default:
                    //throw new ArgumentException("Ocurrió una excepción en el análisis sintáctico línea: " + lexemasSecuencia.ToArray()[1].Linea.ToString() + VerificacionExterna(palabra, lexemasSecuencia));
                    break;

            }

        } 
        //Gestión de errores sintácticos
        string problema;

        public string VerificacionExterna(string palabra, List<EstructuraLexica> lexemasSecuencia)
        {
            //Reglas de derivación de la gramática del lenguaje
            //Función aritmética simple

            //1. que se encuantre bien escrita la funcion de 3 letras en mayúsculas (de SUM,SUB,MLT,DIV,MOD)
            //2.Que se hayan cerrado correctamente los paréntesis externos
            //3. que se haya escrito correctamente el número sea entero, flotante o double
            //4. o que si escribió una función anidada se den las condiciones comentadas anteriormente
            //5. después que se respete la coma (,) del delimitador
            //6. que se verifique si es un número o función anidada según las restricciones anteriores
            //7. se cierren cada uno de los paréntesis abiertos 
            //8. que se verifique que la sentencia se terminó con el punto y coma (;)

          
                if (!Regex.IsMatch(palabra.Substring(0,3),@"(SUM|SUB|MLT|DIV|MOD)"))
                {
                    return problema="Nombre de la función aritmética no establecido correctamente";
                }
                else if (Regex.IsMatch(palabra, @"^[A-Z]{3,3}(\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;$"))
                {
                    return problema="paréntesis de apertura no escrito";
                }
                else if (Regex.IsMatch(palabra, @"^[A-Z]{3,3}\(?(\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\;$"))
                {
                    return problema="paréntesis de clausura no escrito";
                }

                








            return problema;
        }
    }
}
