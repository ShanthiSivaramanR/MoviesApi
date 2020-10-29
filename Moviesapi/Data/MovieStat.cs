using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moviesapi.Data
{
    public class MovieStat
    {
        public int MovieId { get; set; }
        public ulong AverageWatchDurationS { get; set; }
    }
}
