using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Data.Base;

namespace Data.Model
{
    public class Transaction : IData
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [JsonProperty("sku")]
        public string Sku { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}