using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace data_library.Models
{
    public class AuditEventItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string BeforeChangeValue { get; set; }
        public string AfterChangeValue { get; set; }
    }
}
