using System;

namespace refactor_gym_warmup_2020.cashier
{
    public interface IDateTimeProvider
    {
        public DateTime Now();
    }
}