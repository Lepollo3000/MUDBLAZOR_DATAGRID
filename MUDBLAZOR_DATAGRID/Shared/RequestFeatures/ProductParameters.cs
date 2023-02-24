using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace MUDBLAZOR_DATAGRID.Shared.RequestFeatures
{
    public class ProductParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }
    }
}
