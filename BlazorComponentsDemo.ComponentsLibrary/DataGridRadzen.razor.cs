using BlazorComponentsDemo.DataModels.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;

namespace BlazorComponentsDemo.ComponentsLibrary
{
    public class DataGridRadzenModel<TType> : ComponentBase
	{
        #region Variable Declaration
        public RadzenDataGrid<TType> dataGridRef;
		public RadzenContextMenu contextMenuRef;
		protected int currentPageSize = 10;
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
		[Parameter][EditorRequired] public Func<LoadDataArgs, Task> LoadData { get; set; } = default!;

		/// <summary>
		/// A boolean value to indicate if the data grid is sortable.
		/// <para>Default: true</para>
		/// </summary>
		[Parameter] public bool AllowSorting { get; set; } = true;

        /// <summary>
        /// A boolean value to indicate if the data grid is pagable.
        /// <para>Default: true</para>
        /// </summary>
        [Parameter] public bool AllowPaging { get; set; } = true;

        /// <summary>
        /// A boolean value to indicate if the data grid columns are resizable.
        /// <para>Default: true</para>
        /// </summary>
        [Parameter] public bool AllowColumnResize { get; set; } = true;

        /// <summary>
        /// A boolean value to indicate if the data grid rows will alternate between white and gray background colors.
        /// <para>Default: true</para>
        /// </summary>
        [Parameter] public bool AllowAlternatingRows { get; set; } = true;

		/// <summary>
		/// An enumerable collection of integers to specify the page size options.
		/// <para>Default: {10, 20, 50, 100, 500}</para>
		/// </summary>
		[Parameter] public IEnumerable<int> PageSizeOptions { get; set; } = new int[] { 10, 20, 50, 100, 500 };

		/// <summary>
		/// A boolean value to indicate if the data grid should show the paging summary.
		/// <para>Default: true</para>
		/// </summary>
		[Parameter] public bool ShowPagingSummary { get; set; } = true;

		/// <summary>
		/// A string value to specify the format of the paging summary.
		/// <para>Default: "Displaying page {0} of {1} &lt; b &gt; (total {2} records) &lt; /b &gt;"</para>
		/// <para>Parameters: {0} = current page, {1} = total pages, {2} = total records</para>
		/// </summary>
        [Parameter] public string PagingSummaryFormat { get; set; } = "Displaying page {0} of {1} <b>(total {2} records)</b>";

        /// <summary>
        /// A boolean value to indicate if the data grid is loading.
        /// </summary>
        [Parameter][EditorRequired] public bool IsLoading { get; set; }

		/// <summary>
		/// The total number of records in the queryable data collection to be loaded into the data grid.
		/// </summary>
		[Parameter][EditorRequired] public int QueryCount { get; set; }

		/// <summary>
		/// A function to handle how the data grid renders each cell based on the specified column and/or criteria.
		/// </summary>
		[Parameter] public Action<DataGridCellRenderEventArgs<TType>> CellRender { get; set; } = (args) => { };

        /// <summary>
        /// A optional render fragment to include custom columns in the data grid.
        /// </summary>
        [Parameter] public RenderFragment? CustomColumnsMarkup { get; set; }

        /// <summary>
        /// A RadzenDataGridEditMode enum value to specify the edit mode of the data grid.
        /// <para>Default: DataGridEditMode.Single (edit one row at a time)</para>
        /// </summary>
        [Parameter] public DataGridEditMode RadzenEditMode { get; set; } = DataGridEditMode.Single;

        /// <summary>
        /// A string value to specify the date format to be used in the data grid.
        /// <para>Default: "{0:MM/dd/yyyy HH:mm}"</para>
        /// </summary>
        [Parameter] public string DateFormat { get; set; } = "{0:MM/dd/yyyy HH:mm}";

        /// <summary>
        /// A string to specify the starting column width for each column of the data grid.
        /// </summary>
        [Parameter] public string StartingColumnWidth { get; set; } = "";

		/// <summary>
		/// A string to specify the maximum height of the data grid.
		/// <para>Default: "100%"</para>
		/// </summary>
		[Parameter] public string MaxDataGridHeight { get; set; } = "100%";

		/// <summary>
		/// A string to specify the maximum width of the data grid.
		/// <para>Default: "100%"</para>
		/// </summary>
		[Parameter] public string MaxDataGridWidth { get; set; } = "100%";

        /// <summary>
        /// A string to specify the additional styles for the data grid.
        /// </summary>
        [Parameter] public string AdditionalDataGridStyles { get; set; } = "";

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
				await dataGridRef.Reload();
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
