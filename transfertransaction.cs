using System;

namespace BankingSystem
{
    public class TransferTransaction : Transaction
    {
        private Account FromAccount { get; }
        private Account ToAccount { get; }
        private decimal Amount { get; }

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
        }

        public override void Execute()
        {
            if (Executed)
            {
                throw new InvalidOperationException("Transaction already executed.");
            }

            // Check if the fromAccount has enough balance
            if (FromAccount.Balance < Amount)
            {
                Console.WriteLine("Insufficient funds for transfer.");
                Success = false;
                Executed = true;
                return;
            }

            // Proceed with the transfer if there are enough funds
            FromAccount.Withdraw(Amount);
            ToAccount.Deposit(Amount);
            Success = true;
            Executed = true;
        }

        public override void Print()
        {
            if (Success)
            {
                Console.WriteLine($"Transfer of {Amount:C} from {FromAccount.Name} to {ToAccount.Name} successful.");
            }
            else
            {
                Console.WriteLine($"Transfer of {Amount:C} from {FromAccount.Name} to {ToAccount.Name} failed.");
            }
        }

        public override void Rollback()
        {
            if (!Executed)
            {
                throw new InvalidOperationException("Transaction not executed.");
            }

            if (Reversed)
            {
                throw new InvalidOperationException("Transaction already rolled back.");
            }

            ToAccount.Withdraw(Amount);
            FromAccount.Deposit(Amount);
            Reversed = true;
        }
    }
}
