using AggregaRisultati.Models;
using AggregaRisultati.Utils;

namespace AggregaRisultati.Writers;

public class CsvWriter : IWriter
{
	public string Write(DirectoryInfo directory, List<DifferenceDto> differences, List<PolizzaInputDto> polizzeInput, List<TimesDto> times)
	{
		string outputFile = Path.Combine(directory.FullName, "Differenze.csv");

		using (var writer = new StreamWriter(outputFile))
		{
			Aggregator headerAggregator = new();
			WriteLine(writer, headerAggregator, true);
			if (polizzeInput.Any())
			{
				AggregateAll(differences, polizzeInput, times, writer);
			}
			else
			{
				AggregateOnlyDifferences(differences, writer);
			}
		}
		return outputFile;
	}

	private static void AggregateAll(List<DifferenceDto> differences, List<PolizzaInputDto> polizzeInput, List<TimesDto> times, StreamWriter writer)
	{
		SortedSet<string> keys = GetKeys(differences, polizzeInput, times);
		int count = 0;
		foreach (string key in keys)
		{
			count++;
			DifferenceDto? difference = differences.FirstOrDefault(x => key == KeyExtractor.Extract(x));
			PolizzaInputDto? polizza = polizzeInput.FirstOrDefault(x => key == KeyExtractor.Extract(x));
			List<TimesDto> timesForKey = times.Where(x => key == KeyExtractor.Extract(x) && x.RiscattoTotale != null).ToList();
			TimesDto? timesTotale = timesForKey.FirstOrDefault(x => x.RiscattoTotale ?? false);
			TimesDto? timesParziale = timesForKey.FirstOrDefault(x => !x.RiscattoTotale ?? false);

			Aggregator aggregator = new(difference, polizza, timesTotale, timesParziale);
			WriteLine(writer, aggregator, false);
		}
	}

	private static SortedSet<string> GetKeys(List<DifferenceDto> differences, List<PolizzaInputDto> polizzeInput, List<TimesDto> times)
	{
		SortedSet<string> keys = new();
		foreach (DifferenceDto item in differences)
		{
			keys.Add(KeyExtractor.Extract(item));
		}
		foreach (PolizzaInputDto item in polizzeInput)
		{
			keys.Add(KeyExtractor.Extract(item));
		}
		foreach (TimesDto item in times)
		{
			keys.Add(KeyExtractor.Extract(item));
		}
		return keys;
	}

	private static void AggregateOnlyDifferences(List<DifferenceDto> differences, StreamWriter writer)
	{
		foreach (DifferenceDto difference in differences)
		{
			Aggregator aggregator = new(difference);
			WriteLine(writer, aggregator, false);
		}
	}

	private static void WriteLine(StreamWriter writer, Aggregator aggregator, bool header)
	{
		for (int i = 0; i < Aggregator.NUMBER_OF_FIELDS; i++)
		{
			string separator = i == 0 ? "" : ";";
			string value = aggregator.Aggregate(i, header);
			writer.Write(separator + Normalize(value));
		}
		writer.WriteLine();
		writer.Flush();
	}

	private static string Normalize(string value)
	{
		return "\"" + value.Replace("\"", "\"\"") + "\"";
	}
}