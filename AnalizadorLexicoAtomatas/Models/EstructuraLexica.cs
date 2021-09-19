using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Model
{
    public class EstructuraLexica
    {
        private string lexema;

        public string Lexema
        {
            get { return lexema; }
            set { lexema = value; }
        }

        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        private string definicion;

        public string Definicion
        {
            get { return definicion; }
            set { definicion = value; }
        }

        private string sintaxis;

        public string Sintaxis
        {
            get { return sintaxis; }
            set { sintaxis = value; }
        }

        private string ejemplo;

        public string Ejemplo
        {
            get { return ejemplo; }
            set { ejemplo = value; }
        }

        //private int linea;

        //public int Linea
        //{
        //    get { return linea; }
        //    set { linea = value; }
        //}

        //private int columna;

        //public int Columna
        //{
        //    get { return columna; }
        //    set { columna = value; }
        //}

    }
}
