using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Entities.Common;

namespace ThreeBits.Data.Common
{
    public class ConfigurationDA
    {
        public RespuestaComunBE GetConfigAPP(ConfiguracionBE item, string conn)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            OleDbCommand Comando = new OleDbCommand();
            OleDbConnection Conexion = new OleDbConnection();
            OleDbDataReader Lector = null;
            string sComando = string.Empty;
            StringBuilder sMensajeError = new StringBuilder();
            string sConexionString = conn;
            //string sConexionString = @"Provider=SQLOLEDB;Server=DESKTOP-VC0BRD2\SQL2012;Database=Seguridad3Bits;UID=sa; Pwd=martinej";
            //string sConexionString = @"Provider=SQLOLEDB;Server=JCMARTINEZ-PC\SQL2012;Database=seguridadlatinoqa;Uid=pvldev; Pwd=dominico";
            string sResultado = string.Empty;

            RespuestaComun.lstConfiguracion = new List<ConfiguracionBE>();
            RespuestaComun.itemError = new ErrorBE();
            RespuestaComun.itemError.psMensaje = new StringBuilder(string.Empty);

            try
            {
                Conexion.ConnectionString = sConexionString;
                Conexion.Open();
                Comando.Connection = Conexion;

                sComando = "spGetConfigApp";
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter("IDCONFIGAPP", item.psIDCONFIGAPP)).Direction = ParameterDirection.Input;

                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append("[spGetConfigApp]");
                RespuestaComun.itemError.psMensaje.Append("[spGetConfigApp]");

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        ConfiguracionBE itemLector = new ConfiguracionBE();

                        itemLector.psIDCONFIGAPP = Lector["IDCONFIGAPP"].ToString();
                        itemLector.psDESCRIPCION = Lector["DESCRIPCION"].ToString();
                        itemLector.psVALOR = Lector["VALOR"].ToString();
                        itemLector.psACTIVO = Lector["ACTIVO"].ToString();


                        RespuestaComun.lstConfiguracion.Add(itemLector);
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
        public RespuestaComunBE AddConfigAPP(ConfiguracionBE item, string sConexionString)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            OleDbCommand Comando = new OleDbCommand();
            OleDbConnection Conexion = new OleDbConnection();
            OleDbTransaction Transaccion = null;
            string sComando = string.Empty;
            StringBuilder sMensajeError = new StringBuilder();
            string sResultado = string.Empty;

            RespuestaComun.itemError = new ErrorBE();
            RespuestaComun.itemError.psMensaje = new StringBuilder(string.Empty);

            try
            {
                Conexion.ConnectionString = sConexionString;
                Conexion.Open();
                Transaccion = Conexion.BeginTransaction();
                Comando.Connection = Conexion;
                Comando.Transaction = Transaccion;

                sComando = "spAddConfigApp";
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter("DESCRIPCION", item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("VALOR", item.psVALOR)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("ACTIVO", item.psACTIVO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("IDCONFIGAPPNEW", OleDbType.BigInt)).Direction = ParameterDirection.Output;

                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append("[spGetConfigApp]");
                RespuestaComun.itemError.psMensaje.Append("[spAddConfigApp]");

                Comando.ExecuteNonQuery();
                RespuestaComun.psIDCONFIGAPP = Comando.Parameters["IDCONFIGAPPNEW"].Value.ToString();

                Transaccion.Commit();
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
                Transaccion.Dispose();
                Transaccion = null;
                Comando.Dispose();
                Comando = null;
                Conexion.Close();
                Conexion = null;
            }
            return RespuestaComun;
        }
        public RespuestaComunBE SetConfigAPP(ConfiguracionBE item, string sConexionString)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            OleDbCommand Comando = new OleDbCommand();
            OleDbConnection Conexion = new OleDbConnection();
            OleDbTransaction Transaccion = null;
            string sComando = string.Empty;
            StringBuilder sMensajeError = new StringBuilder();
            string sResultado = string.Empty;

            RespuestaComun.itemError = new ErrorBE();
            RespuestaComun.itemError.psMensaje = new StringBuilder(string.Empty);

            try
            {
                Conexion.ConnectionString = sConexionString;
                Conexion.Open();
                Transaccion = Conexion.BeginTransaction();
                Comando.Connection = Conexion;
                Comando.Transaction = Transaccion;

                sComando = "spSetConfigApp";
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter("IDCONFIGAPP", item.psIDCONFIGAPP)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("DESCRIPCION", item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("VALOR", item.psVALOR)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter("ACTIVO", item.psACTIVO)).Direction = ParameterDirection.Input;


                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append("[spGetConfigApp]");
                RespuestaComun.itemError.psMensaje.Append("[spSetConfigApp]");

                Comando.ExecuteNonQuery();

                Transaccion.Commit();

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
                Transaccion.Dispose();
                Transaccion = null;
                Comando.Dispose();
                Comando = null;
                Conexion.Close();
                Conexion = null;
            }
            return RespuestaComun;
        }
    }
}
