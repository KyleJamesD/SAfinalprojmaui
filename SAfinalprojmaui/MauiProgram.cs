using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
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





    //Equipment class to store Equipment objects to display in Equipment picker wheel
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



    //Equipment Categories to store in database
    public class EquipmentCategories
    {
        public int Category_Number { get; set; }

        public string Category_Description { get; set; }

        // Read-only property to display all information
        public string FullDetails => $"Category Number: {Category_Number} Description: {Category_Description}";

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
        public void InsertCustomerIfNotExists(string f_name, string l_name, int phone_num, string email)
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
        public void DeleteCustomerIfExists(int customer_id)
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








        //************END EQUIPMENT METHODS*******************//


        //************BEGIN EQUIPMENT CATEGORY METHODS*****************//

        public void InsertCategoryIfNotExists(int cate_num, string cate_desc)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO equip_cate (cate_num, cate_desc) value (@cate_num, @cate_desc)"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cate_num", cate_num);
                    command.Parameters.AddWithValue("@cate_desc", cate_desc);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }

                }

                connection.Close();
            }
        }

        //Deletes Category by their category  number , the PK
        public void DeleteCategoryIfExists(int cate_num)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string query = "Delete FROM equip_cate WHERE cate_num = @cate_num"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cate_num", cate_num);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error Deleteing data into the database.");
                    }
                }
                connection.Close();
            }
        }


        //retrieves all categories and adds them to a list, this list will be used by the picker wheels
        public List<EquipmentCategories> FetchAllCategories()
        {
            List<EquipmentCategories> equipmentCategories = new List<EquipmentCategories>();
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT cate_num, cate_desc FROM equip_cate";
                MySqlCommand command = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        equipmentCategories.Add(new EquipmentCategories
                        {
                            Category_Number = reader.GetInt32(0),
                            Category_Description = reader.GetString(1),
                        });
                    }
                }
                connection.Close();
            }
            return equipmentCategories;
        }


        //************END EQUIPMENT CATEGORY METHODS*******************//



        //************BEGIN RENTAL CATEGORY METHODS*****************//

        public void InsertRentalIfNotExists(int customer_id, int equip_id, DateTime current_date_today, DateTime rental_date, DateTime return_date, int rental_cost)
        {
            using (var connection = new MySqlConnection(BuilderString.ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO rental (customer_id, equip_id, current_date_today, rental_date, return_date, rental_cost) value (@customer_id, @equip_id, @current_date_today, @rental_date, @return_date, @rental_cost)"; using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customer_id", customer_id);
                    command.Parameters.AddWithValue("@equip_id", equip_id);
                    command.Parameters.AddWithValue("@current_date_today", current_date_today);
                    command.Parameters.AddWithValue("@rental_date", rental_date);
                    command.Parameters.AddWithValue("@return_date", return_date);
                    command.Parameters.AddWithValue("@rental_cost", rental_cost);
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into the database.");
                    }


                }

                // selects the last AUTO-INCREMENT so we can add rental_id and equipment_id to the bridging table.
                string retrieveIdQuery = "SELECT LAST_INSERT_ID();";
                //unfortunately the select last auto increment features works on a obhect and not an INT(int 32) and the execute scalar returns and in 64 from that object. you must set your renetal_id to a long to hold the int 64
                //What SELECT LAST_INSERT_ID() Returns: The LAST_INSERT_ID() function in MySQL (and MariaDB, by extension) returns the most recently generated AUTO_INCREMENT value in the current session. This value is returned as a 64-bit unsigned integer (UNSIGNED BIGINT in SQL terms), which corresponds to UInt64 in .NET data types. This is because AUTO_INCREMENT columns can be of types up to BIGINT, and the function is designed to return values that can accommodate the full range of possible AUTO_INCREMENT values.
                //Data Type Conversion: When you execute SELECT LAST_INSERT_ID(); via ExecuteScalar(), the return value is an object. For .NET to handle this value in a typed manner, it needs to be converted or cast to a specific data type. In this case, since the value could potentially be larger than what fits in a 32-bit integer (Int32), you convert it to Int64 (a long in C#), which can safely hold any value LAST_INSERT_ID() might return
                //Storing in a long Variable: By using Convert.ToInt64(command.ExecuteScalar());, you're converting the returned object to a long (Int64), ensuring compatibility with the size of the data returned by LAST_INSERT_ID(). This is correct and necessary because trying to store the value directly in an int (Int32) without checking its size could lead to an OverflowException if the value exceeds what an int can store.
                long rental_id;
                using (var command = new MySqlCommand(retrieveIdQuery, connection))
                {
                    rental_id = Convert.ToInt64(command.ExecuteScalar());
                }

                // Now you have the `rentalId`, you can insert into the bridging table
                string insertBridgingTableQuery = "INSERT INTO equipment_rented (rental_id, equip_id) VALUES (@rental_id, @equip_id);"; using (var command = new MySqlCommand(insertBridgingTableQuery, connection))
                // Prepare and execute the insert for the bridging table as needed, using `rentalId`
                {
                    command.Parameters.AddWithValue("@rental_id", rental_id);
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

        //************END RENTAL CATEGORY METHODS*******************//


    }

}
