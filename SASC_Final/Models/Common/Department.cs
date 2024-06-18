using System.Collections.Generic;

namespace SASC_Final.Models.Common
{
    public class Department : IEntity
    {
        public int Id { get; set; }

        public string Abbrev { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
