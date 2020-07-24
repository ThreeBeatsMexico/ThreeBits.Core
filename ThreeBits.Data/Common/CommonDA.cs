using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Entities.Common;

namespace ThreeBits.Data.Common
{
   public class CommonDA
    {
        
        /// <summary>
        /// Inserta en la base de datos un error a nivel base de datos
        /// </summary>
        /// <param name="MessageErr">Mensaje de error</param>
        /// <param name="st">El trace</param>
        /// <param name="user">Usuario en caso de haberlo</param>
        /// <param name="sApp">El ID de la aplicacion</param>
        public void insErrorDB(string MessageErr, StackTrace st, string user, string sApp)
        {
            try
            {
                if (MessageErr.Length > 445) MessageErr = MessageErr.Substring(0, 445);
                //Obtiene el hostname
                String strHostname = Dns.GetHostName();
                IPHostEntry myself = Dns.GetHostEntry(strHostname);

                ////Obtiene la Ip del usuario que genero el error
                System.Net.IPAddress ip = myself.AddressList.Where(n => n.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
                string sIp = ip.ToString();

                StringBuilder strStackTrace = new StringBuilder();
                foreach (StackFrame f in st.GetFrames())
                {
                    strStackTrace.Append(f.ToString());
                    break;
                }

                //if (MessageErr.Length > 490) MessageErr = MessageErr.Substring(0, 448);

                if (strStackTrace.Length > 490)// Validamos la longitud del stack
                {
                    string sStackTrace = strStackTrace.ToString();
                    strStackTrace.Clear();
                    strStackTrace.Append(sStackTrace.Substring(0, 490));
                }

                if (sIp.Length > 39) sIp = sIp.Substring(0, 39);
                if (strHostname.Length > 148) strHostname = strHostname.Substring(0, 148);

                //Inserta en la tabla del log de errores
                Models.Seguridad.Seguridad3BitsEntities1 oEnt = new Models.Seguridad.Seguridad3BitsEntities1();
                oEnt.sp_insLogError(Int64.Parse(sApp), MessageErr, strHostname, sIp, strStackTrace.ToString(), DateTime.Now, user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta el sl_help de la tabla que se envia
        /// </summary>
        /// <param name="sNombreTabla"> Nombre de la tabla a consultar</param>
        /// <param name="sConexionString"> cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>En lstCatalogos[].DESCRIPCION se regresa el nombre de las columnas de la tabla. En psIdentityTabla Se regresa el Identity de la tabla 
        ///</returns>
        public RespuestaComunBE GetDefinicionTabla(string sNombreTabla, string sConexionString)
        {
            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            OleDbCommand Comando = new OleDbCommand();
            OleDbConnection Conexion = new OleDbConnection();
            OleDbDataReader Lector = null;
            string sComando = string.Empty;
            StringBuilder sMensajeError = new StringBuilder();
            string sResultado = string.Empty;

            RespuestaComun.lstCatalogo = new List<CatalogosBE>();
            RespuestaComun.itemError = new ErrorBE();
            RespuestaComun.itemError.psMensaje = new StringBuilder(string.Empty);

            try
            {
                Conexion.ConnectionString = sConexionString;
                Conexion.Open();
                Comando.Connection = Conexion;

                sComando = "spGetDefinicionTabla";
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter("NombreTabla", sNombreTabla)).Direction = ParameterDirection.Input;


                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append("[spGetConfigApp]");
                RespuestaComun.itemError.psMensaje.Append("[spGetDefinicionTabla]");

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {

                    Lector.NextResult();
                    while (Lector.Read())
                    {
                        CatalogosBE itemLector = new CatalogosBE();

                        itemLector.DESCRIPCION = Lector["Column_name"].ToString();

                        RespuestaComun.lstCatalogo.Add(itemLector);
                    }

                    Lector.NextResult();
                    while (Lector.Read())
                    {
                        //Se encuentra el nombre de la columna IDentity
                        RespuestaComun.psIdentityTabla = Lector["Identity"].ToString();
                        RespuestaComun.psDescripcionTabla = "Descripcion";
                    }
                }

                RespuestaComun.itemError.pbFlag = true;

            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append("[");
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append("]");
                RespuestaComun.itemError.pbFlag = false;
            }
            finally
            {
                Lector.Close();
                Lector.Dispose();
                Lector = null;
                Comando.Dispose();
                Comando = null;
                Conexion.Close();
                Conexion = null;
            }
            return RespuestaComun;
        }
    }
}
