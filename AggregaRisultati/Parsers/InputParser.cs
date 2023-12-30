using AggregaRisultati.Models;

namespace AggregaRisultati.Parsers;

public class InputParser
{
	public const string FolderNotFound = "Folder {0} not found";
	public const string DifferencesFileNotFound = "Differences file not found in folder {0}";
	public const string DifferencesFileName = "polizze-riscattabili-con-differenze.txt";
	public const string InputFileName = "polizze-riscattabili-input.txt";
	public const string TimesFileName = "polizze-riscattabili-tempi-albedino.txt";

	public Input Parse(string[] args)
	{
		string folderPath = GetFolderPathOrDefaultToCurrentDirectory(args);
		DirectoryInfo directory = GetFolderIfExists(folderPath);
		FileInfo differencesFile = FindDifferencesFileIfExists(folderPath, directory);
		FileInfo? inputFile = directory.GetFiles().FirstOrDefault(x => x.Name == InputFileName);
		FileInfo? timesFile = directory.GetFiles().FirstOrDefault(x => x.Name == TimesFileName);
		return new(directory, differencesFile, inputFile, timesFile);
	}

	private static string GetFolderPathOrDefaultToCurrentDirectory(string[] args)
	{
		return args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
	}

	private static DirectoryInfo GetFolderIfExists(string folderPath)
	{
		DirectoryInfo directory = new(folderPath);
		if (!directory.Exists)
		{
			throw new InputValidationException(string.Format(FolderNotFound, folderPath));
		}

		return directory;
	}

	private static FileInfo FindDifferencesFileIfExists(string folderPath, DirectoryInfo directory)
	{
		FileInfo? file = directory.GetFiles().FirstOrDefault(x => x.Name == DifferencesFileName);
		if (file == null || !file.Exists)
		{
			throw new InputValidationException(string.Format(DifferencesFileNotFound, folderPath));
		}
		return file;
	}
}
