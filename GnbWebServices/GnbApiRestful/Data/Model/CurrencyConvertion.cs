using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Data.Base;

namespace Data.Model
{
    public class CurrencyConvertion : IData
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}