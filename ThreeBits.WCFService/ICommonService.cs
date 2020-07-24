using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ThreeBits.Entities.Common;

namespace ThreeBits.WCFService
{
    // NOTA: puede usar el comando 'Rename' del menú 'Refactorizar' para cambiar el nombre de interfaz 'ICommonService' en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        RespuestaComunBE GetCatGenerales(CatGeneralesBE item);
        [OperationContract]
        RespuestaComunBE AddCatGenerales(CatGeneralesBE item);
        [OperationContract]
        RespuestaComunBE SetCatGenerales(CatGeneralesBE item);

        [OperationContract]
        RespuestaComunBE GetCatEspecifico(string sIdCatalogo, string sValorFiltro = "");
        [OperationContract]
        RespuestaComunBE AddCatEspecifico(string sIdCatalogo, string sDescripcion);


        [OperationContract]
        RespuestaComunBE GetConfigAPP(ConfiguracionBE item);
        [OperationContract]
        RespuestaComunBE AddConfigAPP(ConfiguracionBE item);
        [OperationContract]
        RespuestaComunBE SetConfigAPP(ConfiguracionBE item);

        [OperationContract]
        RespuestaComunBE GetDefinicionTabla(string sNombreTabla);
    }

    [DataContract]
    public class ComunDC
    {
        [DataMember]
        RespuestaComunBE RespuesComun = new RespuestaComunBE();
        [DataMember]
        public List<CatalogosBE> lstCatalogo = new List<CatalogosBE>();
        [DataMember]
        public List<CatGeneralesBE> lstCatGenerales = new List<CatGeneralesBE>();
        [DataMember]
        public List<ConfiguracionBE> lstConfiguracion = new List<ConfiguracionBE>();
        [DataMember]
        public ErrorBE itemError = new ErrorBE();
    }
}
