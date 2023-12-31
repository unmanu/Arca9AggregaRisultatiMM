using AggregaRisultati.Models;
using System.Text.RegularExpressions;

namespace AggregaRisultati.Parsers;

public class TimesBlockParser
{

	public TimesDto Parse(string block)
	{
		try
		{
			return DoParse(block);
		}
		catch (Exception)
		{
			return new();
		}
	}

	private TimesDto DoParse(string block)
	{
		TimesDto times = new();
		string[] lines = Regex.Split(block, "\r\n|\r|\n");
		lines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
		if (lines.Length != 2)
		{
			return times;
		}
		AddPolizzaKey(times, lines[0]);
		AddTimesData(times, lines[1]);

		return times;
	}

	private void AddPolizzaKey(TimesDto times, string line)
	{
		int indexOfNumeroPolizza = line.IndexOf("numeroPolizza\":");
		int indexOfNumeroCollettiva = line.IndexOf("numeroCollettiva\":");
		int indexOfCategoria = line.IndexOf("sottoCategoria\":");
		int indexOfCodiceSocieta = line.IndexOf("codiceSocieta\":");
		int indexOfCodiceAgenzia = line.IndexOf("codiceAgenzia\":");
		int indexOfRiscattoTotale = line.IndexOf("riscattoTotale\":");
		int indexOfImportoParziale = line.IndexOf("importoRiscattoParziale\":");
		int indexOfEnd = line.Length;

		times.NumeroPolizza = ExtractAndClean(line, indexOfNumeroPolizza, indexOfNumeroCollettiva);
		times.NumeroCollettiva = ExtractAndClean(line, indexOfNumeroCollettiva, indexOfCategoria);
		times.Categoria = ExtractAndClean(line, indexOfCategoria, indexOfCodiceSocieta);
		times.CodiceSocieta = ExtractAndClean(line, indexOfCodiceSocieta, indexOfCodiceAgenzia);
		times.CodiceAgenzia = ExtractAndClean(line, indexOfCodiceAgenzia, indexOfRiscattoTotale);
		times.RiscattoTotale = ExtractAndClean(line, indexOfRiscattoTotale, indexOfImportoParziale) == "true";
		times.ImportoRiscattoParziale = ExtractAndClean(line, indexOfImportoParziale, indexOfEnd);
		times.ImportoRiscattoParziale = times.ImportoRiscattoParziale == "null" ? "" : times.ImportoRiscattoParziale;
	}

	private void AddTimesData(TimesDto times, string line)
	{
		int indexOfIniziato = line.IndexOf("iniziato: ");
		int indexOfFinito = line.IndexOf("finito: ");
		int indexOfTempoImpiegato = line.IndexOf("tempo impiegato: ");
		int indexOfMillis = line.IndexOf("millis: ");
		int indexOfEnd = line.Length;

		times.Inizio = ExtractAndClean(line, indexOfIniziato, indexOfFinito);
		times.Fine = ExtractAndClean(line, indexOfFinito, indexOfTempoImpiegato);
		times.TempoImpiegato = ExtractAndClean(line, indexOfTempoImpiegato, indexOfMillis);
		times.Millisecondi = ExtractAndClean(line, indexOfMillis, indexOfEnd);
	}

	private string ExtractAndClean(string text, int from, int to)
	{
		int length = to - from;
		string extracted = text.Substring(from, length);
		int indexOfEquals = extracted.IndexOf(':') + 1;
		return extracted.Substring(indexOfEquals).Replace("'", "").Replace(",", "").Replace("\"", "").Replace("{", "").Replace("}", "").Trim();
	}
}
