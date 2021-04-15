using System;
using System.IO;
using System.Text;

using GreenPayApi.Utils;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace GreenPayApi.RSA
{
    public class RSABouncyCastle{

        /**
            Nombre: ReadPublicKeyFromFile
            Descripción: Carga una llave pública a partir de un archivo de llave pública (PEM).
            Retorna: Una llave pública en formato de la librería Bouncy Castle.
            Parámetros: - pemFileName: Nombre del archivo pem que contiene la llave pública (Incluye el path si fuera necesario).
        */
        public static AsymmetricKeyParameter ReadPublicKeyFromFile(string pemFilename)
        {
            var fileStream = System.IO.File.OpenText (pemFilename);
            var pemReader = new Org.BouncyCastle.OpenSsl.PemReader (fileStream);
            var KeyParameter = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)pemReader.ReadObject ();
            return KeyParameter;
        }

        /**
            Nombre: RSAEncryptWithPublic
            Descripción: Cifra en RSA con la librería Bouncy Castle.
            Retorna: Una cadena con los datos cifrados con la llave pública.
            Parámetros: - clearText: Texto a cifrar.
                        - publicKey: Llave pública en formato de la librería Bouncy Castle.
        */
        private static string RSAEncryptWithPublic(string clearText, AsymmetricKeyParameter publicKey)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(clearText);

            var encryptEngine = new Org.BouncyCastle.Crypto.Encodings.Pkcs1Encoding(new Org.BouncyCastle.Crypto.Engines.RsaEngine());

            encryptEngine.Init(true, publicKey);
            byte[] ciphered = encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
            var encrypted = Convert.ToBase64String(ciphered);

            return encrypted;

        }

        /**
            Nombre: EncryptRSAWithPublicKeyFromFile
            Descripción: Cifra en RSA con la librería Bouncy Castle.
            Retorna: Una cadena con los datos cifrados con la llave pública.
            Parámetros: - input: Texto a cifrar.
                        - publicKeyFileName: Nombre o ruta del archivo que contiene la llave pública.
        */
        public static string EncryptRSAWithPublicKeyFromFile(string input, string publicKeyFileName)
        {
            AsymmetricKeyParameter publicKey = ReadPublicKeyFromFile(publicKeyFileName);
            string output = string.Empty;
            output = RSAEncryptWithPublic(input, publicKey);

            return output;
        }

        /**
            Nombre: EncryptRSAWithPublicKey
            Descripción: Cifra en RSA con la librería Bouncy Castle.
            Retorna: Una cadena con los datos cifrados con la llave pública.
            Parámetros: - input: Texto a cifrar.
                        - publicKeyFileStr: Contenido de la llave pública. Ejemplo: -----BEGIN PUBLIC KEY-----CONTENIDO-----END PUBLIC KEY-----
        */
        public static string EncryptRSAWithPublicKey(string input, string publicKeyStr)
        {
            string output = string.Empty;
            AsymmetricKeyParameter publicKey;

            using (var txtreader = new StringReader(publicKeyStr))
            {
                publicKey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)new Org.BouncyCastle.OpenSsl.PemReader(txtreader).ReadObject();
            }
            
            output = RSAEncryptWithPublic(input, publicKey);

            return output;
        }

        /**
            Nombre: VerifySignature
            Descripción: Valida una cadena SHA256 generada por el servidor contra una generada en la aplicación.
            Retorna: True si la cadena a validar se pudo validar de forma correcta con la cadena de referencia. False en caso contrario.
            Parámetros: - valueToVerify: Valor a verificar contra la firma.
                        - referenceValue: Valor de referencia o firma contra la cual se valida.
                        - publicKey: Llave pública en formato de la librería Bouncy Castle.
        */
        private static bool VerifySignature(string valueToVerify, string referenceValue, AsymmetricKeyParameter publicKey)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(valueToVerify);
            byte[] sigBytes = StringUtils.ConvertHexStringToByteArray(referenceValue);  // La respuesta en el signature está en formato HEX

            ISigner sig = SignerUtilities.GetSigner("SHA256withRSA");        
            sig.Init(false, publicKey);
            sig.BlockUpdate(msgBytes, 0, msgBytes.Length);
            bool verified = sig.VerifySignature(sigBytes);

            return verified;
        }

        /**
            Nombre: VerifySignatureWithPublicKeyFromFile
            Descripción: Valida una cadena SHA256 generada por el servidor contra una generada en la aplicación.
            Retorna: True si la cadena a validar se pudo validar de forma correcta con la cadena de referencia. False en caso contrario.
            Parámetros: - valueToVerify: Valor a verificar contra la firma.
                        - referenceValue: Valor de referencia o firma contra la cual se valida.
                        - publicKeyFileName: Nombre o ruta del archivo que contiene la llave pública.
        */
        public static bool VerifySignatureWithPublicKeyFromFile(string valueToVerify, string referenceValue, string publicKeyFileName)
        {
            AsymmetricKeyParameter publicKey = ReadPublicKeyFromFile(publicKeyFileName);

            return VerifySignature(valueToVerify, referenceValue, publicKey);
        }

        /**
            Nombre: VerifySignatureWithPublicKey
            Descripción: Valida una cadena SHA256 generada por el servidor contra una generada en la aplicación.
            Retorna: True si la cadena a validar se pudo validar de forma correcta con la cadena de referencia. False en caso contrario.
            Parámetros: - valueToVerify: Valor a verificar contra la firma.
                        - referenceValue: Valor de referencia o firma contra la cual se valida.
                        - publicKeyFileStr: Contenido de la llave pública. Ejemplo: -----BEGIN PUBLIC KEY-----CONTENIDO-----END PUBLIC KEY-----
        */
        public static bool VerifySignatureWithPublicKey(string valueToVerify, string referenceValue, string publicKeyStr)
        {
            AsymmetricKeyParameter publicKey;

            using (var txtreader = new StringReader(publicKeyStr))
            {
                publicKey = (Org.BouncyCastle.Crypto.AsymmetricKeyParameter)new Org.BouncyCastle.OpenSsl.PemReader(txtreader).ReadObject();
            }
            
            return VerifySignature(valueToVerify, referenceValue, publicKey);
        }
    }
}