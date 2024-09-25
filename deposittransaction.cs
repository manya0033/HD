using System;

namespace BankingSystem
{
    public class DepositTransaction : Transaction
    {
        private Account _account;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override void Execute()
        {
            if (Executed)
            {
                throw new InvalidOperationException("Transaction has already been attempted.");
            }

            Executed = true;
            Success = _account.Deposit(_amount);
            SetDateStamp();
        }

        public override void Rollback()
        {
            if (!Executed || Reversed)
            {
                throw new InvalidOperationException("Transaction has not been finalized or has already been reversed.");
            }

            Reversed = _account.Withdraw(_amount);
            if (!Reversed)
            {
                throw new InvalidOperationException("Rollback failed.");
            }

            SetDateStamp();
        }

        public override void Print()
        {
            Console.WriteLine($"Deposit Transaction: {_amount:C} to {_account.Name}'s account");
            base.Print();
        }
    }
}
