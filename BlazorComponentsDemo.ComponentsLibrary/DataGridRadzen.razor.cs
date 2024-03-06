using BlazorComponentsDemo.DataModels.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace BlazorComponentsDemo.ComponentsLibrary
{
	public class DataGridRadzenModel<TType> : ComponentBase
	{
        #region Variable Declaration
        protected RadzenDataGrid<TType> dataGrid;
		protected IEnumerable<int> PageSizeOptions { get; set; } = new int[] { 10, 20, 50, 100, 500 };
		protected int pageSize = 10;
		protected string pagingSummaryFormat = "Displaying page {0} of {1} <b>(total {2} records)</b>";
		protected bool isReloading = false;

		/// <summary>
		/// An array of <typeparamref name="TType"/> objects.
		/// </summary>
		[Parameter][EditorRequired] public IEnumerable<TType>? Data { get; set; }

		/// <summary>
		/// A function to load data (as a queryable collection) into the data grid.
		/// <para>LoadDataArgs includes the following properties:</para>
		/// <para>Top: int (current page size), default = 10</para>
		/// <para>Skip: int (# of records to skip on each page), default = 0</para>
		/// <para>OrderBy: string (sort expression as a string)</para>
		/// <para>Filter: string (filter expression as a string)</para>
		/// </summary>
		[Parameter][EditorRequired] public Func<LoadDataArgs, Task> LoadData { get; set; }

		/// <summary>
		/// A boolean value to indicate if the data grid is loading.
		/// </summary>
		[Parameter][EditorRequired] public bool IsLoading { get; set; }

		/// <summary>
		/// The total number of records in the queryable data collection to be loaded into the data grid.
		/// </summary>
		[Parameter][EditorRequired] public int QueryCount { get; set; }

		/// <summary>
		/// A string value to specify the date format to be used in the data grid.
		/// <para>Default: "{0:MM/dd/yyyy HH:mm}"</para>
		/// </summary>
		[Parameter] public string DateFormat { get; set; } = "{0:MM/dd/yyyy HH:mm}";

		/// <summary>
		/// Dictionary mapping of the property names of <typeparamref name="TType"/> class to desired column header names.
		/// <para>Note: It is not required to include all of the property names in the dictionary.</para>
		/// <para>E.g. Property: PurchaseDate > {"PurchaseDate", "Purchase Date"}</para>
		/// </summary>
		[Parameter] public Dictionary<string, string> PropertyToHeaderName { get; set; } = new();

		/// <summary>
		/// A list of <see cref="Status"/> objects to be used to specify styles based on the specified status label.
		/// </summary>
		[Parameter] public List<Status> Statuses { get; set; } = new List<Status>() {
			new Status() { Label = "New", Style = "background-color:#DAD9D9;color:#636262;"},
			new Status() { Label = "Active", Style = "background-color:#E5F2E8;color:#28A745;"},
			new Status() { Label = "Deleted", Style = "background-color:#FAE5E5;color:#DC5151;"}
		};

		/// <summary>
		/// A list of columns to exclude from datagrid.
		/// </summary>
		[Parameter] public List<string> ExcludedColumns { get; set; } = new();

		/// <summary>
		/// A boolean value to indicate if the data grid needs to be updated. Usually used after handling an event.
		/// </summary>
		[Parameter] public bool Updated { get; set; } = false;
        #endregion

        #region Life-Cycle Methods
        protected override async Task OnParametersSetAsync()
		{
			if (Updated && !isReloading)
			{
				isReloading = true;
				await dataGrid.Reload();
				isReloading = false;
				Updated = false;
			}

		}
        #endregion

        #region Other Methods
        protected string GetPropertyHeaderName(string propertyName)
		{
			if (PropertyToHeaderName.ContainsKey(propertyName))
			{
				return PropertyToHeaderName[propertyName];
			}
			else
			{
				return propertyName;
			}
		}
        #endregion
    }
}
