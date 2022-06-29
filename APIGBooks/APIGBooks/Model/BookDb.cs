namespace APIGBooks.Model
{
    public class BookDb
    {
        public string kind { get; set; }
        public string Id { get; set; }
        public string etag { get; set; }
        public string title { get; set; }
        public string authors { get; set; }
        public string publishedDate { get; set; }
        public int printedPageCount { get; set; }
        public string categories { get; set; }
        public string maturityRating { get; set; }
        public string contentVersion { get; set; }
        public string language { get; set; }
        public string canonicalVolumeLink { get; set; }
    }
}
