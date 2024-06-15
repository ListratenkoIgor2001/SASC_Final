namespace SASC_Final.Models.Common.DTOs
{
    public class DepartmentDto : IDtoEntity
    {
        public int Id { get; set; }

        public string Abbrev { get; set; }

        public string Name { get; set; }
    }
}
