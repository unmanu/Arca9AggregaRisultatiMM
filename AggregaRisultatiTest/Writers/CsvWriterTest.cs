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
		string outputFilePath = _writer.Write(_directoryPath, [], [], []);

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
		SortedDictionary<string, DifferenceDto> differences = new();
		differences.Add("1", new()
		{
			AgenziaGestione = "001",
			CodiceProdotto = "961",
			CodiceSocieta = "341",
			Categoria = "11",
			NumeroCollettiva = "0",
			NumeroPolizza = "123456789",
			Errore = "errore",
			ErroreCics = @"errore cics
errore seconda riga",
			ErroreAlbedino = "errore albedino",
			IsParziale = false,
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
		File.Exists(outputFilePath).Should().BeTrue();

		using (var reader = new StreamReader(outputFilePath))
		using (var csv = new CsvHelper.CsvReader(reader, CreateConfiguration()))
		{
			csv.HeaderRecord?.Length.Should().Be(Aggregator.NUMBER_OF_FIELDS);
			var records = csv.GetRecords<CsvReadDto>();
			CsvReadDto firstRow = records.FirstOrDefault() ?? throw new Exception();
			firstRow.AgenziaGestione.Should().Be(differences.First().Value.AgenziaGestione);
			firstRow.CodiceProdotto.Should().Be(differences.First().Value.CodiceProdotto);
			firstRow.CodiceSocieta.Should().Be(differences.First().Value.CodiceSocieta);
			firstRow.Categoria.Should().Be(differences.First().Value.Categoria);
			firstRow.NumeroCollettiva.Should().Be(differences.First().Value.NumeroCollettiva);
			firstRow.NumeroPolizza.Should().Be(differences.First().Value.NumeroPolizza);
			firstRow.Errore.Should().Be(differences.First().Value.Errore);
			firstRow.TipoRiscatto.Should().Be("TOTALE");
			firstRow.ErroreCics.Should().Be(differences.First().Value.ErroreCics);
			firstRow.ErroreAlbedino.Should().Be(differences.First().Value.ErroreAlbedino);
			firstRow.AgenziaGestione.Should().Be(differences.First().Value.AgenziaGestione);
			firstRow.ImportoNettoCics.Should().Be(differences.First().Value.ImportoNettoCics);
			firstRow.ImportoLordoCics.Should().Be(differences.First().Value.ImportoLordoCics);
			firstRow.ImposteLordoCics.Should().Be(differences.First().Value.ImposteLordoCics);
			firstRow.ImportoNettoAlbedino.Should().Be(differences.First().Value.ImportoNettoAlbedino);
			firstRow.ImportoLordoAlbedino.Should().Be(differences.First().Value.ImportoLordoAlbedino);
			firstRow.ImposteLordoAlbedino.Should().Be(differences.First().Value.ImposteLordoAlbedino);
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
