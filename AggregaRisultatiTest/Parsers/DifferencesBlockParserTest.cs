using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class DifferencesBlockParserTest
{
	private readonly DifferencesBlockParser _parser;

	public DifferencesBlockParserTest()
	{
		_parser = new DifferencesBlockParser();
	}

	[Fact]
	public void Parse_AlbedinoError_ReturnsDifferenceWithAlbedinoError()
	{
		string block = ReadBlock("albedino-error.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("700");
		difference.CodiceProdotto.Should().Be("919");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("1110856");
		difference.ImportoNettoCics.Should().Be("751.152");
		difference.ImportoLordoCics.Should().Be("761.502");
		difference.ImposteLordoCics.Should().Be("10.350");
		difference.ErroreCics.Should().BeNull();
		difference.ImportoNettoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ImportoLordoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ImposteLordoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ErroreAlbedino.Should().Be("Errore Albedino");
	}

	[Fact]
	public void Parse_AlbedinoErrorException_ReturnsDifferenceWithAlbedinoError()
	{
		string block = ReadBlock("albedino-error-exception.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("700");
		difference.CodiceProdotto.Should().Be("919");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("1110857");
		difference.ImportoNettoCics.Should().Be("751.152");
		difference.ImportoLordoCics.Should().Be("761.502");
		difference.ImposteLordoCics.Should().Be("10.350");
		difference.ErroreCics.Should().BeNull();
		difference.ImportoNettoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ImportoLordoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ImposteLordoAlbedino.Should().NotBeNull().And.BeEmpty();
		difference.ErroreAlbedino.Should().Be(@"Errore albedino
	at com.1
	at com.2
Caused by ...
	at sun.1
	...");
	}

	[Fact]
	public void Parse_CicsAlbedinoErrorException_ReturnsDifferenceWithCicsAndAlbedinoError()
	{
		string block = ReadBlock("cics-albedino-error-exception.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("001");
		difference.CodiceProdotto.Should().Be("961");
		difference.CodiceSocieta.Should().Be("700");
		difference.Categoria.Should().Be("21");
		difference.NumeroCollettiva.Should().Be("10");
		difference.NumeroPolizza.Should().Be("1337262");
		difference.ImportoNettoCics.Should().Be("0.000");
		difference.ImportoLordoCics.Should().NotBeNull().And.BeEmpty();
		difference.ImposteLordoCics.Should().Be("0.000");
		difference.ErroreCics.Should().Be("Errore cics");
		difference.ImportoNettoAlbedino.Should().Be("18970.680");
		difference.ImportoLordoAlbedino.Should().Be("18970.680");
		difference.ImposteLordoAlbedino.Should().Be("0.000");
		difference.ErroreAlbedino.Should().Be(@"Errore albedino
	at com.1
	at com.2
Caused by ...
	at sun.1
	...");
	}


	[Fact]
	public void Parse_CicsError_ReturnsDifferenceWithCicsError()
	{
		string block = ReadBlock("cics-error.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("001");
		difference.CodiceProdotto.Should().Be("961");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("1337263");
		difference.ImportoNettoCics.Should().Be("0.000");
		difference.ImportoLordoCics.Should().NotBeNull().And.BeEmpty();
		difference.ImposteLordoCics.Should().Be("0.000");
		difference.ErroreCics.Should().Be("Errore cics");
		difference.ImportoNettoAlbedino.Should().Be("18970.680");
		difference.ImportoLordoAlbedino.Should().Be("18970.680");
		difference.ImposteLordoAlbedino.Should().Be("0.000");
		difference.ErroreAlbedino.Should().BeNull();
	}


	[Fact]
	public void Parse_CicsErrorException_ReturnsDifferenceWithCicsError()
	{
		string block = ReadBlock("cics-error-exception.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("001");
		difference.CodiceProdotto.Should().Be("961");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("1337264");
		difference.ImportoNettoCics.Should().Be("0.000");
		difference.ImportoLordoCics.Should().NotBeNull().And.BeEmpty();
		difference.ImposteLordoCics.Should().Be("0.000");
		difference.ErroreCics.Should().Be(@"Errore cics
	at com.1
	at com.2
Caused by ...
	at sun.1
	...");
		difference.ImportoNettoAlbedino.Should().Be("18970.680");
		difference.ImportoLordoAlbedino.Should().Be("18970.680");
		difference.ImposteLordoAlbedino.Should().Be("0.000");
		difference.ErroreAlbedino.Should().BeNull();
	}


	[Fact]
	public void Parse_CicsKo_ReturnsDifferenceWithCicsError()
	{
		string block = ReadBlock("cics-ko.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("209");
		difference.CodiceProdotto.Should().Be("761");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("895306");
		difference.ImportoNettoCics.Should().BeNull();
		difference.ImportoLordoCics.Should().BeNull();
		difference.ImposteLordoCics.Should().BeNull();
		difference.ErroreCics.Should().Be(@"com.ibm.connector2.cics.CICSTxnAbendException: CTG9638E Transaction Abend occurred in CICS. Abend Code=: AZI6, error code: AZI6
	at com.ibm.connector2.cics.ECIManagedConnection.checkReturnCode(Unknown Source)
	at com.ibm.connector2.cics.ECIManagedConnection.call(Unknown Source)
	at com.ibm.connector2.cics.ECIConnection.call(Unknown Source)
	at com.ibm.connector2.cics.ECIInteraction.execute(Unknown Source)
	at com.arca.ecci.runtime.CicsProgramCaller.execute(CicsProgramCaller.java:104)
	at com.arca.riscatto.riv04.CalcoloImportiRiscattoParziale.calcola(CalcoloImportiRiscattoParziale.java:28)
	at com.arca.riscatto.riv04.RegressioneTestExecuter.run(RegressioneTestExecuter.java:51)
	at java.lang.Thread.run(Unknown Source)");
		difference.ImportoNettoAlbedino.Should().BeNull();
		difference.ImportoLordoAlbedino.Should().BeNull();
		difference.ImposteLordoAlbedino.Should().BeNull();
		difference.ErroreAlbedino.Should().BeNull();
	}

	[Fact]
	public void Parse_DifferentValues_ReturnsDifferenceWithCicsAndAlbedinoError()
	{
		string block = ReadBlock("different-values.txt");

		DifferenceDto difference = _parser.Parse(block);

		difference.Should().NotBeNull();
		difference.AgenziaGestione.Should().Be("145");
		difference.CodiceProdotto.Should().Be("750");
		difference.CodiceSocieta.Should().Be("341");
		difference.Categoria.Should().Be("11");
		difference.NumeroCollettiva.Should().Be("0");
		difference.NumeroPolizza.Should().Be("890284");
		difference.ImportoNettoCics.Should().Be("26677.790");
		difference.ImportoLordoCics.Should().Be("26677.780");
		difference.ImposteLordoCics.Should().Be("0.000");
		difference.ErroreCics.Should().BeNull();
		difference.ImportoNettoAlbedino.Should().Be("26677.790");
		difference.ImportoLordoAlbedino.Should().Be("26677.790");
		difference.ImposteLordoAlbedino.Should().Be("0.000");
		difference.ErroreAlbedino.Should().BeNull();
	}

	private static string ReadBlock(string fileName)
	{
		string filePath = Path.Combine("Resources", "Differences", fileName);
		return File.ReadAllText(filePath);
	}
}
