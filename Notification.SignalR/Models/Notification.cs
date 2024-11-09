using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Notification.SignalR.Models;

public class Notification
{
    [BsonId]  
    public ObjectId Id { get; set; }

    [BsonElement("Action")]
    [MaxLength(255)]
    public string Action { get; set; }

    [BsonElement("GroupAction")]
    [MaxLength(255)]
    public string GroupAction { get; set; }

    [BsonElement("GroupID")]
    [MaxLength(36)]
    public string GroupID { get; set; }

    [BsonElement("AppCode")]
    [MaxLength(50)]
    public string AppCode { get; set; }

    [BsonElement("RawData")]
    public string RawData { get; set; }

    [BsonElement("Message")]
    public string Message { get; set; }

    [BsonElement("UserID")]
    [MaxLength(36)]
    public string UserID { get; set; }

    [BsonElement("SenderID")]
    [MaxLength(36)]
    public string SenderID { get; set; }

    [BsonElement("SenderName")]
    [MaxLength(100)]
    public string SenderName { get; set; }

 
    [BsonElement("CreatedAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [BsonElement("FromAppCode")]
    [MaxLength(50)]
    public string FromAppCode { get; set; }

    [BsonElement("IsReaded")]
    public bool IsReaded { get; set; }
}