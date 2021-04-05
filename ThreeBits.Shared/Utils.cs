using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThreeBits.Entities.Common;

namespace ThreeBits.Shared
{
  public class Utils
    {
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


        public ProcessResult ValidaDatosMiPortal(string sEmail, string sTel, string sPassword, string sCPassword)
        {
            ProcessResult oRes = new ProcessResult();
            if (!ValidaExpresion(sEmail, @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,3})$"))
            {
                oRes.errorMessage = "El correo electronico no es correcto";
                oRes.flag = false;
                return oRes;
            }
            if (!ValidaExpresion(sTel, @"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$"))
            {
                oRes.errorMessage = "El Telefono no es correcto";
                oRes.flag = false;
                return oRes;
            }
            if (!ValidaPassword(sPassword, sCPassword))
            {
                oRes.errorMessage = "Las contraseñas no coinciden";
                oRes.flag = false;
                return oRes;
            }

            //if (ValidaUsuario(sEmail))
            //{
            //    oRes.errorMessage = "El Usuario ya ha sido usado con anterioridad";
            //    oRes.flag = false;
            //    return oRes;
            //}
            return oRes;

        }



        public ProcessResult ValidaDatos(string sEmail, string sPassword, string sCPassword, bool bAplicaSeguridadPassword, bool bConciciones = true)
        {
            ProcessResult oRes = new ProcessResult();
            oRes.flag = true;
            if (!ValidaExpresion(sEmail, @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,3})$"))
            {
                oRes.errorMessage = "El correo electronico no es correcto";
                oRes.flag = false;
            }
            if (!ValidaPassword(sPassword, sCPassword))
            {
                oRes.flag = false;
                oRes.errorMessage = "No coinciden las contraseñas";
            }
            if (bAplicaSeguridadPassword)
            {
                if (!ValidaExpresion(sEmail, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
                {
                    oRes.errorMessage = "El pasword no es seguro Verifica que: </br> " +
                        "La Contraseña contenga al menos una letra mayúscula </br> " +
                        "La Contraseña contenga al menos una letra minúscula </br> " +
                        "La Contraseña contenga al menos un número o caracter especial </br> " +
                        "La Contraseña cuya longitud sea como mínimo 8 caracteres </br> " +
                        "La Contraseña cuya longitud máxima no debe ser arbitrariamente limitada";
                    oRes.flag = false;
                }
            }
            if (bConciciones == false)
            {
                oRes.errorMessage = "Debes Aceptar los terminos y condiciones ";
                oRes.flag = false;
            }
            //if (ValidaSiExisteUsuario(sEmail)) { oRes.errorMessage = "El Usuario ya ha sido usado con anterioridad";
            //    oRes.flag = false;
            //}

            return oRes;
        }

        public ProcessResult ValidaDatosFov(string sRFC, string sEmail, string sPassword, string sCPassword, bool bAplicaSeguridadPassword, bool bConciciones = true)
        {
            ProcessResult oRes = new ProcessResult();
            oRes.flag = true;
            string regexRFC = string.Empty;
            if (!ValidaExpresion(sEmail, @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,3})$"))
            {
                oRes.errorMessage = "El correo electronico no es correcto";
                oRes.flag = false;
            }
            if (sRFC.Length == 12)
            {
                regexRFC = "^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            }
            else
            {
                regexRFC = @"^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))";
            }
            if (!ValidaExpresion(sRFC, regexRFC))
            {
                oRes.errorMessage = "El RFC no es valido";
                oRes.flag = false;
            }
            if (!ValidaPassword(sPassword, sCPassword))
            {
                oRes.flag = false;
                oRes.errorMessage = "No coinciden las contraseñas";
            }
            if (bAplicaSeguridadPassword)
            {
                if (!ValidaExpresion(sEmail, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
                {
                    oRes.errorMessage = "El pasword no es seguro Verifica que: </br> " +
                        "La Contraseña contenga al menos una letra mayúscula </br> " +
                        "La Contraseña contenga al menos una letra minúscula </br> " +
                        "La Contraseña contenga al menos un número o caracter especial </br> " +
                        "La Contraseña cuya longitud sea como mínimo 8 caracteres </br> " +
                        "La Contraseña cuya longitud máxima no debe ser arbitrariamente limitada";
                    oRes.flag = false;
                }
            }
            if (bConciciones == false)
            {
                oRes.errorMessage = "Debes Aceptar los terminos y condiciones ";
                oRes.flag = false;
            }
            //if (ValidaSiExisteUsuario(sEmail)) { oRes.errorMessage = "El Usuario ya ha sido usado con anterioridad";
            //    oRes.flag = false;
            //}

            return oRes;
        }
    }
}
