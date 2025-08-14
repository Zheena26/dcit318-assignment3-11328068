using WarehouseInventory.Services;

class Program
{
    static void Main()
    {
        var manager = new WareHouseManager();
        manager.Run();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
