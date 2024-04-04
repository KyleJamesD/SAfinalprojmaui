using MySqlConnector;
namespace SAfinalprojmaui.Pages;


//CONATINS ALL METHODS TO DO WITH THE UI CUSTOMERS
public partial class ManageCustomers : ContentPage
{

    public ManageCustomers()
    {
        InitializeComponent();
        //call the update_picker() function as soonn as page loads to populate the picker wheel
        update_Picker_Customer();
    }

        // on button click method
        public void Save_Cust_Button_Click(object sender, EventArgs e)
        {
            CustomerAdd();

            update_Picker_Customer();

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
        //Methods that read/write from DB must be in DB class and called with DB object
        dbAccess.InsertCustomerIfNotExists(userInput1, userInput2, userInput3, userInput4);

            // CONFIRM CUSTOMER HAS BEEN SAVED
            displaycustomerEntry.Text = $"{userInput1} has been Saved!";
        }
         

        public void update_Picker_Customer()         //Update the picker wheel Method
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

            //Methods that read/write from DB must be in DB class and called with DB object
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
            update_Picker_Customer();
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
            //Methods that read/write from DB must be in DB class and called with DB object
            dbAccess.DeleteCustomerIfExists(userInput1);

            // CONFIRM CUSTOMER HAS BEEN Deleted
            displaycustomerEntry_del.Text = $"Customer:{userInput1} has been Deleted!";
        }

}










