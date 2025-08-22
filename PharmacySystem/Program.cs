using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PharmacySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            string connectionString = configuration.GetConnectionString("PharmacyDBConnection");

            while (true)
            {
                Console.WriteLine("1. List Medicines\n2. Add Medicine\n3. Search Medicine\n4. Update Stock\n5. Record Sale\n6. Exit");
                string choice = Console.ReadLine();
                if (choice == "6") break;

                switch (choice)
                {
                    case "1":
                        ListMedicines(connectionString);
                        break;
                    case "2":
                        AddMedicine(connectionString);
                        break;
                    case "3":
                        SearchMedicine(connectionString);
                        break;
                    case "4":
                        UpdateStock(connectionString);
                        break;
                    case "5":
                        RecordSale(connectionString);
                        break;
                }
            }
        }

        static void ListMedicines(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetAllMedicines", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["MedicineID"]}, Name: {reader["Name"]}, Category: {reader["Category"]}, Price: {reader["Price"]}, Quantity: {reader["Quantity"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void AddMedicine(string connectionString)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Category: ");
            string category = Console.ReadLine();
            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) return;
            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AddMedicine", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Medicine added.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void SearchMedicine(string connectionString)
        {
            Console.Write("Search term: ");
            string searchTerm = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SearchMedicine", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["MedicineID"]}, Name: {reader["Name"]}, Category: {reader["Category"]}, Price: {reader["Price"]}, Quantity: {reader["Quantity"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void UpdateStock(string connectionString)
        {
            Console.Write("Medicine ID: ");
            if (!int.TryParse(Console.ReadLine(), out int medicineID)) return;
            Console.Write("New Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateStock", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MedicineID", medicineID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Stock updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void RecordSale(string connectionString)
        {
            Console.Write("Medicine ID: ");
            if (!int.TryParse(Console.ReadLine(), out int medicineID)) return;
            Console.Write("Quantity Sold: ");
            if (!int.TryParse(Console.ReadLine(), out int quantitySold)) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("RecordSale", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MedicineID", medicineID);
                    cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Sale recorded.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}