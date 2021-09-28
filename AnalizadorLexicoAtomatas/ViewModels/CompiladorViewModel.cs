using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AnalizadorLexicoAtomatas.Model;
using AnalizadorLexicoAtomatas.Models;
using GalaSoft.MvvmLight.Command;

namespace AnalizadorLexicoAtomatas.ViewModels
{
    public class CompiladorViewModel:INotifyPropertyChanged
    {
        //Esta lista es para agregar registros identificados (resultado después del analizador léxico) al controlador de la tabla "analizador léxico" 
        public ObservableCollection<EstructuraLexica> lista { get; set; } = new ObservableCollection<EstructuraLexica>();
        //Esta es la lista de cada uno de los resultados que salen de la ejecución del código
        public ObservableCollection<AnalizadorResultado> listaCompilador { get; set; } = new ObservableCollection<AnalizadorResultado>();
       
        //implementación de la interfaz INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LexicoCommand
        {
            get; set;
        }
        public ICommand CompilarCommand
        {
            get; set;
        }
        public ICommand AnalisisCommand
        {
            get; set;
        }
        //si se genera algún error en la compilación o un aviso de alguna excepción
        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error")); }
        }
        //Aquí se recibe y guarda el Código fuente (el primer paso de el compilador) *sourceCode*
        private string sourceCode;
        public string SourceCode
        {
            get { return sourceCode; }
            set { sourceCode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceCode")); }
        }

        //En estructuraSave se guardan el resultado de la identificación de cada elemento resultado del análisis del código
        private List<EstructuraLexica> estructuraSave;

        public List<EstructuraLexica> EstructuraSave
        {
            get { return estructuraSave; }
            set { estructuraSave = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceCode")); }
        }


        private List<AnalizadorResultado> estructuraResultados;

        public List<AnalizadorResultado>  EstructuraResultados
        {
            get { return estructuraResultados; }
            set { estructuraResultados = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EstructuraResultados")); }
        }

      

        public CompiladorViewModel()
        {
            //en enlace entre los metodos del ViewModel y la interfaz de usuario, el usuario manda a ejecutar las acciones mediante los comandos
            //la interfaz ICommand hace esto posible
            LexicoCommand=new RelayCommand(Lexico);
            CompilarCommand = new RelayCommand(Compilar);
            AnalisisCommand = new RelayCommand(Analizar);
            
        }
        //aquí se llaman los dos clases sintáctica-semántica
        GrammerSintactico gramatica = new GrammerSintactico();
        public void Analizar()
        {
            gramatica.SequenceCheck(EstructuraSave);

        }
        AnalizadorLexico analiza;

        public void Lexico()
        {
            Error = "";
            try
            {
                //intancias un objeto del tipo AnalizadorLexico
                analiza = new AnalizadorLexico();
                //*****ejecutas el método y colocas como parametro el código fuente (quita solo los espacios en blanco) 
                //*falta agg que devualva la parte de string ya sin espacios a la propiedad code source
                analiza.PreLectura(SourceCode);
                //ejecutas el metodo identificador de elementos lexicos, convirtiendo el código fuente en lexemas 
                EstructuraSave = analiza.Identificador();
                //limpiar la lista si había algo.
                lista.Clear();
                //añadir cada uno de los lexemas ya identificados y con información adicional que permite una mejor lectura
                foreach (EstructuraLexica item in EstructuraSave)
                {
                    //añadir un nuevo lexema 
                    lista.Add(item);
                }
            }
            //si hay alguna excepción que la muestre a través de la propiedad idicada para ello.
            catch (ArgumentException ex)
            {
                Error = ex.Message;
            }
            catch (Exception c)
            {
                Error = c.Message;
            }
            //analiza.PreLectura(SourceCode);
           // lista.Add(new EstructuraLexica { Lexema = analiza.PreLectura(SourceCode), Token="Función"});
            //estructuraSave.Add(new EstructuraLexica() {  = analiza.PreLectura(SourceCode) });
         
        }

        //próxima parte del código en la 2da entrega (en fase experimental).
        private Compilador comp;
        private AnalizadorSintactico2 sintactico;

        //este es parte de la compilación
        public void Compilar()
        {
            //   comp = new Compilador();
            //   EstructuraResultados = comp.Compilar(EstructuraSave);
            //    listaCompilador.Clear();
            //    foreach (AnalizadorResultado item in EstructuraResultados)
            //    {
            //        listaCompilador.Add(item);
            //    }

            //se ejecuta la parte del proceso de identificar cada lexema (analizador léxico)
            Lexico();
            //se crea un objeto *borrar esta parte hasta saber que hacer con ella**
            comp = new Compilador();
            //objeto del tipo AnalizadorSintactico2
            sintactico = new AnalizadorSintactico2();
            //ejecuta el metodo con el parametro la propiedad que regresa el resultado y lo agg a otra propiedad donde guarda los resultados
            EstructuraResultados = sintactico.SintaxisCheck(EstructuraSave);
            //se borra la lista de resultados, borrar resultados pasados
            listaCompilador.Clear();
             
            //se añaden todos los elementos 
            foreach (AnalizadorResultado item in EstructuraResultados)
            {
                listaCompilador.Add(item);
            }
        }
    }
}
