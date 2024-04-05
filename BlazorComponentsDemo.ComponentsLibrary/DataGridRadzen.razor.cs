using BlazorComponentsDemo.DataModels.ViewModels;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using System;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor.Rendering;

namespace BlazorComponentsDemo.ComponentsLibrary
{
	public class DataGridRadzenModel<TType> : ComponentBase
	{
        #region Variable Declaration
        public RadzenDataGrid<TType> dataGridRef;
		public RadzenContextMenu contextMenuRef;
		protected IEnumerable<int> PageSizeOptions { get; set; } = new int[] { 10, 20, 50, 100, 500 };
		protected int pageSize = 10;
		protected string pagingSummaryFormat = "Displaying page {0} of {1} <b>(total {2} records)</b>";
		protected bool isReloading = false;

        protected List<TType> rowsToInsert = new List<TType>();

        [Inject] protected ContextMenuService ContextMenuService { get; set; }

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
        /// A optional render fragment to include custom columns in the data grid.
        /// </summary>
        [Parameter] public RenderFragment? CustomColumnsMarkup { get; set; }

		/// <summary>
		/// A RadzenDataGridEditMode enum value to specify the edit mode of the data grid.
		/// </summary>
        [Parameter] public DataGridEditMode RadzenEditMode { get; set; }

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

   //     protected void ShowContextMenu(MouseEventArgs args)
   //     {
   //         ContextMenuService.Open(args,
			//	new List<ContextMenuItem> {
			//		// icons are from Material Icons UI (https://materialui.co/icons)
			//		new ContextMenuItem(){ Text = "Edit Row", Value = 2, Icon = "edit" },
			//		new ContextMenuItem(){ Text = "Add Row", Value = 1, Icon = "add" },
			//	},
			//(e) => {
			//	Console.WriteLine($"Menu item with Value={e.Value} clicked. Column: {args.Column.Property}");
			//}
			//);
   //     }

        protected void OnCellContextMenu(DataGridCellMouseEventArgs<TType> args)
        {
			ContextMenuService.Open(
				args,
				new List<ContextMenuItem> {
					// icons are from Material Icons UI (https://materialui.co/icons)
					new ContextMenuItem(){ Text = "Edit Row", Value = 1, Icon = "edit" },
				},
				(e) =>
				{
					Console.WriteLine($"Menu item with Value={e.Value} clicked. Column: {args.Column.Property}");

					if (e.Text == "Edit Row")
					{
						dataGridRef.EditRow(args.Data);
					}
				}
			);
		}
        #endregion
    }
}
