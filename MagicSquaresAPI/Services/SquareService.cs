using MagicSquaresAPI.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class SquareService : ISquareService
{
    private static Random _random = new Random();
    private static string _lastColor = "";
    private const string _jsonFilePath = "squares.json";

    public string GenerateRandomColor()
    {
        string color;
        do
        {
            color = $"#{_random.Next(0x1000000):X6}";
        } while (color == _lastColor || color == "#FFFFFF");

        _lastColor = color;
        return color;
    }

    public async Task<bool> SaveSquares(List<Square> squares)
    {
        try
        {
            var jsonData = JsonSerializer.Serialize(squares);
            await File.WriteAllTextAsync(_jsonFilePath, jsonData);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<Square>> GetSquares()
    {
        if (!File.Exists(_jsonFilePath))
        {
            return new List<Square>();
        }

        var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
        return JsonSerializer.Deserialize<List<Square>>(jsonData);
    }

    public async Task<bool> ClearSquares()
    {
        try
        {
            await File.WriteAllTextAsync(_jsonFilePath, JsonSerializer.Serialize(new List<Square>()));
            return true;
        }
        catch
        {
            return false;
        }
    }
}
