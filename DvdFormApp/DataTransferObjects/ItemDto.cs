namespace DvdFormApp.DataTransferObjects
{
    public class ItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public int? BookshelfId { get; set; }
    }
}
