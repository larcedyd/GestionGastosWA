using System;

namespace GreenPayApi.Models{
    public class AESKey {
        public int[] Key { get; set; }

        /**
            Nombre: GetKeyValue
            Descripción: Obtiene el valor de la llave como un arreglo de bytes.
            Retorna: el valor de la llave como un arreglo de bytes.
        */
        public byte[] GetKeyValue(){
            byte[] keyBytes = new byte[16];
            for(int i=0; i < 16; ++i){
                keyBytes[i] = Convert.ToByte(Key[i]);
            }
            return keyBytes;
        }
    }
}