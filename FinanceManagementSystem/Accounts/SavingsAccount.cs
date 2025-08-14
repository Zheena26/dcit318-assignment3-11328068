using System;

namespace FinanceManagementSystem
{
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance) { }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("[SavingsAccount] Insufficient funds for transaction.");
            }
            else
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"[SavingsAccount] Transaction successful. New balance: {Balance:C}");
            }
        }
    }
}
