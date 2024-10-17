
using MagicSquaresAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISquareService
{
    string GenerateRandomColor();
    Task<bool> SaveSquares(List<Square> squares);
    Task<List<Square>> GetSquares();
    Task<bool> ClearSquares();
}
