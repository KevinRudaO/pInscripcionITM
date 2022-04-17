using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using pInscripcionITM.Clases;

namespace pInscripcionITM.Servidor
{
    /// <summary>
    /// Descripción breve de ControladorInscripcion
    /// </summary>
    public class ControladorInscripcion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosInscripcion;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosInscripcion = reader.ReadToEnd();

            viewEstudiante vEstudiante = JsonConvert.DeserializeObject<viewEstudiante>(DatosInscripcion);

            clsInscripcion oInscripcion = new clsInscripcion();
            oInscripcion.vEstudiante = vEstudiante;
            switch (oInscripcion.vEstudiante.Comando.ToUpper())
            {
                case "GRABAR":
                    context.Response.Write(GrabarInscripcion(oInscripcion));
                    break;

                default:
                    context.Response.Write("Comando sin definir");
                    break;
            }
        }
        private string GrabarInscripcion(clsInscripcion oInscripcion)
        {
            return (oInscripcion.ProcesarInscripcion());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}