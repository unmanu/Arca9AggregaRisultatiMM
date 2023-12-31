using AggregaRisultati.Models;
using System.Text;

namespace AggregaRisultati.Parsers;

public class TimesFileParser
{

	public List<TimesDto> Parse(FileInfo? timesFile)
	{
		List<TimesDto> times = new();
		if (timesFile == null)
		{
			return times;
		}
		StringBuilder stringBuilder = new();
		TimesBlockParser blockParser = new();

		var lines = File.ReadLines(timesFile.FullName);
		foreach (var line in lines)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				if (stringBuilder.Length > 0)
				{
					times.Add(blockParser.Parse(stringBuilder.ToString()));
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
			times.Add(blockParser.Parse(stringBuilder.ToString()));
			stringBuilder.Clear();
		}

		return times;
	}

}