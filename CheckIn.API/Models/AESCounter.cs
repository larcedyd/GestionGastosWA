using System;

namespace GreenPayApi.Models{
    public class AESCounter {
        public int Counter { get; set; }

        /**
            Nombre: GetCounterValue
            Descripción: Obtiene el valor del Counter como un arreglo de bytes.
            Retorna: el valor del Counter como un arreglo de bytes.
        */
        public byte[] GetCounterValue(){
            byte[] counterBytes = new byte[16];
            byte[] intBytes = BitConverter.GetBytes(Counter);
            int counterReverseIndex = 15;
            for(int i=0; i < intBytes.Length; ++i){
                counterBytes[counterReverseIndex] = intBytes[i];
                --counterReverseIndex;
            }
            return counterBytes;
        }
    }
}