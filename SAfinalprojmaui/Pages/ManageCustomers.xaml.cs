using MySqlConnector;
namespace SAfinalprojmaui.Pages;

public partial class ManageCustomers : ContentPage
{




    public ManageCustomers()
    {
        InitializeComponent();
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
        // Set the text of the Label to the user input
        displayf_nameEntry.Text = userInput1;
        // Get the text from the Entry
        string userInput2 = l_nameEntry.Text;
        // Set the text of the Label to the user input
        displayl_nameEntry.Text = userInput2;
        // Get the text from the Entry
        string userInput3 = phoneEntry.Text;
        // Set the text of the Label to the user input
        displayphoneEntry.Text = userInput3;
        // Get the text from the Entry
        string userInput4 = emailEntry.Text;
        // Set the text of the Label to the user input
        displayemailEntry.Text = userInput4;

        int phone_num = int.Parse(phoneEntry.Text);
            
        dbAccess.InsertRecordIfNotExists(userInput1, userInput2, phone_num, userInput4);
    }


}