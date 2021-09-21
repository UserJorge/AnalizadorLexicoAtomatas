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

    //public List<AnalizadorResultado> Compilar(List<EstructuraLexica> r)
    //{

    //Aquí se pondrá el código que se propondrá en la segunda parte
    // se generará código intermedio y máquina que recorrerá los tokens que vienen de List<EstructuraLexica>
    //se pasará a codigo entandible mediante la máquina de turing con un algoritmo.

    //}
    public class Compilador
        {
            public List<AnalizadorResultado> Estructuras;
            public List<AnalizadorResultado> Compilar(List<EstructuraLexica> r)
            {

            Stack<EstructuraLexica> est = new Stack<EstructuraLexica>();
               Estructuras = new List<AnalizadorResultado>();
                string midCode = "";

                //  Aquí se pondrá el código que se propondrá en la segunda parte
                // se generará código intermedio y máquina que recorrerá los tokens que vienen de List<EstructuraLexica>
                //se pasará a codigo entandible mediante la máquina de turing con un algoritmo.
                int resultado = 0;
               // int count = 0;
                char[] simbolos;
                foreach (EstructuraLexica item in r)
                {
                    est.Push(item);
                    simbolos = new char[midCode.Length];

                if (Regex.IsMatch(item.Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$"))
                {

                    switch (item.Lexema)
                    {
                        case "SUM": Estructuras.Add(new AnalizadorResultado { Estructura = "Función suma", Detalle = "Palabra Reservada"}); midCode += "+"; break;
                        case "SUB": Estructuras.Add(new AnalizadorResultado { Estructura = "Función resta", Detalle = "Palabra Reservada"}); midCode += "-"; break;
                        case "MLT": Estructuras.Add(new AnalizadorResultado { Estructura = "Función multiplicación", Detalle = "Palabra Reservada"}); midCode += "*"; break;
                        case "DIV": Estructuras.Add(new AnalizadorResultado { Estructura = "Función división", Detalle = "Palabra Reservada" }); midCode += "/"; break;
                        case "MOD": Estructuras.Add(new AnalizadorResultado { Estructura = "Función residuo", Detalle = "Palabra Reservada" }); midCode += "%"; break;

                        default: 
                            throw new ArgumentException("Interrupción, ninguna coincidencia en palabras reservadas");
                    }
                  
                }

                    if (item.Lexema.ToString() == ";")
                    {
                        Estructuras.Add(new AnalizadorResultado { Estructura = "RESULTADO: " + resultado.ToString(), Detalle = "ENT" });

                    }
                //else if (!item.Lexema.Contains(";"))
                //{
                //    throw new ArgumentException("No se colocó el punto y coma");
                //}
            }
                return Estructuras;
            }

           



        }


    }

