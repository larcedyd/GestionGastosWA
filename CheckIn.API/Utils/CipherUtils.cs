using GreenPayApi.Models;
using System;

namespace GreenPayApi.Utils{

    public class CipherUtils{

        /**
            Nombre: GenerateAESKey
            Descripción: Genera un arreglo de enteros (0-255) aleatorios que funciona como llave privada para cifrado simétrico AES
            Retorna: Un arreglo de números enteros aleatorios que se usan como llave de cifrado.
        */
        public static AESKey GenerateAESKey(){
            int[] key = new int[16];
            Random random = new Random();
            for(int i=0; i < 16; ++i){
                int randomNumber = random.Next(256);
                key[i] = (int)randomNumber;
            }

            AESKey aesKey = new AESKey();
            aesKey.Key = key;

            return aesKey;
        }

        /**
            Nombre: GenerateAESIV
            Descripción: Genera un arreglo de enteros (0-255) aleatorios que funciona como llave privada para cifrado simétrico AES
            Retorna: Un arreglo de números enteros aleatorios que se usan como llave de cifrado.
        */
        public static AESCounter GenerateAESCounter(){
            AESCounter aesCounter = new AESCounter();
            aesCounter.Counter = (new Random()).Next(256);
            return aesCounter;
        }

    }
}