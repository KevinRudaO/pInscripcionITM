using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pInscripcionITM.Clases
{
    public class viewEstudiante
    {
        public Int32 idEstudiante { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Colegio { get; set; }
        public Int32 EstratoColegio { get; set; }
        public Int32 EstratoVivienda { get; set; }
        public string Error { get; set; }
        public string Comando { get; set; }
        public List<viewProgramas> lstProgramas { get; set; }
    }
    public class viewProgramas
    {
        public Int32 idPrograma { get; set; }
        public string Opcion { get; set; }
        public string Error { get; set; }
    }
}