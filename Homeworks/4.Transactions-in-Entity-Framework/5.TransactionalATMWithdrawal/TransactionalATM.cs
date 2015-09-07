using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMDatabase;

namespace TransactionalATMWithdrawal
{
    class TransactionalATM
    {
        static void Main()
        {
            MoneyWithdraw();
        }

        private static void MoneyWithdraw()
        {
            var context = new ATMEntities();
            using (var dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
            {
                try
                {
                    Console.WriteLine("Enter valid PIN");
                    string cardPin = Console.ReadLine();

                    Console.WriteLine("Enter valid Card Number");
                    string cardNumber = Console.ReadLine();

                    Console.WriteLine("Enter amount of money to withdrawal");
                    decimal withdrawalMoney = decimal.Parse(Console.ReadLine());

                    if (cardPin.Length != 4 || cardNumber.Length != 10 || withdrawalMoney < 0)
                    {
                        throw new ArgumentException("The requested arguments are with not valid length or are null");
                    }

                    var allAccounts = context.CardAccounts;

                    if (allAccounts.All(c => c.CardPIN != cardPin))
                    {
                        throw new ArgumentException("Invalid PIN !");
                    }

                    if (allAccounts.All(c => c.CardNumber != cardNumber))
                    {
                        throw new ArgumentException("Invalid Card Number !");
                    }

                    var account = context.CardAccounts
                        .Where(c => c.CardNumber == cardNumber);

                    if (withdrawalMoney > account.First().CardCash)
                    {
                        throw new ArgumentException("Not enough money in account");
                    }

                    account.First().CardCash -= withdrawalMoney;
                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
