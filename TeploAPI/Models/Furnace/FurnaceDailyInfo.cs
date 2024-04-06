using System.ComponentModel.DataAnnotations.Schema;

namespace TeploAPI.Models.Furnace
{
    [Table("DailyInfo")]
    public class FurnaceDailyInfo : FurnaceBaseParam
    {
        public Guid Id { get; set; }

        public DateTime Day { get; set; }
    }
}
