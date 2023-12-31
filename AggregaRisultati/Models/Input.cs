namespace AggregaRisultati.Models;

public class Input(DirectoryInfo directory, FileInfo differencesFile, FileInfo? polizzeInputFile, FileInfo? timesFile)
{
	public DirectoryInfo Directory { get; set; } = directory;
	public FileInfo DifferencesFile { get; set; } = differencesFile;
	public FileInfo? PolizzeInputFile { get; set; } = polizzeInputFile;
	public FileInfo? TimesFile { get; set; } = timesFile;

}