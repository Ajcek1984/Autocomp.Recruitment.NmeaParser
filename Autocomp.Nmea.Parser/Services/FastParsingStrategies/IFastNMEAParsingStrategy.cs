namespace Autocomp.Nmea.Parser.Services.FastParsingStrategies
{
    /// <summary>
    /// Strategia parsowania wiadomości, umożliwiająca uniknięcie boxingu i refleksji.
    /// W przypadku braku strategii dla danego typu, użyta zostanie standardowa refleksyjna implementacja.
    /// </summary>
    public interface IFastNMEAParsingStrategy
    {
        /// <summary>
        /// Identyfikator wiadomości, dla któego przeznaczona jest ta implementacja
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Parsuje kolejkę wartości pól i zwraca obiekt wiadomości.
        /// </summary>
        /// <param name="values">Kolejka wartości</param>
        /// <returns>Obiekt wiadomości</returns>
        object Parse(Queue<string> values);
    }
}