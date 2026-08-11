using POSInventorySystem.Models;

Console.WriteLine("=================================");
Console.WriteLine("      PAYMENT METHODS TEST");
Console.WriteLine("=================================");
Console.WriteLine();

Payment payment;

payment = new CashPayment(300.00m, 500.00m);
payment.ProcessPayment();

Console.WriteLine();

payment = new GCashPayment(250.00m);
payment.ProcessPayment();

Console.WriteLine();

payment = new CardPayment(450.00m);
payment.ProcessPayment();