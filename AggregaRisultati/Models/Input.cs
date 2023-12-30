namespace AggregaRisultati.Models;

public class Input(DirectoryInfo directory, FileInfo differencesFile, FileInfo? inputFile, FileInfo? timesFile)
{
	public DirectoryInfo Directory { get; set; } = directory;
	public FileInfo DifferencesFile { get; set; } = differencesFile;
	public FileInfo? InputFile { get; set; } = inputFile;
	public FileInfo? TimesFile { get; set; } = timesFile;

}