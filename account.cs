using System;

namespace BankingSystem
{
    public class Account
    {
        private decimal _balance;
        private string _name;

        public string Name => _name;
        public decimal Balance => _balance;

        public Account(string name, decimal balance)
        {
            if (balance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }
            _name = name;
            _balance = balance;
        }

        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                Console.WriteLine($"Deposited {amount:C} to {Name}'s account.");
                return true;
            }
            else
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
                return false;
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew {amount:C} from {Name}'s account.");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return false;
            }
        }

        public void Print()
        {
            Console.WriteLine($"Account Name: {Name}");
            Console.WriteLine($"Balance: {_balance:C}");
        }
    }
}
