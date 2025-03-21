namespace DISS_2.BackEnd.Files;

public static class FileSaver
{
    public static string SaveToFile<T>(List<T> lines, string fileName = "")
    {
        Directory.CreateDirectory(Config.TesterOutputDir);

        if (fileName != "")
        {
            fileName += "_";
        }

        string timestamp = DateTime.Now.ToString("yyyy.MM.dd_HH:mm:ss");
        fileName += $"{timestamp}.txt";

        string filePath = Path.Combine(Config.TesterOutputDir, fileName);

        using StreamWriter writer = new StreamWriter(filePath);

        foreach (T line in lines)
        {
            writer.WriteLine(line?.ToString());
        }

        return filePath;
    }
}