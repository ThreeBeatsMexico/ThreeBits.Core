using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Data.Common;
using ThreeBits.Entities.Common;

namespace ThreeBits.Business.Common
{
    public class ConfiguracionBR
    {
        public RespuestaComunBE GetConfigAPP(ConfiguracionBE item)
        {
            ConfigurationDA oConfiguracionDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            Respuesta = oConfiguracionDA.GetConfigAPP(item);

            return Respuesta;
        }
        public RespuestaComunBE AddConfigAPP(ConfiguracionBE item)
        {
            ConfigurationDA oConfiguracionDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfiguracionDA.GetConfigAPP(itemConfig);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oConfiguracionDA.AddConfigAPP(item, sConexionString);

            return Respuesta;
        }
        public RespuestaComunBE SetConfigAPP(ConfiguracionBE item)
        {
            ConfigurationDA oConfiguracionDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfiguracionDA.GetConfigAPP(itemConfig);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oConfiguracionDA.SetConfigAPP(item, sConexionString);

            return Respuesta;
        }
    }
}
