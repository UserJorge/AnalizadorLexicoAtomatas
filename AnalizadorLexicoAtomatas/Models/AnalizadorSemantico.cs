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

        public void SequenceCheck(List<EstructuraLexica> secuenciaTokens)
        {
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
            Regex mIF = new Regex(@"IF\(([a-z]+|\d+)\=\=([a-z]+|\d+)\)\r\nTHEN\r\n[A-Z]{3,3}\((\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;\r\nENDIF\r\nELSE\r\n[A-Z]{3,3}\((\d+|[A-Z]{3,3}\(\d+\,\d+\))\,(\d+|[A-Z]{3,3}\(\d+\,\d+\))\)\;\r\nENDELSE\;");
            //verificar que una función aritmetica con valores estáticos es correcta en su forma gramatical-sintáctica.
            for (int i = 0; i < secuenciaTokens.Count(); i++)
            {
                if (secuenciaTokens.ToArray()[i].Linea==1)
                {

                }
            }
        }

    }
}
