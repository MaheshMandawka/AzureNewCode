using System;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? Date { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public int? BookshelfId { get; set; }
    public string Type { get; set; }
}