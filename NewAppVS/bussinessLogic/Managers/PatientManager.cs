using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Managers.Exceptions;
using UPB.BusinessLogic.Models;

namespace UPB.BusinessLogic.Managers
{
    public class PatientManager
    {
        private string patientsFilePath;
        private string[] bloodTypes = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        public PatientManager()
        {
            patientsFilePath = "patients.txt";
        }

        public List<Patient> GetAll()
        {
            string allPatients = File.ReadAllText(patientsFilePath);
            string[] patientsData = allPatients.Split('\n');

            List<Patient> _patients = new List<Patient>();

            foreach(string patientData in patientsData)
            {
                string[] data = patientData.Split(',');
                if (data.Length != 4) continue;
                _patients.Add(new Patient()
                {
                    name = data[0],
                    lastName = data[1],
                    ci = int.Parse(data[2]),
                    bloodType = data[3]
                });
            }

            Log.Information("Client requested patients list");
            return _patients;
        }

        public Patient CreatePatient(string name, string lastName, int ci)
        {
            Patient createdPatient = new Patient()
            {
                name = name,
                lastName = lastName,
                ci = ci,
            };

            Random random = new Random();
            createdPatient.bloodType = bloodTypes[random.Next(bloodTypes.Length)];

            Log.Information($"Successfuly created patient with ci {ci}");
            AddPatient(createdPatient);
            return createdPatient;
        }

        public void AddPatient(Patient patient)
        {
            File.AppendAllText(patientsFilePath, patient.GetInfo());
        }

        private int GetPatientCIindex(int ci)
        {
            string allPatients = File.ReadAllText(patientsFilePath);
            string[] patientsData = allPatients.Split('\n');

            bool found = false;
            int i;

            for (i=0; i<patientsData.Length && !found; i++)
            {
                Console.WriteLine(patientsData[i]);
                string[] patient = patientsData[i].Split(',');
                if (patient.Length != 4) break;

                found = int.Parse(patient[2]) == ci;
            }

            return found ? i-1 : -1;
        }

        public Patient GetPatientByCI(int ci)
        {
            int foundIndex = GetPatientCIindex(ci);
            if (foundIndex == -1)
            {
                BackingServiceException patientNotFoundException = new BackingServiceException("Patient Not Found Exception");
                Log.Error(patientNotFoundException.GetMessageForLogs("GetPatientByCI"));
                throw patientNotFoundException;
            }

            string allPatients = File.ReadAllText(patientsFilePath);
            string[] patientsData = allPatients.Split('\n');

            string foundPatientString = patientsData[foundIndex];
            string[] foundPatientData = foundPatientString.Split(",");
            Patient foundPatient = new Patient()
            {
                name = foundPatientData[0],
                lastName = foundPatientData[1],
                ci = ci,
                bloodType = foundPatientData[3]
            };

            Log.Information(foundPatientString);

            return foundPatient;
        }

        public void DeletePatient(int ci)
        {
            int foundIndex = GetPatientCIindex(ci);
            Console.WriteLine("Found at index: ", foundIndex);

            string allPatients = File.ReadAllText(patientsFilePath);
            string[] patientsData = allPatients.Split('\n');

            string newFileInfo = "";
            for (int i = 0; i < patientsData.Length; i++)
            {
                if (i != foundIndex && patientsData[i].Split(',').Length > 1)
                {
                    newFileInfo += patientsData[i] + '\n';
                }
            }

            File.WriteAllText(patientsFilePath, newFileInfo);
            Log.Information("Deleted patient " + patientsData[foundIndex]);
        }

        public void UpdatePatient(int ci, Patient newPatientValues)
        {
            Patient foundPatient = GetPatientByCI(ci);
            DeletePatient(ci);

            foundPatient.name = newPatientValues.name;
            foundPatient.lastName = newPatientValues.lastName;

            AddPatient(foundPatient);
            Log.Information($"Updated name and last name of patient with ci {ci}");
        }
    }
}
