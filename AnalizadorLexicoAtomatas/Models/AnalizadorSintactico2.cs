using AnalizadorLexicoAtomatas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace AnalizadorLexicoAtomatas.Models
{
    public class AnalizadorSintactico2
    {

        private Stack<EstructuraLexica> pila;

        public Stack<EstructuraLexica> Pila
        {
            get { return pila; }
            set { pila = value; }
        }

        private int a;

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        private int b;

        public int B
        {
            get { return b; }
            set { b = value; }
        }

        private int layerone;

        public int LayerOne
        {
            get { return layerone; }
            set { layerone = value; }
        }
        private int layertwo;

        public int LayerTwo
        {
            get { return layertwo; }
            set { layertwo = value; }
        }


        private int res;
        public int Resultado
        {
            get { return res; }
            set { res = value; }
        }


        //El método de sintaxis check recibe una lista del tipo EstructuraLexica para verificar y hacer las operaciones necesarias
        //para compilar los valores y dar un resultado al planteamiento.
        List<AnalizadorResultado> Estructuras;
        public List<AnalizadorResultado> SintaxisCheck(List<EstructuraLexica> estructuras)
        {
            //se instancía la pila
            pila = new Stack<EstructuraLexica>();
            //se crea una instancia de una lista de estructura lexica
            Estructuras = new List<AnalizadorResultado>();

            //coloca todas aquellos elementos en un stack
            foreach (EstructuraLexica item in estructuras)
            {
                Pila.Push(item);
            }


            //se declaran variables (detallar funciones de cada una) y en donde tienen funcionalidad (cite la linea)
            //count se utiliza como bandera la cual indica el estado del escaneo donde se encuentra el ciclo de asignación 
            //a las variables A y B que son los nodos hoja del arbol que estas variables representan los operandos
            int count = 0;
            int operation = 0;
            //int sideA = 0;
            int incrementa = 0;
            int auxiliar = 0;
            int auxiliarA = 0;
            int auxiliarB = 0;
            string palabra = "";
            string palabraFuncion = "";

            //se hace un ciclo for con terminación en la cantidad de elementos en la pila
            for (int i = 0; i < Pila.Count; i++)
            {
                //si acaso te encuentras con un dígito (número)
                //entonces colocalo en la variable A, si ya está ocupada elige la rama B
                //despues de asignar en la B por en el estado 0 a la variable 'count'
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"\d"))
                {
                    //count es la bandera que indica en que estado estamos al momento de trazar donde van los 
                    //operandos
                    count++;
                    if (count == 1)
                    {
                        A = int.Parse(Pila.ToArray()[i].Lexema);
                    }
                    if (count == 2)
                    {
                        B = int.Parse(Pila.ToArray()[i].Lexema);
                        count = 0;
                    }
                }

                /* Acepta formulas del tipo SUM(4,MLT(2,4));*/
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$") && i != Pila.Count() - 1&& !Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']\d['(']"))
                {
                    operation++;
                    if (operation == 1)
                    {
                        switch (Pila.ToArray()[i].Lexema)
                        {
                            case "SUM": LayerOne += Suma(B, A); break;
                            case "SUB": LayerOne += Resta(B, A); break;
                            case "DIV": LayerOne += Division(B, A); break;
                            case "MLT": LayerOne += Multiplicacion(B, A); break;
                            case "MOD": LayerOne += Modular(B, A); break;

                        }

                    }
                    else if (operation == 2)
                    {
                        switch (Pila.ToArray()[i].Lexema)
                        {
                            case "SUM": LayerTwo += Suma(B, A); break;
                            case "SUB": LayerTwo += Resta(B, A); break;
                            case "DIV": LayerTwo += Division(B, A); break;
                            case "MLT": LayerTwo += Multiplicacion(B, A); break;
                            case "MOD": LayerTwo += Modular(B, A); break;
                        }
                    }



                }
                /* Formula  del tipo SUM(SUB(8,6),MLT(5,4));*/
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$") && i == Pila.Count() - 1 && LayerTwo != 0 && LayerOne != 0)
                {
                    switch (Pila.ToArray()[i].Lexema)
                    {
                        case "SUM": Resultado += Suma(LayerTwo, LayerOne); break;
                        case "SUB": Resultado += Resta(LayerTwo, LayerOne); break;
                        case "DIV": Resultado += Division(LayerTwo, LayerOne); break;
                        case "MLT": Resultado += Multiplicacion(LayerTwo, LayerOne); break;
                        case "MOD": Resultado += Modular(LayerTwo, LayerOne); break;
                    }
                   
                }
                if (palabra.Length < 16 && i != 0)
                {
                    palabra += Pila.ToArray()[i].Lexema;
                }
                else
                {
                    palabra = "";
                }

                /*Corregir el código if*/
                /*Ejemplo quiero hacer La fórmula SUM(8,SUB(8,4));*/

                if (Regex.IsMatch(palabra, @"^[')'][')']\d[',']\d['('](SUM|SUB|DIV|MLT|MOD)[',']\d['('](SUM|SUB|DIV|MLT|MOD)$") && LayerTwo == 0 && LayerOne != 0)
                {

                    //sideA++;
                    for (int j = 0; j < palabra.Length; j++)
                    {
                        if (j == 10)
                        {
                            B = int.Parse(palabra.ToArray()[j].ToString());
                        }
                        if (j == 12 || j == 13 || j == 14)
                        {
                            palabraFuncion += palabra.ToArray()[j].ToString();
                        }

                    }

                    switch (palabraFuncion)
                    {

                        case "SUM": LayerTwo = B; Resultado += Suma(LayerTwo, LayerOne); break;
                        case "SUB": LayerTwo = B; Resultado += Resta(LayerTwo, LayerOne); break;
                        case "DIV": LayerTwo = B; Resultado += Division(LayerTwo, LayerOne); break;
                        case "MLT": LayerTwo = B; Resultado += Multiplicacion(LayerTwo, LayerOne); break;
                        case "MOD": LayerTwo = B; Resultado += Modular(LayerTwo, LayerOne); break;

                    }

                }
                //control del flujo de colocación del número utilizando auxiliares
                if (Regex.IsMatch(palabra, @"^[')']\d$"))
                {
                    for (int k = 0; k < palabra.Length; k++)
                    {
                        if (Regex.IsMatch(palabra.ToArray()[k].ToString(), @"\d"))
                        {
                            incrementa++;
                            if (incrementa == 1)
                            {
                                auxiliar = A;
                                //A = 0;
                                //B = 0;
                            }

                        }
                                                   
                    }
                }

                if (Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']"))
                {
                    auxiliarB++;
                    if (auxiliarB == 1)
                    {
                        auxiliarA = int.Parse(palabra.ToArray()[i - 2].ToString());
                        //B = auxiliarA;
                        // LayerTwo = auxiliarA - A;
                    }
                }
                /*Corregir el código if 2*/
                /*Ejemplo quiero hacer La fórmula SUM(SUM(8,4),4);*/
                /*[')']\d[',']{1}[')']{1}\d[',']{1}\d['(']{1}['(']{1}*/
                /*@"[')']\d[','][')']\d[',']\d['('][A-Z]{3}['('][A-Z]{3}" */

                if (Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']\d['('][A-Z]{3}")&&!Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']\d['('][A-Z]{3}['(']"))
                {

                    for (int m = 0; m < palabra.Length; m++)
                    {
                        if (Regex.IsMatch(Pila.ToArray()[m].Lexema, @"(SUM|SUB|DIV|MLT|MOD)") && m != Pila.Count() - 1)
                        {
                            switch (Pila.ToArray()[i].Lexema)
                            {
                                case "SUM": LayerTwo += Suma(A, B); break;
                                case "SUB": LayerTwo += Resta(A, B); break;
                                case "DIV": LayerTwo += Division(A, B); break;
                                case "MLT": LayerTwo += Multiplicacion(A, B); break;
                                case "MOD": LayerTwo += Modular(A, B); break;
                            }
                        }
                    }

                }




                if (Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']\d['('][A-Z]{3}['('][A-Z]{3}"))
                {
                   // TraductorLateralDer(estructuras);
                    for (int j = 0; j < palabra.Length; j++)
                    {                     
                        if (j == 12 || j == 13 || j == 14)
                        {
                            palabraFuncion += palabra.ToArray()[j].ToString();
                        }

                    }
                    if (Resultado < 0)
                    {
                        Resultado = 0;
                    }

                    switch (palabraFuncion)
                    {

                        case "SUM": Resultado += Suma(LayerTwo, auxiliar); break;
                        case "SUB": Resultado += Resta(LayerTwo, auxiliar); break;
                        case "DIV": Resultado += Division(LayerTwo, auxiliar); break;
                        case "MLT": Resultado += Multiplicacion(LayerTwo, auxiliar); break;
                        case "MOD": Resultado += Modular(LayerTwo, auxiliar); break;
                    }
                }

                /* Fórmula simple del tipo SUM(8,3)*/
                /*&& B != 0 && A != 0*/
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$") && LayerTwo == 0 && LayerOne == 0 && !Regex.IsMatch(palabra, @"[')']\d[','][')']\d[',']\d['('][A-Z]{3}"))
                {
                    switch (Pila.ToArray()[i].Lexema)
                    {
                        case "SUM": Resultado += Suma(B, A); break;
                        case "SUB": Resultado += Resta(B, A); break;
                        case "DIV": Resultado += Division(B, A); break;
                        case "MLT": Resultado += Multiplicacion(B, A); break;
                        case "MOD": Resultado += Modular(B, A); break;
                    }
                }
            }
            Estructuras.Add(new AnalizadorResultado { Estructura = "RESULTADO: " + Resultado.ToString(), Detalle = "ENT" });
            return Estructuras;
        }

        //Se colocan métodos que resualvan las operaciones 

        public int Suma(int a, int b)
        {
            return a + b;
        }
        public int Resta(int a, int b)
        {
            return a - b;
        }
        public int Division(int a, int b)
        {
            return a / b;
        }
        public int Multiplicacion(int a, int b)
        {
            return a * b;
        }
        public int Modular(int a, int b)
        {
            return a % b;
        }


        static double Evaluar(string expresion)
        {
            DataTable table = new DataTable();
            
            object resultado = table.Compute(expresion, String.Empty);
            return Convert.ToDouble(resultado);

        }
    }
}


