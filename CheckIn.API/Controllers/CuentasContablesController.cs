﻿using CheckIn.API.Models;
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
    public class CuentasContablesController: ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);
                var Cuentas = db.CuentasContables.ToList();

                if (!string.IsNullOrEmpty(filtro.Texto))
                {
                    Cuentas = Cuentas.Where(a => a.Nombre.ToUpper().Contains(filtro.Texto.ToUpper())).ToList();
                }

                

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Cuentas);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/CuentasContables/Consultar")]
        public HttpResponseMessage GetOne([FromUri]int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);


                var Cuentas = db.CuentasContables.Where(a => a.idCuentaContable == id).FirstOrDefault();


                if (Cuentas == null)
                {
                    throw new Exception("Esta cuenta no se encuentra registrada");
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
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CuentasContables cuenta)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == cuenta.idCuentaContable).FirstOrDefault();

                if (Cuenta == null)
                {
                    cuenta = new CuentasContables();
                    cuenta.Nombre = cuenta.Nombre;
             

                    db.CuentasContables.Add(cuenta);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Esta cuenta YA existe");
                }

                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Cuenta);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("api/CuentasContables/Actualizar")]
        public HttpResponseMessage Put([FromBody] CuentasContables cuenta)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == cuenta.idCuentaContable).FirstOrDefault();

                if (Cuenta != null)
                {
                    db.Entry(Cuenta).State = EntityState.Modified;
                    Cuenta.Nombre = cuenta.Nombre;
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Cuenta no existe");
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Cuenta);
            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("api/CuentasContables/Eliminar")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == id).FirstOrDefault();

                if (Cuenta != null)
                {


                    db.CuentasContables.Remove(Cuenta);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception("Cuenta no existe");
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
}