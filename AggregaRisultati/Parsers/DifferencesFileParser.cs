using AggregaRisultati.Models;
using System.Text;

namespace AggregaRisultati.Parsers;

public class DifferencesFileParser
{

	public List<DifferenceDto> Parse(FileInfo differencesFile)
	{
		List<DifferenceDto> differences = new();
		StringBuilder stringBuilder = new();
		DifferencesBlockParser blockParser = new();

		var lines = File.ReadLines(differencesFile.FullName);
		foreach (var line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (stringBuilder.Length > 0)
				{
					differences.Add(blockParser.Parse(stringBuilder.ToString()));
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
			differences.Add(blockParser.Parse(stringBuilder.ToString()));
			stringBuilder.Clear();
		}

		return differences;
	}

}
