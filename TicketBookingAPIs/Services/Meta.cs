namespace TicketBookingAPIs.Services
{
    public class Meta
    {
        public Meta()
        {
            CurrentPage = 1;
            TotalPage = 1;
            ItemsPerPage = 10;
            TotalItems = 1;
        }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
    }
}
