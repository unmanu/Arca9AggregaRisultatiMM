using AggregaRisultati.Models;

namespace AggregaRisultati.Writers;

public class Aggregator(DifferenceDto? difference)
{
	public const int NUMBER_OF_FIELDS = 14;
	private DifferenceDto? _difference = difference;

	public string Aggregate(int position, bool header)
	{
		if (position >= NUMBER_OF_FIELDS || position < 0)
		{
			return "";
		}
		return position switch
		{
			0 => header ? "Agenzia Gestione" : _difference?.AgenziaGestione ?? "",
			1 => header ? "Codice Prodotto" : _difference?.CodiceProdotto ?? "",
			2 => header ? "Codice Societa" : _difference?.CodiceSocieta ?? "",
			3 => header ? "Categoria" : _difference?.Categoria ?? "",
			4 => header ? "Numero Collettiva" : _difference?.NumeroCollettiva ?? "",
			5 => header ? "Numero Polizza" : _difference?.NumeroPolizza ?? "",
			6 => header ? "Errore Cics" : _difference?.ErroreCics ?? "",
			7 => header ? "Errore Albedino" : _difference?.ErroreAlbedino ?? "",
			8 => header ? "Importo Netto\r\nCics" : _difference?.ImportoNettoCics ?? "",
			9 => header ? "Importo Lordo\r\nCics" : _difference?.ImportoLordoCics ?? "",
			10 => header ? "Imposte Lordo\r\nCics" : _difference?.ImposteLordoCics ?? "",
			11 => header ? "Importo Netto\r\nAlbedino" : _difference?.ImportoNettoAlbedino ?? "",
			12 => header ? "Importo Lordo\r\nAlbedino" : _difference?.ImportoLordoAlbedino ?? "",
			13 => header ? "Imposte Lordo\r\nAlbedino" : _difference?.ImposteLordoAlbedino ?? "",
			_ => "",
		};
	}

}