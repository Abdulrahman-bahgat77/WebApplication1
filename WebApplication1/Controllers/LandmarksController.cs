using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class LandmarksController : ControllerBase
{
    private readonly LandmarkService _landmarkService;

    public LandmarksController(LandmarkService landmarkService)
    {
        _landmarkService = landmarkService;
    }

    [HttpGet("classical")]
    public IActionResult GetClassicalHighlights()
    {
        var landmarks = _landmarkService.GetLandmarks("/Users/Abdo/Desktop/task/model2/egypt_landmarks.xlsx");
        var classicalHighlights = landmarks.Where(l => l.Type == "Ancient Ruins" || l.Type == "Historic").ToList();
        return Ok(classicalHighlights);
    }

    [HttpGet("tourist")]
    public IActionResult GetTouristHighlights()
    {
        var landmarks = _landmarkService.GetLandmarks("/Users/Abdo/Desktop/task/model2/egypt_landmarks.xlsx");
        var touristHighlights = landmarks.Where(l => l.Type == "Landmark" || l.Type == "Religious" || l.Type == "Amusement").ToList();
        return Ok(touristHighlights);
    }
}