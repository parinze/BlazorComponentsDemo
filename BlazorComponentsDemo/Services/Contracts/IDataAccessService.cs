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
        List<TreatmentCardGrid> GetTreatmentCardEntries();
        void AddTreatmentCardEntry(TreatmentCardGrid treatmentEntry);
        void UpdateTreatmentCardEntry(TreatmentCardGrid treatmentEntry);
        void DeleteTreatmentCardEntry(TreatmentCardGrid treatmentEntry);
    }
}
