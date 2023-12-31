using AggregaRisultati.Models;
using AggregaRisultati.Writers;
using ClosedXML.Excel;

namespace AggregaRisultatiTest.Parsers;

public class ExcelWriterTest
{

	private readonly ExcelWriter _writer;
	private readonly DirectoryInfo _directoryPath;

	public ExcelWriterTest()
	{
		_writer = new ExcelWriter();
		_directoryPath = new(Path.Combine("Resources", "Output"));
		if (_directoryPath.Exists)
		{
			_directoryPath.Delete(true);
		}
		_directoryPath.Create();
	}

	[Fact]
	public void Write_NoDifference_ReturnsPathToExcelWithOnlyHeader()
	{
		string outputFilePath = _writer.Write(_directoryPath, [], [], []);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		using var workbook = new XLWorkbook(outputFilePath);
		workbook.Worksheets.Count().Should().Be(1);
		var worksheet = workbook.Worksheet(1);
		worksheet.Should().NotBeNull();
		worksheet.Name.Should().Be("differenze");
		var lastCellUsedAddress = worksheet.LastCellUsed().Address;
		lastCellUsedAddress.RowNumber.Should().Be(1);
	}

	[Fact]
	public void Write_MultipleDifferences_ReturnsPathToExcelWithMultipleRows()
	{
		SortedDictionary<string, DifferenceDto> differences = new();
		differences.Add("1", new()
		{
			NumeroPolizza = "123456789"
		});
		differences.Add("2", new()
		{
			NumeroPolizza = "111222333"
		});
		differences.Add("3", new()
		{
			NumeroPolizza = "444555666"
		});

		string outputFilePath = _writer.Write(_directoryPath, differences, [], []);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		using var workbook = new XLWorkbook(outputFilePath);
		workbook.Worksheets.Count().Should().Be(1);
		var worksheet = workbook.Worksheet(1);
		worksheet.Should().NotBeNull();
		worksheet.Name.Should().Be("differenze");
		var lastCellUsedAddress = worksheet.LastCellUsed().Address;
		lastCellUsedAddress.RowNumber.Should().Be(4);
	}

	[Fact]
	public void Write_SpecificDifference_ReturnsPathToExcelWithAllTheDataOfTheDifference()
	{
		SortedDictionary<string, DifferenceDto> differences = new();
		differences.Add("1", new()
		{
				AgenziaGestione = "001",
				CodiceProdotto = "961",
				CodiceSocieta = "341",
				Categoria = "11",
				NumeroCollettiva = "0",
				NumeroPolizza = "123456789",
				ErroreCics = @"errore cics
errore seconda riga",
				ErroreAlbedino = "errore albedino",
				ImportoNettoCics = "5.500",
				ImportoLordoCics = "0.000",
				ImposteLordoCics = "",
				ImportoNettoAlbedino = "15923.301",
				ImportoLordoAlbedino = "1.000",
				ImposteLordoAlbedino = "0.000"
			}
		);

		string outputFilePath = _writer.Write(_directoryPath, differences, [], []);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		using var workbook = new XLWorkbook(outputFilePath);
		workbook.Worksheets.Count().Should().Be(1);
		var worksheet = workbook.Worksheet(1);
		worksheet.Should().NotBeNull();
		worksheet.Name.Should().Be("differenze");
		var lastCellUsedAddress = worksheet.LastCellUsed().Address;
		lastCellUsedAddress.RowNumber.Should().Be(2);
		int currentRow = 2;
		DifferenceDto difference = differences.First().Value;
		worksheet.Cell(currentRow, 3).GetText().Should().Be(difference.AgenziaGestione);
		worksheet.Cell(currentRow, 4).GetText().Should().Be(difference.CodiceProdotto);
		worksheet.Cell(currentRow, 5).GetText().Should().Be(difference.CodiceSocieta);
		worksheet.Cell(currentRow, 6).GetText().Should().Be(difference.Categoria);
		worksheet.Cell(currentRow, 7).GetText().Should().Be(difference.NumeroCollettiva);
		worksheet.Cell(currentRow, 8).GetText().Should().Be(difference.NumeroPolizza);
		var multilineErrorWorksheet = NormalizeNewLine(worksheet.Cell(currentRow, 20).Value.GetText());
		var multilineErrorInput = NormalizeNewLine(difference.ErroreCics);
		multilineErrorWorksheet.Should().Be(multilineErrorInput);
		worksheet.Cell(currentRow, 21).GetText().Should().Be(difference.ErroreAlbedino);
		worksheet.Cell(currentRow, 14).GetText().Should().Be(difference.ImportoNettoCics);
		worksheet.Cell(currentRow, 15).GetText().Should().Be(difference.ImportoLordoCics);
		worksheet.Cell(currentRow, 16).IsEmpty().Should().BeTrue();
		worksheet.Cell(currentRow, 17).GetText().Should().Be(difference.ImportoNettoAlbedino);
		worksheet.Cell(currentRow, 18).GetText().Should().Be(difference.ImportoLordoAlbedino);
		worksheet.Cell(currentRow, 19).GetText().Should().Be(difference.ImposteLordoAlbedino);
	}

	private string NormalizeNewLine(string? text)
	{
		if (text == null)
		{
			return "";
		}
		return text.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n");
	}
}
