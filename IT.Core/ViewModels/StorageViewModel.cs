using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Core.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        public double StockIn { get; set; }
        public double StockOut { get; set; }
        public int From { get; set; }
        public int SiteId { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public string Source { get; set; }
        public string UserName { get; set; }
        public string SiteName { get; set; }
        public bool Opration { get; set; }
    }
}
