using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Models
{
    //******Esta clase no se utiliza*******
    public class DiccionarioDeSimbolos
    {
        public List<TablaDeSimbolos> tSimbolos = new List<TablaDeSimbolos>();
        public List<Complete> datos = new List<Complete>();
        public DiccionarioDeSimbolos() { } 
        public List<TablaDeSimbolos> TSimbolos { get => tSimbolos; set => tSimbolos = value; }

        public void Tokens()
        {
            //(int iD_Token, string token, string tipo, string descripcion_Tipo)
            TablaDeSimbolos tk = new TablaDeSimbolos(0, "<!--", "MSG", "Inicio de una linea de comentario");
            tSimbolos.Add(tk);
            TablaDeSimbolos tk1 = new TablaDeSimbolos(1, "-->", "MSG", "Fin de una linea de comentario");
            tSimbolos.Add(tk1);
            TablaDeSimbolos tk2 = new TablaDeSimbolos(2, "<<--", "MSG", "Inicio de mas de una linea de comentario");
            tSimbolos.Add(tk2);
            TablaDeSimbolos tk3 = new TablaDeSimbolos(3, "<<--", "MSG", "Fin de mas de una linea de comentario");
            tSimbolos.Add(tk3);
            TablaDeSimbolos tk4 = new TablaDeSimbolos(4, "{", "BLQ", "Inicio de un bloque");
            tSimbolos.Add(tk4);
            TablaDeSimbolos tk5 = new TablaDeSimbolos(5, "}", "BLQ", "Fin de un bloque");
            tSimbolos.Add(tk5);
            TablaDeSimbolos tk6 = new TablaDeSimbolos(6, "INT", "TIY", "Número entero");
            tSimbolos.Add(tk6);
            TablaDeSimbolos tk7 = new TablaDeSimbolos(7, "DOUBLE", "TIY", " Número con decimales");
            tSimbolos.Add(tk7);
            TablaDeSimbolos tk8 = new TablaDeSimbolos(8, "STRING", "TIY", " Cadena de caracteres");
            tSimbolos.Add(tk8);
            TablaDeSimbolos tk9 = new TablaDeSimbolos(9, "BOOL", "TIY", " Booleano true or false");
            tSimbolos.Add(tk9);
            TablaDeSimbolos tk10 = new TablaDeSimbolos(10, "SUB", "FUN", " Representa una resta");
            tSimbolos.Add(tk10);
            TablaDeSimbolos tk11 = new TablaDeSimbolos(11, "SUM", "FUN", "Representa una suma");
            tSimbolos.Add(tk11);
            TablaDeSimbolos tk12 = new TablaDeSimbolos(12, "MLT", "FUN", "Representa una multiplicacion");
            tSimbolos.Add(tk12);
            TablaDeSimbolos tk13 = new TablaDeSimbolos(13, "DIV", "FUN", "Representa una divisón");
            tSimbolos.Add(tk13);
            TablaDeSimbolos tk14 = new TablaDeSimbolos(14, ":=", "ASI", "Símbolo de asignación");
            tSimbolos.Add(tk14);
            TablaDeSimbolos tk15 = new TablaDeSimbolos(15, ">", "OPE", "Mayor que");
            tSimbolos.Add(tk15);
            TablaDeSimbolos tk16 = new TablaDeSimbolos(16, "<", "OPE", "Menor que");
            tSimbolos.Add(tk16);
            TablaDeSimbolos tk17 = new TablaDeSimbolos(17, ">=", "OPE", "Mayor o igual que");
            tSimbolos.Add(tk17);
            TablaDeSimbolos tk18 = new TablaDeSimbolos(18, "<=", "OPE", "Menor o igual que");
            tSimbolos.Add(tk18);
            TablaDeSimbolos tk19 = new TablaDeSimbolos(19, "&&", "OPL", "Condición esto y esto");
            tSimbolos.Add(tk19);
            TablaDeSimbolos tk20 = new TablaDeSimbolos(20, "||", "OPL", "Condición esto o esto");
            tSimbolos.Add(tk20);
            TablaDeSimbolos tk21 = new TablaDeSimbolos(21, "::", "OPL", "Condición esto es igual a esto");
            tSimbolos.Add(tk21);
            TablaDeSimbolos tk22 = new TablaDeSimbolos(22, "!:", "OPL", "Condición diferente a");
            tSimbolos.Add(tk22);
            TablaDeSimbolos tk23 = new TablaDeSimbolos(23, "!", "OPL", "Condición de negación");
            tSimbolos.Add(tk23);
            TablaDeSimbolos tk24 = new TablaDeSimbolos(24, "ESCIRBE", "REW", "Muestra en consola");
            tSimbolos.Add(tk24);
            TablaDeSimbolos tk25 = new TablaDeSimbolos(25, "LEE", "REW", "captura un valor desde consola");
            tSimbolos.Add(tk25);
            TablaDeSimbolos tk26 = new TablaDeSimbolos(26, "METHOD", "REW", "Define una función");
            tSimbolos.Add(tk26);
            TablaDeSimbolos tk27 = new TablaDeSimbolos(27, "CLASS", "REW", "Define una clase");
            tSimbolos.Add(tk27);
            TablaDeSimbolos tk28 = new TablaDeSimbolos(28, "IF", "REW", "Si tal condicion se cumple");
            tSimbolos.Add(tk28);
            TablaDeSimbolos tk29 = new TablaDeSimbolos(29, "ELSE", "REW", "Si tal condición no se cumple");
            tSimbolos.Add(tk29);
            TablaDeSimbolos tk30 = new TablaDeSimbolos(30, "(", "DEL", "Delimitador ");
            tSimbolos.Add(tk30);
            TablaDeSimbolos tk31 = new TablaDeSimbolos(31, ")", "DEL", "Delimitador");
            tSimbolos.Add(tk31);
            TablaDeSimbolos tk32 = new TablaDeSimbolos(32, "~", "CNC", " Permite concatenar variables");
            tSimbolos.Add(tk32);
            TablaDeSimbolos tk33 = new TablaDeSimbolos(33, "'", "TSY", " Indica donde comienza y termina un string");
            tSimbolos.Add(tk33);
            TablaDeSimbolos tk34 = new TablaDeSimbolos(33, ";", "TER", "Termina la sentencia");
            tSimbolos.Add(tk34);
            TablaDeSimbolos tk35 = new TablaDeSimbolos(33, ",", "COM", "Delimitador");
            tSimbolos.Add(tk35);
            TablaDeSimbolos tk36 = new TablaDeSimbolos(33, ".", "DOT", "Símbolo conversor");
            tSimbolos.Add(tk36);
            TablaDeSimbolos tk37 = new TablaDeSimbolos(33, "THEN", "THE", "Entonces tipo if");
            tSimbolos.Add(tk37);
            
        }

        public List<TablaDeSimbolos> ObtenerTokens()
        {
            return tSimbolos;
        }

        public List<Complete> BuscarToken(string argumento, int linea, string regla)
        {
            foreach (var word in tSimbolos)
            {
                if (word.Token1 == argumento)
                {
                    if (Verificar((linea + 1).ToString()) == true)
                    {
                        return datos;
                    }
                    else
                    {
                        datos.Add(new Complete(word.Token1, word.Tipo1, (linea + 1).ToString(), word.ID_Token1.ToString(), regla, word.Descripcion_Tipo1));
                        return datos;
                    }
                }
            }
            return null;
        }

        private bool Verificar(string linea)
        {
            foreach (var x in datos)
            {
                if (x.Linea == linea)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

