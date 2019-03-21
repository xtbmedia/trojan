using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace data_library.Models
{
    public class Aeroplane
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public bool InService { get; set; }
    }
}
