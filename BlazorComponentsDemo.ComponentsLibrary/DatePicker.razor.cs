using BlazorDateRangePicker;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace BlazorComponentsDemo.ComponentsLibrary
{
	public class DatePickerModel<TDateType> : ComponentBase
	{
		#region Variable Declaration
		[Parameter] public TDateType StartDateValue { get; set; }
		[Parameter] public TDateType EndDateValue { get; set; }
		[Parameter] public EventCallback<(TDateType, TDateType)> OnDateRangeValueChanged { get; set; }
		[Parameter] public bool EnableSingleDateSelection { get; set; } = false;
		[Parameter] public string PlaceholderText { get; set; } = "Select Date";

		protected DateTimeOffset? StartDate { get; set; }
		protected DateTimeOffset? EndDate { get; set; }
		#endregion

		#region Life-Cycle Methods
		protected override void OnInitialized()
		{
		}
		#endregion

		#region Other Methods
		protected async Task OnRangeSelect(DateRange range)
		{
			// Parse and convert range start and end values based on the type parameter
			// Comparing FullName property of Type object instead due to System.RuntimeType being set for TValue
			switch (typeof(TDateType).FullName)
			{
				case var dt when dt == typeof(DateTime).FullName || dt == typeof(DateTime?).FullName:
					StartDateValue = (TDateType)(object)range.Start.DateTime;
					EndDateValue = (TDateType)(object)range.End.DateTime;
					break;

				case var dto when dto == typeof(DateTimeOffset).FullName || dto == typeof(DateTimeOffset?).FullName:
					StartDateValue = (TDateType)(object)range.Start;
					EndDateValue = (TDateType)(object)range.End;
					break;

				default:
					throw new InvalidOperationException($"Unsupported type: {typeof(TDateType)}");
			}

			// Invoke the event handler with the selected range
			await OnDateRangeValueChanged.InvokeAsync((StartDateValue, EndDateValue));
		}

		protected async Task ClearPicker(MouseEventArgs e, DateRangePicker picker)
		{
			StartDate = null;
			EndDate = null;

			await picker.Close();
			await picker.Reset();
			await picker.OnRangeSelect.InvokeAsync(new DateRange());
		}

		#endregion
	}
}
