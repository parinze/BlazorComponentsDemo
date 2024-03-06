using System;
using System.Collections.Generic;

namespace BlazorComponentsDemo.DataModels.Models
{
	public class StatementQueryDto
	{
		public Guid StatementId { get; set; }
		public Guid? PatientId { get; set; }
		public Guid? BillingPartyId { get; set; }
		public DateTime Datetime { get; set; }
		public string PrintStatus { get; set; }
		public string StatusDescription { get; set; }
		public int? Manual { get; set; }
		public int? FutureDays { get; set; }
		public DateTime? AgeDate { get; set; }
		public DateTime? FutureDate { get; set; }
		public string SavedDocument { get; set; }
		public int? ShowBilled { get; set; }
		public byte? Status { get; set; }
		public Guid? UpdatedByEmpId { get; set; }
		public DateTime? DateUpdated { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public string EmailCc { get; set; }
		public string EmailBcc { get; set; }
		public string MailMethod { get; set; }
		public string EmailTo { get; set; }
		public int? ManualEmail { get; set; }
		public int? Printed { get; set; }
		public int? Emailed { get; set; }
		public int? ShowOpenOnly { get; set; }
		public Guid? PatientInsuranceId { get; set; }
		public int SpecialtyTypeID { get; set; }
		public Guid? ScheduledJobId { get; set; }


		public string BillingPartyName { get; set; }
		public string Patients { get; set; }

		public string CityState { get; set; }
		public string LocationName { get; set; }
		public decimal AccountBalance { get; set; }
		public decimal NewClaimsAmount { get; set; }
		public decimal UnpaidClaimsAmount { get; set; }
		public decimal TotalDue { get; set; }
		public decimal CurrentDue { get; set; }
		public decimal Over30 { get; set; }
		public decimal Over60 { get; set; }
		public decimal Over90 { get; set; }
		public decimal UnpaidBenefit { get; set; }
		public decimal UnbilledAmount { get; set; }
		public string Code { get; set; }
		public string Day { get; set; }
		public string Send { get; set; }
		public bool ARFieldsLoaded { get; set; } = false;
		public virtual IEnumerable<PatientNameLocation> PatientNames { get; set; }
	}
}
