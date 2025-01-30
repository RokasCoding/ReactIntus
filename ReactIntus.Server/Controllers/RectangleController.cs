using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Threading;
using ReactIntus.Server.Models;
using Microsoft.Extensions.Logging;


[ApiController]
[Route("api/[controller]")]
public class RectangleController : ControllerBase
{
    private readonly string _jsonFilePath;
    private readonly ILogger<RectangleController> _logger;

    public RectangleController(IWebHostEnvironment env, ILogger<RectangleController> logger)
    {
        _logger = logger;
        // Combine the web root path with the JSON file name
        _jsonFilePath = Path.Combine(env.WebRootPath, "rectangleDimensions.json");
        _logger.LogInformation($"RectangleDimensions.json path: {_jsonFilePath}");
    }

    [HttpGet]
    public async Task<IActionResult> GetDimensions()
    {
        if (!System.IO.File.Exists(_jsonFilePath))
        {
            // Initialize with default dimensions if the file doesn't exist
            var defaultDimensions = new RectangleDimensions { Width = 150, Height = 250 };
            var defaultJson = JsonSerializer.Serialize(defaultDimensions);
            await System.IO.File.WriteAllTextAsync(_jsonFilePath, defaultJson);
            _logger.LogInformation("Initialized rectangleDimensions.json with default values.");
        }

        try
        {
            var json = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            var dimensions = JsonSerializer.Deserialize<RectangleDimensions>(json);
            if (dimensions != null)
            {
                _logger.LogInformation($"Fetched dimensions: Width={dimensions.Width}, Height={dimensions.Height}");
                return Ok(dimensions);
            }
            else
            {
                _logger.LogError("Deserialized dimensions are null.");
                return StatusCode(500, "Failed to deserialize rectangle dimensions.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading or deserializing rectangleDimensions.json.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDimensions([FromBody] RectangleDimensions dimensions)
    {
        // Artificial delay to simulate long-lasting calculations
        await Task.Delay(10000);

        if (dimensions.Width > dimensions.Height)
        {
            return BadRequest("Width cannot exceed height.");
        }

        var json = JsonSerializer.Serialize(dimensions);
        await System.IO.File.WriteAllTextAsync(_jsonFilePath, json);
        return Ok(dimensions);
    }
}
