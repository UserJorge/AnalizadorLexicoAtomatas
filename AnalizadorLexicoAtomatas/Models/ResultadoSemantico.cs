using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexicoAtomatas.Models
{
    public class ResultadoSemantico
    {
        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private string detalle;

        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }


    }
}
