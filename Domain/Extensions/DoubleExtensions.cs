using System;

namespace contas.api.Domain.Extensions
{
    public static class DoubleExtensions
    {
        public static double Rnd0(this double self)
        {
            return Math.Round(self, 0, MidpointRounding.AwayFromZero);
        }
        public static double Rnd2(this double self)
        {
            return Math.Round(self, 2, MidpointRounding.AwayFromZero);
        }
        public static double Rnd4(this double self)
        {
            return Math.Round(self, 4, MidpointRounding.AwayFromZero);
        }
        public static double Rnd6(this double self)
        {
            return Math.Round(self, 6, MidpointRounding.AwayFromZero);
        }
        public static double Rnd14(this double self)
        {
            return Math.Round(self, 14, MidpointRounding.AwayFromZero);
        }
    }
}