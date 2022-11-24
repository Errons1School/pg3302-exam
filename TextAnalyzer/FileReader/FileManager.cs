using TextAnalyzer.UI;

namespace TextAnalyzer;

/// <summary>
/// Handles txt files by storing them in Queue's, displays them from directories and return them
/// </summary>
public class FileManager
{
    private List<string>? _textFileArrayList;
    private List<string>? _textFileNames;
    private int _selectedFile;
    private bool _notValidInput;

    /// <summary>
    /// Reads .txt files and adds strings separated by an empty space to a queue.
    /// </summary>
    /// <param name="filepath">takes in a file from  bin/debug/net6.0/resources</param>
    /// <returns>a list of strings in a queue</returns>
    public static IEnumerator<string> GetText(string filepath)
    {
        IEnumerable<string> text = File.ReadLines(filepath);
        return text.GetEnumerator();
    }

    /// <summary>
    /// Displays files that are located in the resources directory
    /// checks in user input is valid based on files from directory
    /// </summary>
    internal void DisplayStoredFiles()
    {
        _notValidInput = true;
        _textFileArrayList = new List<string>();
        _textFileNames = new List<string>();

        IOManager.ClearConsole();
        IOManager.Write("Display texts that arent analysed.");
        var directoryInfo = new DirectoryInfo("Resources"); //Insert directory
        FileInfo[] files = directoryInfo.GetFiles("*.txt"); //Get files the end with .txt
        var counter = 0;
        foreach (FileInfo file in files)
        {
            counter++;
            Console.WriteLine(counter + ". " + file.Name);
            _textFileNames.Add(file.Name);
            _textFileArrayList.Add(file.FullName);
        }

        IOManager.Write("\nType in menu option number and press <Enter> to analyse text");

        var inputInt = Convert.ToInt32(Console.ReadLine());
        while (_notValidInput)
        {
            if (inputInt > _textFileArrayList.Count || inputInt <= 0)
            {
                Console.WriteLine("Input to high, try again:");
                inputInt = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                _notValidInput = false;
                Console.Clear();
            }

            _selectedFile = inputInt;
        }
    }

    /// <summary>
    /// Checks if there are existing files added previously
    /// </summary>
    /// <returns> returns selected file path with user input </returns>
    public string GetSelectedFile()
    {
        if (_textFileArrayList != null) return _textFileArrayList[_selectedFile - 1];
        return "No files are stored on disk";
    }


    /// <summary>
    /// Checks if there are existing file names added previously
    /// </summary>
    /// <returns> returns selected file name with user input </returns>
    public string RetriveAllFileNames()
    {
        if (_textFileNames != null) return _textFileNames[_selectedFile - 1];
        return "No files are stored on disk";
    }
}