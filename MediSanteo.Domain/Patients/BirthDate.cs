using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Patients
{
    public record BirthDate(DateTime Value)
    {
        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - Value.Year;
            return age;
        }
    }
    
}
