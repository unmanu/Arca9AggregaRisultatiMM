namespace AggregaRisultati.Models;

public class Input(DirectoryInfo directory, FileInfo differencesFile)
{
	public DirectoryInfo Directory { get; set; } = directory;
	public FileInfo DifferencesFile { get; set; } = differencesFile;

}