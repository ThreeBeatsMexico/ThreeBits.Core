using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Business.Common;
using ThreeBits.Entities.Common;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CommonService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione CommonService.svc o CommonService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CommonService : ICommonService
    {
        #region Catalogos
        public RespuestaComunBE GetCatGenerales(CatGeneralesBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CatalogosBR oCatalogosBR = new CatalogosBR();

            Respuesta = oCatalogosBR.GetCatGenerales(item);

            return Respuesta;
        }
        public RespuestaComunBE AddCatGenerales(CatGeneralesBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CatalogosBR oCatalogosBR = new CatalogosBR();

            Respuesta = oCatalogosBR.AddCatGenerales(item);

            return Respuesta;
        }
        public RespuestaComunBE SetCatGenerales(CatGeneralesBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CatalogosBR oCatalogosBR = new CatalogosBR();

            Respuesta = oCatalogosBR.SetCatGenerales(item);

            return Respuesta;
        }


        public RespuestaComunBE GetCatEspecifico(string sIdCatalogo, string sValorFiltro = "")
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CatalogosBR oCatalogosBR = new CatalogosBR();

            Respuesta = oCatalogosBR.GetCatEspecifico(sIdCatalogo, sValorFiltro);

            return Respuesta;
        }
        public RespuestaComunBE AddCatEspecifico(string sIdCatalogo, string sDescripcion)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CatalogosBR oCatalogosBR = new CatalogosBR();

            Respuesta = oCatalogosBR.AddCatEspecifico(sIdCatalogo, sDescripcion);

            return Respuesta;
        }
        #endregion 

        #region Common
        public RespuestaComunBE GetDefinicionTabla(string sNombreTabla)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            CommonBR oCommonBR = new CommonBR();

            Respuesta = oCommonBR.GetDefinicionTabla(sNombreTabla);

            return Respuesta;
        }
        #endregion

        #region Configuracion
        public RespuestaComunBE GetConfigAPP(ConfiguracionBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBR oConfiguracionBR = new ConfiguracionBR();

            Respuesta = oConfiguracionBR.GetConfigAPP(item);

            return Respuesta;
        }
        public RespuestaComunBE AddConfigAPP(ConfiguracionBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBR oConfiguracionBR = new ConfiguracionBR();

            Respuesta = oConfiguracionBR.AddConfigAPP(item);

            return Respuesta;
        }
        public RespuestaComunBE SetConfigAPP(ConfiguracionBE item)
        {
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            ConfiguracionBR oConfiguracionBR = new ConfiguracionBR();

            Respuesta = oConfiguracionBR.SetConfigAPP(item);

            return Respuesta;
        }
        #endregion
    }
}
