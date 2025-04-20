namespace Readify.BLL.Helpers
{
    public class Metadata
    {
        public Pagination Pagination { get; set; } = new Pagination();
        public List<string> SearchBy { get; set; } = new List<string>();
    }
}
