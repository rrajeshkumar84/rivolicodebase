using Andy.Guard.Scanning;
using Andy.Guard.Scanning.InputScanners;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/prompt-scan")]
public class PromptScanController : ControllerBase
{
    private readonly IInputScanner _scanner;

    public PromptScanController(IInputScanner scanner)
    {
        _scanner = scanner;
    }

    public class ScanRequest
    {
        public string Text { get; set; } = string.Empty;
    }

    public class ScanResponse
    {
        public bool IsThreatDetected { get; set; }
        public float ConfidenceScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<ActionResult<ScanResponse>> Scan([FromBody] ScanRequest request)
    {
        var result = await _scanner.ScanAsync(request.Text);
        return Ok(new ScanResponse
        {
            IsThreatDetected = result.IsThreatDetected,
            ConfidenceScore = result.ConfidenceScore,
            RiskLevel = result.RiskLevel.ToString()
        });
    }
}
