using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Patients
{
    public record VitalSigns(
        decimal HeartRate,
        decimal BloodPressure,
        decimal Tempertature,
        DateTime Timestamp
        );
}
