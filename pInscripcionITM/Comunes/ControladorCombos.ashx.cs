using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using libComunes.CapaObjetos;

namespace pProgramacionDistribuida.Comunes
{
    /// <summary>
    /// Descripción breve de ControladorCombos
    /// </summary>
    public class ControladorCombos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosCombo;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosCombo = reader.ReadToEnd();

            viewCombo vCombo = JsonConvert.DeserializeObject<viewCombo>(DatosCombo);
            string Respuesta;

            switch (vCombo.Comando.ToUpper())
            {
                case "TIPOPROGRAMAS":
                    Respuesta = LlenarCombo(vCombo, "Programa_LlenarCombo");
                    break;
                default:
                    Respuesta = "Comando sin definir";
                    break;
            }

            context.Response.Write(Respuesta);
        }
        private string LlenarCombo(viewCombo vCombo, string SQL)
        {
            vCombo.SQL = SQL;
            clsComboListas oCombo = new clsComboListas();
            oCombo.vCombo = vCombo;
            return JsonConvert.SerializeObject(oCombo.ListarCombos());
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