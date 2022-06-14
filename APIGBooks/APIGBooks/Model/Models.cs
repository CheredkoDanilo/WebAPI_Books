namespace APIGBooks.Model
{
    public class Models
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public volumeInfo volumeInfo { get; set; }
        public saleInfo saleInfo { get; set; }
        public accessInfo accessInfo { get; set; }
    }
    public class volumeInfo
    {
        public string title { get; set; }
        public List<string> authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public int printedPageCount { get; set; }
        public string printType { get; set; }
        public List<string> categories { get; set; }
        public string maturityRating { get; set; }
        public string contentVersion { get; set; }
        public string language { get; set; }
        public string canonicalVolumeLink { get; set; }

    }
    public class saleInfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }

    }
    public class accessInfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public string textToSpeechPermission { get; set; }
        public string webReaderLink { get; set; }

    }
}

