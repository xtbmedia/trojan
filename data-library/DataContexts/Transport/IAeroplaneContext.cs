using data_library.Interfaces;
using data_library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace data_library.DataContexts.Transport
{
    public interface IAeroplaneContext : IDataContext
    {
        DbSet<Aeroplane> Aeroplanes { get; set; }
    }
}
