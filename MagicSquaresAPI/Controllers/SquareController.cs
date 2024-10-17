using MagicSquaresAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SquareController : ControllerBase
{
    private readonly ISquareService _squareService;

    public SquareController(ISquareService squareService)
    {
        _squareService = squareService;
    }

    [HttpGet("randomcolor")]
    public IActionResult GetRandomColor()
    {
        var color = _squareService.GenerateRandomColor();
        return Ok(new { color });
    }

    [HttpPost("squares")]
    public async Task<IActionResult> SaveSquares([FromBody] List<Square> squares)
    {
        var result = await _squareService.SaveSquares(squares);
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }
        return Ok();
    }

    [HttpGet("squares")]
    public async Task<IActionResult> GetSquares()
    {
        var squares = await _squareService.GetSquares();
        return Ok(squares);
    }

    [HttpPost("squares/clear")]
    public async Task<IActionResult> ClearSquares()
    {
        var result = await _squareService.ClearSquares();
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }
        return Ok();
    }
}
