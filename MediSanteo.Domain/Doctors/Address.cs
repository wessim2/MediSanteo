using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Doctors
{
    public record Address
    (string Street,
     string City,
     string Country
        );
}
