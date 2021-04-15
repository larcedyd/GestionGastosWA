using System;
using System.Text;

namespace GreenPayApi.Utils{

    public class StringUtils{

        /**
            Nombre: HexStringToString
            Descripción: Convierte una cadena en formato Hexadecimal en una cadena regular.
            Retorna: Una cada regular que representa la cadena en formato Hexadecimal de entrada.
        */
        public static string HexStringToString(string HexString) {
            string stringValue = "";
            for (int i = 0; i < HexString.Length / 2; i++) {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                stringValue += Char.ConvertFromUtf32(hexValue);
            }
            return stringValue;
        }

        /**
            Nombre: ByteArrayToHexString
            Descripción: Convierte un arreglo de bytes en una cadena de caracteres en formato Hexadecimal.
            Retorna: La representación Hexadecimal del arreglo de bytes parámetro.
        */
        public static string ByteArrayToHexString(byte[] array){
            StringBuilder result = new StringBuilder(array.Length * 2);
            foreach(byte b in array){
                result.AppendFormat("{0:x2}", b);
            }

            return result.ToString();
        }

        /**
            Nombre: ConvertHexStringToByteArray
            Descripción: Convierte una cadena de caracteres en formato Hexadecimal en su representación como arreglo de bytes.
            Retorna: La representación como arreglo de bytes de la cadena Hexadecimal parámetro.
        */
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }

            return data; 
        }

    }
}