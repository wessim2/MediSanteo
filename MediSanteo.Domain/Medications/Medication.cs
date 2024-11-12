using MediSanteo.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Medications
{
    public class Medication : Entity
    {
        public Name Name { get; set; }
        public Description Description { get; set; }
        public Dosage Dosage { get; set; }


    }
}
