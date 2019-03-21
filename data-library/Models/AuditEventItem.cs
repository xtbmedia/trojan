using System;
using System.Collections.Generic;
using System.Text;

namespace data_library.Models
{
    public class AuditEventItem
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string BeforeChangeValue { get; set; }
        public string AfterChangeValue { get; set; }
    }
}
