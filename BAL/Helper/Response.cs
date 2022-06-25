using System;

namespace BAL
{
    public class SuccessResponse
    {
        public int code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
        public SuccessResponse()
        {
            flag = true;
            message = "Success";
        }
    }
    public class SuccessLoginResponse
    {
        public int code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public LoginResponse data { get; set; } = new LoginResponse();
        public SuccessLoginResponse()
        {
            flag = true;
            message = "Success";
        }
    }
    public class LoginResponse
    {
        public dynamic user { get; set; }
        public dynamic pageList { get; set; }

        public dynamic userPermission { get; set; }
        public dynamic acesstoken { get; set; }
    }

    public class ErrorResponse
    {
        public int code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
        public ErrorResponse()
        {
            flag = false;
            message = "Error";
        }
    }

    public class FilterSetting
    {
        public Guid cashbook_id { get; set; }
        public Guid customer_id { get; set; }
        public string sales_person_name { get; set; }
        public string customer_ids { get; set; }
        public string salesperson_ids { get; set; }
        public string shape { get; set; }
        public string department_id { get; set; }
        public string search { get; set; }

        public string transType { get; set; }
        public int page { get; set; }
        public int limit { get; set; }

        public int ID { get; set; }
        public int role_id { get; set; }
        public int? is_due { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }
        public string sortColumn { get; set; }
        public int total_rows { get; set; }
        public int isExcel { get; set; }

        public int isExcelPdf { get; set; }
        public int print_address { get; set; }
        public int is_email { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public DateTime? rec_fromdate { get; set; }
        public DateTime? rec_todate { get; set; }
        public string group_by { get; set; }
        public string date_filter { get; set; }
        public int is_compare { get; set; }
        public DateTime? startDate1 { get; set; }
        public DateTime? endDate1 { get; set; }
        public string expense_head_ids { get; set; }
        public int range1 { get; set; }
        public int last1 { get; set; }
        public int range2 { get; set; }
        public int last2 { get; set; }
        public int is_addtional_charges { get; set; }
        public string invoice_no { get; set; }
        public Double balance_amount { get; set; }
        public DateTime? lastSyncDate { get; set; }
        public string token { get; set; }
        public string type { get; set; }
        public int is_transit { get; set; }

        public decimal discount { get; set; }

        public decimal amount { get; set; }
        public decimal price { get; set; }

        public string columns { get; set; }

        public string LOTNO { get; set; }

        public decimal MARKUP { get; set; }
    }
}
