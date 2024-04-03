using MySqlConnector;
namespace SAfinalprojmaui.Pages;


//This Class holds all UI functionality
public partial class ManageCustomers : ContentPage
{

    public ManageCustomers()
    {
        InitializeComponent();
    }

        // on button click method
        public void Save_Cust_Button_Click(object sender, EventArgs e)
        {
            CustomerAdd();
            update_picker();

        }


        public void CustomerAdd()
        {
            //DataBase Connection
            // Create a new instance of the MySQL connection string builder
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "1234",
                Database = "villagerentals1",
            };
            DatabaseAccess dbAccess = new DatabaseAccess(builder);  //create object of the MSQL string builder



        // Get the text from the Entry
        string userInput1 = f_nameEntry.Text;
            string userInput2 = l_nameEntry.Text;
            int userInput3 = int.Parse(phoneEntry.Text);
            string userInput4 = emailEntry.Text;

            // call method to insert new customer into custoemr table, pass along arguments from Entry fields
            dbAccess.InsertRecordIfNotExists(userInput1, userInput2, userInput3, userInput4);

            // CONFIRM CUSTOMER HAS BEEN SAVED
            displaycustomerEntry.Text = $"{userInput1} has been Saved!";
        }
         
        public void Load_Picker_Button_Click(object sender, EventArgs e) // on Button x:Name="load_customer_list" click Call these methods
        {
            update_picker();
        }

        public void update_picker()         //Update the picker wheel Method
        {
        //DataBase Connection
        // Create a new instance of the MySQL connection string builder
        var builder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "1234",
            Database = "villagerentals1",
        };
        DatabaseAccess dbAccess = new DatabaseAccess(builder);  //create object of the MSQL string builder


            List<Customer> customers = dbAccess.FetchAllCustomers(); // Fetch the list of customers
            // Set picker item source to list of customers
            customerPicker.ItemsSource = customers;
            //display the string line full detaisl from EACH object 
            customerPicker.ItemDisplayBinding = new Binding("FullDetails");
        }

        // on Button x:Name="del_button" click call these methods
        public void Delete_Cust_Button_Click(object sender, EventArgs e)
        {
            CustomerDelete();
            update_picker();
        }

        // Delete the Customer by ID
        public void CustomerDelete()
        {
            //DataBase Connection
            // Create a new instance of the MySQL connection string builder
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "1234",
                Database = "villagerentals1",
            };
            DatabaseAccess dbAccess = new DatabaseAccess(builder);  //create object of the MSQL string builder


        // Get the text from the Entry
        int userInput1 = int.Parse(del_entry.Text);

            // call method to insert new customer into custoemr table, pass along arguments from Entry fields
            dbAccess.DeleteRecordIfExists(userInput1);

            // CONFIRM CUSTOMER HAS BEEN Deleted
            displaycustomerEntry_del.Text = $"Customer:{userInput1} has been Deleted!";
        }

}





//customer class to store customer objects to display in picker wheel
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






// this class holds all methods for retrieving and deleting Database information
public class DatabaseAccess
{
    public MySqlConnectionStringBuilder BuilderString { get; set; }

    public DatabaseAccess(MySqlConnectionStringBuilder builderString)
    {
        BuilderString = builderString;
    }

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




}