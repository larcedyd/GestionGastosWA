using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class NormasRepartoController: ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var Norma = db.NormasReparto.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Norma = Norma.Where(a => a.Nombre.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }

                if(filtro.Codigo1 > 0)
                {
                    Norma = Norma.Where(a => a.idLogin == filtro.Codigo1).ToList();
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Norma);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/NormasReparto/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var Norma = db.NormasReparto.Where(a => a.id == id).FirstOrDefault();


                if (Norma == null)
                {
                    throw new Exception("Esta norma no se encuentra registrada");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK,Norma);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/NormasReparto/ConsultarQAD")]
        public async Task<HttpResponseMessage> GetQADAsync()
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var ParametrosQAD = db.ParametrosQAD.FirstOrDefault();
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response2 = await cliente.GetAsync(ParametrosQAD.urlNormasRepartoQAD);


                if (response2.IsSuccessStatusCode)
                {
                    response2.Content.Headers.ContentType.MediaType = "application/json";
                    var resp2 = await response2.Content.ReadAsAsync<NormasRepartoQAD>();

                    foreach (var item in resp2.dssub.ttsub)
                    {
                        var Norma = db.NormasReparto.Where(a => a.CodSAP == item.ts_sub).FirstOrDefault();
                        if (Norma == null)
                        {
                            Norma = new NormasReparto();
                            Norma.idLogin = db.Login.Where(a => a.Nombre.ToLower().Contains(item.ts_description.ToLower())).FirstOrDefault() == null ? 0 : db.Login.Where(a => a.Nombre.ToLower().Contains(item.ts_description.ToLower())).FirstOrDefault().id;
                            Norma.CodSAP = item.ts_sub;
                            Norma.Nombre = item.ts_description;
                            Norma.idDimension = 0;


                            db.NormasReparto.Add(Norma);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Entry(Norma).State = EntityState.Modified;
                            Norma.CodSAP = item.ts_sub;
                            Norma.Nombre = item.ts_description;
                            db.SaveChanges();
                        }

                    }


                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, "Terminado con exito");
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Metodo = "Insertar Norma de Reparto desde QAD";
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] NormasReparto norma)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Norma = db.NormasReparto.Where(a => a.id == norma.id).FirstOrDefault();

                if (Norma == null)
                {
                    Norma = new NormasReparto();
                    Norma.idLogin = norma.idLogin;
                    Norma.CodSAP = norma.CodSAP;
                    Norma.Nombre = norma.Nombre;
                    Norma.idDimension = norma.idDimension;

                    db.NormasReparto.Add(Norma);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Esta norma de reparto YA existe");
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Norma);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Metodo = "Insercion de Norma de Reparto";
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/NormasReparto/Actualizar")]
        public HttpResponseMessage Put([FromBody] NormasReparto norma)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Normas = db.NormasReparto.Where(a => a.id == norma.id).FirstOrDefault();

                if (Normas != null)
                {
                    db.Entry(Normas).State = EntityState.Modified;
                    Normas.CodSAP = norma.CodSAP;
                    Normas.Nombre = norma.Nombre;
                    Normas.idDimension = norma.idDimension;
                    Normas.idLogin = norma.idLogin;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Norma no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Normas);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Metodo = "Actualizar Norma de Reparto";
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/NormasReparto/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Norma = db.NormasReparto.Where(a => a.id == id).FirstOrDefault();

                if (Norma != null)
                {


                    db.NormasReparto.Remove(Norma);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Norma no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Metodo = "Eliminar Norma de Reparto";
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}