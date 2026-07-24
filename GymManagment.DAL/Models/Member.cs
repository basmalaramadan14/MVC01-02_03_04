using gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Member : GymUser
    {
        public String ? Photo {  get; set; }
        #region Relationships

        public HealthRecord HealthRecord { get; set; } = default;
        public ICollection<MemberShip> MemberShipPlans {  get; set; }

        public ICollection<Booking> MemberSession { get; set; }

        #endregion


    }
}
