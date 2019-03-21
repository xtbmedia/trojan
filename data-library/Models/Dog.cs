using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace data_library.Models
{
    public class Dog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public bool Tagged { get; set; }
    }
}
