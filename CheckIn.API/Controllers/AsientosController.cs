using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using Newtonsoft.Json;
using S22.Imap;
using SAPbobsCOM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class AsientosController: ApiController
    {
        ModelCliente db;
        G G = new G();


        public string Get()
        {


            try
            {
                int resp = Conexion.Company.Connect();
                if (resp != 0)
                {
                    return Conexion.Company.GetLastErrorDescription();
                }
                else
                {
                    return resp.ToString();
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }


        [Route("api/Asientos/Insertar")]
        public HttpResponseMessage GetAsientos([FromUri] int idCierre = 0)
        {

            object resp;
            decimal imp1 = 0;
            decimal imp2 = 0;
            decimal imp4 = 0;
            decimal imp8 = 0;
            decimal imp13 = 0;
            try
            {
                G.AbrirConexionAPP(out db);
                var Cierre = db.EncCierre.Where(a => a.idCierre == idCierre).FirstOrDefault();

                if(Cierre.ProcesadaSAP == true)
                {
                    throw new Exception("Esta liquidación ya fue procesada");
                }

                var Detalle = db.DetCierre.Where(a => a.idCierre == Cierre.idCierre).ToList();

                List<EncCompras> enc = new List<EncCompras>();

                foreach(var item in Detalle)
                {
                    var compra = db.EncCompras.Where(a => a.id == item.idFactura).FirstOrDefault();
                    enc.Add(compra);
                }

                var login = db.Login.Where(a => a.id == Cierre.idLogin).FirstOrDefault();
                var param = db.Parametros.FirstOrDefault();


                var oInvoice = (Documents)Conexion.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                


                oInvoice.DocObjectCode = BoObjectTypes.oPurchaseInvoices;

                oInvoice.CardCode = login.CardCode; //CardCode que viene de login
                oInvoice.DocDate = Cierre.FechaInicial; //Inicio del periodo de cierre
                oInvoice.DocDueDate = Cierre.FechaFinal; //Final del periodo de cierre
                oInvoice.DocCurrency = (Cierre.CodMoneda == "CRC" ? "COL": Cierre.CodMoneda); //Moneda de la liquidacion
                oInvoice.DocType = BoDocumentTypes.dDocument_Service;
                oInvoice.NumAtCard = "Liquidación: " + idCierre.ToString();
                var i = 0;
                foreach(var item in enc)
                {

                    var TipoGasto = db.Gastos.Where(a => a.idTipoGasto == item.idTipoGasto).FirstOrDefault();
                    var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == TipoGasto.idCuentaContable).FirstOrDefault();
                    var Norma = db.NormasReparto.Where(a => a.id == item.idNormaReparto).FirstOrDefault();
                    var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();

                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = item.CodProveedor +"-"+ item.NomProveedor ;//"3102751358 - D y D Consultores"; // Factura -> Cedula 
                    oInvoice.Lines.AccountCode = Cuenta.CodSAP; //"6-01-02-05-000"; //Cuenta contable del gasto
                    //if(item.TotalImpuesto > 0)
                    //{

                    //    oInvoice.Lines.TaxCode = "IVA"; //EX-IVA -> Factura 

                    //}
                    //else
                    //{
                        oInvoice.Lines.TaxCode = "EX";
                    //}

                    imp1 += item.Impuesto1;
                    imp2 += item.Impuesto2;
                    imp4 += item.Impuesto4;
                    imp8 += item.Impuesto8;
                    imp13 += item.Impuesto13;

                    //Normas de reparto
                    switch (Dimension.codigoSAP)
                    {
                        case "1":
                            {
                                oInvoice.Lines.CostingCode = Norma.CodSAP;
                                oInvoice.Lines.CostingCode2 = "";
                                oInvoice.Lines.CostingCode3 = "";
                                oInvoice.Lines.CostingCode4 = "";
                                oInvoice.Lines.CostingCode5 = "";
                                break;
                            }
                        case "2":
                            {
                                oInvoice.Lines.CostingCode = "";
                                oInvoice.Lines.CostingCode2 = Norma.CodSAP;
                                oInvoice.Lines.CostingCode3 = "";
                                oInvoice.Lines.CostingCode4 = "";
                                oInvoice.Lines.CostingCode5 = "";
                                break;
                            }
                        case "3":
                            {
                                oInvoice.Lines.CostingCode = "";
                                oInvoice.Lines.CostingCode2 = "";
                                oInvoice.Lines.CostingCode3 = Norma.CodSAP;
                                oInvoice.Lines.CostingCode4 = "";
                                oInvoice.Lines.CostingCode5 = "";
                                break;
                            }
                        case "4":
                            {
                                oInvoice.Lines.CostingCode = "";
                                oInvoice.Lines.CostingCode2 = "";
                                oInvoice.Lines.CostingCode3 = "";
                                oInvoice.Lines.CostingCode4 = Norma.CodSAP;
                                oInvoice.Lines.CostingCode5 = "";
                                break;
                            }
                        case "5":
                            {
                                oInvoice.Lines.CostingCode = "";
                                oInvoice.Lines.CostingCode2 = "";
                                oInvoice.Lines.CostingCode3 = "";
                                oInvoice.Lines.CostingCode4 = "";
                                oInvoice.Lines.CostingCode5 = Norma.CodSAP;
                                break;
                            }
                        default:

                            {
                                oInvoice.Lines.CostingCode = Norma.CodSAP;
                                oInvoice.Lines.CostingCode2 = "";
                                oInvoice.Lines.CostingCode3 = "";
                                oInvoice.Lines.CostingCode4 = "";
                                oInvoice.Lines.CostingCode5 ="";
                                break;
                            }
                    }

                    oInvoice.Lines.LineTotal = Convert.ToDouble(item.TotalComprobante.Value - item.TotalImpuesto);

                    if(TipoGasto.Nombre.ToUpper().Contains("Combustible".ToUpper()))
                    {
                        var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                        oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = DetalleFac.Cantidad;
                        oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Super".ToUpper()) ? "Gasolina Super" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Regular".ToUpper()) ? "Gasolina Regular" : "Diesel");
                    }

                    oInvoice.Lines.Add();
                    //
                    //Si es de combustible la factura
                    //   oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = 100;
                    //  oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = "Gasolina Super";
                    //
                    i++;
                }


                if(imp1 > 0)
                {
                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = "Impuesto 1";
                    oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                    oInvoice.Lines.TaxCode = "EX";
                    oInvoice.Lines.AccountCode = param.CI1;

                    oInvoice.Lines.Add();
                    i++;
                }
              
                if(imp2 > 0)
                {
                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = "Impuesto 2";
                    oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                    oInvoice.Lines.TaxCode = "EX";
                    oInvoice.Lines.AccountCode = param.CI2; 
                    oInvoice.Lines.Add();
                    i++;
                }

               if(imp4 > 0)
                {
                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = "Impuesto 4";
                    oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                    oInvoice.Lines.TaxCode = "EX";
                    oInvoice.Lines.AccountCode = param.CI4 ;
                    oInvoice.Lines.Add();
                    i++;
                }

                if (imp8 > 0)
                {
                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = "Impuesto 8";
                    oInvoice.Lines.LineTotal = Convert.ToDouble(imp8);
                    oInvoice.Lines.TaxCode = "EX";
                    oInvoice.Lines.AccountCode = param.CI8;
                    oInvoice.Lines.Add();
                    i++;
                }

                if (imp13 > 0)
                {
                    oInvoice.Lines.SetCurrentLine(i);
                    oInvoice.Lines.ItemDescription = "Impuesto 13";
                    oInvoice.Lines.LineTotal = Convert.ToDouble(imp13);
                    oInvoice.Lines.TaxCode = "EX";
                    oInvoice.Lines.AccountCode = param.CI13;
                    oInvoice.Lines.Add();
                    i++;
                }

             

              

               



                var respuesta = oInvoice.Add();

                if (respuesta == 0)
                {
                    var docEntry = Conexion.Company.GetNewObjectKey();

                    db.Entry(Cierre).State = EntityState.Modified;
                    Cierre.ProcesadaSAP = true;
                    db.SaveChanges();
                    resp = new 
                    {
                        
                        DocEntry = docEntry,
                        //  Series = pedido.Series.ToString(),
                        Type = "oPurchaiseInvoice",
                        Status = 1,
                        Message = "Factura creada exitosamente",
                        User = Conexion.Company.UserName
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }

                resp = new
                {
                    //   Series = pedido.Series.ToString(),
                    DocEntry = 0,
                    Type = "oPurchaiseInvoice",
                    Status = 0,
                    Message = Conexion.Company.GetLastErrorDescription(),
                    User = Conexion.Company.UserName
                };

                




                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                resp = new
                {
                    DocEntry = 0,
                    Type = "oPurchaiseInvoice",
                    Status = 0,
                    Message = "[Stack] -> " + ex.StackTrace + " -- [Message] --> " + ex.Message,
                    User = Conexion.Company.UserName
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError,resp);
            }


        }

        public static string QuitarTilde(string inputString)
        {
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

    }
}