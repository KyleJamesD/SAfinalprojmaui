using MySqlConnector;

namespace SAfinalprojmaui.Pages;


//CONATINS ALL METHODS TO DO WITH THE UI EQUIPMENT
public partial class ManageEquipment : ContentPage
{
	public ManageEquipment()
	{
		InitializeComponent();
	}


    // on button click method
    public void Save_Equip_Button_Click(object sender, EventArgs e)
    {
        SaveEquipment();
        update_picker();

    }


    public void SaveEquipment()
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
        int userInput1 = int.Parse(cate_num.Text);
        string userInput2 = equip_name.Text;
        int userInput3 = int.Parse(daily_cost.Text);
        string userInput4 = equip_description.Text;

        // call method to insert new customer into custoemr table, pass along arguments from Entry fields
        //Methods that read/write from DB must be in DB class and called with DB object
        dbAccess.InsertEquipmentIfNotExists(userInput1, userInput2, userInput3, userInput4);

        // CONFIRM CUSTOMER HAS BEEN SAVED
        DisplayEquipmentEntry.Text = $"{userInput1} has been Saved!";
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

        // Fetch the list of Equipment
        List<Equipment> equipment_list = dbAccess.FetchAllEquipment(); 
        
        // Set picker item source to list of customers
        EquipmentPicker.ItemsSource = equipment_list;
        //display the string line full detaisl from EACH object 
        EquipmentPicker.ItemDisplayBinding = new Binding("FullDetails");
    }

    // on Button x:Name="del_button" click call these methods
    public void Delete_Equip_Button_Click(object sender, EventArgs e)
    {
        EquipmentDelete();
        update_picker();
    }

    // Delete the Customer by ID
    public void EquipmentDelete()
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
        DatabaseAccess dbAccess = new DatabaseAccess(builder);  //create object of the MYSQL string builder


        // Get the text from the Entry
        int userInput1 = int.Parse(del_entry.Text);

        // call method to insert new customer into custoemr table, pass along arguments from Entry fields
        //Methods that read/write from DB must be in DB class and called with DB object
        dbAccess.DeleteEquipmentIfExists(userInput1);

        // CONFIRM CUSTOMER HAS BEEN Deleted
        DisplayEquipmentEntry_del.Text = $"Equipment:{userInput1} has been Deleted!";
    }


}






