using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using static System.Collections.Specialized.BitVector32;

using System.Web.Configuration;
using System.Security.Claims;
using CheckIn.API.Models.ModelCliente;
using CheckIn.API.Models.ModelMain;
using CheckIn.API.Models;
using CheckIn.API.ViewModels;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CheckIn.API.Controllers
{
    public class G
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        internal void AbrirConexionAPP(out ModelCliente db)
        {
            try
            {
             
                var claims1 = HttpContext.Current.User.Identity.Name.ToString();

                
                db = new ModelCliente(claims1);


            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void CerrarConexionAPP( ModelCliente db)
        {
            try
            {




                db.Database.Connection.Close();
                db.Database.Connection.Dispose();


            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string ObtenerConfig(string v)
        {
            try
            {
                return WebConfigurationManager.AppSettings[v];
            }
            catch
            {
                return "";
            }
        }

        public string Encrypt(string stringToEncrypt, bool UsuarioAdmin = false)
        {
            try
            {
                string SEncryptionKey = HttpContext.Current.User.Identity.Name.ToString();

                if(UsuarioAdmin)
                {
                    SEncryptionKey = G.ObtenerConfig("SicKey");
                }

                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
   
    }
}