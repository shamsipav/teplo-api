namespace TeploAPI.Models.Furnace
{
    public class FurnaceDailyInfo : FurnaceBaseParam
    {
        public Guid Id { get; set; }

        public DateTime Day { get; set; }
    }
}
