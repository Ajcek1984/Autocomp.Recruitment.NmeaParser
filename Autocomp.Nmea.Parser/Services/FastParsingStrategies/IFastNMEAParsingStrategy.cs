namespace Autocomp.Nmea.Parser.Services.FastParsingStrategies
{
    /// <summary>
    /// Strategia parsowania wiadomości, umożliwiająca uniknięcie boxingu i refleksji. Stosować w przypadku, 
    /// gdy wiadomość określonego typu przychodzi szczególnie często i chcemy zaoszczędzić na wydajności.
    /// Jeśli nie ma zaimplementowanej strategii dla danego typu, użyta zostanie standardowa refleksyjna implementacja parsowania.
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