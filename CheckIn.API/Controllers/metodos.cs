using CheckIn.API.Models.ModelCliente;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CheckIn.API.Controllers
{
    public class metodos
    {


        public bool SendV2(string para, string copia, string copiaOculta, string de, string displayName, string asunto,
           string html, string HostServer, int Puerto, bool EnableSSL, string UserName, string Password, List<Attachment> ArchivosAdjuntos = null)
        {
            try
            {

                MailMessage mail = new MailMessage();
                mail.Subject = asunto;
                mail.Body = html;
                mail.IsBodyHtml = true;

                // * mail.From = new MailAddress(WebConfigurationManager.AppSettings["UserName"], displayName);
                mail.From = new MailAddress(de, displayName);

                var paraList = para.Split(';');
                foreach (var p in paraList)
                {
                    if (p.Trim().Length > 0)
                        mail.To.Add(p.Trim());
                }
                var ccList = copia.Split(';');
                foreach (var cc in ccList)
                {
                    if (cc.Trim().Length > 0)
                        mail.CC.Add(cc.Trim());
                }
                var ccoList = copiaOculta.Split(';');
                foreach (var cco in ccoList)
                {
                    if (cco.Trim().Length > 0)
                        mail.Bcc.Add(cco.Trim());
                }



                if (ArchivosAdjuntos != null)
                {
                    foreach (var archivo in ArchivosAdjuntos)
                    {
                        //if (!string.IsNullOrEmpty(archivo))
                        mail.Attachments.Add(archivo);
                    }
                }


                SmtpClient client = new SmtpClient();
                client.Host = HostServer; // WebConfigurationManager.AppSettings["HostName"];
                client.Port = Puerto; // int.Parse(WebConfigurationManager.AppSettings["Port"].ToString());
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = EnableSSL; // bool.Parse(WebConfigurationManager.AppSettings["EnableSsl"]);
                client.Credentials = new NetworkCredential(UserName, Password);

                client.Send(mail);
                client.Dispose();
                mail.Dispose();

                return true;

            }
            catch (Exception ex)
            {


                return false;
            }
        }

        public bool EnviarCorreo(string correoElectronicoDestino, string codigo, CorreoEnvio correo)
        {
            try
            {


                var html = "<!DOCTYPE html> <html> <head><meta charset='utf - 8'>";
                html += " <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css' integrity='sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm' crossorigin='anonymous'> ";
                html += " <script src='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js' integrity='sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl' crossorigin='anonymous'></script> ";
                html += " </head><body><div class='row' style='margin-left: 30%; margin-top: 7%;'><div class='col-sm-10'> ";
                html += " <h3> Se ha detectado un inicio de sesión </h3><p> A continuación más información:  </p> ";
                html += " <ul><li>Correo Electrónico: <b>" + correoElectronicoDestino + "</b></li> ";
                html += " <li>Codigo de verificación: <b>" + codigo + " </b></li></ul></div></div></body></html> ";

                var resp = SendV2(correoElectronicoDestino, "larce@dydconsultorescr.com", "", correo.RecepcionEmail, "Codigo de Verificacion Gestión de Gastos", "Codigo de Verificacion", html, correo.RecepcionHostName, correo.EnvioPort, correo.RecepcionUseSSL, correo.RecepcionEmail, correo.RecepcionPassword);

                return resp;

            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}