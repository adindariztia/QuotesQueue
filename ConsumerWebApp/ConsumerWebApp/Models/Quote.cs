

namespace ConsumerWebApp.Models
{
    public class Quote
    {
        public string _id { get; set; }

        public string content { get; set; }

        public string author { get; set; }

        public List<string> tags { get; set; }

        public string authorSlug { get; set; }

        public int length { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime dateModified { get; set; }
    }
}
