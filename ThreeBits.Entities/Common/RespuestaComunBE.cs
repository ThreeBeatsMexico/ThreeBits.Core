using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Common
{
    [DataContract]
    public class RespuestaComunBE
    {
        [DataMember]
        public List<CatalogosBE> lstCatalogo { get; set; }
        [DataMember]
        public List<CatGeneralesBE> lstCatGenerales { get; set; }
        [DataMember]
        public List<ConfiguracionBE> lstConfiguracion { get; set; }
        [DataMember]
        public ErrorBE itemError { get; set; }
        [DataMember]
        public string psIDCONFIGAPP { get; set; }
        [DataMember]
        public string psIdentityTabla { get; set; }
        [DataMember]
        public string psDescripcionTabla { get; set; }
    }
}
