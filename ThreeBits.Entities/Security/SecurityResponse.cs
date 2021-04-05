using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeBits.Entities.Security
{
  public class SecurityResponse
    {
        public List<PermisosXObjetosBE> PermisoXObjetos = new List<PermisosXObjetosBE>();
        public List<PermisoXElementosObjBE> ElementosObjetos = new List<PermisoXElementosObjBE>();
        public List<PermisosXMenuBE> PermisosXMenu = new List<PermisosXMenuBE>();
        public List<PermisoXSubmenuBE> PermisosXSubmenu = new List<PermisoXSubmenuBE>();
        public EncryptionBE Encriptacion = new EncryptionBE();
        public List<AplicacionBE> Aplicaciones = new List<AplicacionBE>();
        public List<WCFMetodosBE> Metodos = new List<WCFMetodosBE>();
    }
}
