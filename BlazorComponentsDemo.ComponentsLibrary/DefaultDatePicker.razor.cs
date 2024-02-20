using Microsoft.AspNetCore.Components;

namespace BlazorComponentsDemo.ComponentsLibrary
{
	public class DefaultDatePickerModel<TValue> : ComponentBase
	{
		#region Variable Declaration
		[Parameter] public TValue DateValue { get; set; }
		[Parameter] public EventCallback<TValue> DateValueChanged { get; set; }
		[Parameter] public string classes { get; set; }
		[Parameter] public string styles { get; set; }
		protected string standardHeight { get; set; } = "height:calc(1rem + (0.575rem * 2) + 2px);";
		#endregion

		#region Life-Cycle Methods
		protected override void OnInitialized()
		{
		}
		#endregion

		#region Other Methods
		protected async Task OnDateChanged(ChangeEventArgs e)
		{
			switch (typeof(TValue))
			{
				case Type dt when dt == typeof(DateTime):
					DateTime.TryParse((string)e.Value!, out var dateTime);
					await DateValueChanged.InvokeAsync((TValue)(object)dateTime);
					break;

				case Type dto when dto == typeof(DateTimeOffset):
					DateTimeOffset.TryParse((string)e.Value!, out var dateTimeOffset);
					await DateValueChanged.InvokeAsync((TValue)(object)dateTimeOffset);
					break;

				case Type donly when donly == typeof(DateOnly):
					DateOnly.TryParse((string)e.Value!, out var dateOnly);
					await DateValueChanged.InvokeAsync((TValue)(object)dateOnly);
					break;

				default:
					throw new InvalidOperationException($"Unsupported type: {typeof(TValue)}");
			}
		}

		#endregion
	}
}
