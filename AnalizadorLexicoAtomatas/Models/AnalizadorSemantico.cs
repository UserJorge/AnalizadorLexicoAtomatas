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
    public class AnalizadorSemantico
    {
        /*
       1. Funciones aritméticas
       2. Asignación 
       3. Conversion de tipo
       4. Declaración de variables
       5. Estructura de control de flujo condicional (sentencia if)  
       */
        string palabra;
        List<EstructuraLexica> lexemasSecuencia;
        public void SequenceCheck(List<EstructuraLexica> secuenciaTokens)
        {
            lexemasSecuencia = new List<EstructuraLexica>();
            //verificar una función aritmética
            Regex mFUN = new Regex(@"^[A-Z]{3,3}\((\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;$");
           //verificar una asignación simple
            Regex mASI = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=\d+\.?\d*(D?|F?)\;$");
           //Verificar la asignación de una función
            Regex mASIFUN = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=[A-Z]{3,3}\((\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;$");
            //verificar la conversión de tipos
            Regex mCONV = new Regex(@"^[A-Z]{3,3}\s?[a-z]\:\=[A-Z]{3,3}\.[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;$");
            //declaración de una variable simple
            Regex mDECL = new Regex(@"^[A-Z]{3,3}\s?[a-z]\;$");
            //estructura de control de flojo condicional
            Regex mIF = new Regex(@"IF\(([a-z]+|\d+\.?\d*(D?|F?))\=\=([a-z]+|\d+\.?\d*(D?|F?))\)\r\nTHEN\r\n[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;\r\nENDIF\r\nELSE\r\n[A-Z]{3,3}\((\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\,(\d+\.?\d*(D?|F?)|[A-Z]{3,3}\(\d+\.?\d*(D?|F?)\,\d+\.?\d*(D?|F?)\))\)\;\r\nENDELSE\;");
            //verificar que una función aritmetica con valores estáticos es correcta en su forma gramatical-sintáctica.
            for (int i = 0; i < secuenciaTokens.Count(); i++)
            {
                //cuando llegue a un punto y coma se salta el for porque ya termino de analizar la sentencia
                if (secuenciaTokens.ToArray()[i].Linea==1)
                {
                    palabra += secuenciaTokens.ToArray()[i].Lexema;
                    lexemasSecuencia.Add(secuenciaTokens.ToArray()[i]);
                }
            }
            if (mFUN.IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else if (mASI.IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else if (mASIFUN.IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else if (mCONV.IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else if (mDECL.IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else if (mIF .IsMatch(palabra))
            {
                palabra = "";
                lexemasSecuencia.Clear();
            }
            else
            {
                
               throw new ArgumentException("Ocurrió una excepción en el análisis sintáctico línea: "+lexemasSecuencia.ToArray()[1].Linea.ToString()+ VerificacionExterna(palabra, lexemasSecuencia));
            }
        }
        string problema;
        private string VerificacionExterna(string palabra, List<EstructuraLexica> lexemasSecuencia)
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










            return problema;
        }
    }
}
