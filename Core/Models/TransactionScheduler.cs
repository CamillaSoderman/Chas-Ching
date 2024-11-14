using Chas_Ching.Core.Enums;
using System.Timers;

namespace Chas_Ching.Core.Models
{
    public class TransactionScheduler
    {
        private readonly Queue<Transaction> pendingTransactions; // Holds transactions waiting to be processed, in FIFO order.
        private System.Timers.Timer transactionTimer; // Timer to schedule transaction processing. The intervall is fixed
        private readonly TimeSpan processingInterval; // Defines the interval between transaction processing
        private readonly object locker; // Ensures thread when locking the queue for processing transactions
        public static int TransactionDelayMinutes { get; set; } = 15; // Transaction delay in minutes (15 min referense to backlog)

        public static DateTime GetExpectedCompletionTime(DateTime transactionDate)
        {   // Helper method to calculate the expected completion time of a transaction
            return transactionDate.AddMinutes(TransactionDelayMinutes);
        }

        public TransactionScheduler()
        {
            pendingTransactions = new Queue<Transaction>();
            processingInterval = TimeSpan.FromMinutes(TransactionDelayMinutes); // Set the processing time interval
            locker = new object();
        }

        public void EnqueueTransaction(Transaction transaction)
        {
            lock (locker)
            {
                pendingTransactions.Enqueue(transaction);
                TransactionLog.LogTransaction(transaction);
            }
        }

        public void Start()
        {
            transactionTimer = new System.Timers.Timer(processingInterval.TotalMilliseconds);
            transactionTimer.Elapsed += OnTimerElapsed;
            transactionTimer.AutoReset = true;
            transactionTimer.Enabled = true;
            Console.WriteLine("Transaction scheduler started.");
        }

        public void Stop()
        {
            if (transactionTimer != null)
            {
                transactionTimer.Stop();
                transactionTimer.Dispose();
                transactionTimer = null;
                Console.WriteLine("Transaction scheduler stopped.");
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ProcessPendingTransactions();
        }

        public void ProcessPendingTransactions()
        {
            lock (locker)
            {
                List<Transaction> retryTransactions = new List<Transaction>();

                while (pendingTransactions.Count > 0)
                {
                    Transaction transaction = pendingTransactions.Dequeue();

                    if ((DateTime.Now - transaction.Date).TotalMinutes >= TransactionDelayMinutes) // If "TransactionDelayMinutes" minutes have passed since the transaction was created
                    {
                        try
                        {
                            transaction.ProcessTransaction();

                            if (transaction.Status == TransactionStatus.Completed)
                            {
                                Console.WriteLine($"Transaction {transaction.TransactionId} processed successfully.");
                                TransactionLog.LogTransaction(transaction);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.RetryCount++;
                            Console.WriteLine($"Transaction {transaction.TransactionId} failed: {ex.Message}.");

                            if (transaction.RetryCount < Transaction.MaxRetries)
                            {
                                retryTransactions.Add(transaction);
                            }
                            else
                            {
                                Console.WriteLine($"Transaction {transaction.TransactionId} reached max retry limit");
                                transaction.Status = TransactionStatus.Failed;
                                TransactionLog.LogTransaction(transaction);
                            }
                        }
                    }
                    foreach (var failedTransaction in retryTransactions)
                    {
                        pendingTransactions.Enqueue(failedTransaction);
                    }
                }
            }
        }
    }
}