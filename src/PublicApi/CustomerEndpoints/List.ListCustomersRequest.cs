namespace BlazorApp.PublicApi.CustomerEndpoints {
    public class ListCustomersRequest : BaseRequest {
        public int PageNumber { get; set; }
        public int PageSize { set; get; }
    }
}