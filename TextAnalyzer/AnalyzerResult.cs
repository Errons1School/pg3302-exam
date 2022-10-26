namespace TextAnalyzer; 

public class AnalyzerResult {
    
    public AnalyzerResult() {
        // FileSize = 0;
        TotalWordCount = 0;
        TotalCharCount = 0;
        LongestWord = "";
        HeatmapWord = new Dictionary<string, int>();
        HeatmapChar = new Dictionary<string, int>();
    }

    // public int FileSize { get; set; }
    public int TotalWordCount { get; set; }
    public int TotalCharCount { get; set; }
    public string LongestWord { get; set; }
    public Dictionary<string, int> HeatmapWord { get; set; }
    public Dictionary<string, int> HeatmapChar { get; set; }

}