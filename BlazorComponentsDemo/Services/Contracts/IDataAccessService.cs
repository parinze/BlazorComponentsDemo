using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.DataModels.ViewModels;
using System.Collections;

namespace BlazorComponentsDemo.Services.Contracts
{
    public interface IDataAccessService
    {
        List<TreatmentCardGrid> TreatmentCardGridEntries { get; set; }
        Task<List<PeopleTestData>> GetPeopleTestData();
        Task<bool> UpdatePeopleTestDataStatus(int id, PeopleTestData testDataRecord);
        Task<StatementQueryVm> GetStatements();
        Task<List<TreatmentCardGrid>> GetTreatmentCardEntries();
        void HandleTreatmentCardEntry(TreatmentCardGrid treatmentEntry, bool delete);
        void AddTreatmentCardEntry(TreatmentCardGrid treatmentEntry);
        void UpdateTreatmentCardEntry(TreatmentCardGrid treatmentEntry, TreatmentCardGrid existingEntry);
        void DeleteTreatmentCardEntry(TreatmentCardGrid treatmentEntry);
    }
}
