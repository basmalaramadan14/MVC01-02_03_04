using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class HealthRecord : BaseEntity
    {
        public decimal  Heigth { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }
        public string ? Note { get; set; }
        #region Relationships

        public Member Member { get; set; } = default;

        public int MemberId { get; set; } // FK

        #endregion

        // UpdateAt of BaseEntity => LastUpdate

    }
}
