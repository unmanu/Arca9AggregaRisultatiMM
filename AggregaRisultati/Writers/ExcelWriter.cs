using AggregaRisultati.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AggregaRisultati.Writers;

public class ExcelWriter : IWriter
{
	public string Write(DirectoryInfo directory, SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput, SortedDictionary<string, TimesDto> times)
	{
		string outputFile = Path.Combine(directory.FullName, "Differenze.xlsx");
		using var workbook = new XLWorkbook();
		var worksheet = workbook.AddWorksheet("differenze");
		int currentRow = 1;
		WriteHeader(worksheet, currentRow++);
		if (polizzeInput.Any())
		{
			AggregateAll(worksheet, differences, polizzeInput, times, currentRow);
		}
		else
		{
			AggregateOnlyDifferences(worksheet, differences, currentRow);
		}
		worksheet.Columns().AdjustToContents();
		worksheet.SheetView.FreezeRows(1);
		workbook.SaveAs(outputFile);
		return outputFile;
	}

	private static void AggregateAll(IXLWorksheet worksheet, SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput, SortedDictionary<string, TimesDto> times, int currentRow)
	{
		SortedSet<string> keys = GetKeys(differences, polizzeInput);
		foreach (string key in keys)
		{
			differences.TryGetValue(key, out DifferenceDto? difference);
			polizzeInput.TryGetValue(key, out PolizzaInputDto? polizza);
			times.TryGetValue(key + "_True", out TimesDto? timesTotale);
			times.TryGetValue(key + "_False", out TimesDto? timesParziale);

			Aggregator headerAggregator = new(difference, polizza, timesTotale, timesParziale);
			WriteAggregation(worksheet, currentRow, headerAggregator, false);
			AdjustRowHeight(worksheet, currentRow);
			currentRow++;
		}
	}

	private static void AdjustRowHeight(IXLWorksheet worksheet, int currentRow)
	{
		worksheet.Row(currentRow).AdjustToContents();
		if (worksheet.Row(currentRow).Height > 100)
		{
			worksheet.Row(currentRow).Height = 100;
		}
	}

	private static SortedSet<string> GetKeys(SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput)
	{
		SortedSet<string> keys = new();
		foreach (var item in differences)
		{
			keys.Add(item.Key);
		}
		foreach (var item in polizzeInput)
		{
			keys.Add(item.Key);
		}
		return keys;
	}

	private void AggregateOnlyDifferences(IXLWorksheet worksheet, SortedDictionary<string, DifferenceDto> differences, int currentRow)
	{
		foreach (var difference in differences)
		{
			Aggregator headerAggregator = new(difference.Value);
			WriteAggregation(worksheet, currentRow, headerAggregator, false);
			AdjustRowHeight(worksheet, currentRow);
			currentRow++;
		}
	}

	private void WriteHeader(IXLWorksheet worksheet, int currentRow)
	{
		Aggregator headerAggregator = new();
		WriteAggregation(worksheet, currentRow, headerAggregator, true);
		worksheet.Row(currentRow).Style.Font.SetBold(true);
		worksheet.Row(currentRow).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
		worksheet.Row(currentRow).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
		worksheet.Row(currentRow).AdjustToContents();
		worksheet.Range(currentRow, 1, currentRow, Aggregator.NUMBER_OF_FIELDS).SetAutoFilter(true);
	}
	private static void WriteAggregation(IXLWorksheet worksheet, int currentRow, Aggregator headerAggregator, bool header)
	{
		for (int i = 0; i < Aggregator.NUMBER_OF_FIELDS; i++)
		{
			string value = headerAggregator.Aggregate(i, header);
			worksheet.Cell(currentRow, i + 1).Value = value;
		}
	}
}