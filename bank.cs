using System;
using System.Collections.Generic;

namespace BankingSystem
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();
        private List<Transaction> transactions = new List<Transaction>();

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        public Account? GetAccount(string name)
        {
            return accounts.Find(a => a.Name == name);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            if (transaction.Success)
            {
                transactions.Add(transaction); // Log successful transactions
            }
        }

        public void PrintTransactionHistory()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transaction history available.");
                return;
            }

            Console.WriteLine("Transaction History:");
            for (int i = 0; i < transactions.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                transactions[i].Print();
            }
        }

        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
            Console.WriteLine("Transaction rolled back.");
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }
    }
}
