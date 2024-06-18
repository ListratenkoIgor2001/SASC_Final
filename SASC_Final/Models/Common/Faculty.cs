namespace SASC_Final.Models.Common
{
    public class Faculty : IEntity
    {
        public int Id { get; set; }

        public string Abbrev { get; set; }

        public string Name { get; set; }
    }
}
