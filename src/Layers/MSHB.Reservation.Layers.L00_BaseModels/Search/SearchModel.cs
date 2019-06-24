
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSHB.Reservation.Layers.L00_BaseModels.Search
{
    public class SearchModel
    {
        public SortModel SortModel { get; set; }
        public int PageIndex { get; set; }
        [Required,MinLength(1)]
        public int PageSize { get; set; }
    }

    public class SortModel
    {
        public string Col { get; set; }
        public string Sort { get; set; }
    }
}
