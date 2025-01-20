using MongoDB.Bson.Serialization.Attributes;
using NGrid.Customer.Framework.Templates.DataBase.Repository.Attributes;
using NGrid.Customer.Framework.Templates.DataBase.Repository.Model;

namespace NGrid.Customer.ToReplace.Infrastructure;

[BsonIgnoreExtraElements]
public class ToReplaceDao : BaseDao
{
    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonKeyFieldElement("key")]
    public int Key { get; set; }
}