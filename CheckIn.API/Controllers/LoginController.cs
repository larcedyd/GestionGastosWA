﻿using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using CheckIn.API.Models.ModelMain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Login = CheckIn.API.Models.ModelCliente.Login;

namespace CheckIn.API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController: ApiController
    {
        ModelLicencias dbLogin = new ModelLicencias();
        ModelCliente db;
        G G = new G();

        [Route("api/Login/Conectar")]
        public async Task<HttpResponseMessage> GetLoginAsync([FromUri] string email, string clave)
        {
            try
            {
                var LicenciaUsuarios = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(email.ToUpper())).FirstOrDefault();
                var Licencia = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == LicenciaUsuarios.CedulaJuridica).FirstOrDefault();

                if(Licencia == null)
                {
                    throw new Exception("Empresa no existe");
                }

                if(!Licencia.Activo.Value)
                {
                    throw new Exception("Empresa no se encuentra activa");
                }
                
                if(!LicenciaUsuarios.Activo)
                {
                    throw new Exception("Este usuario no esta activo");
                }

               if(! BCrypt.Net.BCrypt.Verify(clave, LicenciaUsuarios.Clave))
                {
                    throw new Exception("Clave o Usuario incorrectos");
                }


                var BD = Licencia.CadenaConexionBD;

                db = new ModelCliente(BD);
                var token = TokenGenerator.GenerateTokenJwt(Licencia.NombreEmpresa, BD);

                DevolucionLogin de = new DevolucionLogin();
                de.NombreUsuario = LicenciaUsuarios.Nombre;
                de.Email = LicenciaUsuarios.Email;
                de.CedulaJuridica = LicenciaUsuarios.CedulaJuridica;
                de.FechaVencimiento = Licencia.FechaVencimiento.Value;
                de.token = token;


                return Request.CreateResponse(HttpStatusCode.OK, de);

            }
            catch (Exception ex)
            {


                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] LoginViewModel usuario)
        {
            try
            {
                var Empresa = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == usuario.CedulaJuridica).FirstOrDefault();
                if(Empresa == null)
                {
                    throw new Exception("Empresa no existe");
                }

                db = new ModelCliente(Empresa.CadenaConexionBD);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            var t = db.Database.BeginTransaction();
            var d = dbLogin.Database.BeginTransaction();
            try
            {

                var User = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(usuario.Email.ToUpper()) && a.Activo == true).FirstOrDefault();

                if (User == null)
                {
                    User = new LicUsuarios();
                    User.Nombre = usuario.Nombre;
                    User.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                    User.Email = usuario.Email;
                    User.Activo = true;
                    User.CedulaJuridica = usuario.CedulaJuridica;

                    Login login = new Login();
                    login.Nombre = User.Nombre;
                    login.Clave = User.Clave;
                    login.Activo = true;
                    login.idRol = usuario.idRol;
                    login.Email = User.Email;

                    db.Login.Add(login);

                    dbLogin.LicUsuarios.Add(User);
                    dbLogin.SaveChanges();
                    db.SaveChanges();

                    d.Commit();
                    t.Commit();
                }
                else
                {
                    throw new Exception("Este usuario YA existe");
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                t.Rollback();
                d.Rollback();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        //Actualiza la contraseña del usuario 
        [HttpPut]
        [Route("api/Login/Actualizar")]
        public HttpResponseMessage Put([FromBody] LoginViewModel usuario)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Usuario = dbLogin.LicUsuarios.Where(a => a.Email.ToUpper().Contains(usuario.Email.ToUpper()) && a.Activo == true).FirstOrDefault();
                var User = db.Login.Where(a => a.Email.ToUpper().Contains(usuario.Email.ToUpper())).FirstOrDefault();

                if (Usuario != null && User != null)
                {
                    dbLogin.Entry(Usuario).State = EntityState.Modified;
                    db.Entry(User).State = EntityState.Modified;

                    if (!string.IsNullOrEmpty(usuario.Clave))
                    {

                        Usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                        User.Clave = Usuario.Clave;
                    }
       
                    if (!string.IsNullOrEmpty(usuario.Nombre))
                    {
                        Usuario.Nombre = usuario.Nombre;
                        User.Nombre = Usuario.Nombre;
                    }

                    if (!string.IsNullOrEmpty(usuario.Email))
                    {
                        Usuario.Email = usuario.Email;
                        User.Email = Usuario.Email;
                    }

                    if (usuario.idRol > 0)
                    {
                        User.idRol = usuario.idRol;
                    }

                    dbLogin.SaveChanges();
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Usuario no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/Login/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Usuario = dbLogin.LicUsuarios.Where(a => a.idLogin == id).FirstOrDefault();
                var User = db.Login.Where(a => a.Email.ToUpper().Contains(Usuario.Email.ToUpper())).FirstOrDefault();


                if (Usuario != null && User != null)
                {

                    db.Entry(User).State = EntityState.Modified;
                    dbLogin.Entry(Usuario).State = EntityState.Modified;

                    if(Usuario.Activo)
                    {
                        Usuario.Activo = false;
                        User.Activo = Usuario.Activo;

                    }
                    else
                    {
                        Usuario.Activo = true;
                        User.Activo = Usuario.Activo;
                    }



                    dbLogin.SaveChanges();
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Usuario no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }

    internal class DevolucionLogin
    {
        public DevolucionLogin()
        {
        }

        public string NombreUsuario { get; set; }
        public string CedulaJuridica { get; set; }
        public string Email { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string token { get; set; }

    }
}