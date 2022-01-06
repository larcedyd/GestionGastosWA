using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using PdfSharp;
using PdfSharp.Pdf;
using S22.Imap;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class ProveedoresController : ApiController
    {
        ModelCliente db;
        G G = new G();

        public async Task<HttpResponseMessage> Get([FromUri] Filtros filtro)
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Proveedores = db.Proveedores.ToList();

                if(!string.IsNullOrEmpty(filtro.Texto))
                {

                 Proveedores = Proveedores.Where(a => a.RUC.Replace("-", "").Replace("-", "") == filtro.Texto.Replace("-", "").Replace("-", "")).ToList();
                }
                
                if(!string.IsNullOrEmpty(filtro.Texto2))
                {
                    Proveedores = Proveedores.Where(a =>  a.DV == filtro.Texto2).ToList();

                }

                if(!string.IsNullOrEmpty(filtro.Texto3))
                {
                    Proveedores = Proveedores.Where(a => a.Nombre.ToUpper().Contains(filtro.Texto3.ToUpper())).ToList();
                }
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, Proveedores);

            }
            catch (Exception ex)
            {
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}