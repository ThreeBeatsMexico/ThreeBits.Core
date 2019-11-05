using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThreeBits.Timbrado.Portal.COMMONWCF;
using ThreeBits.Timbrado.Portal.Helpers;
using ThreeBits.Timbrado.Portal.Models;
using ThreeBits.Timbrado.Portal.Properties;

namespace ThreeBits.Timbrado.Portal.Controllers
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
            string strMensaje = string.Empty;
            int id = 0;
            bool success = false;
            recordar = recordar == null ? false : true;
            if (fnLogin(sUsuario, password, (bool)recordar))
            {
                id = -1;
                strMensaje = Url.Content("~/Home");
                success = true;
            }
            else
            {
                id = 1;
                success = false;
                strMensaje = "Ocurrio un error, revisa tus credenciales";
            }
            return Json(new Response { IsSuccess = success, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
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

                reglas.IDAPP = long.Parse(ResourceApp.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                //resUsuario.DatosUsuario.Contactos
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
                    ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(resUsuario.DatosUsuario.Usuario.PASSWORD, 2, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

                    if (ValidaPassword(sPassword, ResDesencriptaPass.Encriptacion.VALOROUT.ToString()))
                    {
                        ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(resUsuario.DatosUsuario.Usuario.IDUSUARIO.ToString(), 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
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
                reglas.IDAPP = long.Parse(ResourceApp.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                resUsuarioRol = seguridad.getRolesXApp(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

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

                oSecurity = SeguridadLatino.getMenuXAppRol(dRol, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                oListaMenu = oSecurity.PermisosXMenu.OrderBy(x => x.ORDENMENU).ToList();


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
            oSecurity = SeguridadLatino.getSubMenuXIdMenu(idPermisoMenu, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            oListaSubMenu = oSecurity.PermisosXSubmenu.OrderBy(x => x.ORDENSUBMENU).ToList();
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
            if (Session["USER_SESSION"] != null)
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
                reglas.IDAPP = long.Parse(ResourceApp.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

                ViewBag.ResUsuario = resUsuario;
                HelperTools hHelp = new HelperTools();
                List<CatalogosBE> lstSexo = new List<CatalogosBE>();
                lstSexo = hHelp.SetDdlCatalogos("8");
                List<CatalogosBE> lstEdoCivil = new List<CatalogosBE>();
                lstEdoCivil = hHelp.SetDdlCatalogos("5");

                ViewBag.IdSexo = new SelectList(lstSexo, "ID", "DESCRIPCION", resUsuario.DatosUsuario.Usuario.IDSEXO);
                ViewBag.IdEstadoCivil = new SelectList(lstEdoCivil, "ID", "DESCRIPCION", resUsuario.DatosUsuario.Usuario.IDESTADOCIVIL);


            }
            return View();
        }
        [HttpPost]
        public ActionResult Perfil(string idUsuario, string sNombre, string sPaterno, string sMaterno, DateTime FNacimiento, int IdSexo, int IdEstadoCivil,
            string sCalle, string sNumExt, string sNumInt, string sCP, string sColonia, string sMunicipio, string sEstado,
            string sTelefono, string sCorreoElectronico, string sPassword, string sCPassword,
           IEnumerable<HttpPostedFileBase> pFiles)
        {
            StringBuilder strMensaje = new StringBuilder();
            int id = 0;
            bool success = false;

            SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC ResDesencriptaPass = new SECURITYWCF.SecutityDC();
            USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
            USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
            USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
            USERSECURITYWCF.UsuariosBE Usuario = new USERSECURITYWCF.UsuariosBE();

            reglas.TIPOBUSQUEDA = 1;
            reglas.USUARIO = idUsuario;

            reglas.IDAPP = long.Parse(ResourceApp.IdApp);
            resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            Usuario = resUsuario.DatosUsuario.Usuario;

            if (Usuario.IDUSUARIO.ToString() != "0")
            {
                if (!string.IsNullOrEmpty(sPassword))
                {
                    if (ValidaExpresion(sPassword, sCPassword))
                    {
                        ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(sPassword, 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                        Usuario.PASSWORD = ResDesencriptaPass.Encriptacion.VALOROUT;
                    }
                    else { return Json(new Response { IsSuccess = false, Message = "Los Passwords no coinciden, por favor verifica", Id = 0 }, JsonRequestBehavior.AllowGet); }
                }
                if (pFiles.Count() > 0)
                {
                    foreach (var item in pFiles)
                    {
                        //string path = Server.MapPath("~/Uploads/");
                        string path = ConfigurationManager.AppSettings["Store"].ToString() + @"/Avatar/";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fName = idUsuario;
                        string fExtension = Path.GetExtension(item.FileName);
                        fName += fExtension;
                        item.SaveAs(path + fName);
                        Usuario.RUTAFOTOPERFIL = fName;
                    }
                }
                Usuario.NOMBRE = sNombre;
                Usuario.APATERNO = sPaterno;
                Usuario.AMATERNO = sMaterno;
                Usuario.IDSEXO = IdSexo;
                Usuario.IDESTADOCIVIL = IdEstadoCivil;
                Usuario.FECHANACCONST = FNacimiento;

                resUsuario.DatosUsuario.Domicilios[0].CALLE = sCalle;
                resUsuario.DatosUsuario.Domicilios[0].NUMEXT = sNumExt;
                resUsuario.DatosUsuario.Domicilios[0].NUMINT = sNumInt;
                resUsuario.DatosUsuario.Domicilios[0].CP = sCP;
                resUsuario.DatosUsuario.Domicilios[0].COLONIA = sColonia;
                resUsuario.DatosUsuario.Domicilios[0].MUNICIPIO = sMunicipio;
                resUsuario.DatosUsuario.Domicilios[0].ESTADO = sEstado;

                resUsuario.DatosUsuario.Contactos[0].VALOR = sTelefono;
                resUsuario.DatosUsuario.Contactos[1].VALOR = sCorreoElectronico;


                seguridad.updateUsuario(reglas, Usuario, resUsuario.DatosUsuario.Domicilios, resUsuario.DatosUsuario.Contactos, resUsuario.DatosUsuario.RolesXUsuario, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                success = true;
                id = 1;
                strMensaje.Append("Se actualizo la informacion de manera Correcta.");
            }
            else
            {
                success = false;
                strMensaje.Append("Ocurrio un error!, intenta mas tarde.");
            }







            return Json(new Response { IsSuccess = success, Message = strMensaje.ToString(), Id = id }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Crea la parte de menu padres del sistema asociados al rol
        /// </summary>
        /// <param name="IdRol">Es el rol con el que accede el usuario al sistem</param>
        /// <returns></returns>

        //public ActionResult RecoverPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult RecoverPassword(string sEmail)
        //{
        //    if (fnLogin(sUsuario, sPassword, sRecordarcuenta))
        //    {
        //        return RedirectToAction("Index", "Home");

        //    }
        //    return View();
        //}



        //public ActionResult Register()
        //{

        //}

        public ActionResult Registrarme()
        {
            return View();
        }


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

        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(string CorreoElectronico)
        {
            string strMensaje = string.Empty;
            int id = -1;
            bool suc = false;
            USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
            USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
            USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
            USERSECURITYWCF.UsuariosBE Usuario = new USERSECURITYWCF.UsuariosBE();

            reglas.TIPOBUSQUEDA = 3;
            reglas.USUARIO = CorreoElectronico;

            reglas.IDAPP = long.Parse(ResourceApp.IdApp);
            resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            Usuario = resUsuario.DatosUsuario.Usuario;


            if (Usuario.IDUSUARIO.ToString() == "0")
            {
                suc = false;
                strMensaje = "El correo no se encuentra registrado.";
            }
            else
            {
                HelperTools helper = new HelperTools();

                helper.BuilEmailTemplate(Usuario, "RecuperaPassword", Request);
                suc = true;
                id = 1;
                strMensaje = "Se envío un correo con la información requerida para recuperar su cuenta.";

            }





            return Json(new Response { IsSuccess = suc, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ResetPass(string tkn)
        {
            if (!string.IsNullOrEmpty(tkn))
            {
                string strMensaje = string.Empty;
                int id = -1;
                bool suc = false;
                SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
                SECURITYWCF.SecutityDC ResDesencriptaPass = new SECURITYWCF.SecutityDC();
                ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(tkn, 2, long.Parse(ResourceApp.IdApp), ResourceApp.Password);


                USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
                USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
                USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
                USERSECURITYWCF.UsuariosBE Usuario = new USERSECURITYWCF.UsuariosBE();

                reglas.TIPOBUSQUEDA = 2;
                reglas.USUARIO = ResDesencriptaPass.Encriptacion.VALOROUT;

                reglas.IDAPP = long.Parse(ResourceApp.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                Usuario = resUsuario.DatosUsuario.Usuario;




                if (Usuario.IDUSUARIO.ToString() != "0")
                {
                    ViewBag.tkn = Usuario.IDUSUARIOAPP;
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ResetPass(string Password, string tkn)
        {
            string strMensaje = string.Empty;
            int id = -1;
            bool suc = false;
            SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC ResDesencriptaPass = new SECURITYWCF.SecutityDC();
            ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(tkn, 2, long.Parse(ResourceApp.IdApp), ResourceApp.Password);


            USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
            USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
            USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
            USERSECURITYWCF.UsuariosBE Usuario = new USERSECURITYWCF.UsuariosBE();

            reglas.TIPOBUSQUEDA = 2;
            reglas.USUARIO = tkn;

            reglas.IDAPP = long.Parse(ResourceApp.IdApp);
            resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            Usuario = resUsuario.DatosUsuario.Usuario;

            if (Usuario.IDUSUARIO.ToString() != "0")
            {
                ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(Password, 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
                Usuario.PASSWORD = ResDesencriptaPass.Encriptacion.VALOROUT;
                seguridad.updateUsuario(reglas, Usuario, resUsuario.DatosUsuario.Domicilios, resUsuario.DatosUsuario.Contactos, resUsuario.DatosUsuario.RolesXUsuario, long.Parse(ResourceApp.IdApp), ResourceApp.Password);






                suc = true;
                id = 1;
                strMensaje = "Se actualizó la contraseña correctamente, ya puede entrar al sistema INVENTARIOS.";
            }
            else
            {
                suc = false;
                strMensaje = "El token se encuentra vencido, necesita recuperar nuevamente su cuenta.";
            }







            return Json(new Response { IsSuccess = suc, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }


        public FileContentResult UserPhotos()
        {
            HelperTools oHelp = new HelperTools();
            byte[] imageData = null;
            // string fileName = HttpContext.Server.MapPath(@"/noImg.png");
            string fileName = ConfigurationManager.AppSettings["Store"].ToString() + @"/Avatar/noImg.jpg";

            if (Session["USER_SESSION"] != null)
            {
                USERSECURITYWCF.UsuariosBE itemSecurity = new USERSECURITYWCF.UsuariosBE();
                itemSecurity = (USERSECURITYWCF.UsuariosBE)Session["USER_SESSION"];
                USERSECURITYWCF.UserSecurityServiceClient seguridad = new USERSECURITYWCF.UserSecurityServiceClient();
                USERSECURITYWCF.ReglasBE reglas = new USERSECURITYWCF.ReglasBE();
                USERSECURITYWCF.UsuarioDC resUsuario = new USERSECURITYWCF.UsuarioDC();
                USERSECURITYWCF.UsuarioDC resUsuarioRol = new USERSECURITYWCF.UsuarioDC();
                SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
                SECURITYWCF.SecutityDC oSecurity = new SECURITYWCF.SecutityDC();
                reglas.TIPOBUSQUEDA = 1;
                reglas.USUARIO = itemSecurity.IDUSUARIO.ToString();
                reglas.IDAPP = long.Parse(ResourceApp.IdApp);
                resUsuario = seguridad.getUsuarioFull(reglas, long.Parse(ResourceApp.IdApp), ResourceApp.Password);

                if (resUsuario == null)
                {
                    imageData = oHelp.UrltoByte(fileName);
                    return File(imageData, "image/png");
                }


                imageData = oHelp.UrltoByte(ConfigurationManager.AppSettings["Store"].ToString() + @"/Avatar/" + resUsuario.DatosUsuario.Usuario.RUTAFOTOPERFIL);
                return new FileContentResult(imageData, "image /jpeg");
            }
            else
            {
                imageData = oHelp.UrltoByte(fileName);
                return File(imageData, "image/png");
            }







        }




    }
}