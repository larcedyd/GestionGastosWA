using Newtonsoft.Json;

namespace GreenPayApi.Models{
    public class AESPair {
        [JsonProperty(PropertyName = "k")]
        public int[] Key { get; set; }

        [JsonProperty(PropertyName = "s")]
        public int Counter { get; set; }
    }
}