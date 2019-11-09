using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ThreeBits.Data.Common;
using ThreeBits.Entities.Common;
using ThreeBits.Entities.User;

namespace ThreeBits.Data.User
{
    public class UsersDA
    {
        
        Models.Seguridad.Seguridad3BitsEntities1 dboSecurity = new Models.Seguridad.Seguridad3BitsEntities1();
        public UsuariosBE addUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App)
        {
            
            DbTransaction scopeGrl;
            //dboSecurity.Connection.Open();
            //scopeGrl = dboSecurity.Connection.BeginTransaction();
            //dboSecurity.Transaction = scopeGrl;
            UsuariosBE UsuarioRes = new UsuariosBE();
            try
            {
                //using (scopeGrl)
                //{
                    ////Insertamos al usuario
                    var USUARIO = dboSecurity.sp_insUser(Usuario.IDAPLICACION, (Usuario.IDSEXO > 0 ? (Int32?)Usuario.IDSEXO : null), (Usuario.IDTIPOPERSONA > 0 ? (Int32?)Usuario.IDTIPOPERSONA : null), (Usuario.IDESTADOCIVIL > 0 ? (Int32?)Usuario.IDESTADOCIVIL : null), (Usuario.IDAREA > 0 ? (Int32?)Usuario.IDAREA : null), (Usuario.IDTIPOUSUARIO > 0 ? (Int32?)Usuario.IDTIPOUSUARIO : null), Usuario.IDUSUARIOAPP, Usuario.APATERNO, Usuario.AMATERNO, Usuario.NOMBRE, Usuario.FECHANACCONST, Usuario.USUARIO, Usuario.PASSWORD, Usuario.RUTAFOTOPERFIL, DateTime.Now, Usuario.ACTIVO, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                    Int64 IdUsuario = 0;
                    foreach (var s in USUARIO)
                    {
                        
                        IdUsuario = Int64.Parse(Usuario.IDUSUARIO.ToString());
                        UsuarioRes.IDUSUARIO = IdUsuario;
                        break;
                    }

                    ////Insertamos sus Domicilios
                    foreach (DomicilioBE Dom in Domicilios)
                    {
                        dboSecurity.sp_insDomicilio(IdUsuario, Dom.CALLE, Dom.NUMEXT, Dom.NUMINT, (Int32.Parse(Dom.IDESTADO) > 0 ? (Int32?)Int32.Parse(Dom.IDESTADO) : null), Dom.ESTADO, (Int32.Parse(Dom.IDMUNICIPIO) > 0 ? (Int32?)Int32.Parse(Dom.IDMUNICIPIO) : null), Dom.MUNICIPIO, (Int32.Parse(Dom.IDCOLONIA) > 0 ? (Int32?)Int32.Parse(Dom.IDCOLONIA) : null), Dom.COLONIA, Dom.CP, DateTime.Now, true, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                    }

                    ////Insertamos sus Contactos
                    foreach (ContactoBE Contacto in Contactos)
                    {
                        dboSecurity.sp_insContacto(IdUsuario, Contacto.IDTIPOCONTACTO, Contacto.VALOR, DateTime.Now, true, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                    }


                    ////Insertamos Aplicaciones X Usuario
                    //if (Usuario.IDAPLICACION == 0)
                    //{
                    foreach (var Rol in RolesXUsuario)
                    {
                        dboSecurity.spAddUsuarioXAplicacion(long.Parse(Rol.IDAPLICACION), IdUsuario, true);
                    }
                    //}

                    ////Insertamos sus Roles con estaciones en caso de tenerlas
                    foreach (RolesXUsuarioBE Rol in RolesXUsuario)
                    {
                        dboSecurity.sp_insRolXUserApp(Rol.IDROL, IdUsuario, (Rol.IDESTACIONXAPP > 0 ? (Int64?)Rol.IDESTACIONXAPP : null), (Rol.IDROLXUSUARIO > 0 ? (Int64?)Rol.IDROLXUSUARIO : null), Rol.ACTIVO);
                    }

                //    scopeGrl.Commit();
                //}
                return UsuarioRes;
            }
            catch (Exception ex)
            {
                //scopeGrl.Rollback();
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
            finally
            {
                //scopeGrl.Dispose();
                //dboSecurity.Connection.Close();
                //dboSecurity.Connection.Dispose();
            }
        }

        public DatosUsuarioBE GetUsuarioFull(ReglasBE Reglas, Int64 App)
        {
            
            try
            {
                DatosUsuarioBE DatosUsuarioRES = new DatosUsuarioBE();

                var Cliente = dboSecurity.sp_getUsuario(Reglas.TIPOBUSQUEDA, Reglas.USUARIO, Reglas.IDAPP);
                foreach (var s in Cliente)
                {
                    UsuariosBE UsuarioRES = new UsuariosBE();
                    UsuarioRES.IDUSUARIO = s.IDUSUARIO;
                    UsuarioRES.IDAPLICACION = s.IDAPLICACION;
                    UsuarioRES.IDSEXO = s.IDSEXO ?? 0;
                    UsuarioRES.IDTIPOPERSONA = s.IDTIPOPERSONA ?? 0;
                    UsuarioRES.IDESTADOCIVIL = s.IDESTADOCIVIL ?? 0;
                    UsuarioRES.IDAREA = s.IDAREA ?? 0;
                    UsuarioRES.DESCAREA = s.DESCAREA;
                    UsuarioRES.IDTIPOUSUARIO = s.IDTIPOUSUARIO ?? 0;
                    UsuarioRES.DESCTIPOUSUARIO = s.DESCIDTIPOUSUARIO;
                    UsuarioRES.IDUSUARIOAPP = s.IDUSUARIOAPP;
                    UsuarioRES.APATERNO = s.APATERNO;
                    UsuarioRES.AMATERNO = s.AMATERNO;
                    UsuarioRES.NOMBRE = s.NOMBRE;
                    UsuarioRES.FECHANACCONST = s.FECHANACCONST;
                    UsuarioRES.USUARIO = s.USUARIO;
                    UsuarioRES.PASSWORD = s.PASSWORD;
                    UsuarioRES.RUTAFOTOPERFIL = s.RUTAFOTOPERFIL;
                    UsuarioRES.FECHAALTA = s.FECHAALTA ?? DateTime.Now;
                    UsuarioRES.ACTIVO = s.ACTIVO ?? false;
                    DatosUsuarioRES.Usuario = UsuarioRES;
                }
                if (DatosUsuarioRES.Usuario.IDUSUARIO == 0) throw new Exception('EL USUARIO NO SE A SIDO DADO DE ALTO O NO TIENE PERMISOS');

                var Domicilios = dboSecurity.sp_getDomicilios(DatosUsuarioRES.Usuario.IDUSUARIO);
                foreach (var s in Domicilios)
                {
                    DomicilioBE DomicilioRES = new DomicilioBE();
                    DomicilioRES.IDDOMICILIO = s.IDDOMICILIO;
                    DomicilioRES.IDUSUARIO = s.IDUSUARIO ?? 0;
                    DomicilioRES.CALLE = s.CALLE;
                    DomicilioRES.NUMEXT = s.NUMEXT;
                    DomicilioRES.NUMINT = s.NUMINT;
                    DomicilioRES.IDESTADO = string.IsNullOrEmpty(s.IDESTADO) ? '0' : s.IDESTADO;
                    //DomicilioRES.IDESTADO = (s.IDESTADO ?? 0).ToString();
                    DomicilioRES.ESTADO = s.ESTADO;
                    DomicilioRES.IDMUNICIPIO = string.IsNullOrEmpty(s.IDMUN) ? '0' : s.IDMUN;
                    //DomicilioRES.IDMUNICIPIO = (s.IDMUN ?? 0).ToString();
                    DomicilioRES.MUNICIPIO = s.MUNICIPIO;
                    DomicilioRES.IDCOLONIA = string.IsNullOrEmpty(s.IDCOLONIA) ? '0' : s.IDCOLONIA;
                    //DomicilioRES.IDCOLONIA = (s.IDCOLONIA ?? 0).ToString();
                    DomicilioRES.COLONIA = s.COLONIA;
                    DomicilioRES.CP = s.CP;
                    DomicilioRES.FECHAALTA = s.FECHAALTA ?? DateTime.Now;
                    DomicilioRES.ACTIVO = s.ACTIVO ?? false;
                    DatosUsuarioRES.Domicilios.Add(DomicilioRES);
                }

                var Contactos = dboSecurity.sp_getContactos(DatosUsuarioRES.Usuario.IDUSUARIO);
                foreach (var s in Contactos)
                {
                    ContactoBE ContactoRES = new ContactoBE();
                    ContactoRES.IDCONTACTO = s.IDCONTACTO;
                    ContactoRES.IDUSUARIO = s.IDUSUARIO ?? 0;
                    ContactoRES.IDTIPOCONTACTO = s.IDTIPOCONTACTO ?? 0;
                    ContactoRES.TIPOCONTACTO = s.TIPOCONTACTO;
                    ContactoRES.VALOR = s.VALOR;
                    ContactoRES.FECHAALTA = s.FECHAALTA ?? DateTime.Now;
                    ContactoRES.ACTIVO = s.ACTIVO ?? false;
                    DatosUsuarioRES.Contactos.Add(ContactoRES);
                }

                var Roles = dboSecurity.sp_getRolesXUserApp(Reglas.TIPOBUSQUEDA, Reglas.USUARIO, Reglas.IDAPP);

                ////DatosUsuarioRES.RolesXUsuario = Utilidades<ROLESXUSUARIO, RolesXUsuarioBE>.Transformar(dboSecurity.sp_getRolesXUserApp(Reglas.TIPOBUSQUEDA, Reglas.USUARIO, Reglas.IDAPP));

                foreach (var Rol in Roles)
                {
                    RolesXUsuarioBE RolesXUsuario = new RolesXUsuarioBE();
                    RolesXUsuario.IDROLXUSUARIO = Rol.IDROLXUSUARIO;
                    RolesXUsuario.IDROL = Rol.IDROL ?? 0;
                    RolesXUsuario.DESCROL = Rol.DESCROL;
                    RolesXUsuario.IDUSUARIO = Rol.IDUSUARIO ?? 0;
                    RolesXUsuario.IDESTACIONXAPP = Rol.IDESTACIONXAPP ?? 0;
                    RolesXUsuario.IDAPLICACION = Rol.IDAPLICACION.ToString();
                    RolesXUsuario.APLICACION = Rol.DescripcionAplicacion.ToString();
                    RolesXUsuario.ACTIVO = Rol.ACTIVO ?? false;
                    DatosUsuarioRES.RolesXUsuario.Add(RolesXUsuario);
                }

                return DatosUsuarioRES;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
            finally
            {
                //dboSecurity.Connection.Close();
                //dboSecurity.Connection.Dispose();
            }
        }

        public List<UsuariosBE> GetUsuarios(UsuariosBE item, Int64 App)
        {
            
            List<UsuariosBE> lstUsuarios = new List<UsuariosBE>();
            try
            {

                DatosUsuarioBE DatosUsuarioRES = new DatosUsuarioBE();

                var lstLector = dboSecurity.spGetUsuarios(int.Parse(item.IDAPLICACION.ToString()), item.NOMBRE, item.AMATERNO, item.APATERNO, item.USUARIO);
                foreach (var Lector in lstLector)
                {
                    UsuariosBE itemLector = new UsuariosBE();
                    itemLector.IDUSUARIO = Lector.IDUSUARIO;
                    itemLector.IDAPLICACION = Lector.IDAPLICACION;
                    itemLector.DESCAREA = Lector.AREA;
                    itemLector.APATERNO = Lector.APATERNO;
                    itemLector.AMATERNO = Lector.AMATERNO;
                    itemLector.NOMBRE = Lector.NOMBRE;
                    itemLector.FECHANACCONST = Lector.FECHANACCONST;
                    itemLector.USUARIO = Lector.USUARIO;
                    itemLector.ACTIVO = bool.Parse(string.IsNullOrEmpty(Lector.ACTIVO.ToString()) ? 'false' : 'true');

                    lstUsuarios.Add(itemLector);
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
            finally
            {
                //dboSecurity.Connection.Close();
                //dboSecurity.Connection.Dispose();
            }
            return lstUsuarios;
        }

        public List<UsuariosBE> GetUsuario(UsuariosBE item, Int64 App)
        {
            
            List<UsuariosBE> lstUsuarios = new List<UsuariosBE>();
            try
            {

                DatosUsuarioBE DatosUsuarioRES = new DatosUsuarioBE();

                var lstLector = dboSecurity.spGetUsuario(item.IDUSUARIO.ToString());
                foreach (var Lector in lstLector)
                {
                    UsuariosBE itemLector = new UsuariosBE();
                    itemLector.IDUSUARIO = Lector.IDUSUARIO;
                    itemLector.IDSEXO = Lector.IDSEXO ?? 0;
                    itemLector.IDTIPOPERSONA = Lector.IDTIPOPERSONA ?? 0;
                    itemLector.IDESTADOCIVIL = Lector.IDESTADOCIVIL ?? 0;
                    itemLector.IDAREA = Lector.IDAREA ?? 0;
                    itemLector.IDTIPOUSUARIO = Lector.IDTIPOUSUARIO ?? 0;
                    itemLector.APATERNO = Lector.APATERNO;
                    itemLector.AMATERNO = Lector.AMATERNO;
                    itemLector.NOMBRE = Lector.NOMBRE;
                    itemLector.FECHANACCONST = Lector.FECHANACCONST;
                    itemLector.USUARIO = Lector.USUARIO;
                    itemLector.PASSWORD = Lector.PASSWORD;
                    itemLector.RUTAFOTOPERFIL = Lector.RUTAFOTOPERFIL;
                    itemLector.FECHAALTA = DateTime.Parse(Lector.FECHAALTA.ToString());
                    itemLector.ACTIVO = bool.Parse(Lector.ACTIVO.ToString());

                    lstUsuarios.Add(itemLector);
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
            finally
            {
                //dboSecurity.Connection.Close();
                //dboSecurity.Connection.Dispose();
            }

            return lstUsuarios;
        }

        public bool actDeactivateUsuario(ReglasBE Reglas, Int64 App)
        {
            try
            {
                

                bool Respuesta = true;
                dboSecurity.sp_actDeactUsuario(Reglas.TIPOBUSQUEDA, Reglas.ACTIVO, Reglas.USUARIO, Reglas.IDAPP, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                return Respuesta;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool updateUsuario(ReglasBE Reglas, UsuariosBE Usuario, List<DomicilioBE> Domicilios, List<ContactoBE> Contactos, List<RolesXUsuarioBE> RolesXUsuario, Int64 App)
        {
            
            DbTransaction scopeGrl;
            //dboSecurity.Connection.Open();
            //scopeGrl = dboSecurity.Connection.BeginTransaction();
            //dboSecurity.Transaction = scopeGrl;
            UsuariosBE UsuarioRes = new UsuariosBE();
            bool bFlag = true;
            try
            {
                //using (scopeGrl)
                //{
                    ////Actualizamos al usuario
                    dboSecurity.sp_updUsuario(Usuario.IDUSUARIO, App, (Usuario.IDSEXO > 0 ? (Int32?)Usuario.IDSEXO : null), (Usuario.IDTIPOPERSONA > 0 ? (Int32?)Usuario.IDTIPOPERSONA : null), (Usuario.IDESTADOCIVIL > 0 ? (Int32?)Usuario.IDESTADOCIVIL : null), (Usuario.IDAREA > 0 ? (Int32?)Usuario.IDAREA : null), (Usuario.IDTIPOUSUARIO > 0 ? (Int32?)Usuario.IDTIPOUSUARIO : null), Usuario.IDUSUARIOAPP, Usuario.APATERNO, Usuario.AMATERNO, Usuario.NOMBRE, Usuario.FECHANACCONST, Usuario.USUARIO, Usuario.PASSWORD, Usuario.RUTAFOTOPERFIL, Usuario.ACTIVO, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);

                    ////Insertamos sus Domicilios
                    if (Domicilios != null)
                    {
                        foreach (DomicilioBE Dom in Domicilios)
                        {
                            if (Dom.IDDOMICILIO > 0)
                                dboSecurity.sp_updDomicilio(Usuario.IDUSUARIO, Dom.IDDOMICILIO, Dom.CALLE, Dom.NUMEXT, Dom.NUMINT, (Int32.Parse(Dom.IDESTADO) > 0 ? (Int32?)Int32.Parse(Dom.IDESTADO) : null), Dom.ESTADO, (Int32.Parse(Dom.IDMUNICIPIO) > 0 ? (Int32?)Int32.Parse(Dom.IDMUNICIPIO) : null), Dom.MUNICIPIO, (Int32.Parse(Dom.IDCOLONIA) > 0 ? (Int32?)Int32.Parse(Dom.IDCOLONIA) : null), Dom.COLONIA, Dom.CP, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                            else
                                dboSecurity.sp_insDomicilio(Usuario.IDUSUARIO, Dom.CALLE, Dom.NUMEXT, Dom.NUMINT, (Int32.Parse(Dom.IDESTADO) > 0 ? (Int32?)Int32.Parse(Dom.IDESTADO) : null), Dom.ESTADO, (Int32.Parse(Dom.IDMUNICIPIO) > 0 ? (Int32?)Int32.Parse(Dom.IDMUNICIPIO) : null), Dom.MUNICIPIO, (Int32.Parse(Dom.IDCOLONIA) > 0 ? (Int32?)Int32.Parse(Dom.IDCOLONIA) : null), Dom.COLONIA, Dom.CP, DateTime.Now, true, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                        }
                    }

                    ////Insertamos sus Contactos
                    if (Contactos != null)
                    {
                        foreach (ContactoBE Contacto in Contactos)
                        {
                            if (Contacto.IDCONTACTO > 0)
                                dboSecurity.sp_updContacto(Usuario.IDUSUARIO, Contacto.IDCONTACTO, Contacto.IDTIPOCONTACTO, Contacto.VALOR, Contacto.ACTIVO, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                            else
                                dboSecurity.sp_insContacto(Usuario.IDUSUARIO, Contacto.IDTIPOCONTACTO, Contacto.VALOR, DateTime.Now, true, App, (Reglas.IDUSRMODIF > 0 ? (Int64?)Reglas.IDUSRMODIF : null), App);
                        }
                    }

                    ////Insertamos Aplicaciones X Usuario
                    //if (Usuario.IDAPLICACION == 0)
                    //{
                    foreach (var Rol in RolesXUsuario)
                    {
                        dboSecurity.spAddUsuarioXAplicacion(long.Parse(Rol.IDAPLICACION), Usuario.IDUSUARIO, true);
                    }
                    //}

                    if (RolesXUsuario != null)
                    {
                        foreach (RolesXUsuarioBE Rol in RolesXUsuario)
                        {
                            dboSecurity.sp_insRolXUserApp(Rol.IDROL, Usuario.IDUSUARIO, (Rol.IDESTACIONXAPP > 0 ? (Int64?)Rol.IDESTACIONXAPP : null), (Rol.IDROLXUSUARIO > 0 ? (Int64?)Rol.IDROLXUSUARIO : null), Rol.ACTIVO);
                        }
                    }

                    //Verificar Actualizacion de Apliacion X Usuario
                //    scopeGrl.Commit();
                //}
                return bFlag;
            }
            catch (Exception ex)
            {
                //scopeGrl.Rollback();

                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
            finally
            {
                //scopeGrl.Dispose();
                //dboSecurity.Connection.Close();
                //dboSecurity.Connection.Dispose();
            }
        }

        public bool checkUsrXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                
                DatosUsuarioBE DatosUsuarioRES = new DatosUsuarioBE();
                bool bFlag = false;

                var Cliente = dboSecurity.sp_checkUsrXApp(Reglas.TIPOBUSQUEDA, Reglas.IDAPP, Reglas.USUARIO);

                foreach (var s in Cliente)
                {
                    bFlag = true;
                    break;
                }
                return bFlag;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<UsuarioXAppBE> getAppXUsuario(ReglasBE Reglas, Int64 App)
        {
            try
            {
                
                List<UsuarioXAppBE> ListaApps = new List<UsuarioXAppBE>();
                var Consulta = dboSecurity.sp_getAppsXUsuario(Reglas.TIPOBUSQUEDA, Reglas.USUARIO);

                foreach (var s in Consulta)
                {
                    UsuarioXAppBE AppItem = new UsuarioXAppBE();
                    AppItem.IDAPLICACION = s.IDAPLICACION.ToString();
                    AppItem.DESCRIPCION = s.DESCRIPCION;
                    AppItem.URLINICIO = s.URLINICIO;
                    AppItem.ACTIVO = s.ACTIVO.ToString();
                    AppItem.IDUSRSXAPP = s.IDUSRSXAPP.ToString();
                    ListaApps.Add(AppItem);
                }
                return ListaApps;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<EstacionesXAppBE> getEstacionesXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                
                List<EstacionesXAppBE> ListaEstaciones = new List<EstacionesXAppBE>();
                var Consulta = dboSecurity.sp_getEstacionesXApps(Reglas.IDAPP);

                foreach (var s in Consulta)
                {
                    EstacionesXAppBE EstacionesItem = new EstacionesXAppBE();
                    EstacionesItem.IDESTACIONXAPP = s.IDESTACIONXAPP;
                    EstacionesItem.IDAPLICACION = s.IDAPLICACION ?? 0;
                    EstacionesItem.IDESTACION = s.IDESTACION ?? 0;
                    EstacionesItem.ACTIVO = s.ACTIVO ?? false;
                    ListaEstaciones.Add(EstacionesItem);
                }
                return ListaEstaciones;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool addRolesXUsuario(ReglasBE Reglas, List<RolesXUsuarioBE> RolesXUsuario, long App)
        {
            try
            {
                
                bool bFlag = true;

                foreach (RolesXUsuarioBE Rol in RolesXUsuario)
                {
                    dboSecurity.sp_insRolesXUsuario(Rol.IDROL, Rol.IDUSUARIO, (Rol.IDESTACIONXAPP > 0 ? (Int64?)Rol.IDESTACIONXAPP : null));
                }

                return bFlag;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<RolesXUsuarioBE> GetRolesVsUser(ReglasBE Reglas, Int64 App)
        {
            try
            {
                
                List<RolesXUsuarioBE> RolesVSUsuarios = new List<RolesXUsuarioBE>();

                var RolesVsUsuario = dboSecurity.spGetRolesVSUsuario(Reglas.USUARIO, Reglas.IDAPP.ToString());

                foreach (var item in RolesVsUsuario)
                {
                    RolesXUsuarioBE RolesXUsuario = new RolesXUsuarioBE();

                    RolesXUsuario.IDROL = item.IDROL;
                    RolesXUsuario.DESCROL = item.DESCRIPCION;
                    RolesXUsuario.IDAPLICACION = item.IDAPLICACION.ToString();
                    RolesXUsuario.APLICACION = item.APLICACION;
                    RolesXUsuario.IDROLXUSUARIO = item.IDROLXUSUARIO ?? 0;
                    RolesXUsuario.ACTIVO = (item.ROLASIGNADO == 0) ? false : true;


                    RolesVSUsuarios.Add(RolesXUsuario);
                }
                return RolesVSUsuarios;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool addUsuarioXAplicacion(ReglasBE Reglas, List<UsuarioXAppBE> lstUSuarioXApp, Int64 App)
        {
            try
            {
                
                bool bFlag = true;

                foreach (UsuarioXAppBE item in lstUSuarioXApp)
                {
                    dboSecurity.sp_addUsuarioXAplicacion(item.IDUSRSXAPP, item.IDAPLICACION, item.IDUSUARIO, item.ACTIVO);
                }

                return bFlag;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<RolesBE> getRolesXApp(ReglasBE Reglas, Int64 App)
        {
            try
            {
                
                List<RolesBE> RolesXApp = new List<RolesBE>();
                var Consulta = dboSecurity.sp_getRolesXApp(Reglas.IDAPP);

                foreach (var s in Consulta)
                {
                    RolesBE RolXApp = new RolesBE();
                    RolXApp.IDROL = s.IDROL;
                    RolXApp.IDAPLICACION = s.IDAPLICACION ?? 0;
                    RolXApp.DESCRIPCION = s.DESCRIPCION;
                    RolXApp.ACTIVO = s.ACTIVO ?? false;
                    RolesXApp.Add(RolXApp);
                }
                return RolesXApp;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public List<CatalogosBE> getCatSelection(int IdCatGeneral, int IdSubCatalogo, Int64 App)
        {
            try
            {
                

                List<CatalogosBE> ListaCatalogo = new List<CatalogosBE>();
                CatalogosBE items = new CatalogosBE();

                ////Obtenemos el catalogo especifico por id
                var lnqgetCat = (from c in dboSecurity.CAT_GRALS
                                 where c.IDCATGRAL == IdCatGeneral
                                 select new
                                 {
                                     IDCATGRAL = c.IDCATGRAL,
                                     NOMBRETABLA = c.NOMBRETABLA,
                                     IDTABLA = c.IDTABLA,
                                     DESCRIPCIONTABLA = c.DESCRIPCIONTABLA,
                                     IDFILTRO = c.IDFILTRO,
                                 }).ToList();

                Cat_GralsBE CatGrasl = new Cat_GralsBE();
                foreach (var a in lnqgetCat)
                {
                    CatGrasl.IDCATGRAL = a.IDCATGRAL;
                    CatGrasl.NOMBRETABLA = a.NOMBRETABLA;
                    CatGrasl.IDTABLA = a.IDTABLA;
                    CatGrasl.DESCRIPCIONTABLA = a.DESCRIPCIONTABLA;
                    CatGrasl.IDFILTRO = a.IDFILTRO;
                }


                ////Obtenemos finalmente el catalogo

                StringBuilder sComando = new StringBuilder(string.Empty);
                sComando.Append('SELECT CONVERT(VARCHAR(250),');
                sComando.Append(CatGrasl.IDTABLA); sComando.Append(') AS ID,');
                sComando.Append('CONVERT(VARCHAR(250),');
                sComando.Append(CatGrasl.DESCRIPCIONTABLA); sComando.Append(')');
                sComando.Append(' AS DESCRIPCION');
                sComando.Append(' FROM ');
                sComando.Append(CatGrasl.NOMBRETABLA);

                if (!string.IsNullOrEmpty(CatGrasl.IDFILTRO) && IdSubCatalogo != 0)
                {
                    sComando.Append(' WHERE ');
                    sComando.Append(CatGrasl.IDFILTRO);
                    sComando.Append('='');
                    sComando.Append(IdSubCatalogo);
                    sComando.Append('' AND ACTIVO = 1 ');
                }
                else sComando.Append(' WHERE ACTIVO = 1 ');

                var lnqConsulta = dboSecurity.Database.SqlQuery<CatalogosBE>(sComando.ToString());
                List<CatalogosBE> ListCatalogo = new List<CatalogosBE>();

                foreach (var a in lnqConsulta)
                {
                    CatalogosBE item = new CatalogosBE();
                    item.ID = a.ID.ToString();
                    item.DESCRIPCION = a.DESCRIPCION.ToUpper();
                    ListaCatalogo.Add(item);
                }
                lnqConsulta = null;
                List<CatalogosBE> ListaGrlSort = ListaCatalogo.OrderBy(x => x.DESCRIPCION).ThenBy(x => x.DESCRIPCION).ToList();
                CatalogosBE lista = new CatalogosBE();
                lista.ID = '0';
                lista.DESCRIPCION = '[SELECCIONE UNA OPCIÓN]';
                ListaGrlSort.Insert(0, lista);

                return ListaGrlSort;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }

        public bool updateRol(ReglasBE Reglas, RolesXUsuarioBE RolXUsuario, long App)
        {
            try
            {
                

                bool Respuesta = true;
                dboSecurity.sp_updRol(RolXUsuario.IDROLXUSUARIO, RolXUsuario.IDROL, RolXUsuario.IDESTACIONXAPP);
                return Respuesta;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(true);
                CommonDA ComunDA = new CommonDA();
                ComunDA.insErrorDB('Error: ' + ex.Message + ' En El Metodo: ' + MethodBase.GetCurrentMethod().Name, st, '', App.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
