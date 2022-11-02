using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class SortSettings
    {
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }

        public SortSettings(string sortBy, bool sortAscending)
        {
            SortBy = sortBy;
            SortAscending = sortAscending;
        }
    }
}
