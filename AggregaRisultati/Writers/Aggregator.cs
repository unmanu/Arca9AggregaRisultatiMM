using AggregaRisultati.Models;

namespace AggregaRisultati.Writers;

public class Aggregator(DifferenceDto? difference = null, PolizzaInputDto? polizza = null, TimesDto? timesTotale = null, TimesDto? timesParziale = null)
{
	public const int NUMBER_OF_FIELDS = 30;
	private DifferenceDto? _difference = difference;

	public string Aggregate(int position, bool header)
	{
		if (position >= NUMBER_OF_FIELDS || position < 0)
		{
			return "";
		}
		return position switch
		{
			0 => header ? "Riga" : polizza?.NumeroRiga ?? "",
			1 => header ? "Input" : polizza?.Input ?? "",
			2 => header ? "Agenzia Gestione" : _difference?.AgenziaGestione ?? polizza?.AgenziaGestione ?? "",
			3 => header ? "Codice Prodotto" : _difference?.CodiceProdotto ?? polizza?.CodiceProdotto ?? "",
			4 => header ? "Codice Societa" : _difference?.CodiceSocieta ?? polizza?.CodiceSocieta ?? "",
			5 => header ? "Categoria" : _difference?.Categoria ?? polizza?.Categoria ?? "",
			6 => header ? "Numero Collettiva" : _difference?.NumeroCollettiva ?? polizza?.NumeroCollettiva ?? "",
			7 => header ? "Numero Polizza" : _difference?.NumeroPolizza ?? polizza?.NumeroPolizza ?? "",
			8 => header ? "Data Decorrenza" : polizza?.DataDecorrenza ?? "",
			9 => header ? "Tipi Riscatto" : polizza?.TipiRiscatto ?? "",
			10 => header ? "Convenzione" : polizza?.Convenzione ?? "",
			11 => header ? "Errore" : _difference?.Errore ?? "",
			12 => header ? "Tipo Riscatto Errore" : TipoRiscatto(),
			13 => header ? "Importo Netto\r\nCics" : _difference?.ImportoNettoCics ?? "",
			14 => header ? "Importo Lordo\r\nCics" : _difference?.ImportoLordoCics ?? "",
			15 => header ? "Imposte Lordo\r\nCics" : _difference?.ImposteLordoCics ?? "",
			16 => header ? "Importo Netto\r\nAlbedino" : _difference?.ImportoNettoAlbedino ?? "",
			17 => header ? "Importo Lordo\r\nAlbedino" : _difference?.ImportoLordoAlbedino ?? "",
			18 => header ? "Imposte Lordo\r\nAlbedino" : _difference?.ImposteLordoAlbedino ?? "",
			19 => header ? "Errore Cics" : _difference?.ErroreCics ?? "",
			20 => header ? "Errore Albedino" : _difference?.ErroreAlbedino ?? "",
			21 => header ? "Importo\r\nrParziale" : timesParziale?.ImportoRiscattoParziale ?? "",
			22 => header ? "Inizio\r\nrTotale" : timesTotale?.Inizio ?? "",
			23 => header ? "Fine\r\nrTotale" : timesTotale?.Fine ?? "",
			24 => header ? "Tempo Impiegato\r\nrTotale" : timesTotale?.TempoImpiegato ?? "",
			25 => header ? "Millisecondi\r\nrTotale" : timesTotale?.Millisecondi ?? "",
			26 => header ? "Inizio\r\nrParziale" : timesParziale?.Inizio ?? "",
			27 => header ? "Fine\r\nrParziale" : timesParziale?.Fine ?? "",
			28 => header ? "Tempo Impiegato\r\nrParziale" : timesParziale?.TempoImpiegato ?? "",
			29 => header ? "Millisecondi\r\nrParziale" : timesParziale?.Millisecondi ?? "",
			_ => "",
		};
	}

	private string TipoRiscatto()
	{
		if (_difference == null)
		{
			return "";
		}
		return _difference.IsParziale ? "PARZIALE" : "TOTALE";
	}
}