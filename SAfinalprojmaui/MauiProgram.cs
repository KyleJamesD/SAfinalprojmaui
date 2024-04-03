using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace SAfinalprojmaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();


        }

    }





    //customer class to store customer objects to display in Customer picker wheel
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        // Read-only property to display all information
        public string FullDetails => $"ID: {CustomerId}, Name: {FirstName} {LastName}, Phone: {PhoneNumber}, Email: {Email}";

    }





    //customer class to store customer objects to display in Customer picker wheel
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public int Cate_Num { get; set; }
        public string Equip_Name { get; set; }
        public int Daily_Cost { get; set; }
        public string Equip_Description { get; set; }

        // Read-only property to display all information
        public string FullDetails => $"ID: {EquipmentId}, Category: {Cate_Num} Equipment Name:{Equip_Name}, Daily Cost: ${Daily_Cost}, Description: {Equip_Description}";

    }



    // this class holds all methods for retrieving and deleting Database information
    public class DatabaseAccess
    {
        public MySqlConnectionStringBuilder BuilderString { get; set; }

        public DatabaseAccess(MySqlConnectionStringBuilder builderString)
        {
            BuilderString = builderString;
        }





        //************BEGIN CUSTOMER METHODS*******************//



        // Saves new customers to the customer table
        public void InsertRecordIfNotExists(string f_name, string l_name, int phone_num, string email)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO customers (f_name, l_name, phone_num, email) value (@f_name, @l_name, @phone_num, @email)"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@f_name", f_name);
                    command.Parameters.AddWithValue("@l_name", l_name);
                    command.Parameters.AddWithValue("@phone_num", phone_num);
                    command.Parameters.AddWithValue("@email", email);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }

                }

                connection.Close();
            }
        }

        //Deletes Customer by their Customer ID, the PK
        public void DeleteRecordIfExists(int customer_id)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string query = "Delete FROM customers WHERE customer_id = @customer_id"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customer_id", customer_id);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }
                }
                connection.Close();
            }
        }


        //retrieves all customers and adds them to a list, this list will be used by the picker wheels
        public List<Customer> FetchAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT customer_id, f_name, l_name, phone_num, email FROM customers";
                MySqlCommand command = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetInt32(3),
                            Email = reader.GetString(4)// Assuming the phone number is the fourth column
                        });
                    }
                }
                connection.Close();
            }
            return customers;
        }

        //************END CUSTOMER METHODS*******************//



        //************BEGIN EQUIPMENT METHODS*****************//


        // Saves new Equipment to the Equipment table
        public void InsertEquipmentIfNotExists(int cate_num, string equip_name, int daily_cost, string equip_description)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO equipment (cate_num, equip_name, daily_cost, description) value (@cate_num, @equip_name, @daily_cost, @equip_description)"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cate_num", cate_num);
                    command.Parameters.AddWithValue("@equip_name", equip_name);
                    command.Parameters.AddWithValue("@daily_cost", daily_cost);
                    command.Parameters.AddWithValue("@equip_description", equip_description);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }

                }

                connection.Close();
            }
        }


        //retrieves all Equipment and adds them to a list, this list will be used by the picker wheel
        public List<Equipment> FetchAllEquipment()
        {
            List<Equipment> equipment = new List<Equipment>();
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT equip_id, cate_num, equip_name, daily_cost, description FROM equipment";
                MySqlCommand command = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipment.Add(new Equipment
                        {
                            EquipmentId = reader.GetInt32(0),
                            Cate_Num = reader.GetInt32(1),
                            Equip_Name = reader.GetString(2),
                            Daily_Cost = reader.GetInt32(3),
                            Equip_Description = reader.GetString(4),
                        });
                    }
                }
                connection.Close();
            }
            return equipment;
        }



        //Deletes Equipment by its equipment_id primary Key
        public void DeleteEquipmentIfExists(int equip_id)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string query = "Delete FROM equipment WHERE equip_id = @equip_id"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@equip_id", equip_id);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }
                }
                connection.Close();
            }
        }




        //************END EQUIPMENT METHODS*******************//

    }










}
