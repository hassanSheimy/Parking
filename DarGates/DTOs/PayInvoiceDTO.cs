using System.Collections.Generic;

namespace DarGates.DTOs
{
    public class PayInvoiceDTO
    {
        public List<int> GateLogIDs { get; set; }
        public List<int> PrinterLogIDs { get; set; }
    }
}
