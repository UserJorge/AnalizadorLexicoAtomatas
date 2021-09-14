using AnalizadorLexicoAtomatas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace AnalizadorLexicoAtomatas.Models
{
   public class AnalizadorSintactico
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

        List<AnalizadorResultado> Estructuras;
        public List<AnalizadorResultado> SintaxisCheck(List<EstructuraLexica> estructuras)
        {
            pila = new Stack<EstructuraLexica>();
            Estructuras = new List<AnalizadorResultado>();
            foreach (EstructuraLexica item in estructuras)
            {
                Pila.Push(item);
            }
            int count=0;
            int operation = 0;
            int sideA = 0;
            int incrementa = 0;
            int auxiliar = 0;
            int auxiliarA = 0;
            string palabra = "";
            string palabraFuncion = "";
            for (int i = 0; i < Pila.Count; i++)
            {
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"\d"))
                {
                    count++;
                    if (count==1)
                    {
                        A = int.Parse(Pila.ToArray()[i].Lexema);
                    }
                    if (count==2)
                    {
                        B = int.Parse(Pila.ToArray()[i].Lexema);
                        count = 0;
                    }
                    //A = 0;
                    //B = 0;
                }
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$")&&i!=Pila.Count()-1)
                {
                    operation++;
                    if (operation==1)
                    {
                        switch (Pila.ToArray()[i].Lexema)
                        {
                            case "SUM": LayerOne += Suma(B, A); break;
                            case "SUB": LayerOne += Resta(B, A); break;
                            case "DIV": LayerOne += Division(B, A); break;
                            case "MLT": LayerOne += Multiplicacion(B, A); break;
                            case "MOD": LayerOne += Modular(B, A); break;
                        }
                        A = 0;
                        B = 0;
                    }
                   else if (operation==2) 
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
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$") && i == Pila.Count()-1 && LayerTwo!=0 && LayerOne!=0)
                {
                    switch (Pila.ToArray()[i].Lexema)
                    {
                        case "SUM": Resultado += Suma(LayerTwo, LayerOne); break;
                        case "SUB": Resultado += Resta(LayerTwo, LayerOne); break;
                        case "DIV": Resultado += Division(LayerTwo, LayerOne); break;
                        case "MLT": Resultado += Multiplicacion(LayerTwo, LayerOne); break;
                        case "MOD": Resultado += Modular(LayerTwo, LayerOne); break;
                    }
                    LayerOne = 0;
                    LayerTwo = 0;
                }
                if (palabra.Length<16&&i!=0)
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
                        if (j==10)
                        {
                            B = int.Parse(palabra.ToArray()[j].ToString());
                        }
                        if (j==12||j==13||j==14)
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

                if (Regex.IsMatch(palabra,@"^[')']\d[','][')']$"))
                {
                    for (int k = 0; k < palabra.Length; k++)
                    {
                        if (Regex.IsMatch(palabra.ToArray()[k].ToString(), @"\d"))
                        {
                            incrementa++;
                            if (incrementa == 1)
                            {
                                auxiliar = A;
                            }
                            if (incrementa == 3)
                            {
                                auxiliarA = A;
                                A = B;
                                B = auxiliarA;
                                LayerTwo = auxiliarA + A;

                            }
                        }
                    }
                }
                /*Corregir el código if 2*/
                /*Ejemplo quiero hacer La fórmula SUM(SUM(8,4),4);*/
                /*[')']\d[',']{1}[')']{1}\d[',']{1}\d['(']{1}['(']{1}*/
                if (Regex.IsMatch(palabra, @"[A-Z]{3}['('][A-Z]{3}")  )
                {
                    

                    for (int j = 0; j < palabra.Length; j++)
                    {
                        if (j == 1)
                        {
                            A = auxiliar;
                           
                        }
                        if (j == 12 || j == 13 || j == 14)
                        {
                            palabraFuncion += palabra.ToArray()[j].ToString();
                        }

                    }
                    switch (palabraFuncion)
                    {
                        case "SUM": LayerOne = A; Resultado += Suma(LayerTwo, auxiliar); break;
                        case "SUB": LayerOne = A; Resultado += Resta(LayerTwo, auxiliar); break;
                        case "DIV": LayerOne = A; Resultado += Division(LayerTwo, auxiliar); break;
                        case "MLT": LayerOne = A; Resultado += Multiplicacion(LayerTwo, auxiliar); break;
                        case "MOD": LayerOne = A; Resultado += Modular(LayerTwo, auxiliar); break;
                    }
                }

                /* Fórmula simple del tipo SUM(8,3)*/
                if (Regex.IsMatch(Pila.ToArray()[i].Lexema, @"^(SUM|SUB|DIV|MLT|MOD)$") && LayerTwo == 0 && LayerOne == 0 &&B!=0&&A!=0)
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


    }
}
