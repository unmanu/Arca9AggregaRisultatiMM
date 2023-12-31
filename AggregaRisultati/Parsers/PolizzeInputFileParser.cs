using AggregaRisultati.Models;
using AggregaRisultati.Utils;

namespace AggregaRisultati.Parsers;

public class PolizzeInputFileParser
{

	public SortedDictionary<string, PolizzaInputDto> Parse(FileInfo? polizzeInputFile)
	{
		SortedDictionary<string, PolizzaInputDto> polizze = new();
		if (polizzeInputFile == null)
		{
			return polizze;
		}

		var lines = File.ReadLines(polizzeInputFile.FullName);
		int rowNumber = 0;
		foreach (var line in lines)
		{
			rowNumber++;
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			PolizzaInputDto polizzaInputDto = CreatePolizzeInput(line, rowNumber);
			bool added = polizze.TryAdd(KeyExtractor.Extract(polizzaInputDto), polizzaInputDto);
			if (!added)
			{
				Console.WriteLine("Failed to insert a second row for " + KeyExtractor.Extract(polizzaInputDto));
			}

		}

		return polizze;
	}

	private PolizzaInputDto CreatePolizzeInput(string line, int rowNumber)
	{
		PolizzaInputDto polizza = new();
		string[] splits = line.Split(";");
		polizza.NumeroRiga = rowNumber.ToString();
		polizza.Input = line;
		polizza.AgenziaGestione = TrimIfPresent(splits, 0);
		polizza.CodiceProdotto = TrimIfPresent(splits, 1);
		polizza.CodiceSocieta = TrimIfPresent(splits, 2);
		polizza.Categoria = TrimIfPresent(splits, 3);
		polizza.NumeroCollettiva = TrimIfPresent(splits, 4);
		polizza.NumeroPolizza = TrimIfPresent(splits, 5);
		polizza.DataDecorrenza = TrimIfPresent(splits, 6);
		polizza.TipiRiscatto = TrimIfPresent(splits, 7);
		polizza.Convenzione = TrimIfPresent(splits, 8);
		return polizza;
	}

	private string TrimIfPresent(string[] splits, int index)
	{
		if (splits.Length <= index)
		{
			return "";
		}
		return splits[index].Trim();
	}
}