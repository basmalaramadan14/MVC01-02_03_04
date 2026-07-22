using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt {  get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
