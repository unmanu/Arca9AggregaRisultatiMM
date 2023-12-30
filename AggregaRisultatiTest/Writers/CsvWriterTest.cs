using AggregaRisultati.Models;
using AggregaRisultati.Writers;
using CsvHelper.Configuration;
using System.Globalization;

namespace AggregaRisultatiTest.Writers;

public class CsvWriterTest
{


	private readonly CsvWriter _writer;
	private readonly DirectoryInfo _directoryPath;

	public CsvWriterTest()
	{
		_writer = new CsvWriter();
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
		string outputFilePath = _writer.Write(_directoryPath, []);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		File.Exists(outputFilePath).Should().BeTrue();
		using (var reader = new StreamReader(outputFilePath))
		using (var csv = new CsvHelper.CsvReader(reader, CreateConfiguration()))
		{
			csv.HeaderRecord?.Length.Should().Be(Aggregator.NUMBER_OF_FIELDS);
			var records = csv.GetRecords<CsvReadDto>();
			records.Count().Should().Be(0);
		}
	}

	[Fact]
	public void Write_MultipleDifferences_ReturnsPathToExcelWithMultipleRows()
	{
		List<DifferenceDto> differences = [
			new()
			{
				NumeroPolizza = "123456789"
			},
			new()
			{
				NumeroPolizza = "111222333"
			},
			new()
			{
				NumeroPolizza = "444555666"
			}
		];

		string outputFilePath = _writer.Write(_directoryPath, differences);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		File.Exists(outputFilePath).Should().BeTrue();

		using (var reader = new StreamReader(outputFilePath))
		using (var csv = new CsvHelper.CsvReader(reader, CreateConfiguration()))
		{
			csv.HeaderRecord?.Length.Should().Be(Aggregator.NUMBER_OF_FIELDS);
			var records = csv.GetRecords<CsvReadDto>();
			records.Count().Should().Be(3);
		}
	}

	[Fact]
	public void Write_SpecificDifference_ReturnsPathToExcelWithAllTheDataOfTheDifference()
	{
		List<DifferenceDto> differences = [
			new()
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
		];

		string outputFilePath = _writer.Write(_directoryPath, differences);

		outputFilePath.Should().NotBeNull().And.NotBeEmpty();
		File.Exists(outputFilePath).Should().BeTrue();

		using (var reader = new StreamReader(outputFilePath))
		using (var csv = new CsvHelper.CsvReader(reader, CreateConfiguration()))
		{
			csv.HeaderRecord?.Length.Should().Be(Aggregator.NUMBER_OF_FIELDS);
			var records = csv.GetRecords<CsvReadDto>();
			CsvReadDto firstRow = records.FirstOrDefault() ?? throw new Exception();
			firstRow.AgenziaGestione.Should().Be(differences[0].AgenziaGestione);
			firstRow.CodiceProdotto.Should().Be(differences[0].CodiceProdotto);
			firstRow.CodiceSocieta.Should().Be(differences[0].CodiceSocieta);
			firstRow.Categoria.Should().Be(differences[0].Categoria);
			firstRow.NumeroCollettiva.Should().Be(differences[0].NumeroCollettiva);
			firstRow.NumeroPolizza.Should().Be(differences[0].NumeroPolizza);
			firstRow.ErroreCics.Should().Be(differences[0].ErroreCics);
			firstRow.ErroreAlbedino.Should().Be(differences[0].ErroreAlbedino);
			firstRow.AgenziaGestione.Should().Be(differences[0].AgenziaGestione);
			firstRow.ImportoNettoCics.Should().Be(differences[0].ImportoNettoCics);
			firstRow.ImportoLordoCics.Should().Be(differences[0].ImportoLordoCics);
			firstRow.ImposteLordoCics.Should().Be(differences[0].ImposteLordoCics);
			firstRow.ImportoNettoAlbedino.Should().Be(differences[0].ImportoNettoAlbedino);
			firstRow.ImportoLordoAlbedino.Should().Be(differences[0].ImportoLordoAlbedino);
			firstRow.ImposteLordoAlbedino.Should().Be(differences[0].ImposteLordoAlbedino);
		}
	}

	private static CsvConfiguration CreateConfiguration()
	{
		return new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			Delimiter = ";",
			Quote = '"'
		};
	}
}
