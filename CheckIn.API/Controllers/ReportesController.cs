using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class ReportesController: ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Liquidaciones = db.EncCierre.Where(a => a.Estado != "R").ToList();

                if(filtro.FechaInicio.Date != new DateTime().Date )
                {
                    filtro.FechaFinal = filtro.FechaFinal.AddDays(1);
                    Liquidaciones = Liquidaciones.Where(a => a.FechaCierre >= filtro.FechaInicio && a.FechaCierre <= filtro.FechaFinal).ToList();
                }

                if(filtro.Codigo1 > 0)
                {
                    Liquidaciones = Liquidaciones.Where(a => a.idLogin == filtro.Codigo1).ToList();
                }

                if(!string.IsNullOrEmpty(filtro.CodMoneda))
                {
                    Liquidaciones = Liquidaciones.Where(a => a.CodMoneda == filtro.CodMoneda).ToList();
                }

                var MontoAcumulado = Liquidaciones.Sum(a => a.Total);

                List<DetCierre> det = new List<DetCierre>();
                List<EncCompras> comp = new List<EncCompras>();

                foreach (var item in Liquidaciones)
                {
                    var detalle = db.DetCierre.Where(a => a.idCierre == item.idCierre).ToList();
                    foreach (var ite in detalle)
                    {
                        var fac = db.EncCompras.Where(a => a.id == ite.idFactura).FirstOrDefault();
                        comp.Add(fac);
                    }

                }


                var compa = comp.Select(a => new {

                    a.id,
                    a.CodEmpresa
                 ,
                    a.CodProveedor,
                    a.NomProveedor
                 ,
                    a.TipoDocumento
                 ,
                    a.NumFactura
                 ,
                    a.FecFactura
                 ,
                    a.TipoIdentificacionCliente
                 ,
                    a.CodCliente
                 ,
                    a.NomCliente
                 ,
                    a.EmailCliente
                 ,
                    a.DiasCredito
                 ,
                    a.CondicionVenta
                 ,
                    a.ClaveHacienda
                 ,
                    a.ConsecutivoHacienda
                 ,
                    a.MedioPago
                 ,
                    a.Situacion
                 ,
                    a.CodMoneda
                 ,
                    a.TotalServGravados
                 ,
                    a.TotalServExentos
                 ,
                    a.TotalMercanciasGravadas
                 ,
                    a.TotalMercanciasExentas
                 ,
                    a.TotalExento
                 ,
                    a.TotalVenta
                 ,
                    a.TotalDescuentos
                 ,
                    a.TotalVentaNeta
                 ,
                    a.TotalImpuesto
                 ,
                    a.TotalComprobante
                 ,

                    a.FechaGravado
                 ,
                    a.TotalServExonerado
                 ,
                    a.TotalMercExonerada
                 ,
                    a.TotalExonerado
                 ,
                    a.TotalIVADevuelto
                 ,
                    a.TotalOtrosCargos
                 ,
                    a.CodigoActividadEconomica
                 ,
                    a.idLoginAsignado
                 ,
                    a.FecAsignado

                 ,

                    a.idNormaReparto
                 ,
                    a.idTipoGasto
                 ,
                 
                    TipoGasto = db.Gastos.Where(d => d.idTipoGasto == a.idTipoGasto).FirstOrDefault().Nombre,
                    a.idCierre,
                    NormaUsuario = db.NormasReparto.Where(d => d.idLogin == a.idLoginAsignado).FirstOrDefault() == null ? a.idNormaReparto : db.NormasReparto.Where(d => d.idLogin == a.idLoginAsignado).FirstOrDefault().id

                }).Where(a => a.idNormaReparto != a.NormaUsuario).ToList();


                decimal Total = 0;

                foreach(var item in compa)
                {
                    Total += ((item.TotalVenta - item.TotalDescuentos) + (item.TotalImpuesto + item.TotalOtrosCargos)).Value;
                }

                MontoAcumulado = MontoAcumulado - Total;
                HeaderReportViewModel he = new HeaderReportViewModel();
                he.MontoAcumulado = MontoAcumulado.Value;
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, he);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        [Route("api/Reportes/Graficos")]
        public async Task<HttpResponseMessage> GetGraficos([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);

               

                var Liquidaciones = db.EncCierre.Where(a => a.Estado != "R").Select( a => new {

                    a.idCierre,
                    a.FechaCierre,
                    a.idLogin,
                    a.CodMoneda
                    

                }).ToList();
                if (filtro != null)
                {

                    if (filtro.FechaInicio.Date != new DateTime().Date)
                    {
                        filtro.FechaFinal = filtro.FechaFinal.AddDays(1);
                        Liquidaciones = Liquidaciones.Where(a => a.FechaCierre >= filtro.FechaInicio && a.FechaCierre <= filtro.FechaFinal).ToList();
                    }

                    if (filtro.Codigo1 > 0)
                    {
                        Liquidaciones = Liquidaciones.Where(a => a.idLogin == filtro.Codigo1).ToList();
                    }


                    if (!string.IsNullOrEmpty(filtro.CodMoneda))
                    {
                        Liquidaciones = Liquidaciones.Where(a => a.CodMoneda == filtro.CodMoneda).ToList();
                    }
                }
                List<DetCierre> det = new List<DetCierre>();
                List<EncCompras> comp = new List<EncCompras>();

                foreach(var item in Liquidaciones)
                {
                    var detalle = db.DetCierre.Where(a => a.idCierre == item.idCierre).ToList();
                    foreach(var ite in detalle)
                    {
                        var fac = db.EncCompras.Where(a => a.id == ite.idFactura).FirstOrDefault();
                        comp.Add(fac);
                    }

                }

                var compa = comp.Select(a => new {

                    a.id,
                    a.CodEmpresa
                 ,
                    a.CodProveedor,
                    a.NomProveedor
                 ,
                    a.TipoDocumento
                 ,
                    a.NumFactura
                 ,
                    a.FecFactura
                 ,
                    a.TipoIdentificacionCliente
                 ,
                    a.CodCliente
                 ,
                    a.NomCliente
                 ,
                    a.EmailCliente
                 ,
                    a.DiasCredito
                 ,
                    a.CondicionVenta
                 ,
                    a.ClaveHacienda
                 ,
                    a.ConsecutivoHacienda
                 ,
                    a.MedioPago
                 ,
                    a.Situacion
                 ,
                    a.CodMoneda
                 ,
                    a.TotalServGravados
                 ,
                    a.TotalServExentos
                 ,
                    a.TotalMercanciasGravadas
                 ,
                    a.TotalMercanciasExentas
                 ,
                    a.TotalExento
                 ,
                    a.TotalVenta
                 ,
                    a.TotalDescuentos
                 ,
                    a.TotalVentaNeta
                 ,
                    a.TotalImpuesto
                 ,
                    a.TotalComprobante
                 ,

                    a.FechaGravado
                 ,
                    a.TotalServExonerado
                 ,
                    a.TotalMercExonerada
                 ,
                    a.TotalExonerado
                 ,
                    a.TotalIVADevuelto
                 ,
                    a.TotalOtrosCargos
                 ,
                    a.CodigoActividadEconomica
                 ,
                    a.idLoginAsignado
                 ,
                    a.FecAsignado

                 ,

                    a.idNormaReparto
                 ,
                    a.idTipoGasto
                 ,
                    TipoGasto = db.Gastos.Where(d => d.idTipoGasto == a.idTipoGasto).FirstOrDefault().Nombre,
                    a.idCierre,
                    NormaUsuario = db.NormasReparto.Where(d => d.idLogin == a.idLoginAsignado).FirstOrDefault() == null ? a.idNormaReparto: db.NormasReparto.Where(d => d.idLogin == a.idLoginAsignado).FirstOrDefault().id

                }).Where(a => a.idNormaReparto == a.NormaUsuario).ToList();

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, compa);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}