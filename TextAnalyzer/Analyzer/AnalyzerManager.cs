namespace TextAnalyzer.Analyzer; 

public class AnalyzerManager {

    private static Queue<string> Text { get; set; } = new();
    private Thread[] Threads { get; }
    private AnalyzerResult[] Results { get; }

    public AnalyzerManager(Queue<string> text, int threadCount = 1) {
        Threads = new Thread[threadCount];
        Results = new AnalyzerResult[threadCount];
        Text = text;
    }

    public AnalyzerResult StartAnalyze() {
        
        // Parallel.For(0, Threads.Length, i => {
        //     Results[i] = new AnalyzerResult();
        //     var analyzer = new AnalyzerThread(Results[i], Text);
        //     Threads[i] = new Thread(analyzer.Start);
        //     Threads[i].Start();
        // });

        for (int i = 0; i < Threads.Length; i++) {
            Results[i] = new AnalyzerResult();
            var analyzer = new AnalyzerThread(Results[i], Text);
            
            Threads[i] = new Thread(analyzer.Start);
            Threads[i].Start();
        }

        var isRunning = true;
        while (isRunning) {
            int counter = 0;
            
            for (int i = 0; i < Threads.Length; i++) {
                if (!Threads[i].IsAlive) counter++;
            }

            if (counter == Threads.Length) isRunning = false;
            else counter = 0; 
        }
        
        var finishedResult = new AnalyzerResult();
        foreach (var result in Results) {
            finishedResult += result;
        }
        
        return finishedResult;
    }

}