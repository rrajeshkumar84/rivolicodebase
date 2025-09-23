using Andy.Guard.Scanning;
using Andy.Guard.Scanning.OutputScannerRegistry;
using Andy.Guard.Scanning.OutputScanners;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers;

[ApiController]
[Route("api/output-scan")]
public class OutputScanController : ControllerBase
{
    private readonly IOutputScannerRegistry _registry;

    public OutputScanController(IOutputScannerRegistry registry)
    {
        _registry = registry;
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
        var results = await _registry.ScanAsync(request.Text);
        var result = results.FirstOrDefault();
        if (result == null)
            return Ok(new ScanResponse { IsThreatDetected = false, ConfidenceScore = 0, RiskLevel = "Low" });
        return Ok(new ScanResponse
        {
            IsThreatDetected = result.IsThreatDetected,
            ConfidenceScore = result.ConfidenceScore,
            RiskLevel = result.RiskLevel.ToString()
        });
    }
}
