using MongoDB.Bson;

namespace Data.Base
{
    public interface IData
    {
        ObjectId Id { get; set; }
    }
}