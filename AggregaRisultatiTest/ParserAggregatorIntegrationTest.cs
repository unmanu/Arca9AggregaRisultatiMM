using AggregaRisultati;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest;

public class ParserAggregatorIntegrationTest
{

	private readonly DirectoryInfo _directoryPath;

	public ParserAggregatorIntegrationTest()
	{
		_directoryPath = new(Path.Combine("Resources", "Integration", "KindaBig"));
		foreach (var file in _directoryPath.GetFiles())
		{
			if (file.Name != InputParser.DifferencesFileName && file.Name != InputParser.InputFileName && file.Name != InputParser.TimesFileName)
			{
				file.Delete();
			}
		}
	}

	[Fact]
	public void Write_NoDifference_ReturnsPathToExcelWithOnlyHeader()
	{
		ParserAggregator.Aggregate([_directoryPath.FullName]);
	}
}
