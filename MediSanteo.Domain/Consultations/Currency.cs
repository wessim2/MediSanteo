using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Consultations
{
    public record Currency
    {
        public static readonly Currency Tnd = new("TND");

        public Currency(string code) => Code = code;
        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ??
                throw new ApplicationException("The currency code is invalid ");
        }

        public static IReadOnlyCollection<Currency> All = new[]
        {
            Tnd
        };
    }
}
