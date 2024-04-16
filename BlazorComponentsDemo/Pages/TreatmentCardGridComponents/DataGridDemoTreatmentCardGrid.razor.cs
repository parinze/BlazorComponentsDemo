using BlazorComponentsDemo.ComponentsLibrary;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;

namespace BlazorComponentsDemo.Pages.TreatmentCardGridComponents
{
    public class DataGridDemoTreatmentCardGridModel : ComponentBase
    {
        protected DataGridRadzen<TreatmentCardGrid> dataGridParentRef;
        protected RadzenTextArea radzenTextAreaRef;
        protected List<TreatmentCardGrid>? treatmentCardGridEntriesData;
        protected string? cachedTreatmentCardEntryAsstOnEdit;
        protected string? cachedTreatmentCardEntryCommentOnEdit;

        protected bool loadingTreatmentCardGrid = false;
        protected int count;

        protected string deleteButtonStyle = "background:none;outline:none;border:none";

        // for modals
        protected ModalDemoVisitEntry visitModalParentRef;
        protected ModalDemoCommentEntry commentModalParentRef;
        protected TreatmentCardGrid treatmentCardEntryModel = new TreatmentCardGrid();

        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] IDataAccessService DataAccessService { get; set; }

        protected async Task LoadTreatmentCardGridEntries(LoadDataArgs args)
        {
            loadingTreatmentCardGrid = true;

            var data = await DataAccessService.GetTreatmentCardEntries();

            var query = data?.AsQueryable();

            // Enable sorting with Dynamic LINQ (System.Linq.Dynamic.Core)
            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                // Sort via the OrderBy method
                query = query?.OrderBy(args.OrderBy);
            }

            count = query?.Count() ?? 0;

            treatmentCardGridEntriesData = data;

            await Task.Delay(5000); // for demo loading purposes


            loadingTreatmentCardGrid = false;
        }

        // for in-line editing (EditTemplate)
        protected async Task EditRow(TreatmentCardGrid treatmentEntry)
        {
            cachedTreatmentCardEntryAsstOnEdit = treatmentEntry.Asst!;
            cachedTreatmentCardEntryCommentOnEdit = treatmentEntry.Comment!;
            await dataGridParentRef.dataGridRef.EditRow(treatmentEntry);
        }

        // for in-line editing (EditTemplate)
        protected void CancelEdit(TreatmentCardGrid treatmentEntry)
        {
            dataGridParentRef.dataGridRef.CancelEditRow(treatmentEntry);

            // revert cancel edit state
            var currentEntry = DataAccessService.TreatmentCardGridEntries.Where(t => t.Id == treatmentEntry.Id).FirstOrDefault();
            currentEntry!.Asst = cachedTreatmentCardEntryAsstOnEdit;
            currentEntry.Comment = cachedTreatmentCardEntryCommentOnEdit;

            dataGridParentRef.dataGridRef.Reload();

            cachedTreatmentCardEntryAsstOnEdit = null;
            cachedTreatmentCardEntryCommentOnEdit = null;
        }

        protected async Task SaveRow(TreatmentCardGrid treatmentEntry)
        {
            await dataGridParentRef.dataGridRef.UpdateRow(treatmentEntry); // for in-line editing
            DataAccessService.HandleTreatmentCardEntry(treatmentEntry, false);
            await dataGridParentRef.dataGridRef.Reload();
        }

        protected async Task DeleteRow(TreatmentCardGrid treatmentEntry)
        {
            DataAccessService.HandleTreatmentCardEntry(treatmentEntry, true);
            await dataGridParentRef.dataGridRef.Reload();
        }

        protected void CellRender(DataGridCellRenderEventArgs<TreatmentCardGrid> args)
        {
            // override Proc column value for comment row to start on this column
            if (args.Data.Type == "C" && args.Column.Property == "Proc")
            {
                args.Data.Proc = args.Data.Comment;

                // span comment row to the remaining width of datagrid
                args.Attributes.Add("colspan", 12);
            }
        }

        protected void OpenEditVisitModal(TreatmentCardGrid treatmentEntry)
        {
            treatmentCardEntryModel.Id = treatmentEntry.Id;
            treatmentCardEntryModel.Asst = treatmentEntry.Asst;
            treatmentCardEntryModel.Proc = treatmentEntry.Proc;
            treatmentCardEntryModel.DR = treatmentEntry.DR;
            treatmentCardEntryModel.TxNotes = treatmentEntry.TxNotes;
            treatmentCardEntryModel.AWU = treatmentEntry.AWU;
            treatmentCardEntryModel.BB = treatmentEntry.BB;
            treatmentCardEntryModel.AWL = treatmentEntry.AWL;
            treatmentCardEntryModel.OH = treatmentEntry.OH;
            treatmentCardEntryModel.EL = treatmentEntry.EL;
            treatmentCardEntryModel.NVT = treatmentEntry.NVT;
            treatmentCardEntryModel.WKS = treatmentEntry.WKS;
            treatmentCardEntryModel.Chart = treatmentEntry.Chart;
            treatmentCardEntryModel.Test = treatmentEntry.Test;

            visitModalParentRef.visitModalRef.Open();
        }

        protected async Task HandleFormSubmission(TreatmentCardGrid treatmentEntry)
        {
            await SaveRow(treatmentEntry);
            treatmentCardEntryModel = new TreatmentCardGrid();
        }

        protected void HandleOpenModal(bool isVisitEntry)
        {
            treatmentCardEntryModel = new TreatmentCardGrid();

            if (isVisitEntry)
            {
                visitModalParentRef.visitModalRef.Open();
            }
            else
            {
                commentModalParentRef.commentModalRef.Open();
            }
        }

        //protected async Task AutoResizeTextAreaHeight(ElementReference element)
        //{
        //    await JSRuntime.InvokeAsync<object>("window.autoResizeTextAreaHeight", element);
        //}

        //private bool canExecute = true;

        //protected async Task AutoResizeTextAreaHeight(ElementReference element)
        //{
        //    if (!canExecute)
        //    {
        //        return;
        //    }

        //    canExecute = false;

        //    await JSRuntime.InvokeAsync<object>("window.autoResizeTextAreaHeight", element);

        //    await Task.Delay(150);

        //    canExecute = true;
        //}
    }
}