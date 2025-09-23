namespace Andy.Guard.Scanning;

public class ScanOptions
{
    public float Threshold { get; set; } = 0.5f;
    public bool IncludeMetadata { get; set; } = true;
    public int MaxTokenLength { get; set; } = 512;
}
