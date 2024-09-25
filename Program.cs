using System;

namespace BankingSystem
{
    class BankSystem
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            // Add default accounts
            bank.AddAccount(new Account("Arushi", 1000)); // Default account for Arushi with balance 1000
            bank.AddAccount(new Account("Mia", 500)); // Default account for Mia with balance 500
            bank.AddAccount(new Account("Ansh", 200)); // Default account for Ansh with balance 200

            while (true)
            {
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Print Account");
                Console.WriteLine("6. Print Transaction History");
                Console.WriteLine("7. Rollback Transaction");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DoAddAccount(bank);
                        break;
                    case "2":
                        DoDeposit(bank);
                        break;
                    case "3":
                        DoWithdraw(bank);
                        break;
                    case "4":
                        DoTransfer(bank);
                        break;
                    case "5":
                        DoPrint(bank);
                        break;
                    case "6":
                        PrintTransactionHistory(bank);
                        break;
                    case "7":
                        DoRollback(bank);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void DoAddAccount(Bank bank)
        {
            Console.Write("Enter account name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid account name.");
                return;
            }

            Console.Write("Enter initial balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal balance))
            {
                if (balance < 0)
                {
                    Console.WriteLine("Initial balance cannot be negative.");
                    return;
                }

                Account newAccount = new Account(name, balance);
                bank.AddAccount(newAccount);
                Console.WriteLine($"Account for {name} added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid initial balance.");
            }
        }

        static void DoDeposit(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter amount to deposit: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    DepositTransaction depositTransaction = new DepositTransaction(account, amount);
                    bank.ExecuteTransaction(depositTransaction);
                    depositTransaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
        }

        static void DoWithdraw(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account != null)
            {
                Console.Write("Enter amount to withdraw: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);
                    bank.ExecuteTransaction(withdrawTransaction);
                    withdrawTransaction.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount entered.");
                }
            }
        }

        static void DoTransfer(Bank bank)
        {
            Console.WriteLine("Enter the name of the account to debit: ");
            Account? fromAccount = FindAccount(bank);
            if (fromAccount != null)
            {
                Console.WriteLine("Enter the name of the account to credit: ");
                Account? toAccount = FindAccount(bank);
                if (toAccount != null)
                {
                    Console.Write("Enter amount to transfer: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
                        bank.ExecuteTransaction(transferTransaction);
                        transferTransaction.Print();
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered.");
                    }
                }
            }
        }

        static void DoPrint(Bank bank)
        {
            Account? account = FindAccount(bank);
            if (account != null)
            {
                account.Print();
            }
        }

        static Account? FindAccount(Bank bank)
        {
            Console.Write("Enter account name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid account name.");
                return null;
            }
            return bank.GetAccount(name);
        }

        static void PrintTransactionHistory(Bank bank)
        {
            bank.PrintTransactionHistory();

            Console.WriteLine("Do you want to rollback a transaction? Enter its index, or -1 to cancel.");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index >= 0)
                {
                    try
                    {
                        bank.RollbackTransaction(bank.GetTransactions()[index - 1]); // Adjust index to be zero-based
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Rollback failed: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void DoRollback(Bank bank)
        {
            bank.PrintTransactionHistory();
            Console.Write("Enter the index of the transaction to rollback: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= bank.GetTransactions().Count)
            {
                try
                {
                    bank.RollbackTransaction(bank.GetTransactions()[index - 1]); // Adjust index to be zero-based
                    Console.WriteLine("Transaction rolled back successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Rollback failed: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid index. Please try again.");
            }
        }
    }
}
