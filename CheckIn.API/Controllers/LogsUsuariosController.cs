using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CheckIn.API.Controllers
{

    [Authorize]
    public class LogsUsuariosController : ApiController
    {
        ModelCliente db;
        G G = new G();

        public HttpResponseMessage GetAll([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var time = new DateTime(); // La hora inicial 01/01/0001 00:00:00
                if (filtro.FechaFinal != time)
                {
                    filtro.FechaFinal = filtro.FechaFinal.AddDays(1);
                }

                var LogsUsuarios = db.LogsUsuarios.Where(a => (filtro.FechaInicio != time ? a.Fecha >= filtro.FechaInicio : true) && (filtro.FechaFinal != time ? a.Fecha <= filtro.FechaFinal : true)).ToList();

                if (filtro.Codigo1 > 0)
                {
                    LogsUsuarios = LogsUsuarios.Where(a => a.idUsuario == filtro.Codigo1).ToList();
                }
                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    LogsUsuarios = LogsUsuarios.Where(a => a.Tipo == filtro.Texto).ToList();
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, LogsUsuarios);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.Metodo = "Error de GET LogsUsuarios";
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }

        }
        [Route("api/LogsUsuarios/Consultar")]
        public HttpResponseMessage GetOne([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                LogsUsuarios logsUsuarios = db.LogsUsuarios.Where(a => a.id == id).FirstOrDefault();

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, logsUsuarios);
            }
            catch (Exception ex)
            {
                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Fecha = DateTime.Now;
                be.Metodo = "Error de GET One LogsUsuarios";
                db.BitacoraErrores.Add(be);
                db.SaveChanges();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);

            }

        }
    }
}