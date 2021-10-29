namespace Honey.Core.Modrinth.Filters
{
    public class SubStatement
    {
        public string Statement { get; set; } = string.Empty;
        public FilterBuilder Builder { get; set; } = new FilterBuilder();
    }
}