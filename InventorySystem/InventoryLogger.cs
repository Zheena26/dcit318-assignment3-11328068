using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class InventoryLogger<T> where T : IInventoryEntity
{
	private readonly List<T> _log = new();
	private readonly string _filePath;

	public InventoryLogger(string filePath)
	{
		_filePath = filePath;
	}

	public void Add(T item)
	{
		_log.Add(item);
		Console.WriteLine($"? Item added: {item}");
	}

	public List<T> GetAll() => new List<T>(_log);

	public void SaveToFile()
	{
		try
		{
			string json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
			Console.WriteLine($"?? Data saved to {_filePath}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"? Error saving file: {ex.Message}");
		}
	}

	public void LoadFromFile()
	{
		try
		{
			if (File.Exists(_filePath))
			{
				string json = File.ReadAllText(_filePath);
				var items = JsonSerializer.Deserialize<List<T>>(json);
				_log.Clear();
				if (items != null) _log.AddRange(items);
				Console.WriteLine($"?? Data loaded from {_filePath}");
			}
			else
			{
				Console.WriteLine("? No saved file found.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"? Error loading file: {ex.Message}");
		}
	}
}
