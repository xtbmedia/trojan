using data_library.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace data_library.Models
{
    public class AuditEvent
    {
        public string Id { get; set; }
        public DateTime Occured { get; set; }
        public Operation EventType { get; set; }
        public ICollection<AuditEventItem> Changes { get; set; }

        public AuditEvent()
        {
            Changes = new List<AuditEventItem>();
        }
    }
}
