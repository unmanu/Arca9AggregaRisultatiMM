using AggregaRisultati.Models;
using AggregaRisultati.Utils;
using System.Text;

namespace AggregaRisultati.Parsers;

public class DifferencesFileParser
{

	public SortedDictionary<string, DifferenceDto> Parse(FileInfo differencesFile)
	{
		SortedDictionary<string, DifferenceDto> differences = new();
		StringBuilder stringBuilder = new();
		DifferencesBlockParser blockParser = new();

		var lines = File.ReadLines(differencesFile.FullName);
		foreach (var line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (stringBuilder.Length > 0)
				{
					DifferenceDto differenceDto = blockParser.Parse(stringBuilder.ToString());
					bool added = differences.TryAdd(KeyExtractor.Extract(differenceDto), differenceDto);
					if (!added)
					{
						Console.WriteLine("Failed to insert a second row for " + KeyExtractor.Extract(differenceDto));
					}
					stringBuilder.Clear();
				}
			}
			else
			{
				stringBuilder.AppendLine(line);
			}
		}
		if (stringBuilder.Length > 0)
		{
			DifferenceDto differenceDto = blockParser.Parse(stringBuilder.ToString());
			bool added = differences.TryAdd(KeyExtractor.Extract(differenceDto), differenceDto);
			if (!added)
			{
				Console.WriteLine("Failed to insert a second row for " + KeyExtractor.Extract(differenceDto));
			}
			stringBuilder.Clear();
		}

		return differences;
	}

}
