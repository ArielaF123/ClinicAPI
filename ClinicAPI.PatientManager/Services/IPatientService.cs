using System.Collections.Generic;
using ClinicAPI.PatientManager.Models;

namespace ClinicAPI.PatientManager.Services
{
    public interface IPatientService
    {
        List<Patient> GetAll();
        Patient GetByCI(string ci);
        bool Create(Patient patient);
        bool Update(string ci, Patient patient);
        bool Delete(string ci);
    }
}