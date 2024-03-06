namespace BlazorComponentsDemo.DataModels.ViewModels
{
    public class StatementsViewVm
    {
        public DateTime DateEntered { get; set; }
        public string BillingParty { get; set; }
        public string Patients { get; set; }
        public string CityState { get; set; }
        public string Location { get; set; }
        public string Code { get; set; }
        public string Day { get; set; }
        public string Send { get; set; }
        public string Status { get; set; }
        public int? ShowBilled { get; set; }
        public string ShowBilledString
        {
            get
            {
                if (ShowBilled == 0)
                {
                    return "Yes";
                }
                else if (ShowBilled == 1)
                {
                    return "No";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
