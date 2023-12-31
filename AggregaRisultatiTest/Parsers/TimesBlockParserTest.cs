using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class TimesBlockParserTest
{
	private readonly TimesBlockParser _parser;

	public TimesBlockParserTest()
	{
		_parser = new TimesBlockParser();
	}

	[Fact]
	public void Parse_Parziale_ReturnsTimesOfParziale()
	{
		string block = ReadBlock("parziale.txt");

		TimesDto times = _parser.Parse(block);

		times.Should().NotBeNull();
		times.NumeroPolizza.Should().Be("1337636");
		times.NumeroCollettiva.Should().Be("0");
		times.Categoria.Should().Be("11");
		times.CodiceSocieta.Should().Be("341");
		times.CodiceAgenzia.Should().BeEmpty();
		times.RiscattoTotale.Should().BeFalse();
		times.ImportoRiscattoParziale.Should().Be("548580.508");
		times.Inizio.Should().Be("2023-12-25 00:09:53.159");
		times.Fine.Should().Be("2023-12-25 00:09:53.347");
		times.TempoImpiegato.Should().Be("00:00.188");
		times.Millisecondi.Should().Be("188");
	}

	[Fact]
	public void Parse_Totale_ReturnsTimesOfParziale()
	{
		string block = ReadBlock("totale.txt");

		TimesDto times = _parser.Parse(block);

		times.Should().NotBeNull();
		times.NumeroPolizza.Should().Be("1337636");
		times.NumeroCollettiva.Should().Be("0");
		times.Categoria.Should().Be("11");
		times.CodiceSocieta.Should().Be("341");
		times.CodiceAgenzia.Should().Be("5");
		times.RiscattoTotale.Should().BeTrue();
		times.ImportoRiscattoParziale.Should().BeEmpty();
		times.Inizio.Should().Be("2023-12-25 00:09:52.597");
		times.Fine.Should().Be("2023-12-25 00:09:53.050");
		times.TempoImpiegato.Should().Be("00:00.453");
		times.Millisecondi.Should().Be("453");
	}

	[Fact]
	public void Parse_WrongLinesNumber_ReturnsEmptyTimes()
	{
		string block = ReadBlock("numero-linee-errato.txt");

		TimesDto times = _parser.Parse(block);

		times.Should().NotBeNull();
		times.NumeroPolizza.Should().BeNull();
		times.NumeroCollettiva.Should().BeNull();
		times.Categoria.Should().BeNull();
		times.CodiceSocieta.Should().BeNull();
		times.CodiceAgenzia.Should().BeNull();
		times.RiscattoTotale.Should().BeNull();
		times.ImportoRiscattoParziale.Should().BeNull();
		times.Inizio.Should().BeNull();
		times.Fine.Should().BeNull();
		times.TempoImpiegato.Should().BeNull();
		times.Millisecondi.Should().BeNull();
	}


	[Fact]
	public void Parse_ParsingThrowsException_ReturnsEmptyTimes()
	{
		string block = ReadBlock("unparseable.txt");

		TimesDto times = _parser.Parse(block);

		times.Should().NotBeNull();
		times.NumeroPolizza.Should().BeNull();
		times.NumeroCollettiva.Should().BeNull();
		times.Categoria.Should().BeNull();
		times.CodiceSocieta.Should().BeNull();
		times.CodiceAgenzia.Should().BeNull();
		times.RiscattoTotale.Should().BeNull();
		times.ImportoRiscattoParziale.Should().BeNull();
		times.Inizio.Should().BeNull();
		times.Fine.Should().BeNull();
		times.TempoImpiegato.Should().BeNull();
		times.Millisecondi.Should().BeNull();
	}


	private static string ReadBlock(string fileName)
	{
		string filePath = Path.Combine("Resources", "Times", fileName);
		return File.ReadAllText(filePath);
	}
}
