using MySqlConnector;
namespace SAfinalprojmaui.Pages;

public partial class ManageCustomers : ContentPage
{

    public ManageCustomers()
    {
        InitializeComponent();
    }
    public void CustomerEntrySubmit(object sender, EventArgs e)
    {

    // Create a new instance of the MySQL connection string builder
    var builder = new MySqlConnectionStringBuilder
    {
        Server = "localhost",
        UserID = "root",
        Password = "1234",
        Database = "villagerentals1",
    };
    //create object of the MSQL string builder
    DatabaseAccess dbAccess = new DatabaseAccess(builder);



        // Get the text from the Entry
        string userInput1 = f_nameEntry.Text;
        string userInput2 = l_nameEntry.Text;
        int userInput3 = int.Parse(phoneEntry.Text);
        string userInput4 = emailEntry.Text;
        // Set the text of the Label to the user input
        displaycustomerEntry.Text = $"{userInput1} has been Saved!";



        dbAccess.InsertRecordIfNotExists(userInput1, userInput2, userInput3, userInput4);
    }


    public void PopulateCustomerPicker()
    {
        // Create a new instance of the MySQL connection string builder
        var builder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "1234",
            Database = "villagerentals1",
        };
        //create object of the MSQL string builder
        DatabaseAccess dbAccess = new DatabaseAccess(builder);


        List<Customer> customers = dbAccess.FetchAllCustomers(); // Fetch the list of customers
        customerPicker.ItemsSource = customers;
        customerPicker.ItemDisplayBinding = new Binding("FullDetails"); // Ensure the picker displays the customer's full name
    }

    public void Picker_Focused(object sender, EventArgs e)
    {
        PopulateCustomerPicker();
    }
}


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


public class DatabaseAccess
{
    public MySqlConnectionStringBuilder BuilderString { get; set; }

    public DatabaseAccess(MySqlConnectionStringBuilder builderString)
    {
        BuilderString = builderString;
    }


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

    /*        public void PrintAllCustomers()
            {
                using (var connection = new MySqlConnection(BuilderString.ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        string sql = "SELECT * FROM customers";

                        MySqlCommand command = new MySqlCommand(sql, connection);
                        MySqlDataReader reader = command.ExecuteReader();

                        // Display retrieved data from the table
                        while (reader.Read())
                        {
                            int customer_id = reader.GetInt32(0);
                            string f_name = reader.GetString(1);
                            string l_name = reader.GetString(2);
                            int quantity = reader.GetInt32(3);
                            string phone_num = reader.GetString(4);

                        }

                        connection.Close();
                    }
                    catch (MySqlException ex)
                    {
                        // Handle any exceptions that occur during database operations
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }


        }*/



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




}