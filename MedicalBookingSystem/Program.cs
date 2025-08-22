using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MedicalBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            string connectionString = configuration.GetConnectionString("MedicalDBConnection");

            while (true)
            {
                Console.WriteLine("1. List Doctors\n2. Book Appointment\n3. List Appointments\n4. Exit");
                string choice = Console.ReadLine();
                if (choice == "4") break;

                switch (choice)
                {
                    case "1":
                        ListDoctors(connectionString);
                        break;
                    case "2":
                        BookAppointment(connectionString);
                        break;
                    case "3":
                        ListAppointments(connectionString);
                        break;
                }
            }
        }

        static void ListDoctors(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Doctors", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["DoctorID"]}, Name: {reader["FullName"]}, Specialty: {reader["Specialty"]}, Available: {reader["Availability"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void BookAppointment(string connectionString)
        {
            Console.Write("Doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorID)) return;
            Console.Write("Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientID)) return;
            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date)) return;
            Console.Write("Notes: ");
            string notes = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Appointments WHERE DoctorID = @DoctorID AND AppointmentDate = @Date", conn);
                    checkCmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    checkCmd.Parameters.AddWithValue("@Date", date);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Doctor not available.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) VALUES (@DoctorID, @PatientID, @AppointmentDate, @Notes)", conn);
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@AppointmentDate", date);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Appointment booked.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void ListAppointments(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Appointments", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["AppointmentID"]}, DoctorID: {reader["DoctorID"]}, PatientID: {reader["PatientID"]}, Date: {reader["AppointmentDate"]}, Notes: {reader["Notes"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}