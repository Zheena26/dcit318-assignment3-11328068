using System;
using System.Collections.Generic;

public class InventoryApp
{
    private readonly InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n?? Inventory Management System");
            Console.WriteLine("1?? Add new item");
            Console.WriteLine("2?? View all items");
            Console.WriteLine("3?? Save items to file");
            Console.WriteLine("4?? Load items from file");
            Console.WriteLine("5?? Exit");
            Console.Write("? Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddItem();
                    break;
                case "2":
                    PrintAllItems();
                    break;
                case "3":
                    _logger.SaveToFile();
                    break;
                case "4":
                    _logger.LoadFromFile();
                    break;
                case "5":
                    Console.WriteLine("?? Exiting program...");
                    return;
                default:
                    Console.WriteLine("? Invalid choice. Try again.");
                    break;
            }
        }
    }

    private void AddItem()
    {
        try
        {
            Console.Write("Enter Item ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine() ?? "Unknown";

            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine() ?? "0");

            var item = new InventoryItem(id, name, qty, DateTime.Now);
            _logger.Add(item);
        }
        catch (FormatException)
        {
            Console.WriteLine("? Invalid input format. Please enter correct values.");
        }
    }

    private void PrintAllItems()
    {
        var items = _logger.GetAll();
        if (items.Count == 0)
        {
            Console.WriteLine("? No items found.");
            return;
        }

        Console.WriteLine("\n?? Inventory List:");
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Qty: {item.Quantity}, Date Added: {item.DateAdded}");
        }
    }
}
