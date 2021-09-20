using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Models
{
    //******Esta clase no se utiliza*******


    //lo que se hará a continuación 
    //traer el source code de la parte del viewModel
    //identificar cada uno de los identificadores, palabras clave y signos de puntuación del lenguaje
    //hacer un segundo escaneo y agg en otra lista o string solamente los identificadores y
    //mediante una automata finito determinista comprobar una estructura y darle validez
    //así con las demás estructuras o condicionales que se presentan
    //* hay que tomar en cuenta que se va a trabajar con multilínea \n \r y demás simbología
    //RegexString y Regex se van a incluir
    public class AnalizadorInterpreteLexico
    {

        public string SourceCode { get; set; }
        /*
        1. Funciones aritméticas
        2. Asignación 
        3. Conversion de tipo
        4. Declaración de variables
        5. Estructura de control de flujo condicional (sentencia if)  
        */

        /*4 variantes en cuanto a funciones aritméticas
          SUM(8,3);
          SUM(SUB(8,3),2);
          SUM(2,MLT(3,2));
          SUM(SUB(8,3),MLT(2,3));   
        /*
        Asignación y declaración de una variable int, float, bool, string, double
        INT a:= 5;
        */

        /* Conversion de tipos en operaciones de funciones aritméticas
          INT z:=SUM.INT(5,3.2F);
          FLT a:=MLT.FLT(5.7,4);
         */
        /*
          INT b:=5;
          IF(variable1==variable2)
           THEN
           SUM(8,3);
           ENDIF 
           ELSE
           SUB(b,2);
           ELSEEND;

         */
        DiccionarioDeSimbolos diccionario;
        public List<Complete> AnalizarLexico(string codeSource)
        {
            List<Complete> listaLexica = new List<Complete>();
            diccionario = new DiccionarioDeSimbolos();
            TablaDeSimbolos elemento;
            for (int i = 0; i < codeSource.Length; i++)
            {
                if (Regex.IsMatch(codeSource.Substring(i, 3),@"(INT|SUB|SUM|MLT|DIV|LEE)"))
                {
                    elemento = diccionario.TSimbolos.FirstOrDefault(x => x.Token1 == codeSource.Substring(i, 3));
                    listaLexica.Add(new Complete(elemento.Token1, elemento.Tipo1, "0", elemento.ID_Token1.ToString(), "generic", elemento.Descripcion_Tipo1));
                }
            }
            return listaLexica;
        }
















    }
}
