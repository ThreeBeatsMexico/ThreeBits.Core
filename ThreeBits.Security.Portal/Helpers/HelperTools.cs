using Mvc.Mailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web;
using ThreeBits.Security.Portal.Properties;
using ThreeBits.Security.Portal.COMMONWCF;
using System.IO;
using System.Configuration;

namespace ThreeBits.Security.Portal.Helpers
{
    public class HelperTools
    {

        public void BuilEmailTemplate(USERSECURITYWCF.UsuariosBE usuario, string sTemplate, HttpRequestBase request)
        {

            // string sAppName = WebConfigurationManager.AppSettings["AppName"].ToString();
            string sEmpleado = usuario.NOMBRE + " " + usuario.APATERNO;
            string sUsuario = usuario.USUARIO;
            string sPassword = usuario.PASSWORD;

            SECURITYWCF.SecurityServiceClient SeguridadLatino = new SECURITYWCF.SecurityServiceClient();
            SECURITYWCF.SecutityDC ResDesencriptaPass = new SECURITYWCF.SecutityDC();
            ResDesencriptaPass = SeguridadLatino.encryptDesEncrypt(usuario.IDUSUARIOAPP, 1, long.Parse(ResourceApp.IdApp), ResourceApp.Password);
            var baseAddress = UrlOriginal(request) + "/" + ConfigurationManager.AppSettings["AppName"].ToString() + "/Account/ResetPass/?tkn=" + ResDesencriptaPass.Encriptacion.VALOROUT;

            string body = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/EmailTemplate/" + sTemplate + ".html"));
            body = body.Replace("[#USUARIO]", sEmpleado).Replace("[#URLBASE]", baseAddress);
            BuilEmailTemplate("Three Bits School - " + sTemplate, body, usuario.USUARIO);
        }

        public static void BuilEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "jcesarmzamudio@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "julio.martinez@threebits.com.mx";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MvcMailMessage mail = new MvcMailMessage();
            mail.From = new System.Net.Mail.MailAddress(from);
            mail.To.Add(new System.Net.Mail.MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new System.Net.Mail.MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.Bcc.Add(new System.Net.Mail.MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MvcMailMessage mail)
        {
            string sSmtp = WebConfigurationManager.AppSettings["sSmtp"].ToString();
            int sPort = Convert.ToInt32(WebConfigurationManager.AppSettings["sPort"]);
            bool sSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["sSsl"]);
            string sUsrMail = WebConfigurationManager.AppSettings["sUsrMail"].ToString();
            string sPwdMail = WebConfigurationManager.AppSettings["sPwdMail"].ToString();
            SmtpClient client = new SmtpClient();
            client.Host = sSmtp;
            client.Port = sPort;
            client.EnableSsl = sSsl;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(sUsrMail, sPwdMail);
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string UrlOriginal(HttpRequestBase request)
        {
            string hostHeader = request.Headers["host"];

            return string.Format("{0}://{1}",
               request.Url.Scheme,
               hostHeader);
        }

        public List<CatalogosBE> SetDdlCatalogos(string sIdCatalogo, string sValorFiltro = "")
        {
            CommonServiceClient oCommonServiceClient = new CommonServiceClient();
            RespuestaComunBE Respuesta = new RespuestaComunBE();
            Respuesta = oCommonServiceClient.GetCatEspecifico(sIdCatalogo, sValorFiltro);

            List<CatalogosBE> lstDdl = new List<CatalogosBE>();
            CatalogosBE item = new CatalogosBE();

            item.ID = "0";
            item.DESCRIPCION = "Seleccione Opción..";

            lstDdl.Add(item);
            lstDdl.AddRange(Respuesta.lstCatalogo);
            return lstDdl;
            //ddl.DataSource = lstDdl;
            //ddl.DataValueField = "ID";
            //ddl.DataTextField = "DESCRIPCION";

            //ddl.DataBind();
        }
        public byte[] UrltoByte(string fileName)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }



    }
}