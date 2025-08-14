using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareSystem.Models;
using HealthcareSystem.Repository;

namespace HealthcareSystem
{
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new Repository<Patient>();
        private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        public void SeedData()
        {
            // Add Patients
            _patientRepo.Add(new Patient(1, "Alice Johnson", 29, "Female"));
            _patientRepo.Add(new Patient(2, "Michael Smith", 40, "Male"));
            _patientRepo.Add(new Patient(3, "Sarah Brown", 35, "Female"));

            // Add Prescriptions
            _prescriptionRepo.Add(new Prescription(101, 1, "Paracetamol", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(102, 1, "Ibuprofen", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(103, 2, "Amoxicillin", DateTime.Now.AddDays(-7)));
            _prescriptionRepo.Add(new Prescription(104, 3, "Cough Syrup", DateTime.Now.AddDays(-2)));
        }

        public void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();
            foreach (var prescription in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                {
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                }
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }

        public void PrintAllPatients()
        {
            Console.WriteLine("\n--- List of Patients ---");
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine(patient);
            }
        }

        public void PrintPrescriptionsForPatient(int id)
        {
            if (_prescriptionMap.ContainsKey(id))
            {
                Console.WriteLine($"\n--- Prescriptions for Patient ID: {id} ---");
                foreach (var prescription in _prescriptionMap[id])
                {
                    Console.WriteLine(prescription);
                }
            }
            else
            {
                Console.WriteLine("\nNo prescriptions found for this patient.");
            }
        }

        public void Run()
        {
            SeedData();
            BuildPrescriptionMap();

            while (true)
            {
                Console.WriteLine("\n?? Healthcare System Menu:");
                Console.WriteLine("1. View All Patients");
                Console.WriteLine("2. View Prescriptions by Patient ID");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    PrintAllPatients();
                }
                else if (choice == "2")
                {
                    Console.Write("Enter Patient ID: ");
                    if (int.TryParse(Console.ReadLine(), out int patientId))
                    {
                        PrintPrescriptionsForPatient(patientId);
                    }
                    else
                    {
                        Console.WriteLine("? Invalid input. Please enter a valid numeric ID.");
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("?? Exiting Healthcare System...");
                    break;
                }
                else
                {
                    Console.WriteLine("? Invalid option. Please try again.");
                }
            }
        }
    }
}
