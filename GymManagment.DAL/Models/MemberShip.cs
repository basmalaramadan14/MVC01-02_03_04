using gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class MemberShip : BaseEntity
    {


        public Member Members { get; set; }

        public int MemberId { get; set; }
        public Plan Plans { get; set; }
        public int PlanId { get; set; }

        //StartDate == CreatedAt = BaseEntity
        public DateTime EndDate { get; set; }

        //Read only properties
        //Read only pproperties  Doesn't transfer INTO table in Database

        public string Status => EndDate > DateTime.Now ? "Active" : "Expired";
        public bool IsActive => EndDate > DateTime.Now;
        }

    }

