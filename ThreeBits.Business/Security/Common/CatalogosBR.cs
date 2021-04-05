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
   public class CatalogosBR
    {
        string sConnGral = ConfigurationManager.AppSettings["ConnGral"].ToString();
        public RespuestaComunBE GetCatGenerales(CatGeneralesBE item)
        {
            
            CatalogosDA oCatalogosDA = new CatalogosDA();
            ConfigurationDA oConfigurationDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfigurationDA.GetConfigAPP(itemConfig, sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oCatalogosDA.GetCatGenerales(item, sConexionString);

            return Respuesta;
        }
        public RespuestaComunBE AddCatGenerales(CatGeneralesBE item)
        {
            CatalogosDA oCatalogosDA = new CatalogosDA();
            ConfigurationDA oConfigurationDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfigurationDA.GetConfigAPP(itemConfig, sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oCatalogosDA.AddCatGenerales(item, sConexionString);

            return Respuesta;
        }
        public RespuestaComunBE SetCatGenerales(CatGeneralesBE item)
        {
            CatalogosDA oCatalogosDA = new CatalogosDA();
            ConfigurationDA oConfigurationDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();

            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfigurationDA.GetConfigAPP(itemConfig, sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            Respuesta = oCatalogosDA.SetCatGenerales(item, sConexionString);

            return Respuesta;
        }


        public RespuestaComunBE GetCatEspecifico(string sIdCatalogo, string sValorFiltro = "")
        {
            CatalogosDA oCatalogosDA = new CatalogosDA();
            CatGeneralesBE item = new CatGeneralesBE();
            ConfigurationDA oConfigurationDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();


            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

            Respuesta = oConfigurationDA.GetConfigAPP(itemConfig, sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            item.psIDCATGENERALES = sIdCatalogo;

            Respuesta = GetCatGenerales(item);

            item = Respuesta.lstCatGenerales[0];
            item.psVALORFILTRO = sValorFiltro;

            Respuesta = oCatalogosDA.GetCatEspecifico(Respuesta.lstCatGenerales[0], sConexionString);

            return Respuesta;
        }
        public RespuestaComunBE AddCatEspecifico(string sIdCatalogo, string sDescripcion)
        {
            CatalogosDA oCatalogosDA = new CatalogosDA();
            CatGeneralesBE item = new CatGeneralesBE();
            ConfigurationDA oConfigurationDA = new ConfigurationDA();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBE itemConfig = new ConfiguracionBE();


            string sConexionString = string.Empty;

            itemConfig.psIDCONFIGAPP = ConfigurationManager.AppSettings["IdCatConexionString"].ToString();

           
            Respuesta = oConfigurationDA.GetConfigAPP(itemConfig,sConnGral);
            sConexionString = Respuesta.lstConfiguracion[0].psVALOR;

            item.psIDCATGENERALES = sIdCatalogo;

            Respuesta = GetCatGenerales(item);


            Respuesta = oCatalogosDA.AddCatEspecifico(Respuesta.lstCatGenerales[0], sDescripcion, sConexionString);

            return Respuesta;

        }
    }
}
