using System;
using System.Collections.Generic;
using System.Text;

using SASC_Final.Models.Common;
using SASC_Final.Models.Common.DTOs;

namespace SASC_Final.Models
{
    public class PhysicalEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string ImageUrl { get; set; }

        public bool IsSynced { get; }

        #region student
        public string RecordBookNumber { get; set; } = null;

        public string Group { get; set; } = null;
        #endregion
        #region Employee
        public string Degree { get; set; } = null;

        public string Rank { get; set; } = null;

        public string UrlId { get; set; } = null;
        #endregion
        //public bool isStudent { get; set; }
        public PhysicalEntity() {
            IsSynced = false;
        }
        public PhysicalEntity(EmployeeDto ent) 
        {
            Id = ent.Id;
            FirstName = ent.FirstName;
            LastName = ent.LastName;
            MiddleName = ent.MiddleName;
            ImageUrl = ent.ImageURL;
            Degree = ent.Degree;
            Rank = ent.Rank;
            UrlId = ent.UrlId;
            IsSynced = true;
        }
        public PhysicalEntity(StudentDto ent) 
        {
            Id = ent.Id;
            FirstName = ent.FirstName;
            LastName = ent.LastName;
            MiddleName = ent.MiddleName;
            ImageUrl = ent.ImageURL;
            RecordBookNumber = ent.RecordBookNumber;
            Group = ent.GroupNumber;
            IsSynced = true;
        }

    }
}
