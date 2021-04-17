using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
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
    public class ComprasController: ApiController
    {
        ModelCliente db;
        G G = new G();
        [Route("api/Compras/RealizarLecturaEmail")]
       
        public async Task<HttpResponseMessage> GetRealizarLecturaEmailsAsync()
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Parametros = db.Parametros.FirstOrDefault();

                using (ImapClient client = new ImapClient(Parametros.RecepcionHostName, (int)(Parametros.RecepcionPort),
                           Parametros.RecepcionEmail, Parametros.RecepcionPassword, AuthMethod.Login, (bool)(Parametros.RecepcionUseSSL)))
                {
                    IEnumerable<uint> uids = client.Search(SearchCondition.Unseen());

                    DateTime recepcionUltimaLecturaImap = DateTime.Now;
                    if (Parametros.RecepcionUltimaLecturaImap != null)
                        recepcionUltimaLecturaImap = Parametros.RecepcionUltimaLecturaImap.Value;

                    uids.Concat(client.Search(SearchCondition.SentSince(recepcionUltimaLecturaImap)));

                    foreach(var uid in uids)
                    {
                        System.Net.Mail.MailMessage message = client.GetMessage(uid);

                        if (message.Attachments.Count > 0)
                        {
                            try
                            {
                                foreach (var attachment in message.Attachments)
                                {
                                    try
                                    {
                                        System.IO.StreamReader sr = new System.IO.StreamReader(attachment.ContentStream);
                                        string texto = sr.ReadToEnd();

                                        if (texto.Substring(0, 3) == "???")
                                            texto = texto.Substring(3);

                                        if (texto.Contains("FacturaElectronica")
                                                && !texto.Contains("TiqueteElectronico")
                                                && !texto.Contains("NotaCreditoElectronica")
                                                && !texto.Contains("NotaDebitoElectronica"))
                                        {
                                            var emailByteArray = G.Zip(texto);

                                            decimal id = db.Database.SqlQuery<decimal>("Insert Into BandejaEntrada(XmlFactura, Procesado, Asunto, Remitente) " +
                                                    " VALUES (@EmailJson, 0, @Asunto, @Remitente); SELECT SCOPE_IDENTITY(); ",
                                                    new SqlParameter("@EmailJson", emailByteArray),
                                                    new SqlParameter("@Asunto", message.Subject),
                                                    new SqlParameter("@Remitente", message.From.ToString())).First();

                                            try
                                            {

                                                var datos = G.ObtenerDatosXmlRechazado(texto);

                                                db.Database.ExecuteSqlCommand("Update BandejaEntrada set NumeroConsecutivo=@NumeroConsecutivo, " +
                                                    " TipoDocumento = @TipoDocumento, FechaEmision = @FechaEmision , " +
                                                    " NombreEmisor = @NombreEmisor,IdEmisor = @IdEmisor ,CodigoMoneda = @CodigoMoneda , " +
                                                    " TotalComprobante = @TotalComprobante " +
                                                    " WHERE Id=@Id ",
                                                     new SqlParameter("@NumeroConsecutivo", datos.NumeroConsecutivo),
                                                     new SqlParameter("@TipoDocumento", datos.TipoDocumento),
                                                     new SqlParameter("@FechaEmision", datos.FechaEmision),
                                                     new SqlParameter("@NombreEmisor", datos.NombreEmisor),
                                                     new SqlParameter("@IdEmisor", datos.Numero),
                                                     new SqlParameter("@CodigoMoneda", datos.CodigoMoneda),
                                                     new SqlParameter("@TotalComprobante", datos.TotalComprobante),
                                                     new SqlParameter("@Id", id));
                                            }
                                            catch { }
                                        }

                                    }
                                    catch (Exception ex)
                                    {

                                         
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                
                            }
                        }
                        message.Dispose();

                        await System.Threading.Tasks.Task.Delay(100);
                    }

                    Parametros.RecepcionUltimaLecturaImap = DateTime.Now;
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


        [Route("api/Compras/LeerBandejaEntrada")]
        public async Task<HttpResponseMessage> GetLeerBandejaEntradaAsync()
        {
            try
            {
                G.AbrirConexionAPP(out db);

                var Lista = db.BandejaEntrada.Where(a => a.Procesado == "0").ToList();

                foreach (var item in Lista)
                {
                    try
                    {
                        var attachmentBody = G.Unzip(item.XmlFactura);
                        EncCompras factura = new EncCompras();
                        string xmlBase64 = attachmentBody;



                        var xml = G.ConvertirArchivoaXElement(xmlBase64,G.ObtenerCedulaJuridia());

                        if (!xmlBase64.Contains("FacturaElectronica")
                            && !xmlBase64.Contains("TiqueteElectronico")
                            && !xmlBase64.Contains("NotaCreditoElectronica")
                            && !xmlBase64.Contains("NotaDebitoElectronica"))
                            throw new Exception("No es un documento electrónico");

                        factura.ClaveHacienda = G.ExtraerValorDeNodoXml(xml, "Clave");
                        factura.ConsecutivoHacienda = G.ExtraerValorDeNodoXml(xml, "NumeroConsecutivo");
                        factura.FecFactura = DateTime.Parse(G.ExtraerValorDeNodoXml(xml, "FechaEmision"));

                        factura.FechaGravado = DateTime.Now;
                        factura.CodEmpresa = G.ObtenerCedulaJuridia();

                        try
                        {
                            factura.NumFactura = int.Parse(factura.ConsecutivoHacienda.Substring(10, 10));
                        }
                        catch (Exception ex)
                        {
                            factura.NumFactura = int.Parse(factura.ConsecutivoHacienda.Substring(11, 9));
                            // throw new Exception(ex.Message);
                            //factura.NumFactura = (int)Convert.ToInt64(factura.ConsecutivoHacienda.Substring(10, 10));
                        }

                        factura.TipoDocumento = factura.ConsecutivoHacienda.Substring(8, 2);
                        if (factura.TipoDocumento == "04")
                            throw new Exception($"El documento es un Tiquete Electrónico, que no puede ser utilizado como gasto deducible. Debe solicitar al proveedor que genere una Factura Electrónica.");


                        //Informacion del Proveedor o emisor de la factura
                 //       factura.TipoIdentificacionProveedor = G.ExtraerValorDeNodoXml(xml, "Emisor/Identificacion/Tipo");
                        factura.CodProveedor = G.ExtraerValorDeNodoXml(xml, "Emisor/Identificacion/Numero");
                        //   factura.NomProveedor = G.ExtraerValorDeNodoXml(xml, "Emisor/Nombre");
                        // si el nombre se pasa de 80 caracteres debemos cortarlo

                        if (db.EncCompras.Where(m => m.CodEmpresa == factura.CodEmpresa
                   && m.CodProveedor == factura.CodProveedor
                   && m.NumFactura == factura.NumFactura
                   && m.TipoDocumento == factura.TipoDocumento).Count() > 0)
                        {
                            throw new Exception($"El documento ya existe [Clave={factura.ClaveHacienda}] [Consecutivo={factura.ConsecutivoHacienda}]");
                        }

                        //Información del Cliente o Receptor de la factura
                        factura.TipoIdentificacionCliente = G.ExtraerValorDeNodoXml(xml, "Receptor/Identificacion/Tipo");
                        factura.CodCliente = G.ExtraerValorDeNodoXml(xml, "Receptor/Identificacion/Numero");
                        factura.NomCliente = G.ExtraerValorDeNodoXml(xml, "Receptor/Nombre");
                        if (factura.NomCliente.Length > 50)
                            factura.NomCliente = factura.NomCliente.Substring(0, 50);
                        factura.EmailCliente = G.ExtraerValorDeNodoXml(xml, "Receptor/CorreoElectronico");

                        factura.CondicionVenta = G.ExtraerValorDeNodoXml(xml, "CondicionVenta");

                        try
                        {
                            factura.DiasCredito = int.Parse(G.ExtraerValorDeNodoXml(xml, "PlazoCredito", true));
                        }
                        catch
                        {
                            factura.DiasCredito = 0;
                        }

                        factura.MedioPago = G.ExtraerValorDeNodoXml(xml, "MedioPago");
                        if (attachmentBody.Contains("xml-schemas/v4.3"))
                        {
                            factura.CodMoneda = G.ExtraerValorDeNodoXml(xml, "ResumenFactura/CodigoTipoMoneda/CodigoMoneda");
                          //  factura.TipoCA = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/CodigoTipoMoneda/TipoCambio", true));
                        }
                        else
                        {
                            factura.CodMoneda = G.ExtraerValorDeNodoXml(xml, "ResumenFactura/CodigoMoneda");
                            //factura.TipoCambio = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TipoCambio", true));
                        }

                        if (string.IsNullOrWhiteSpace(factura.CodMoneda))
                        {
                            factura.CodMoneda = "CRC";
                            //factura.TipoCambio = 1;
                        }

                        factura.TotalServGravados = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalServGravados", true));
                        factura.TotalServExentos = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalServExentos", true));
                        factura.TotalMercanciasGravadas = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalMercanciasGravadas", true));
                        factura.TotalMercanciasExentas = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalMercanciasExentas", true));
                        //factura.TotalGravado = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalGravado", true));
                        factura.TotalServExonerado = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalServExonerado", true)); 
                        factura.TotalMercExonerada = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalMercExonerada", true)); 
                        factura.TotalExonerado = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalExonerado", true));
                        factura.TotalIVADevuelto = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalIVADevuelto", true));
                       

                        factura.TotalExento = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalExento", true));
                        factura.TotalVenta = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalVenta", true));
                        factura.TotalDescuentos = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalDescuentos", true));
                        factura.TotalVentaNeta = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalVentaNeta", true));
                        factura.TotalImpuesto = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalImpuesto", true));
                        factura.TotalComprobante = decimal.Parse(G.ExtraerValorDeNodoXml(xml, "ResumenFactura/TotalComprobante", true));
                        factura.XmlFacturaRecibida = G.StringToBase64(xmlBase64);

                        foreach (var item2 in xml.Elements().Where(m => m.Name.LocalName == "DetalleServicio").Elements())
                        {
                           var det = new DetCompras();
                            det.CodEmpresa = factura.CodEmpresa;
                            det.NumFactura = factura.NumFactura;
                            det.CodProveedor = factura.CodProveedor;
                            det.TipoDocumento = factura.TipoDocumento;

                            det.NumLinea = short.Parse(G.ExtraerValorDeNodoXml(item2, "NumeroLinea"));

                            if (attachmentBody.Contains("xml-schemas/v4.3"))
                            {
                                det.CodPro = G.ExtraerValorDeNodoXml(item2, "CodigoComercial/Codigo");
                                if (det.CodPro.Length > 20)
                                    det.CodPro = det.CodPro.Substring(0, 20);
                            }
                            else
                            {
                                det.CodPro = G.ExtraerValorDeNodoXml(item2, "Codigo/Codigo");
                                if (det.CodPro.Length > 20)
                                    det.CodPro = det.CodPro.Substring(0, 20);
                            }

                            det.NomPro = G.ExtraerValorDeNodoXml(item2, "Detalle");
                            if (det.NomPro.Length > 60)
                                det.NomPro = det.NomPro.Substring(0, 60);


                            det.UnidadMedida = G.ExtraerValorDeNodoXml(item2, "UnidadMedida");
                            var Decimal = Convert.ToDecimal(G.ExtraerValorDeNodoXml(item2, "Cantidad", true));
                            det.Cantidad = Convert.ToInt32(Decimal);
                            det.PrecioUnitario = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "PrecioUnitario", true));
                            det.MontoTotal = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "MontoTotal", true));
                            //det.NaturalezaDescuento = G.ExtraerValorDeNodoXml(item2.Elements().Where(a => a.Name.LocalName == "Descuento").FirstOrDefault(), "NaturalezaDescuento");
                            det.MontoDescuento = decimal.Parse(G.ExtraerValorDeNodoXml(item2.Elements().Where(a => a.Name.LocalName == "Descuento").FirstOrDefault(), "MontoDescuento", true));
                            det.SubTotal = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "SubTotal", true));

                            //Impuesto
                           // det.ImpuestoCodigo = G.ExtraerValorDeNodoXml(item2, "Impuesto/Codigo");
                            det.ImpuestoTarifa = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "Impuesto/Tarifa", true));
                            det.ImpuestoMonto = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "Impuesto/Monto", true));

                            ////exoneracion
                            //det.ExoneracionTipoDocumento = G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/TipoDocumento");
                            //if (!string.IsNullOrEmpty(det.ExoneracionTipoDocumento))
                            //{
                            //    det.ExoneracionNumeroDocumento = G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/NumeroDocumento");
                            //    det.ExoneracionNombreInstitucion = G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/NombreInstitucion");
                            //    det.ExoneracionFechaEmision = DateTime.Parse(G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/FechaEmision"));
                            //    det.ExoneracionMontoImpuesto = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/MontoImpuesto", true));
                            //    det.ExoneracionPorcentajeCompra = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "Impuesto/Exoneracion/PorcentajeCompra", true));
                            //}

                            
                            det.MontoTotalLinea = decimal.Parse(G.ExtraerValorDeNodoXml(item2, "MontoTotalLinea", true));

                            db.DetCompras.Add(det);

                        }



                        db.EncCompras.Add(factura);
                        db.Database.ExecuteSqlCommand("Update BandejaEntrada SET Procesado=1 WHERE Id=@Id",
                           new SqlParameter("@Id", item.Id));
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            string procesado = "0";
                            //if (ex.Message.ToUpper().Contains("XML NO ES EMITIDO") || ex.Message.ToUpper().Contains("EL DOCUMENTO YA EXISTE"))
                      //      procesado = "1";
                            db.Database.ExecuteSqlCommand("Update BandejaEntrada SET Mensaje=@Mensaje, Procesado=@Procesado WHERE Id=@Id",
                                 new SqlParameter("@Mensaje", ex.Message),
                                 new SqlParameter("@Procesado", procesado),
                                 new SqlParameter("@Id", item.Id));


                            db.SaveChanges();


                        }
                        catch
                        {
                        }

                    }

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