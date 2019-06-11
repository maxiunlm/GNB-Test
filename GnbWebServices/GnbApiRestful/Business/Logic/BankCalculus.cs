using System;

namespace Business
{
    public class BankCalculus : IBankCalculus
    {
        private const int digits = 2;

        public decimal RoundBank(decimal amount)
        {
            return Math.Round(amount, digits, MidpointRounding.AwayFromZero);
        }
    }
}