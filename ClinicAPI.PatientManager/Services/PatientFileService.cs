using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClinicAPI.PatientManager.Models;

namespace ClinicAPI.PatientManager.Services
{
    public class PatientFileService : IPatientService
    {
        private readonly string _filePath;
        private readonly List<string> _bloodGroups = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        public PatientFileService(string filePath)
        {
            _filePath = filePath;
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public List<Patient> GetAll()
        {
            var patients = new List<Patient>();
            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split(',');
                    patients.Add(new Patient
                    {
                        Name = parts[0],
                        LastName = parts[1],
                        CI = parts[2],
                        BloodGroup = parts[3]
                    });
                }
            }

            return patients;
        }

        public Patient GetByCI(string ci)
        {
            return GetAll().FirstOrDefault(p => p.CI == ci);
        }

        public bool Create(Patient patient)
        {
            var patients = GetAll();
            if (patients.Any(p => p.CI == patient.CI))
            {
                return false;
            }

            var random = new Random();
            patient.BloodGroup = _bloodGroups[random.Next(_bloodGroups.Count)];

            File.AppendAllText(_filePath, $"{patient.Name},{patient.LastName},{patient.CI},{patient.BloodGroup}{Environment.NewLine}");
            return true;
        }

        public bool Update(string ci, Patient patient)
        {
            var patients = GetAll();
            var existingPatient = patients.FirstOrDefault(p => p.CI == ci);
            if (existingPatient == null)
            {
                return false;
            }

            existingPatient.Name = patient.Name;
            existingPatient.LastName = patient.LastName;

            var lines = patients.Select(p => 
                p.CI == ci 
                    ? $"{patient.Name},{patient.LastName},{ci},{p.BloodGroup}"
                    : $"{p.Name},{p.LastName},{p.CI},{p.BloodGroup}");

            File.WriteAllLines(_filePath, lines);
            return true;
        }

        public bool Delete(string ci)
        {
            var patients = GetAll();
            var patientToRemove = patients.FirstOrDefault(p => p.CI == ci);
            if (patientToRemove == null)
            {
                return false;
            }

            var updatedPatients = patients.Where(p => p.CI != ci);
            File.WriteAllLines(_filePath, updatedPatients.Select(p => $"{p.Name},{p.LastName},{p.CI},{p.BloodGroup}"));
            return true;
        }
    }
}