﻿using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class PolizzeInputFileParserTest
{

	private readonly PolizzeInputFileParser _parser;

	public PolizzeInputFileParserTest()
	{
		_parser = new PolizzeInputFileParser();
	}

	[Fact]
	public void Parse_NoInputFile_ReturnsEmptyList()
	{
		SortedDictionary<string, PolizzaInputDto> polizze = _parser.Parse(null);

		polizze.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	public void Parse_MultipleLinesWithEmptyLines_ReturnsListWithoutEmptyLines()
	{
		FileInfo file = new(Path.Combine("Resources", "PolizzeInput", "multiple-lines-with-empty-lines.txt"));

		SortedDictionary<string, PolizzaInputDto> polizze = _parser.Parse(file);

		polizze.Should().NotBeNull().And.NotBeEmpty();
		polizze.Count.Should().Be(2);
	}

	[Fact]
	public void Parse_SingleLine_ReturnsPolizzaWithSpecificValues()
	{
		FileInfo file = new(Path.Combine("Resources", "PolizzeInput", "single-line.txt"));

		SortedDictionary<string, PolizzaInputDto> polizze = _parser.Parse(file);

		polizze.Should().NotBeNull().And.NotBeEmpty();
		polizze.Count.Should().Be(1);
		PolizzaInputDto firstPolizza = polizze.First().Value;
		firstPolizza.NumeroRiga.Should().Be("1");
		firstPolizza.Input.Should().Be("073   ;961       ;341;11;0;1337523;2022-12-19;NO0102                        ;073961");
		firstPolizza.AgenziaGestione.Should().Be("073");
		firstPolizza.CodiceProdotto.Should().Be("961");
		firstPolizza.CodiceSocieta.Should().Be("341");
		firstPolizza.Categoria.Should().Be("11");
		firstPolizza.NumeroCollettiva.Should().Be("0");
		firstPolizza.NumeroPolizza.Should().Be("1337523");
		firstPolizza.DataDecorrenza.Should().Be("2022-12-19");
		firstPolizza.TipiRiscatto.Should().Be("NO0102");
		firstPolizza.Convenzione.Should().Be("073961");
	}
}
