using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;

namespace pInscripcionITM.Clases
{
    public class clsInscripcion
    {
        public viewEstudiante vEstudiante { get; set; }
        public clsConexion oConexion = new clsConexion();

        public string ProcesarInscripcion()
        {
            oConexion.AbrirTransaccion();
            if (GrabarEstudiante())
            {
                if (GrabarProgramasEstudiante())
                {
                    oConexion.AceptarTransaccion();
                    return "Estudiante con documento: " + vEstudiante.Documento + " se grabó exitosamente";
                }
                else
                {
                    oConexion.RechazarTransaccion();
                }
            }
            else
            {
                oConexion.RechazarTransaccion();
            }
            return "No se pudo grabar al estudiante de documento: " + vEstudiante.Documento + " Error: " + oConexion.Error;
        }
        private bool GrabarEstudiante()
        {
            oConexion.SQL = "Estudiante_Insertar";
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prDocumento", System.Data.SqlDbType.VarChar, 20, vEstudiante.Documento);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prNombre", System.Data.SqlDbType.VarChar, 200, vEstudiante.Nombre);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prColegio", System.Data.SqlDbType.VarChar, 50, vEstudiante.Colegio);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prEstratoVivienda", System.Data.SqlDbType.Int, 4, vEstudiante.EstratoVivienda);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Input, "@prEstratoColegio", System.Data.SqlDbType.Int, 4, vEstudiante.EstratoColegio);
            oConexion.AgregarParametro(System.Data.ParameterDirection.Output, "@prIdEstudiante", System.Data.SqlDbType.Int, 4, 0);

            if (oConexion.EjecutarSentencia())
            {
                //vEstudiante.idEstudiante = (Int32) (oConexion.oCommand.Parameters["@prIdEstudiante"].Value);
                vEstudiante.idEstudiante = Convert.ToInt32(oConexion.oCommand.Parameters["@prIdEstudiante"].Value);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GrabarProgramasEstudiante()
        {
            //Como el detalle de la factura está en una lista, se requiere recorrerla para pasar todos los elementos al procedimiento
            foreach (viewProgramas oProgramas in vEstudiante.lstProgramas)
            {
                //Utiliza la misma clase de conexión
                oConexion.SQL = "Inscripcion_Insertar";
                oConexion.StoredProcedure = true;
                //Antes de iniciar el proceso de agregar parámetros se deben limpiar
                oConexion.oCommand.Parameters.Clear();
                oConexion.AgregarParametro("@prIdEstudiante", System.Data.SqlDbType.Int, 4, vEstudiante.idEstudiante);
                oConexion.AgregarParametro("@prIdPrograma", System.Data.SqlDbType.Int, 4, oProgramas.idPrograma);
                oConexion.AgregarParametro("@prOpcion", System.Data.SqlDbType.VarChar, 20, oProgramas.Opcion);
                //si no puede ejecutar algún detalle, debe devolver false, para que el método principal devuelva la transacción
                if (!oConexion.EjecutarSentencia())
                {
                    return false;
                }
            }
            //Sólo si termina el ciclo, retorna verdadero
            return true;

        }
    }
}