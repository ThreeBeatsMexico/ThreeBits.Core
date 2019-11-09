using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Entities.Common;

namespace ThreeBits.Data.Common
{
   public class CatalogosDA
    {

        /// <summary>
        /// Consulta de la tabla CatGenerales. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='item'>Se usa CatGeneralesBE.psIDCATGENERALES para la consulta, de no ser proporcionado regresa Resultados sin filtro</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE GetCatGenerales(CatGeneralesBE item, string sConexionString)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            OleDbCommand Comando = new OleDbCommand();
            OleDbConnection Conexion = new OleDbConnection();
            OleDbDataReader Lector = null;
            string sComando = string.Empty;
            StringBuilder sMensajeError = new StringBuilder();
            string sResultado = string.Empty;

            RespuestaComun.lstCatGenerales = new List<CatGeneralesBE>();
            RespuestaComun.itemError = new ErrorBE();
            RespuestaComun.itemError.psMensaje = new StringBuilder(string.Empty);

            try
            {
                Conexion.ConnectionString = sConexionString;
                Conexion.Open();
                Comando.Connection = Conexion;

                sComando = 'spGetCatGenerales';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('IDCATGENERALES', item.psIDCATGENERALES)).Direction = ParameterDirection.Input;


                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spGetCatGenerales]');

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        CatGeneralesBE itemLector = new CatGeneralesBE();

                        itemLector.psIDCATGENERALES = Lector['IDCATGENERALES'].ToString();
                        itemLector.psNOMBRECATALOGO = Lector['NOMBRECATALOGO'].ToString();
                        itemLector.psIDCATALOGO = Lector['IDCATALOGO'].ToString();
                        itemLector.psDESCRIPCION = Lector['DESCRIPCION'].ToString();
                        itemLector.psFILTRO = Lector['FILTRO'].ToString();
                        itemLector.psACTIVO = Lector['ACTIVO'].ToString();


