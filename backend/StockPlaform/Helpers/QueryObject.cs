namespace StockPlaform.Helpers
{
    public class QueryObject
    {
        //for filtering stocks by symbol or company name
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; }=null;


        //for sorting

        public string? SortBy { get; set; } = null;
        public bool IsDescending{ get; set; } = false;



    }
}
