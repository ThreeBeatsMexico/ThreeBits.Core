using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Data.User;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.User;

namespace ThreeBits.Business.User
{
   public class UsersBR
    {
        public UsuariosBE addUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App)
        {
            UsuariosBE UsuarioRes = new UsuariosBE();
            try
            {
                ////Validamos las cadenas....

                ////Checamos la existencia del usuario
                ReglasBE ReglasInt = new ReglasBE();
                UsersDA usuarioDA = new UsersDA();
                ReglasInt.USUARIO = Usuario.IDUSUARIOAPP;
                ReglasInt.TIPOBUSQUEDA = 2;
                ReglasInt.IDAPP = Usuario.IDAPLICACION;
                bool bExistUsr = checkUsrXApp(ReglasInt, App);
                if (bExistUsr) throw new Exception('El usuario ya se encuentra agregado.');

                ////Validamos la existencia de los roles 
                List<RolesBE> RolesXApp = getRolesXApp(Reglas, App);
                bool bFlagExist = false;

                foreach (RolesXUsuarioBE s in RolesXUsuario)
                {
                    bFlagExist = false;
                    foreach (RolesBE RolXApp in RolesXApp)
                    {
                        if (s.IDROL == RolXApp.IDROL)
                        {
                            bFlagExist = true;
                            break;
                        }
                    }
                    if (!bFlagExist) throw new Exception('El rol ' + s.IDROL.ToString() + ' no pertenece a la aplicación o no existe.');
                }

                UsuarioRes = usuarioDA.addUsuario(Reglas, Usuario, Domicilios, Contactos, RolesXUsuario, App);

                return UsuarioRes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool actDeactivateUsuario(ReglasBE Reglas, Int64 App)
        {
            bool Respuesta = new bool();
            try
            {
                UsersDA usuarioDA = new UsersDA();
                Respuesta = usuarioDA.actDeactivateUsuario(Reglas, App);
                return Respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updateUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                bool bResultado = false;
                bResultado = usuarioDA.updateUsuario(Reglas, Usuario, Domicilios, Contactos, RolesXUsuario, App);
                return bResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addRolesXUsuario(ReglasBE Reglas, List<RolesXUsuarioBE> RolesXUsuario, long App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();

                bool bExistUsr = checkUsrXApp(Reglas, App);
                if (!bExistUsr) throw new Exception('El usuario no pertenece a la aplicación o no existe.');

                ////Validamos la existencia de los roles 
                List<RolesBE> RolesXApp = usuarioDA.getRolesXApp(Reglas, App);
                bool bFlagExist = false;

                foreach (RolesXUsuarioBE s in RolesXUsuario)
                {
                    bFlagExist = false;
                    foreach (RolesBE RolXApp in RolesXApp)
                    {
                        if (s.IDROL == RolXApp.IDROL)
                        {
                            bFlagExist = true;
                            break;
                        }
                    }
                    if (!bFlagExist) throw new Exception('El rol ' + s.IDROL.ToString() + ' no pertenece a la aplicación o no existe.');
                }

                return usuarioDA.addRolesXUsuario(Reglas, RolesXUsuario, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addUsuarioXAplicacion(ReglasBE Reglas, List<UsuarioXAppBE> lstUSuarioXApp, Int64 App)
        {
            try
            {
                UsersDA oUsersDA = new UsersDA();
                bool bFlag = false;

                bFlag = oUsersDA.addUsuarioXAplicacion(Reglas, lstUSuarioXApp, App);

                return bFlag;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DatosUsuarioBE getUsuarioFull(ReglasBE Reglas, Int64 App)
        {
            DatosUsuarioBE DatosUsuario = new DatosUsuarioBE();
            try
            {
                UsersDA usuarioDA = new UsersDA();
                DatosUsuario = usuarioDA.GetUsuarioFull(Reglas, App);
                return DatosUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UsuariosBE> GetUsuarios(UsuariosBE item, Int64 App)
        {
            List<UsuariosBE> lstUsuario = new List<UsuariosBE>();
            UsersDA oUsersDA = new UsersDA();

            lstUsuario = oUsersDA.GetUsuarios(item, App);

            return lstUsuario;
        }

        public List<UsuariosBE> GetUsuario(UsuariosBE item, Int64 App)
        {
            List<UsuariosBE> Usuarios = new List<UsuariosBE>();
            UsersDA oUsersDA = new UsersDA();

            Usuarios = oUsersDA.GetUsuario(item, App);

            return Usuarios;
        }



        public List<RolesXUsuarioBE> GetRolesVsUser(ReglasBE Reglas, Int64 App)
        {
            List<RolesXUsuarioBE> RolesVSUsuarios = new List<RolesXUsuarioBE>();
            UsersDA oUsersDA = new UsersDA();

            RolesVSUsuarios = oUsersDA.GetRolesVsUser(Reglas, App);

            return RolesVSUsuarios;
        }


        public List<RolesBE> getRolesXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.getRolesXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool checkUsrXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.checkUsrXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UsuarioXAppBE> getAppXUsuario(ReglasBE Reglas, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.getAppXUsuario(Reglas, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EstacionesXAppBE> getEstacionesXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.getEstacionesXApp(Reglas, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RelacionTipoUsuarioBE> getRelTipoUsuario(ReglasBE Reglas, Int64 App)
        {
            List<RelacionTipoUsuarioBE> lstResultado = new List<RelacionTipoUsuarioBE>();
            try
            {
                UsersDA usuarioDA = new UsersDA();
                //return usuarioDA.getRelTipoUsuario(Reglas, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstResultado;
        }

        public List<CatalogosBE> getCatSelection(int IdCatGeneral, int IdSubCatalogo, Int64 App)
        {
            try
            {
                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.getCatSelection(IdCatGeneral, IdSubCatalogo, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updateRol(ReglasBE Reglas, RolesXUsuarioBE RolXUsuario, long App)
        {
            try
            {
                ////Validamos la existencia de los roles 
                List<RolesBE> RolesXApp = getRolesXApp(Reglas, App);
                bool bFlagExist = false;

                bFlagExist = false;
                foreach (RolesBE RolXApp in RolesXApp)
                {
                    if (RolXUsuario.IDROL == RolXApp.IDROL)
                    {
                        bFlagExist = true;
                        break;
                    }
                }
                if (!bFlagExist) throw new Exception('El rol ' + RolXUsuario.IDROL.ToString() + ' no pertenece a la aplicación o no existe.');

                UsersDA usuarioDA = new UsersDA();
                return usuarioDA.updateRol(Reglas, RolXUsuario, App);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
