using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using NUnit.Framework;
using TextAnalyzer.Analyzer;
using TextAnalyzer.Db;

namespace TextAnalyzerTest;

public class SqliteManagerTest
{
    private const string TestDbPath = "test.db";
    private IDbManager manager;

    [OneTimeSetUp]
    public void DbSetup()
    {
        if (File.Exists(TestDbPath)) File.Delete(TestDbPath);
        manager = new SqliteDb(TestDbPath);
    }

    [OneTimeTearDown]
    public void DbTearDown()
    {
        File.Delete(TestDbPath);
    }

    [Test]
    public void SaveRetrieveTest()
    {
        //  Explicit AnalyzerResult creation instead of using the Analyzer
        //  so the test doesn't fail by an error in AnalyzerManager

        var wordMap = new Dictionary<string, int>();
        wordMap.Add("hello", 1);
        
        var charMap = new Dictionary<string, int>();
        foreach (var c in "hello")
        {
            if (charMap.ContainsKey(c.ToString()))
            {
                charMap[c.ToString()] += 1;
            }
            else
            {
                charMap.Add(c.ToString(), 1);
            }
        }

        var testResult = new AnalyzerResult();
        testResult.SourceName = "saveRetrieveTest";
        testResult.ScanTime = DateTime.Now;
        testResult.TotalWordCount = 1;
        testResult.TotalCharCount = 5;
        testResult.LongestWord = "hello";
        testResult.HeatmapWord = wordMap;
        testResult.HeatmapChar = charMap;
        
        manager.SaveData(testResult);
        var retrieved = manager.GetScan(testResult.SourceName, testResult.ScanTime);
        Assert.NotNull(retrieved);
        // You forced me Nunit, this is your fault!
        Assert.AreEqual(testResult.SourceName, retrieved.SourceName);
        Assert.AreEqual(testResult.ScanTime, retrieved.ScanTime);
        Assert.AreEqual(testResult.TotalWordCount, retrieved.TotalWordCount);
        Assert.AreEqual(testResult.TotalCharCount, retrieved.TotalCharCount);
        Assert.AreEqual(testResult.LongestWord, retrieved.LongestWord);
        Assert.AreEqual(testResult.HeatmapWord, retrieved.HeatmapWord);
        Assert.AreEqual(testResult.HeatmapChar, retrieved.HeatmapChar);
    }

    [Test]
    public void RetrieveAll()
    {
        for (int i = 0; i < 10; i++)
        {
            var scan = new AnalyzerResult();
            scan.SourceName = $"retrieveAllTest{i}";
            scan.ScanTime = DateTime.Now;
            scan.LongestWord = "hello";
            scan.TotalCharCount = 5;
            scan.TotalWordCount = 1;
            var charmap = new Dictionary<string, int>();
            charmap.Add("h", 1);
            charmap.Add("e", 1);
            charmap.Add("l", 2);
            charmap.Add("o", 1);
            scan.HeatmapChar = charmap;
            var wordmap = new Dictionary<string, int>();
            wordmap.Add("hello", 1);
            scan.HeatmapWord = wordmap;
            
            manager.SaveData(scan);
        }

        var scanList = manager.GetAll();
        Assert.That(scanList, Is
            .Not.Contain(null)
            .And.Count.GreaterThanOrEqualTo(10)
        );
    }

    [Test]
    public void GetAllWithSource()
    {
        for (int i = 0; i < 5; i++)
        {
            var scan = new AnalyzerResult();
            scan.SourceName = $"GetWithSource1";
            scan.ScanTime = DateTime.Now;
            scan.LongestWord = "hello";
            scan.TotalCharCount = 5;
            scan.TotalWordCount = 1;
            var charmap = new Dictionary<string, int>();
            charmap.Add("h", 1);
            charmap.Add("e", 1);
            charmap.Add("l", 2);
            charmap.Add("o", 1);
            scan.HeatmapChar = charmap;
            var wordmap = new Dictionary<string, int>();
            wordmap.Add("hello", 1);
            scan.HeatmapWord = wordmap;
            
            manager.SaveData(scan);
        }
        for (int i = 0; i < 7; i++)
        {
            var scan = new AnalyzerResult();
            scan.SourceName = $"GetWithSource2";
            scan.ScanTime = DateTime.Now;
            scan.LongestWord = "hello";
            scan.TotalCharCount = 5;
            scan.TotalWordCount = 1;
            var charmap = new Dictionary<string, int>();
            charmap.Add("h", 1);
            charmap.Add("e", 1);
            charmap.Add("l", 2);
            charmap.Add("o", 1);
            scan.HeatmapChar = charmap;
            var wordmap = new Dictionary<string, int>();
            wordmap.Add("hello", 1);
            scan.HeatmapWord = wordmap;
            
            manager.SaveData(scan);
        }

        var scanlist1 = manager.GetWithSource("GetWithSource1");
        var scanlist2 = manager.GetWithSource("GetWithSource2");
        
        Assert.That(scanlist1, Has.Count.EqualTo(5));
        Assert.That(scanlist2, Has.Count.EqualTo(7));
    }
}