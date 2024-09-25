using System;

namespace BankingSystem
{
    public abstract class Transaction
    {
        // Protected fields
        protected decimal _amount;
        protected bool _success;
        protected bool _executed;
        protected bool _reversed;
        protected DateTime _dateStamp;

        // Read-only properties
        public bool Success { get => _success; set => _success = value; }
        public bool Executed { get => _executed; set => _executed = value; }
        public bool Reversed { get => _reversed; set => _reversed = value; }
        public DateTime DateStamp => _dateStamp;

        // Constructor to initialize amount
        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        // Abstract methods that need to be implemented by derived classes
        public abstract void Execute();
        public abstract void Rollback();

        // Virtual method that can be overridden by child classes
        public virtual void Print()
        {
            Console.WriteLine($"Transaction Amount: {_amount:C}");
            Console.WriteLine($"Executed: {_executed}, Success: {_success}, Reversed: {_reversed}");
            Console.WriteLine($"Date: {_dateStamp}");
        }

        // Set the date stamp to the current time when a transaction is executed or reversed
        protected void SetDateStamp()
        {
            _dateStamp = DateTime.Now;
        }
    }
}
