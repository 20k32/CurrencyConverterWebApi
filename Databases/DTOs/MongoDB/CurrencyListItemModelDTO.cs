using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Databases.DTOs.MongoDB
{
    [Serializable]
    public class CurrencyListItemModelDTO
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("CurrencyName"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; } = null!;

        [BsonElement("CurrencyValue"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }
    }
}
