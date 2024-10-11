using System;
using System.Collections.Generic;

/// <summary>
/// Maintain a Customer Service Queue. Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        var cs = new CustomerService(3);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine(cs);
        cs.ServeCustomer();
        Console.WriteLine(cs);
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        // Ensure the max size is greater than zero
        _maxSize = maxSize > 0 ? maxSize : 10;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information. Put the 
    /// new record into the queue.
    /// </summary>
    public void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum number of customers in the queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()?.Trim() ?? "Unknown";
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()?.Trim() ?? "Unknown";
        Console.Write("Problem: ");
        var problem = Console.ReadLine()?.Trim() ?? "Unknown";

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    public void ServeCustomer() {
        // Ensure there is a customer to serve
        if (_queue.Count == 0) {
            Console.WriteLine("No customers to serve.");
            return;
        }

        // Get the next customer in line, display their information, and remove them
        var customer = _queue[0];
        Console.WriteLine($"Serving customer: {customer}");
        _queue.RemoveAt(0);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[Queue Size: {_queue.Count}/{_maxSize}] Customers: " + string.Join(", ", _queue);
    }

    // Testing the CustomerService class
    public void TestingCustomerService() {
        var cs = new CustomerService(2);

        // Test 1: Add two customers, check if both are in the queue
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine(cs); // Should print two customers in queue

        // Test 2: Try to add a third customer (should fail due to max size)
        cs.AddNewCustomer(); // Should print an error message

        // Test 3: Serve one customer and check the queue
        cs.ServeCustomer();
        Console.WriteLine(cs); // Should print one customer left
    }
}
