using AggregaRisultati.Models;
using AggregaRisultati.Utils;
using System.Text;

namespace AggregaRisultati.Parsers;

public class TimesFileParser
{

	public SortedDictionary<string, TimesDto> Parse(FileInfo? timesFile)
	{
		SortedDictionary<string, TimesDto> times = new();
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
					TimesDto timesDto = blockParser.Parse(stringBuilder.ToString());
					bool added = times.TryAdd(KeyExtractor.Extract(timesDto) + "_" + timesDto.RiscattoTotale, timesDto);
					if (!added)
					{
						Console.WriteLine("Failed to insert a second row for " + KeyExtractor.Extract(timesDto));
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
			TimesDto timesDto = blockParser.Parse(stringBuilder.ToString());
			bool added = times.TryAdd(KeyExtractor.Extract(timesDto) + "_" + timesDto.RiscattoTotale, timesDto);
			if (!added)
			{
				Console.WriteLine("Failed to insert a second row for " + KeyExtractor.Extract(timesDto));
			}
			stringBuilder.Clear();
		}

		return times;
	}

}