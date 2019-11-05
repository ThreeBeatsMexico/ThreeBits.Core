using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThreeBits.Security.Portal.Properties;

namespace ThreeBits.Security.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string sUsuario, string password, bool? recordar)
        {
            recordar = recordar == null ? false : true;
            if (fnLogin(sUsuario, password, (bool)recordar))
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        private bool fnLogin(string sUsuario, string sPassword, bool sRecordarcuenta)
        {
            bool _return = false;
            string NUsuario = string.Empty;


            string parametros = string.Empty;
            if (string.IsNullOrEmpty(sUsuario) || string.IsNullOrEmpty(sPassword))
            {
                ModelState.AddModelError("", "Ninguno de los campos puede estar vacío");
            }
            //else if (!ValidaExpresion(sUsuario, @"^[a-zA-Z0-9]{0,50}$"))
            //{
            //    ModelState.AddModelError("", "El campo Email contiene caracteres no válidos,");
            //}
            else
            {

                USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
                USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
                USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
                USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();

                reglas.TIPOBUSQUEDA = 3;
                reglas.USUARIO = sUsuario;

                reglas.IDAPP = long.Parse(ResourceSec.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceSec.IdApp), ResourceSec.Password);

                if (resUsuario.DatosUsuario.Usuario.IDUSUARIO.ToString() == "0")
                {
                    ModelState.AddModelError("", "El Nombre de usuario no existe!");
                }
                else if (resUsuario.DatosUsuario.Usuario.ACTIVO == false)
                {
                    //  dvLogin.Attributes.Add("style", "display:none"); dvMensajeCliente.Attributes.Add("style", "display:block");
                    //Mensaje.setMensaje("El usuario se encuentra intactivo, debes activarlo desde tu cuenta correo registrada.", "Lo Sentimos", 2);

                    //Comun.Mensaje Men = new Comun.Mensaje();
                    //Men.setMensaje("HOLA", "LO SENTIMOS", 3);
                }
                else
                {
                    SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
                    SECURITYWCF.SecutityDC ResDesencriptaPass = new SECURITYWCF.SecutityDC();
                    ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(resUsuario.DatosUsuario.Usuario.PASSWORD, 2, long.Parse(ResourceSec.IdApp), ResourceSec.Password);

                    if (ValidaPassword(sPassword, ResDesencriptaPass.Encriptacion.VALOROUT.ToString()))
                    {
                        ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(resUsuario.DatosUsuario.Usuario.IDUSUARIO.ToString(), 1, long.Parse(ResourceSec.IdApp), ResourceSec.Password);
                        itemSecurity.NOMBRE = resUsuario.DatosUsuario.Usuario.NOMBRE;
                        itemSecurity.APATERNO = resUsuario.DatosUsuario.Usuario.APATERNO;
                        itemSecurity.AMATERNO = resUsuario.DatosUsuario.Usuario.AMATERNO;
                        itemSecurity.IDUSUARIOAPP = ResDesencriptaPass.Encriptacion.VALOROUT.ToString();
                        itemSecurity.IDUSUARIO = resUsuario.DatosUsuario.Usuario.IDUSUARIO;
                        itemSecurity.RUTAFOTOPERFIL = resUsuario.DatosUsuario.Usuario.RUTAFOTOPERFIL;
                        itemSecurity.USUARIO = resUsuario.DatosUsuario.Usuario.USUARIO;
                        Session.Add("USER_SESSION", itemSecurity);
                        FormsAuthentication.SetAuthCookie(sUsuario, sRecordarcuenta);
                        _return = true;
                    }
                    else { ModelState.AddModelError("", "El Password es incorrecto!"); }
                }
            }
            return _return;
        }

        public bool ValidaExpresion(string sTexto, string sPatron)
        {
            bool iRespuesta = false;
            Match mExpresionMatch = default(Match);
            mExpresionMatch = Regex.Match(sTexto, sPatron);
            if (mExpresionMatch.Success)
            {
                iRespuesta = true;
            }
            return iRespuesta;
        }

        public bool ValidaPassword(string sPassword, string sPasswordBD)
        {
            bool iRespuesta = false;

            if (sPassword == sPasswordBD)
            {
                iRespuesta = true;
            }
            return iRespuesta;
        }

        public ActionResult getMenu()
        {
            try
            {
                USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
                itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];
                USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
                USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
                USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
                USERSECURITYWCF.UsuarioDC resUsuarioRol = new USERSECURITYWCF.UsuarioDC();
                SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
                SECURITYWCF.SecutityDC oSecurity = new SECURITYWCF.SecutityDC();
                List<SECURITYWCF.PermisosXMenuBE> oListaMenu = new List<SECURITYWCF.PermisosXMenuBE>();
                reglas.TIPOBUSQUEDA = 1;
                reglas.USUARIO = itemSecurity.IDUSUARIO.ToString();
                reglas.IDAPP = long.Parse(ResourceSec.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceSec.IdApp), ResourceSec.Password);
                resUsuarioRol = seguridad.getRolesXApp(reglas, long.Parse(ResourceSec.IdApp), ResourceSec.Password);

                string sIdRol = string.Empty;
                string sDescripcionRol = string.Empty;
                foreach (var item in resUsuario.DatosUsuario.RolesXUsuario)
                {
                    if (item.IDAPLICACION == reglas.IDAPP.ToString())
                    {
                        sIdRol = item.IDROL.ToString();
                        sDescripcionRol = item.DESCROL;
                        break;
                    }
                }
                List<USERSECURITYWCF.RolesBE> Rol = (from Resultado in resUsuarioRol.ListaRolesXApp
                                                     where Resultado.IDROL == long.Parse(sIdRol)
                                                     select Resultado).ToList();

                int dRol = Convert.ToInt32(Rol[0].IDROL);

                oSecurity = SeguridadLatino.getMenuXAppRol(dRol, long.Parse(ResourceSec.IdApp), ResourceSec.Password);
                oListaMenu = oSecurity.PermisosXMenu.OrderByDescending(x => x.IDPERMISOSMENU).ToList();


                var pageName = Request.Url.AbsolutePath.Split('/').LastOrDefault();

                if (oListaMenu.Count > 0)
                {
                    ViewBag.oListaMenu = oListaMenu;
                    return PartialView("Menu");
                }
                else
                {
                    return PartialView("Menu", null);
                }

            }
            catch
            {
                return PartialView("Menu", null);
            }



        }

        public void get_sumbenu(int idPermisoMenu)
        {
            SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC oSecurity = new SECURITYWCF.SecutityDC();

            List<SECURITYWCF.PermisoXSubmenuBE> oListaSubMenu = new List<SECURITYWCF.PermisoXSubmenuBE>();
            oSecurity = SeguridadLatino.getSubMenuXIdMenu(idPermisoMenu, long.Parse(ResourceSec.IdApp), ResourceSec.Password);
            oListaSubMenu = oSecurity.PermisosXSubmenu.ToList();
            Session.Add("SubMenu", oListaSubMenu);
        }


        public ActionResult LogOff()
        {
            this.ControllerContext.HttpContext.Response.Cookies.Clear();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();


            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Perfil()
        {
            USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
            itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];
            USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
            USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
            USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
            USERSECURITYWCF.UsuarioDC resUsuarioRol = new USERSECURITYWCF.UsuarioDC();
            SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC oSecurity = new SECURITYWCF.SecutityDC();
            List<SECURITYWCF.PermisosXMenuBE> oListaMenu = new List<SECURITYWCF.PermisosXMenuBE>();
            reglas.TIPOBUSQUEDA = 1;
            reglas.USUARIO = itemSecurity.IDUSUARIO.ToString();
            reglas.IDAPP = long.Parse(ResourceSec.IdApp);
            resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceSec.IdApp), ResourceSec.Password);

            ViewBag.ResUsuario = resUsuario;









            return View();
        }
        [HttpPost]
        public ActionResult Perfil(int IdRol = 0)
        {

            return View();
        }
        /// <summary>
        /// Crea la parte de menu padres del sistema asociados al rol
        /// </summary>
        /// <param name="IdRol">Es el rol con el que accede el usuario al sistem</param>
        /// <returns></returns>

        public ActionResult Registrarme()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Registrarme(string NombreEmpresa, string CorreoElectronico, string Password)
        //{
        //    IRepository repository = new Model.InventariosDB.Repository();
        //    var objUsu = repository.FindEntity<USUARIOS>(c => c.EMAIL == CorreoElectronico);
        //    string strMensaje = "";
        //    int id = 0;
        //    if (objUsu != null)
        //    {
        //        strMensaje = "El usuario ya existe en nuestra base de datos, intente recuperar su cuenta para cambiar su contraseña.";
        //    }
        //    else
        //    {
        //        string strPass = CryptoHelper.ComputeHash(Password, CryptoHelper.Supported_HA.SHA512, null);
        //        var objEmpresa = repository.Create(new EMPRESA
        //        {

        //            CORREOELECTRONICO = CorreoElectronico,
        //            DIRECCION = "",
        //            LOGO = "",
        //            MONEDA = "MX",
        //            NOMBREEMPRESA = NombreEmpresa,
        //            TELEFONO = "",
        //            TIPO_ID = 2,
        //            IDZONAHORARIA = null
        //        });
        //        if (objEmpresa != null)
        //        {
        //            var objUsuNew = repository.Create(new USUARIOS
        //            {
        //                ACTIVO = true,
        //                EMAIL = CorreoElectronico,
        //                IDEMPRESA = objEmpresa.ID,
        //                FECHA = DateTime.Now,
        //                NOMBRE = "",
        //                PASSWORD = strPass,
        //                IDROL = 1,
        //                TELEFONO = ""
        //            });
        //            if (objUsuNew != null)
        //            {
        //                var baseAddress = new Uri(ToolsHelper.UrlOriginal(Request));
        //                string Mensaje = "Gracias por unscribirse al sistema de inventarios, puede entrar con el usuario " +
        //                    "y contraseña registrada. <a href='" + baseAddress + "'>INVENTARIOS</a>";
        //                ToolsHelper.SendMail(CorreoElectronico, "Gracias por registrarte a INVENTARIOS", Mensaje);
        //                strMensaje = "Te registraste correctamente, ya puedes entrar al sistema.";
        //                id = objUsuNew.ID;
        //            }
        //            else
        //            {
        //                strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
        //            }
        //        }
        //        else
        //        {
        //            strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
        //        }
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult RecuperarCuenta()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult RecuperarCuenta(string CorreoElectronico)
        //{
        //    IRepository repository = new Model.InventariosDB.Repository();
        //    var objUsu = repository.FindEntity<USUARIOS>(c => c.EMAIL == CorreoElectronico);
        //    int id = 0;
        //    string strMensaje = "El correo no se encuentra registrado.";
        //    if (objUsu != null)
        //    {
        //        objUsu.ACTIVO = true;
        //        string strToken = objUsu.ID.ToString() + objUsu.EMAIL;
        //        string strTknAjax = CryptoHelper.ComputeHash(strToken, CryptoHelper.Supported_HA.SHA512, null);
        //        objUsu.TOKEN = Server.UrlEncode(strTknAjax);
        //        repository.Update(objUsu);
        //        var baseAddress = ToolsHelper.UrlOriginal(Request) + "/Account/ResetPass/?tkn=" + objUsu.TOKEN;
        //        string Mensaje = "Para restaurar tu cuenta de INVENTARIOS, entra a la siguiente liga y crea una nueva contraseña. <br/><br/> <a href='" + baseAddress + "'>INVENTARIOS recuperar cuenta</a>";
        //        ToolsHelper.SendMail(CorreoElectronico, "Recuperar cuenta de INVENTARIOS", Mensaje);
        //        strMensaje = "Se envío un correo con la información requerida para recuperar su cuenta.";
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult ResetPass(string tkn)
        //{
        //    if (!string.IsNullOrEmpty(tkn))
        //    {
        //        IRepository repository = new Model.InventariosDB.Repository();
        //        tkn = Server.UrlEncode(tkn);
        //        ViewBag.tkn = tkn;
        //        var objUsu = repository.FindEntity<USUARIOS>(c => c.TOKEN == tkn);
        //        if (objUsu != null)
        //        {
        //            return View();
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpPost]
        //public ActionResult ResetPass(string Password, string tkn)
        //{
        //    IRepository repository = new Model.InventariosDB.Repository();
        //    var objUsu = repository.FindEntity<USUARIOS>(c => c.TOKEN == tkn);
        //    string strMensaje = "";
        //    int id = 0;
        //    if (objUsu != null)
        //    {
        //        string strPass = CryptoHelper.ComputeHash(Password, CryptoHelper.Supported_HA.SHA512, null);
        //        objUsu.PASSWORD = strPass;
        //        objUsu.TOKEN = "";
        //        repository.Update(objUsu);
        //        strMensaje = "Se actualizó la contraseña correctamente, ya puede entrar al sistema INVENTARIOS.";
        //    }
        //    else
        //    {
        //        strMensaje = "El token se encuentra vencido, necesita recuperar nuevamente su cuenta.";
        //    }
        //    return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        //}
    
    }
}