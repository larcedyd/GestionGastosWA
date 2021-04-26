using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using Newtonsoft.Json;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class CierreController:ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var EncCierre = db.EncCierre.Select(a => new {

                    a.idCierre,
                    a.idLogin,
                    NombreUsuario = db.Login.Where(d => d.id == a.idLogin).FirstOrDefault() == null ? "": db.Login.Where(d => d.id == a.idLogin).FirstOrDefault().Nombre,
                    a.Periodo,
                    a.FechaCierre,
                    a.FechaInicial,
                    a.FechaFinal,
                    a.SubTotal,
                    a.Descuento,
                    a.Impuestos,
                    a.Impuesto1,
                    a.Impuesto2,
                    a.Impuesto4,
                    a.Impuesto8,
                    a.Impuesto13,
                    a.Total,
                    a.Estado,
                    a.Observacion,
                    a.idLoginAceptacion,
                    Detalle = db.DetCierre.Where(d => d.idCierre == a.idCierre).Select(s => new {
                        s.id,
                        s.idCierre,
                        s.NumLinea,
                        Factura = db.EncCompras.Where(z => z.id == s.idFactura).FirstOrDefault(),

                    }).ToList()
                    


                }).ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    EncCierre = EncCierre.Where(a => a.Periodo.ToLower().Contains(filtro.Texto.ToLower())).ToList();
                }

                DateTime time = new DateTime();
                if (filtro.FechaInicio != time)
                {
                    filtro.FechaFinal = filtro.FechaFinal.AddDays(1);
                    EncCierre = EncCierre.Where(a => a.FechaCierre >= filtro.FechaInicio && a.FechaCierre <= filtro.FechaFinal).ToList();
                }

                if(!string.IsNullOrEmpty(filtro.Estado) && filtro.Estado != "NULL")
                {
                    EncCierre = EncCierre.Where(a => a.Estado == filtro.Estado).ToList();
                }
                
                if(filtro.Codigo1 > 0)
                {
                    EncCierre = EncCierre.Where(a => a.idLogin == filtro.Codigo1).ToList();
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, EncCierre);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }




        [Route("api/Cierre/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var EncCierre = db.EncCierre.Where(a => a.idCierre == id).FirstOrDefault();
                var DetCierre = db.DetCierre.Where(a => a.idCierre == id).ToList();

                var resp = new
                {
                    EncCierre,
                    DetCierre
                };

              

                if (resp == null)
                {
                    throw new Exception("Esta liquidación no se encuentra registrada");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [Route("api/Cierre/Estado")]
        public HttpResponseMessage GetEstado([FromUri]int id, string Estado, string comentario = "", int idLoginAceptacion = 0)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var EncCierre = db.EncCierre.Where(a => a.idCierre == id).FirstOrDefault();

                db.Entry(EncCierre).State = EntityState.Modified;

                EncCierre.Estado = Estado;
                if(!string.IsNullOrEmpty(comentario))
                {

                    EncCierre.Observacion = comentario;
                }
                EncCierre.idLoginAceptacion = idLoginAceptacion;
               
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage Post([FromBody] CierreViewModel gastos)
        {
               
                G.AbrirConexionAPP(out db);
            var t = db.Database.BeginTransaction();
            try
            {
                var Cierre = new EncCierre();
                Cierre.Periodo = gastos.EncCierre.Periodo;
                Cierre.FechaInicial = gastos.EncCierre.FechaInicial;
                Cierre.FechaFinal = gastos.EncCierre.FechaFinal;
                Cierre.idLogin = gastos.EncCierre.idLogin;
                Cierre.SubTotal = gastos.EncCierre.SubTotal;
                Cierre.Total = gastos.EncCierre.Total;
                Cierre.CantidadRegistros = 0;
                Cierre.Descuento = gastos.EncCierre.Descuento;
                Cierre.FechaCierre = DateTime.Now;
                Cierre.Impuestos = gastos.EncCierre.Impuestos;
                Cierre.Impuesto1 = gastos.EncCierre.Impuesto1;
                Cierre.Impuesto2 = gastos.EncCierre.Impuesto2;
                Cierre.Impuesto4 = gastos.EncCierre.Impuesto4;
                Cierre.Impuesto8 = gastos.EncCierre.Impuesto8;
                Cierre.Impuesto13 = gastos.EncCierre.Impuesto13;
                Cierre.idLoginAceptacion = 0;
                Cierre.Estado = "P";
                Cierre.Observacion = "";

                db.EncCierre.Add(Cierre);
                db.SaveChanges();

                var Facturas = db.EncCompras.ToList();
                var Logins = db.Login.ToList();
                var Normas = db.NormasReparto.ToList();

              
                int i = 1;
                foreach (var item in gastos.DetCierre)
                {
                    DetCierre det = new DetCierre();
                    det.idCierre = Cierre.idCierre;
                    det.NumLinea = i;
                    det.idFactura = item.idFactura;
                    i++;
                    db.DetCierre.Add(det);
                    var Factura = Facturas.Where(a => a.id == item.idFactura).FirstOrDefault();
                    db.Entry(Factura).State = EntityState.Modified;
                    Factura.idLoginAsignado = Cierre.idLogin;
                    Factura.FecAsignado = DateTime.Now;

                    if(Normas.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault() == null)
                    {
                        throw new Exception("Este usuario no contiene una norma de reparto asignada");
                    }

                    Factura.idNormaReparto = Normas.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault().id;
                    Factura.idCierre = det.idCierre;
                    db.SaveChanges();


                }
                db.SaveChanges();

                db.Entry(Cierre).State = EntityState.Modified;
                Cierre.CantidadRegistros = i - 1;
                db.SaveChanges();

               
              

                t.Commit();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                t.Rollback();
                G.CerrarConexionAPP(db);
                G.GuardarTxt("ErrorCierre"+DateTime.Now.Day +""+DateTime.Now.Month + ""+DateTime.Now.Year +".txt", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/Cierre/Actualizar")]
        public HttpResponseMessage Put([FromBody] CierreViewModel gastos)
        {

            G.AbrirConexionAPP(out db);
            var t = db.Database.BeginTransaction();
            try
            {
                if (db.EncCierre.Where(a => a.idCierre == gastos.EncCierre.idCierre).FirstOrDefault() != null)
                {




                    var Cierre = db.EncCierre.Where(a => a.idCierre == gastos.EncCierre.idCierre).FirstOrDefault();
 
                 
                        db.Entry(Cierre).State = EntityState.Modified;
                        Cierre.Periodo = gastos.EncCierre.Periodo;
                        Cierre.FechaInicial = gastos.EncCierre.FechaInicial;
                        Cierre.FechaFinal = gastos.EncCierre.FechaFinal;
                        Cierre.idLogin = gastos.EncCierre.idLogin;
                        Cierre.SubTotal = gastos.EncCierre.SubTotal;
                        Cierre.Total = gastos.EncCierre.Total;
                        Cierre.CantidadRegistros = 0;
                        Cierre.Descuento = gastos.EncCierre.Descuento;
                        Cierre.FechaCierre = DateTime.Now;
                        Cierre.Impuestos = gastos.EncCierre.Impuestos;
                        Cierre.Impuesto1 = gastos.EncCierre.Impuesto1;
                        Cierre.Impuesto2 = gastos.EncCierre.Impuesto2;
                        Cierre.Impuesto4 = gastos.EncCierre.Impuesto4;
                        Cierre.Impuesto8 = gastos.EncCierre.Impuesto8;
                        Cierre.Impuesto13 = gastos.EncCierre.Impuesto13;
                        Cierre.idLoginAceptacion = 0;
                        Cierre.Estado = "P";
                        Cierre.Observacion = Cierre.Observacion;

                        
                        db.SaveChanges();

                        var Facturas = db.EncCompras.ToList();
                        var Logins = db.Login.ToList();
                        var Normas = db.NormasReparto.ToList();


                        var Detalle = db.DetCierre.Where(a => a.idCierre == Cierre.idCierre).ToList();

                        foreach (var item in Detalle)
                        {
                            var Factura = Facturas.Where(a => a.id == item.idFactura).FirstOrDefault();
                            db.Entry(Factura).State = EntityState.Modified;
                            Factura.idLoginAsignado = 0;
                            Factura.FecAsignado = null;

                             

                            Factura.idNormaReparto = 0;
                            Factura.idCierre = 0;
                           
                            db.DetCierre.Remove(item);
                            db.SaveChanges();
                        }




                        int i = 1;
                        foreach (var item in gastos.DetCierre)
                        {
                            DetCierre det = new DetCierre();
                            det.idCierre = Cierre.idCierre;
                            det.NumLinea = i;
                            det.idFactura = item.idFactura;
                            i++;
                            db.DetCierre.Add(det);
                            var Factura = Facturas.Where(a => a.id == item.idFactura).FirstOrDefault();
                            db.Entry(Factura).State = EntityState.Modified;
                            Factura.idLoginAsignado = Cierre.idLogin;
                            Factura.FecAsignado = DateTime.Now;

                            if (Normas.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault() == null)
                            {
                                throw new Exception("Este usuario no contiene una norma de reparto asignada");
                            }

                            Factura.idNormaReparto = Normas.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault().id;
                            Factura.idCierre = det.idCierre;
                            db.SaveChanges();


                        }
                        db.SaveChanges();

                        db.Entry(Cierre).State = EntityState.Modified;
                        Cierre.CantidadRegistros = i - 1;
                        db.SaveChanges();
                

                    




                    t.Commit();
                    G.CerrarConexionAPP(db);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    throw new Exception("Este cierre no existe");
                }

            }
            catch (Exception ex)
            {
                t.Rollback();
                G.CerrarConexionAPP(db);
                G.GuardarTxt("ErrorCierre" + DateTime.Now.Day + "" + DateTime.Now.Month + "" + DateTime.Now.Year + ".txt", ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


    }
}