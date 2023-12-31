using AggregaRisultati.Models;

namespace AggregaRisultati.Writers;

public class Aggregator(DifferenceDto? difference = null, PolizzaInputDto? polizza = null, TimesDto? timesTotale = null, TimesDto? timesParziale = null)
{
	public const int NUMBER_OF_FIELDS = 30;
	private DifferenceDto? _difference = difference;
	private PolizzaInputDto? _polizza = polizza;
	private TimesDto? _timesTotale = timesTotale;
	private TimesDto? _timesParziale = timesParziale;

	public string Aggregate(int position, bool header)
	{
		if (position >= NUMBER_OF_FIELDS || position < 0)
		{
			return "";
		}
		return position switch
		{
			0 => header ? "Riga" : _polizza?.NumeroRiga ?? "",
			1 => header ? "Input" : _polizza?.Input ?? "",
			2 => header ? "Agenzia Gestione" : _difference?.AgenziaGestione ?? _polizza?.AgenziaGestione ?? "",
			3 => header ? "Codice Prodotto" : _difference?.CodiceProdotto ?? _polizza?.CodiceProdotto ?? "",
			4 => header ? "Codice Societa" : _difference?.CodiceSocieta ?? _polizza?.CodiceSocieta ?? "",
			5 => header ? "Categoria" : _difference?.Categoria ?? _polizza?.Categoria ?? "",
			6 => header ? "Numero Collettiva" : _difference?.NumeroCollettiva ?? _polizza?.NumeroCollettiva ?? "",
			7 => header ? "Numero Polizza" : _difference?.NumeroPolizza ?? _polizza?.NumeroPolizza ?? "",
			8 => header ? "Data Decorrenza" : _polizza?.DataDecorrenza ?? "",
			9 => header ? "Tipi Riscatto" : _polizza?.TipiRiscatto ?? "",
			10 => header ? "Convenzione" : _polizza?.Convenzione ?? "",
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
			21 => header ? "Importo\r\nrParziale" : _timesParziale?.ImportoRiscattoParziale ?? "",
			22 => header ? "Inizio\r\nrTotale" : _timesTotale?.Inizio ?? "",
			23 => header ? "Fine\r\nrTotale" : _timesTotale?.Fine ?? "",
			24 => header ? "Tempo Impiegato\r\nrTotale" : _timesTotale?.TempoImpiegato ?? "",
			25 => header ? "Millisecondi\r\nrTotale" : _timesTotale?.Millisecondi ?? "",
			26 => header ? "Inizio\r\nrParziale" : _timesParziale?.Inizio ?? "",
			27 => header ? "Fine\r\nrParziale" : _timesParziale?.Fine ?? "",
			28 => header ? "Tempo Impiegato\r\nrParziale" : _timesParziale?.TempoImpiegato ?? "",
			29 => header ? "Millisecondi\r\nrParziale" : _timesParziale?.Millisecondi ?? "",
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