                        RespuestaComun.lstCatGenerales.Add(itemLector);
                    }
                }
                RespuestaComun.itemError.pbFlag = true;

            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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
        /// <summary>
        /// Agrega la tabla CatGenerales. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='item'>Se usa CatGeneralesBE Total mente llena para insertar</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE AddCatGenerales(CatGeneralesBE item, string sConexionString)
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

                sComando = 'spAddCatGenerales';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('NOMBRECATALOGO', item.psNOMBRECATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('IDCATALOGO', item.psIDCATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('DESCRIPCION', item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('FILTRO', item.psFILTRO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('ACTIVO', item.psACTIVO)).Direction = ParameterDirection.Input;

                Comando.Parameters.Add(new OleDbParameter('IDCATGENERALESNEW', OleDbType.BigInt)).Direction = ParameterDirection.Output;



                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spAddCatGenerales]');

                Comando.ExecuteNonQuery();
                RespuestaComun.psIDCONFIGAPP = Comando.Parameters['IDCATGENERALESNEW'].Value.ToString();

                Transaccion.Commit();

                RespuestaComun.itemError.pbFlag = true;
            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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
        /// <summary>
        /// Actualiza la tabla CatGenerales. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='item'>Se usa CatGeneralesBE Total mente llena para insertar</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE SetCatGenerales(CatGeneralesBE item, string sConexionString)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            List<ConfiguracionBE> lstClienteOld = new List<ConfiguracionBE>();
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

                sComando = 'spSetCatGenerales';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('IDCATGENERALES', item.psIDCATGENERALES)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('NOMBRECATALOGO', item.psNOMBRECATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('IDCATALOGO', item.psIDCATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('DESCRIPCION', item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('FILTRO', item.psFILTRO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('ACTIVO', item.psACTIVO)).Direction = ParameterDirection.Input;



                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spSetCatGenerales]');

                Comando.ExecuteNonQuery();
                Transaccion.Commit();

                RespuestaComun.itemError.pbFlag = true;
            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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

        /// <summary>
        /// Consulta de catalogo espeficico. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='item'>Se usa CatGeneralesBE para la consulta, Se requiere el idCatGenerales</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE GetCatEspecifico(CatGeneralesBE item, string sConexionString)
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

                sComando = 'spGetCatEspecifico';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('IDCATGENERALES', item.psIDCATGENERALES)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('NOMBRECATALOGO', item.psNOMBRECATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('IDCATALOGO', item.psIDCATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('DESCRIPCION', item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('FILTRO', item.psFILTRO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('ACTIVO', item.psACTIVO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('VALORFILTRO', item.psVALORFILTRO)).Direction = ParameterDirection.Input;


                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spGetCatEspecifico]');

                Lector = Comando.ExecuteReader();

                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        CatalogosBE itemLector = new CatalogosBE();

                        itemLector.ID = Lector['ID'].ToString();
                        itemLector.DESCRIPCION = Lector['DESCRIPCION'].ToString();

                        RespuestaComun.lstCatalogo.Add(itemLector);
                    }
                }
                RespuestaComun.itemError.pbFlag = true;

            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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
        /// <summary>
        /// Agrega catalogo espeficico. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='itemCatGenerales'>Se usa CatGeneralesBE Total mente llena para insertar</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE AddCatEspecifico(CatGeneralesBE itemCatGenerales, string sDescripcion, string sConexionString)
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

                sComando = 'spAddCatEspecifico';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('NOMBRECATALOGO', itemCatGenerales.psNOMBRECATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('IDCATALOGO', itemCatGenerales.psIDCATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('DESCRIPCION', itemCatGenerales.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('VALORDESCRIPCION', sDescripcion)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('RESPUESTA', DbType.Int16)).Direction = ParameterDirection.Output;


                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spAddCatGenerales]');

                Comando.ExecuteNonQuery();


                Transaccion.Commit();

                RespuestaComun.itemError.pbFlag = true;
            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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
        /// <summary>
        /// Actualiza catalogo espeficico. Definición de todos los catalogos de la aplicación
        /// </summary>
        /// <param name='item'>Se usa CatGeneralesBE Total mente llena para insertar</param>
        /// <param name='sConexionString'>cadena de conexión que se obtiene de la clase ConfiguracionDA</param>
        /// <returns>RespuestaComun.lstCatGenerales</returns>
        public RespuestaComunBE SetCatEspecifico(CatGeneralesBE item, string sConexionString)
        {

            RespuestaComunBE RespuestaComun = new RespuestaComunBE();
            List<ConfiguracionBE> lstClienteOld = new List<ConfiguracionBE>();
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

                sComando = 'spSetCatEspecifico';
                Comando.CommandText = sComando;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandTimeout = 0;
                Comando.Parameters.Clear();

                Comando.Parameters.Add(new OleDbParameter('IDCATGENERALES', item.psIDCATGENERALES)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('NOMBRECATALOGO', item.psNOMBRECATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('IDCATALOGO', item.psIDCATALOGO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('DESCRIPCION', item.psDESCRIPCION)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('FILTRO', item.psFILTRO)).Direction = ParameterDirection.Input;
                Comando.Parameters.Add(new OleDbParameter('ACTIVO', item.psACTIVO)).Direction = ParameterDirection.Input;



                //[15102015][falta agregar la instruccion que obtiene el nombre del metodo]RespuestaComun.itemError.psMensaje.Append('[spGetConfigApp]');
                RespuestaComun.itemError.psMensaje.Append('[spSetCatGenerales]');

                Comando.ExecuteNonQuery();
                Transaccion.Commit();

                RespuestaComun.itemError.pbFlag = true;
            }
            catch (Exception Ex)
            {
                //Generar una deccion para crear Log de errores
                RespuestaComun.itemError.psMensaje.Append('[');
                RespuestaComun.itemError.psMensaje.Append(Ex.Message);
                RespuestaComun.itemError.psMensaje.Append(']');
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
