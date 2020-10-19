using System;

namespace contas.api.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Rnd0(this decimal self)
        {
            return Math.Round(self, 0, MidpointRounding.AwayFromZero);
        }
        public static decimal Rnd2(this decimal self)
        {
            return Math.Round(self, 2, MidpointRounding.AwayFromZero);
        }
    }
}