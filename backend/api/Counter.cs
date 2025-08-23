using Newtonsoft.Json;

namespace Company.Function
{
        public class Counter
        {
            [JsonProperty(PropertyName="Id")]
            public string Id {get; set;}
            
            [JsonProperty(PropertyName= "count")]

            public int Count {get;set;}
        }
}