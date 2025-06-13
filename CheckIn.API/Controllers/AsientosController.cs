using CheckIn.API.Models;
using CheckIn.API.Models.ModelCliente;
using CheckIn.API.Models.ModelMain;
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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace CheckIn.API.Controllers
{
    [Authorize]
    public class AsientosController : ApiController
    {
        ModelLicencias dbLogin = new ModelLicencias();
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
        public async Task<HttpResponseMessage> GetAsientosAsync([FromUri] int idCierre = 0)
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
                G.GuardarTxt("ErrorSAP.txt", "Entro");

                var Compañia = G.ObtenerCedulaJuridia();

                var Licencia = dbLogin.LicEmpresas.Where(a => a.CedulaJuridica == Compañia).FirstOrDefault();
                
                var Pais = Licencia.CadenaConexionSAP;

                if (Cierre.ProcesadaSAP == true)
                {
                    throw new Exception("Esta liquidación ya fue procesada");
                }

                var Detalle = db.DetCierre.Where(a => a.idCierre == Cierre.idCierre).ToList();

                List<EncCompras> enc = new List<EncCompras>();
                var Encabezados = db.EncCompras.Where(a => a.idCierre == Cierre.idCierre).ToList();
                foreach (var item in Detalle)
                {
                    var compra = Encabezados.Where(a => a.id == item.idFactura).FirstOrDefault();
                    enc.Add(compra);
                }

                var login = db.Login.Where(a => a.id == Cierre.idLogin).FirstOrDefault();
                var param = db.Parametros.FirstOrDefault();

                if(!Licencia.QAD)
                {
                    var oInvoice = (Documents)Conexion.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);



                    oInvoice.DocObjectCode = BoObjectTypes.oPurchaseInvoices;

                    oInvoice.CardCode = login.CardCode; //CardCode que viene de login


                    if (login.CambioFecha)
                    {
                        oInvoice.DocDate = DateTime.Now; //Fecha que se realiza el asiento
                        oInvoice.DocDueDate = DateTime.Now; //Fecha que se realiza el asiento
                    }
                    else
                    {
                        oInvoice.DocDate = Cierre.FechaFinal; //Final del periodo de cierre
                        oInvoice.DocDueDate = Cierre.FechaFinal; //Final del periodo de cierre
                    }

                    oInvoice.DocCurrency = (Cierre.CodMoneda == "CRC" ? "COL" : Cierre.CodMoneda == "HNL" ? "LPS" : Cierre.CodMoneda == "GTQ" ? "QTZ" : Cierre.CodMoneda); //Moneda de la liquidacion
                    if (Pais == "P")
                    {
                        if (Cierre.CodMoneda == "USD")
                        {
                            oInvoice.DocCurrency = "$";
                        }
                    }
                    oInvoice.DocType = BoDocumentTypes.dDocument_Service;
                    oInvoice.NumAtCard = "Liquidación: " + idCierre.ToString();
                    var i = 0;
                    foreach (var item in enc)
                    {
                        Gastos TipoGasto = new Gastos();
                        //Esto no va para panama
                        if (Pais == "C")
                        {
                            if (item.RegimenSimplificado)
                            {
                                TipoGasto = db.Gastos.Where(a => a.Nombre.ToUpper().Contains("Regimen Simplificado".ToUpper())).FirstOrDefault();

                            }
                            else
                            {

                                TipoGasto = db.Gastos.Where(a => a.idTipoGasto == item.idTipoGasto).FirstOrDefault();
                            }
                        }
                        else //Panama, Nicaragua, Dominicana, Honduras, Ecuador
                        {
                            TipoGasto = db.Gastos.Where(a => a.idTipoGasto == item.idTipoGasto).FirstOrDefault();
                        }


                        var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == TipoGasto.idCuentaContable).FirstOrDefault();
                        var Norma = db.NormasReparto.Where(a => a.id == item.idNormaReparto).FirstOrDefault();
                        var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();

                        oInvoice.Lines.SetCurrentLine(i);
                        if (Pais == "C")
                        {

                            oInvoice.Lines.ItemDescription = item.CodProveedor + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 
                        }
                        else if (Pais == "P")
                        {

                            oInvoice.Lines.ItemDescription = item.CodProveedor.Split('[')[0] + " - " + item.CodProveedor.Split('[')[1] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "N")
                        {
                            oInvoice.Lines.ItemDescription = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "D")
                        {
                            oInvoice.Lines.ItemDescription = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "H")
                        {
                            oInvoice.Lines.ItemDescription = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "E")
                        {
                            oInvoice.Lines.ItemDescription = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        oInvoice.Lines.AccountCode = Cuenta.CodSAP;   //Cuenta contable del gasto

                        if (Pais == "C" || Pais == "N" || Pais == "D" || Pais == "H" || Pais == "E" || Pais == "G")
                        {
                            oInvoice.Lines.TaxCode = param.IMPEX; //Exento para Panama -> Verificar el codigo C0

                        }
                        else
                        {
                            oInvoice.Lines.VatGroup = param.IMPEX;
                        }

                        //Redifinir para Panama
                        if (Pais == "C")
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;
                            imp4 += item.Impuesto4;
                            imp8 += item.Impuesto8;
                            imp13 += item.Impuesto13;
                        }
                        else if (Pais == "D")
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;
                            imp4 += item.Impuesto4;

                        }
                        else if (Pais == "H")
                        {
                            imp1 += item.Impuesto1; // 18%
                            imp2 += item.Impuesto2; // 15%
                            imp4 += item.Impuesto4; // 10%
                            imp8 += item.Impuesto8; // 4%
                        }
                        else if (Pais == "E")
                        {
                            imp1 += item.Impuesto1; // 15% provisional Ecuador
                            imp4 += item.Impuesto4; // 10% propinas
                            imp8 += item.Impuesto8; // 12% Otros cargos
                            imp13 += item.Impuesto13; //12% IVA
                        }
                        else if (Pais == "G")
                        {

                            imp4 += item.Impuesto4; // Hospedaje
                            imp8 += item.Impuesto8; // PETROLEO
                            imp13 += item.Impuesto13; //12% IVA
                        }
                        else //Panama y Nicaragua
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;

                        }



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
                                    oInvoice.Lines.CostingCode5 = "";
                                    break;
                                }
                        }

                        if (Pais == "G")
                        {
                            oInvoice.Lines.LineTotal = Convert.ToDouble((item.TotalComprobante.Value - item.TotalImpuesto) - item.TotalDescuentos);

                        }
                        else
                        {
                            oInvoice.Lines.LineTotal = Convert.ToDouble(item.TotalComprobante.Value - item.TotalImpuesto);

                        }



                        if (Pais == "C")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Combustible".ToUpper()))
                            {

                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();

                                oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());

                                oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Super".ToUpper()) ? "Gasolina Super" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Regular".ToUpper()) ? "Gasolina Regular" : "Diesel");

                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_NumFactura").Value = item.NumFactura.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFactura").Value = item.FecFactura;
                        }
                        else if (Pais == "P")//Panama
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 95".ToUpper()) ? "Gasolina 95" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 90".ToUpper()) ? "Gasolina 90" : "Gas LP");
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_REFFAC").Value = item.NumFactura.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFac").Value = item.FecFactura;

                            switch (item.CodProveedor.Replace("-", "").Replace("-", "").Length)
                            {
                                case 8:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                                case 9:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                                case 10:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "3";
                                        break;
                                    }
                                case 14:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "2";
                                        break;
                                    }
                                case 15:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "2";
                                        break;
                                    }
                                default:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];
                            try
                            {
                                oInvoice.Lines.UserFields.Fields.Item("U_DV").Value = item.CodProveedor.Split('[')[1].Substring(0, 2);
                            }
                            catch (Exception)
                            {
                                oInvoice.Lines.UserFields.Fields.Item("U_DV").Value = item.CodProveedor.Split('[')[1].Substring(0, 1);

                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;

                        }
                        else if (Pais == "N")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                if (DetalleFac != null)
                                {
                                    oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                    oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 95".ToUpper()) ? "Gasolina 95" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 90".ToUpper()) ? "Gasolina 90" : "Gas LP");

                                }
                                else
                                {
                                    G.GuardarTxt("ErrorSAP.txt", "Esta vacio el detalle: " + DetalleFac.ToString());

                                }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_REFFAC").Value = item.NumFactura.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFac").Value = item.FecFactura;

                            switch (item.CodProveedor.Replace("-", "").Replace("-", "").Length)
                            {


                                case 15:

                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "2";
                                        break;
                                    }
                                default:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];

                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;
                        }
                        else if (Pais == "D")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                if (DetalleFac != null)
                                {
                                    oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                    oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diese Regular".ToUpper()) ? "Diesel Regular" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 95".ToUpper()) ? "Gasolina 95" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 90".ToUpper()) ? "Gasolina 90" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Diesel Premium".ToUpper()) ? "Diesel Premium" : "Gas LP");

                                }
                                else
                                {
                                    G.GuardarTxt("ErrorSAP.txt", "Esta vacio el detalle: " + DetalleFac.ToString());

                                }
                            }
                            oInvoice.Lines.UserFields.Fields.Item("U_REFFAC").Value = item.NumFactura.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFac").Value = item.FecFactura;

                            switch (item.CodProveedor.Replace("-", "").Replace("-", "").Length)
                            {


                                case 9:

                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "2";
                                        break;
                                    }
                                default:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];

                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;

                        }
                        else if (Pais == "H")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                if (DetalleFac != null)
                                {
                                    oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                    oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Diese Regular".ToUpper()) ? "Diesel Regular" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 95".ToUpper()) ? "Gasolina 95" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Gasolina 90".ToUpper()) ? "Gasolina 90" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Diesel Premium".ToUpper()) ? "Diesel Premium" : "Gas LP");

                                }
                                else
                                {
                                    G.GuardarTxt("ErrorSAP.txt", "Esta vacio el detalle: " + DetalleFac.ToString());

                                }
                            }
                            oInvoice.Lines.UserFields.Fields.Item("U_REFFAC").Value = item.NumFactura.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFac").Value = item.FecFactura;

                            switch (item.CodProveedor.Replace("-", "").Replace("-", "").Length)
                            {


                                case 14:

                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                                default:
                                    {
                                        oInvoice.Lines.UserFields.Fields.Item("U_Tipoid").Value = "1";
                                        break;
                                    }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];

                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;
                        }
                        else if (Pais == "E")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                if (DetalleFac != null)
                                {
                                    oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                    oInvoice.Lines.UserFields.Fields.Item("U_Tipo").Value = (DetalleFac.NomPro.ToUpper().Contains("Super".ToUpper()) ? "Gasolina Super" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Eco".ToUpper()) ? "Gasolina Regular" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : "Diesel");

                                }
                                else
                                {
                                    G.GuardarTxt("ErrorSAP.txt", "Esta vacio el detalle: " + DetalleFac.ToString());

                                }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];
                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;
                            oInvoice.Lines.UserFields.Fields.Item("U_NumFactura").Value = item.ClaveHacienda.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFactura").Value = item.FecFactura;
                        }
                        else if (Pais == "G")
                        {
                            G.GuardarTxt("ErrorSAP.txt", "Entro en: " + Pais);
                            if (TipoGasto.Nombre.ToUpper().Contains("Comb".ToUpper()))
                            {
                                var DetalleFac = db.DetCompras.Where(a => a.NumFactura == item.NumFactura && a.ClaveHacienda == item.ClaveHacienda && a.ConsecutivoHacienda == item.ConsecutivoHacienda).FirstOrDefault();
                                if (DetalleFac != null)
                                {
                                    oInvoice.Lines.UserFields.Fields.Item("U_CantLitrosKw").Value = int.Parse(Math.Round(DetalleFac.Cantidad.Value).ToString());
                                    oInvoice.Lines.UserFields.Fields.Item("U_Tipo_G").Value = (DetalleFac.NomPro.ToUpper().Contains("Super".ToUpper()) ? "Gasolina Super" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Eco".ToUpper()) ? "Gasolina Regular" : QuitarTilde(DetalleFac.NomPro).ToUpper().Contains("Diesel".ToUpper()) ? "Diesel" : "Diesel");

                                }
                                else
                                {
                                    G.GuardarTxt("ErrorSAP.txt", "Esta vacio el detalle: " + DetalleFac.ToString());

                                }
                            }

                            oInvoice.Lines.UserFields.Fields.Item("U_RUC").Value = item.CodProveedor.Split('[')[0];
                            oInvoice.Lines.UserFields.Fields.Item("U_Proveedor").Value = item.NomProveedor;
                            oInvoice.Lines.UserFields.Fields.Item("U_NumFactura").Value = item.ClaveHacienda.ToString();
                            oInvoice.Lines.UserFields.Fields.Item("U_FechaFactura").Value = item.FecFactura;
                        }



                        oInvoice.Lines.Add();

                        i++;
                    }


                    //Preguntar por el pais
                    if (Pais == "C")
                    {
                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Impuesto 1";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Impuesto 2";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Impuesto 4";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI4;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Impuesto 8";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp8);
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI8;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Impuesto 13";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp13);
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI13;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "P") //Panama
                    {
                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ITBMS(7%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            oInvoice.Lines.VatGroup = param.IMPEX;
                            //oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ITBMS(10%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                            oInvoice.Lines.VatGroup = param.IMPEX;
                            // oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "N")
                    {
                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "IV(13%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            oInvoice.Lines.VatGroup = param.IMPEX;
                            //oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "IVA(15%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                            oInvoice.Lines.VatGroup = param.IMPEX;
                            // oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "D")
                    {
                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ITBIS(18%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ITBIS(16%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Otros Cargos (10&)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "H")
                    {
                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ISV(18%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "ISV(15%)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp2);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Otros Cargos (10&)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }
                        if (imp8 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "4% (Turismo)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp8);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "E")
                    {
                        if (imp4 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Hospedaje";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Petroleo";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp8);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "IVA (12&)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp13);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI13;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp1 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "IVA (15&)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp1);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI13;
                            oInvoice.Lines.Add();
                            i++;
                        }
                    }
                    else if (Pais == "G")
                    {
                        if (imp4 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Hospedaje";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp4);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI1;

                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "Petróleo";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp8);
                            //oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI2;
                            oInvoice.Lines.Add();
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            oInvoice.Lines.SetCurrentLine(i);
                            oInvoice.Lines.ItemDescription = "IVA (12&)";
                            oInvoice.Lines.LineTotal = Convert.ToDouble(imp13);
                            // oInvoice.Lines.VatGroup = param.IMPEX;
                            oInvoice.Lines.TaxCode = param.IMPEX;
                            oInvoice.Lines.AccountCode = param.CI13;
                            oInvoice.Lines.Add();
                            i++;
                        }


                    }








                    var respuesta = oInvoice.Add();
                    G.GuardarTxt("ErrorSAP.txt", "Respuesta: " + respuesta.ToString());
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

                        try
                        {
                            BitacoraCierres bc = new BitacoraCierres();
                            bc.idUsuarioEnviador = Cierre.idLogin;
                            bc.idUsuarioAceptador = Cierre.idLoginAceptacion.Value;
                            bc.idCierre = Cierre.idCierre;
                            bc.IP = HttpContext.Current.Request.UserHostAddress;
                            bc.Detalle = "Se manda a SAP la liquidacion # " + Cierre.idCierre;
                            bc.Fecha = DateTime.Now;
                            db.BitacoraCierres.Add(bc);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            BitacoraErrores be1 = new BitacoraErrores();
                            be1.Descripcion = ex.Message;
                            be1.StackTrace = ex.StackTrace;
                            be1.Metodo = "Envio a SAP";
                            be1.Fecha = DateTime.Now;
                            db.BitacoraErrores.Add(be1);
                            db.SaveChanges();
                        }
                        G.CerrarConexionAPP(db);
                        Conexion.Desconectar();
                        return Request.CreateResponse(HttpStatusCode.OK, resp);
                    }

                    BitacoraErrores be = new BitacoraErrores();
                    be.Descripcion = Conexion.Company.GetLastErrorDescription();
                    be.StackTrace = Conexion.Company.UserName;
                    be.Metodo = "Insercion de Asiento";
                    be.Fecha = DateTime.Now;
                    db.BitacoraErrores.Add(be);
                    db.SaveChanges();
                    resp = new
                    {
                        //   Series = pedido.Series.ToString(),
                        DocEntry = 0,
                        Type = "oPurchaiseInvoice",
                        Status = 0,
                        Message = Conexion.Company.GetLastErrorDescription(),
                        User = Conexion.Company.UserName
                    };





                    Conexion.Desconectar();
                }
                else
                {
                    var ParametrosQAD = db.ParametrosQAD.FirstOrDefault();
                    
                    PurchaseOrder po = new PurchaseOrder();
                    po.domainCode = ParametrosQAD.domainCode;
                    po.orderType = "PS";
                    po.purchaseOrderNumber = "LIQ" + Cierre.idCierre;
                    if (login.CambioFecha)
                    {
                        po.orderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc);  //Fecha que se realiza el asiento
                        
                    }
                    else
                    {
                        po.orderDate = new DateTime(Cierre.FechaFinal.Year, Cierre.FechaFinal.Month, Cierre.FechaFinal.Day, 0, 0, 0, DateTimeKind.Utc);  //Final del periodo de cierre 
                    }
                    po.supplierCode = login.CardCode;
                    po.siteCode = ParametrosQAD.siteCode;
                    po.buyerCode = ParametrosQAD.buyerCode;
                    po.currencyCode = Cierre.CodMoneda;
                    po.orderStatus = ParametrosQAD.orderStatus;
                    po.orderTotal = Cierre.Total.Value;
                    po.totalAmount = Cierre.Total.Value;
                    po.remarks = Cierre.Observacion;
                    po.daybookSetCode = ParametrosQAD.daybookSetCode;
                    po.purchaseOrderLines = new List<PurchaseOrderLine>();
                    var i = 1;
                    foreach (var item in enc)
                    {
                        Gastos TipoGasto = new Gastos();
                        //Esto no va para panama
                        if (Pais == "C")
                        {
                            if (item.RegimenSimplificado)
                            {
                                TipoGasto = db.Gastos.Where(a => a.Nombre.ToUpper().Contains("Regimen Simplificado".ToUpper())).FirstOrDefault();

                            }
                            else
                            {

                                TipoGasto = db.Gastos.Where(a => a.idTipoGasto == item.idTipoGasto).FirstOrDefault();
                            }
                        }
                        else //Panama, Nicaragua, Dominicana, Honduras, Ecuador
                        {
                            TipoGasto = db.Gastos.Where(a => a.idTipoGasto == item.idTipoGasto).FirstOrDefault();
                        }


                        var Cuenta = db.CuentasContables.Where(a => a.idCuentaContable == TipoGasto.idCuentaContable).FirstOrDefault();
                        var Norma = db.NormasReparto.Where(a => a.id == item.idNormaReparto).FirstOrDefault();
                        var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                        PurchaseOrderLine pol = new PurchaseOrderLine();
                        pol.purchaseOrderLine = i;
                        if (Pais == "C")
                        {

                            pol.description = item.CodProveedor + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 
                        }
                        else if (Pais == "P")
                        {

                            pol.description = item.CodProveedor.Split('[')[0] + " - " + item.CodProveedor.Split('[')[1] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "N")
                        {
                            pol.description = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "D")
                        {
                            pol.description = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "H")
                        {
                            pol.description = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }
                        else if (Pais == "E")
                        {
                            pol.description = item.CodProveedor.Split('[')[0] + "-" + item.NomProveedor;//"3102751358 - D y D Consultores"; // Factura -> Cedula 

                        }

                        pol.purchaseOrderType = "M";
                        pol.itemCode = "000";

                        pol.purchaseAcct = Cuenta.CodSAP;
                        pol.purchaseCC = Dimension.codigoSAP;
                        pol.purchaseSubAcct = Norma.CodSAP;
                        pol.quantityOrdered = 1;
                        pol.unitOfMeasure = "UN";
                        if (Pais == "G")
                        {
                            pol.purchaseCost =  (item.TotalComprobante.Value - item.TotalImpuesto.Value) - item.TotalDescuentos.Value ;
                            pol.totalAmount =  (item.TotalComprobante.Value - item.TotalImpuesto.Value) - item.TotalDescuentos.Value ; 

                        }
                        else
                        {
                            pol.purchaseCost = item.TotalComprobante.Value - item.TotalImpuesto.Value;
                            pol.totalAmount = item.TotalComprobante.Value - item.TotalImpuesto.Value;
                            

                        }
                       
                        pol.siteCode = ParametrosQAD.siteCode;
                        pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                        pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                        pol.receiptType = "N";

                        po.purchaseOrderLines.Add(pol);

                        i++;

                        //Redifinir para Panama
                        if (Pais == "C")
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;
                            imp4 += item.Impuesto4;
                            imp8 += item.Impuesto8;
                            imp13 += item.Impuesto13;
                        }
                        else if (Pais == "D")
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;
                            imp4 += item.Impuesto4;

                        }
                        else if (Pais == "H")
                        {
                            imp1 += item.Impuesto1; // 18%
                            imp2 += item.Impuesto2; // 15%
                            imp4 += item.Impuesto4; // 10%
                            imp8 += item.Impuesto8; // 4%
                        }
                        else if (Pais == "E")
                        {
                            imp1 += item.Impuesto1; // 15% provisional Ecuador
                            imp4 += item.Impuesto4; // 10% propinas
                            imp8 += item.Impuesto8; // 12% Otros cargos
                            imp13 += item.Impuesto13; //12% IVA
                        }
                        else if (Pais == "G")
                        {

                            imp4 += item.Impuesto4; // Hospedaje
                            imp8 += item.Impuesto8; // PETROLEO
                            imp13 += item.Impuesto13; //12% IVA
                        }
                        else //Panama y Nicaragua
                        {
                            imp1 += item.Impuesto1;
                            imp2 += item.Impuesto2;

                        }

                    }

                    //Preguntar por el pais
                    if (Pais == "C")
                    {
                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Impuesto 1";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            i++; 
                        }

                        if (imp2 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Impuesto 2";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp2;
                            pol.totalAmount = imp2;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol); 
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Impuesto 4";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI4;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp4;
                            pol.totalAmount = imp4;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                             
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Impuesto 8";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI8;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp8;
                            pol.totalAmount = imp8;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                            
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Impuesto 13";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI13;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp13;
                            pol.totalAmount = imp13;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                         
                            i++;
                        }
                    }
                    else if (Pais == "P") //Panama
                    {
                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ITBMS(7%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                           
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ITBMS(10%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp2;
                            pol.totalAmount = imp2;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }
                    }
                    else if (Pais == "N")
                    {
                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "IV(13%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                            
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "IVA(15%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp2;
                            pol.totalAmount = imp2;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                           
                            i++;
                        }
                    }
                    else if (Pais == "D")
                    {
                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ITBIS(18%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                           
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ITBIS(16%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp2;
                            pol.totalAmount = imp2;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Otros Cargos (10&)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp4;
                            pol.totalAmount = imp4;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                           
                            i++;
                        }
                    }
                    else if (Pais == "H")
                    {
                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ISV(18%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
 
                            i++;
                        }

                        if (imp2 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "ISV(15%)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp2;
                            pol.totalAmount = imp2;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                             
                            i++;
                        }

                        if (imp4 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Otros Cargos (10&)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp4;
                            pol.totalAmount = imp4;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }
                        if (imp8 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "4% (Turismo)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp8;
                            pol.totalAmount = imp8;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                             
                            i++;
                        }
                    }
                    else if (Pais == "E")
                    {
                        if (imp4 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Hospedaje";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp4;
                            pol.totalAmount = imp4;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                             
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Petroleo";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp8;
                            pol.totalAmount = imp8;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);
                             
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "IVA (12&)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI13;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp13;
                            pol.totalAmount = imp13;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }

                        if (imp1 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "IVA (15&)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI13;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp1;
                            pol.totalAmount = imp1;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }
                    }
                    else if (Pais == "G")
                    {
                        if (imp4 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Hospedaje";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI1;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp4;
                            pol.totalAmount = imp4;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                          
                            i++;
                        }

                        if (imp8 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "Petróleo";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI2;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp8;
                            pol.totalAmount = imp8;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                             
                            i++;
                        }

                        if (imp13 > 0)
                        {
                            PurchaseOrderLine pol = new PurchaseOrderLine();
                            pol.purchaseOrderLine = i;
                            pol.description = "IVA (12&)";
                            pol.purchaseOrderType = "M";
                            pol.itemCode = "000";

                            pol.purchaseAcct = param.CI13;
                            var Norma = db.NormasReparto.Where(a => a.idLogin == Cierre.idLogin).FirstOrDefault();
                            var Dimension = db.Dimensiones.Where(a => a.id == Norma.idDimension).FirstOrDefault();
                            pol.purchaseCC = Dimension.codigoSAP;
                            pol.purchaseSubAcct = Norma.CodSAP;
                            pol.quantityOrdered = 1;
                            pol.unitOfMeasure = "UN";
                            pol.purchaseCost = imp13;
                            pol.totalAmount = imp13;
                            pol.siteCode = ParametrosQAD.siteCode;
                            pol.purchaseSiteCode = ParametrosQAD.purchaseSiteCode;
                            pol.taxEnvironment = ParametrosQAD.taxEnvironment;
                            pol.receiptType = "N";

                            po.purchaseOrderLines.Add(pol);

                            
                            i++;
                        }


                    }

                    var token = "";

             
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response2 = await cliente.PostAsync(ParametrosQAD.UrlTokenQAD,null);


                    if (response2.IsSuccessStatusCode)
                    {
                        response2.Content.Headers.ContentType.MediaType = "application/json";
                        var resp2 = await response2.Content.ReadAsAsync<TokenQAD>();
                        token = resp2.access_token;


                    }

                    if(!string.IsNullOrEmpty(token))
                    {
                        HttpClient clienteProd = new HttpClient();
                        List<PurchaseOrder> ordenes = new List<PurchaseOrder>();
                        ordenes.Add(po);
                        var httpContent2Prod = new StringContent(JsonConvert.SerializeObject(ordenes, new JsonSerializerSettings
                        {
                            DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffK"
                        }), Encoding.UTF8, "application/json");
                        clienteProd.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        clienteProd.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        try
                        {
                            HttpResponseMessage response3 = await clienteProd.PostAsync(ParametrosQAD.UrlEnviarAsientoQAD, httpContent2Prod);
                            if (response3.IsSuccessStatusCode)
                            {
                                db.Entry(Cierre).State = EntityState.Modified;
                                Cierre.ProcesadaSAP = true;
                                db.SaveChanges();

                                resp = new
                                {

                                    DocEntry = 0,
                                    //  Series = pedido.Series.ToString(),
                                    Type = "oPurchaiseInvoice",
                                    Status = 1,
                                    Message = "Factura creada exitosamente",
                                    User = ""
                                };
                                G.CerrarConexionAPP(db);
                                return Request.CreateResponse(HttpStatusCode.OK, resp);
                            }
                            else
                            {
                                throw new Exception("Error al mandar a QAD: ->" + response3.ReasonPhrase);
                            }
                        }
                        catch (Exception exZ)
                        {

                            BitacoraErrores be = new BitacoraErrores();
                             be.Metodo = "Envio de json a QAD";

                            be.Descripcion = exZ.Message;
                            be.StackTrace = exZ.StackTrace;
                            be.Fecha = DateTime.Now;

                            db.BitacoraErrores.Add(be);
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        throw new Exception("Error al obtener el token de QAD");
                    }

                }

               
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.OK, "");

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

                BitacoraErrores be = new BitacoraErrores();
                be.Descripcion = ex.Message;
                be.StackTrace = ex.StackTrace;
                be.Metodo = "Insercion de Asiento";
                be.Fecha = DateTime.Now;
                db.BitacoraErrores.Add(be);
                db.SaveChanges();


                Conexion.Desconectar();
                G.CerrarConexionAPP(db);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, resp);
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