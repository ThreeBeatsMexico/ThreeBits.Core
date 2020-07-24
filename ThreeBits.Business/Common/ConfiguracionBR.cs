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
        string sConnGral = ConfigurationManager.AppSettings["ConnGral"].ToString();
        public RespuestaComunBE GetConfigAPP(ConfiguracionBE item)
        {
            ConfigurationDA oConfiguracionDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            Respuesta = oConfiguracionDA.GetConfigAPP(item,sConnGral);

            return Respuesta;
        }
        public RespuestaComunBE AddConfigAPP(ConfiguracionBE item)
        {
            ConfigurationDA oConfiguracionDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfiguracionDA.GetConfigAPP(itemConfig, sConnGral);
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

            Respuesta = oConfiguracionDA.GetConfigAPP(itemConfig, sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oConfiguracionDA.SetConfigAPP(item, sConexionString);

            return Respuesta;
        }
    }
}
