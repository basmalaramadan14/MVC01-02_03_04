using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Session : BaseEntity
    {
        public String Description {  get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
         public DateTime EndDate { get; set; }

        #region Relationships
        public Trainer Trainer { get; set; }

        public int TrainerId { get; set; }   //FK

        public Category Category { get; set; }

        public int CategoryId { get; set; }  //FK

         public ICollection<Booking> SessionMember {  get; set; }

        #endregion 



        }
}
