using MySqlConnector;

namespace SAfinalprojmaui.Pages;

public partial class ManageCategories : ContentPage
{
	public ManageCategories()
	{
		InitializeComponent();

        //call the update_picker() function as soonn as page loads to populate the picker wheel
        update_Picker_Category();
    }



    // on button click method
    public void Save_Cate_Button_Click(object sender, EventArgs e)
    {
        CategoryAdd();
        //call the update_picker() function as soonn as page loads to populate the picker wheel
        update_Picker_Category();

    }


    public void CategoryAdd()
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
        int userInput1 = int.Parse(category_num_entry.Text);
        string userInput2 = category_description_entry.Text;


        // call method to insert new customer into custoemr table, pass along arguments from Entry fields
        //Methods that read/write from DB must be in DB class and called with DB object
        dbAccess.InsertCategoryIfNotExists(userInput1, userInput2);

        // CONFIRM CUSTOMER HAS BEEN SAVED
        displaycategoryEntry.Text = $"Category#{userInput1} has been Saved!";
    }


    public void update_Picker_Category()         //Update the picker wheel Method
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
        List<EquipmentCategories> equipmentCategories = dbAccess.FetchAllCategories(); // Fetch the list of customers
                                                                 // Set picker item source to list of customers
        customerPicker.ItemsSource = equipmentCategories;
        //display the string line full detaisl from EACH object 
        customerPicker.ItemDisplayBinding = new Binding("FullDetails");
    }

    //button
    public void Delete_Cate_Button_Click(object sender, EventArgs e)
    {
        CategoryDelete();
        update_Picker_Category();
    }

    // Delete the Customer by ID
    public void CategoryDelete()
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
        int userInput1 = int.Parse(del_category_entry.Text);

        // call method to insert new customer into custoemr table, pass along arguments from Entry fields
        //Methods that read/write from DB must be in DB class and called with DB object
        dbAccess.DeleteCategoryIfExists(userInput1);

        // CONFIRM CUSTOMER HAS BEEN Deleted
        displaycategoryEntry_del.Text = $"Category#{userInput1} has been Deleted!";
    }
}