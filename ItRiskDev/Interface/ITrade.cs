using System;

namespace ItRiskDev.Interface
{
    public interface ITrade
    {
        double Value { get; } //indicates the transaction amount in dollars
        string ClientSector { get; } //indicates the client´s sector which can be "Public" or "Private"
        DateTime NextPaymentDate { get; } //indicates when the next payment from the client to the bank is expected
        DateTime ReferenceDate { get; } //indicates when the next payment from the client to the bank is expected
    }
}
