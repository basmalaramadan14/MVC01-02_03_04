using GymManagment.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class GymUser : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Phone {  get; set; }
        public DateOnly DateOfBith {  get; set; }
        public Gender Gender { get; set; }

        //[Address:BNumber ,street, city ]

        public Address Address { get; set; }

    }
    [Owned]
    public class Address
    {
        public string BuildingNumbern { get; set; } = default!;
        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

    }
}
