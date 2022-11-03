﻿namespace TextAnalyzer;

public class Ui
{
    internal static void PrintMenu()
    {
        Console.WriteLine("1. Analyze Texts");
        Console.WriteLine("2. Get text stats");
        Console.WriteLine("3. Analyze New Text");
        Console.WriteLine("4. Write your own text");
        Console.WriteLine("5. Exit");
    }

    internal static void PrintSaveOrDiscard() {
        Console.WriteLine("1. Save data");
        Console.WriteLine("2. Discard data");
    }

    internal static void ProgressBar()
    {
        Console.WriteLine("Analyzing Text ...");
        using (var progress = new ProgressBar())
        {
            for (int i = 0; i <= 100; i++)
            {
                progress.Report((double)i / 100);
                Thread.Sleep(20);
            }
        }
        Console.Clear();
    }

    internal static void PrintBackToMainMenu() {
        Console.WriteLine("1. Back to main menu");
    }
}