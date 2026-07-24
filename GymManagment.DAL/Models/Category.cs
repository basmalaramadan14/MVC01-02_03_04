using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Category : BaseEntity
    {
        public  String CategoryName {  get; set; }


        #region Relationships
        
         public  ICollection<Session> Sessions { get; set; }




        #endregion
    }
}
