using System;
using System.Collections.Generic;
using System.Timers; 
using Chas_Ching.Core.Models; 

namespace Chas_Ching.Core.Models
{
    public class TransactionScheduler
    {
        private readonly Queue<Transaction> pendingTransactions;
        private System.Timers.Timer transactionTimer;
        private readonly TimeSpan processingInterval;
        private readonly object locker;

        public TransactionScheduler()
        {
            pendingTransactions = new Queue<Transaction>();
            processingInterval = TimeSpan.FromMinutes(0.1);
            locker = new object();
        }
        public void EnqueueTransaction(Transaction transaction)
        {
            lock (locker)
            {
                pendingTransactions.Enqueue(transaction);
                Console.WriteLine($"Transaction: {transaction.TransactionId} enqued at {DateTime.Now}");
            }
        }
        public void Start()
        {
            transactionTimer = new System.Timers.Timer(processingInterval.TotalMilliseconds);
            transactionTimer.Elapsed += OnTimerElapsed;
            transactionTimer.AutoReset = true;
            transactionTimer.Enabled = true;
            Console.WriteLine("Transaction schedular started.");
        }
        public void Stop()
        {
            if (transactionTimer != null)
            {
                transactionTimer.Stop();
                transactionTimer.Dispose();
                transactionTimer = null;
                Console.WriteLine("Transaction schedular stopped.");
            }
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Processing pending transactions at {DateTime.Now}");
            ProcessPendingTransactions();
        }

        public void ProcessPendingTransactions()
        {
            List<Transaction> failedTransactions = new List<Transaction>();

            lock (locker)
            {
                while(pendingTransactions.Count > 0)
                {
                    Transaction transaction = pendingTransactions.Dequeue();
                    try
                    {
                        transaction.ProcessTransaction();
                        Console.WriteLine($"Transaction {transaction.TransactionId} processed successfully.");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Transaction {transaction.TransactionId} failed: {ex.Message}.");
                        transaction.Status = Transaction.TransactionStatus.Failed;
                        failedTransactions.Add(transaction);
                    }
                }
                foreach (var failedTransaction in failedTransactions) //Infinite retries, fix
                {
                    pendingTransactions.Enqueue(failedTransaction);
                }
            }
        }
    }
}