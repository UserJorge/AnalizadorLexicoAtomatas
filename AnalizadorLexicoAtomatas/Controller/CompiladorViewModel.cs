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
        public ObservableCollection<EstructuraLexica> lista { get; set; } = new ObservableCollection<EstructuraLexica>();
        public ObservableCollection<AnalizadorResultado> listaCompilador { get; set; } = new ObservableCollection<AnalizadorResultado>();
        private string sourceCode;

        public event PropertyChangedEventHandler PropertyChanged;
        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error")); }
        }


        public string SourceCode
        {
            get { return sourceCode; }
            set { sourceCode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceCode")); }
        }

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

        public ICommand LexicoCommand
        {
            get;set;
        }
        public ICommand CompilarCommand
        {
            get;set;
        }

        public CompiladorViewModel()
        {
            LexicoCommand=new RelayCommand(Lexico);
            CompilarCommand = new RelayCommand(Compilar);
            
        }
        AnalizadorLexico analiza;
        public void Lexico()
        {
            Error = "";
            try
            {
                analiza = new AnalizadorLexico();
                analiza.PreLectura(SourceCode);
                estructuraSave = analiza.Identificador();
                lista.Clear();
                foreach (EstructuraLexica item in estructuraSave)
                {
                    lista.Add(item);
                }
            }
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
        public void Compilar()
        {
            //   comp = new Compilador();
            //   EstructuraResultados = comp.Compilar(EstructuraSave);
            //    listaCompilador.Clear();
            //    foreach (AnalizadorResultado item in EstructuraResultados)
            //    {
            //        listaCompilador.Add(item);
            //    }
            Lexico();
            comp = new Compilador();
            sintactico = new AnalizadorSintactico2();
            EstructuraResultados = sintactico.SintaxisCheck(EstructuraSave);
            listaCompilador.Clear();
            foreach (AnalizadorResultado item in EstructuraResultados)
            {
                listaCompilador.Add(item);
            }
        }
    }
}
