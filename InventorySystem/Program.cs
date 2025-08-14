using System;

public class Program
{
    public static void Main()
    {
        Console.Write("📂 Enter file name to store data (e.g., inventory.json): ");
        string filePath = Console.ReadLine() ?? "inventory.json";

        var app = new InventoryApp(filePath);
        app.Run();
    }
}
