using System;

namespace BankingSystem
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
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
            Success = _account.Withdraw(_amount);
            SetDateStamp();
        }

        public override void Rollback()
        {
            if (!Executed || Reversed)
            {
                throw new InvalidOperationException("Transaction has not been finalized or has already been reversed.");
            }

            Reversed = _account.Deposit(_amount);
            if (!Reversed)
            {
                throw new InvalidOperationException("Rollback failed.");
            }

            SetDateStamp();
        }

        public override void Print()
        {
            Console.WriteLine($"Withdraw Transaction: {_amount:C} from {_account.Name}'s account");
            base.Print();
        }
    }
}
