using System;
using System.Collections.Generic;

namespace FinanceManagementSystem
{
    public class FinanceApp
    {
        private List<Transaction> _transactions = new();

        public void Run()
        {
            Console.WriteLine("=== Finance Management System ===");

            Console.Write("Enter Account Number: ");
            string accNumber = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            decimal initialBalance = Convert.ToDecimal(Console.ReadLine());

            SavingsAccount account = new SavingsAccount(accNumber, initialBalance);

            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\n--- Transaction {i} ---");
                Console.Write("Enter Amount: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                Transaction transaction = new Transaction(i, DateTime.Now, amount, category);

                ITransactionProcessor processor = i switch
                {
                    1 => new MobileMoneyProcessor(),
                    2 => new BankTransferProcessor(),
                    _ => new CryptoWalletProcessor(),
                };

                processor.Process(transaction);
                account.ApplyTransaction(transaction);
                _transactions.Add(transaction);
            }

            Console.WriteLine("\n=== Transaction Summary ===");
            foreach (var t in _transactions)
            {
                Console.WriteLine($"{t.Date}: {t.Category} - {t.Amount:C}");
            }
        }
    }
}
