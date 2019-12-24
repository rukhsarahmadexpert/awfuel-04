using System;
namespace IT.Core.ViewModels
{
    public class SearchViewModel
    {
        public int Id { get; set; }
        public string searchkey { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int CompanyId { get; set; }
        public Boolean Status { get; set; }
        public string Flage { get; set; }
        public string DeviceTiken { get; set; }
        public int DeviceId { get; set; }
        public string CompanyName { get; set; }
        public string NotificationCode { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}