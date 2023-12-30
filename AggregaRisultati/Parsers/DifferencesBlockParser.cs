using AggregaRisultati.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace AggregaRisultati.Parsers;

public class DifferencesBlockParser
{

	public DifferenceDto Parse(string block)
	{
		DifferenceDto difference = new();
		string[] lines = Regex.Split(block, "\r\n|\r|\n");
		bool foundSeparator = false;
		bool cicsError = false;
		bool albedinoError = false;
		StringBuilder cumulative = new();

		foreach (string line in lines)
		{
			if (line.StartsWith("DIFFERENZA / POLIZZA"))
			{
				AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);
				foundSeparator = false;
				cicsError = false;
				albedinoError = false;
				AddPolizzaKey(difference, line);
			}
			else if (line.StartsWith("CICS     :"))
			{
				AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);
				foundSeparator = false;
				cicsError = false;
				albedinoError = false;
				AddCicsData(difference, line);
			}
			else if (line.StartsWith("CICS ERROR: "))
			{
				AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);
				foundSeparator = false;
				albedinoError = false;
				cumulative.AppendLine(line.Replace("CICS ERROR: ", ""));
				cicsError = true;
			}
			else if (line.StartsWith("ALBEDINO :"))
			{
				AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);
				foundSeparator = false;
				cicsError = false;
				albedinoError = false;
				AddAlbedinoData(difference, line);
			}
			else if (line.StartsWith("ALBEDINO ERROR: "))
			{
				AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);
				foundSeparator = false;
				cicsError = false;
				cumulative.AppendLine(line.Replace("ALBEDINO ERROR: ", ""));
				albedinoError = true;
			}
			else if (line.StartsWith("-------------"))
			{
				foundSeparator = true;
			}
			else if (foundSeparator || cicsError || albedinoError)
			{
				cumulative.AppendLine(line);
			}
		}
		AddErrorData(difference, foundSeparator, cicsError, albedinoError, cumulative);

		AddFinalError(difference);

		return difference;
	}

	private void AddFinalError(DifferenceDto difference)
	{
		string cicsError = ReadFirstLine(difference.ErroreCics);
		string albedinoError = ReadFirstLine(difference.ErroreAlbedino);

		string error = cicsError;
		if (!string.IsNullOrEmpty(error) && !string.IsNullOrEmpty(albedinoError))
		{
			error += Environment.NewLine;
		}
		error += albedinoError;

		if (string.IsNullOrEmpty(error))
		{
			error += "importi diversi";
		}

		difference.Errore = error;
	}

	private static string ReadFirstLine(string? text)
	{
		var reader = new StringReader(text ?? "");
		return reader.ReadLine() ?? "";
	}

	private void AddPolizzaKey(DifferenceDto difference, string line)
	{
		int indexOfCodiceSocieta = line.IndexOf("codiceSocieta=");
		int indexOfCategoria = line.IndexOf("categoria=");
		int indexOfNumeroCollettiva = line.IndexOf("numeroCollettiva=");
		int indexOfNumeroPolizza = line.IndexOf("numeroPolizza=");
		int indexOfAgenziaGestione = line.IndexOf("agenziaGestione=");
		int indexOfCodiceProdotto = line.IndexOf("codiceProdotto=");
		int indexOfDataDecorrenza = line.IndexOf("dataDecorrenza=");

		difference.CodiceSocieta = ExtractAndClean(line, indexOfCodiceSocieta, indexOfCategoria);
		difference.Categoria = ExtractAndClean(line, indexOfCategoria, indexOfNumeroCollettiva);
		difference.NumeroCollettiva = ExtractAndClean(line, indexOfNumeroCollettiva, indexOfNumeroPolizza);
		difference.NumeroPolizza = ExtractAndClean(line, indexOfNumeroPolizza, indexOfAgenziaGestione);
		difference.AgenziaGestione = ExtractAndClean(line, indexOfAgenziaGestione, indexOfCodiceProdotto);
		difference.CodiceProdotto = ExtractAndClean(line, indexOfCodiceProdotto, indexOfDataDecorrenza);
		difference.IsParziale = line.Contains("RISCATTO PARZIALE");
	}

	private void AddCicsData(DifferenceDto difference, string line)
	{
		int nettoEqual = line.IndexOf("=") + 1;
		int nettoSeparator = line.IndexOf(",", nettoEqual);
		int lordoEqual = line.IndexOf("=", nettoSeparator) + 1;
		int lordoSeparator = line.IndexOf(",", lordoEqual);
		int imposteEqual = line.IndexOf("=", lordoSeparator) + 1;
		int imposteSeparator = line.Length;

		difference.ImportoNettoCics = ExtractAndClean(line, nettoEqual, nettoSeparator);
		difference.ImportoLordoCics = ExtractAndClean(line, lordoEqual, lordoSeparator);
		difference.ImposteLordoCics = ExtractAndClean(line, imposteEqual, imposteSeparator);
	}

	private void AddAlbedinoData(DifferenceDto difference, string line)
	{
		int nettoEqual = line.IndexOf("=") + 1;
		int nettoSeparator = line.IndexOf(",", nettoEqual);
		int lordoEqual = line.IndexOf("=", nettoSeparator) + 1;
		int lordoSeparator = line.IndexOf(",", lordoEqual);
		int imposteEqual = line.IndexOf("=", lordoSeparator) + 1;
		int imposteSeparator = line.Length;

		difference.ImportoNettoAlbedino = ExtractAndClean(line, nettoEqual, nettoSeparator);
		difference.ImportoLordoAlbedino = ExtractAndClean(line, lordoEqual, lordoSeparator);
		difference.ImposteLordoAlbedino = ExtractAndClean(line, imposteEqual, imposteSeparator);
	}

	private void AddErrorData(DifferenceDto difference, bool foundSeparator, bool cicsError, bool albedinoError, StringBuilder cumulative)
	{
		if (cumulative.Length <= 0)
		{
			return;
		}
		string error = cumulative.ToString().Remove(cumulative.ToString().LastIndexOf(Environment.NewLine));
		if (foundSeparator || cicsError)
		{
			difference.ErroreCics = error;
		}
		else if (albedinoError)
		{
			difference.ErroreAlbedino = error;
		}
		cumulative.Clear();
	}

	private string ExtractAndClean(string text, int from, int to)
	{
		int length = to - from;
		string extracted = text.Substring(from, length);
		int indexOfEquals = extracted.IndexOf('=') + 1;
		return extracted.Substring(indexOfEquals).Replace("'", "").Replace(",", "").Trim();
	}
}
