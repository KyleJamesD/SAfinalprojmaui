using MySqlConnector;

namespace SAfinalprojmaui.Pages;

public partial class CreateRental : ContentPage
{
	public CreateRental()
	{
		InitializeComponent();
        update_Picker_Equipment();
		update_Picker_Customer();
        // Set date variable to selected date pickers
        startDatePicker.DateSelected += OnStartDateSelected;
        returnDatePicker.DateSelected += OnEndDateSelected;
        // set the start date time to todays date without having to click on the Date picker
        StartDate = DateTime.Today;
        startDatePicker.Date = DateTime.Today;


    }



    DateTime current_date_today = DateTime.Today;




    public void update_Picker_Equipment()         //Update the picker wheel Method
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


    // on selection of date set date variables to selected date
    private void OnStartDateSelected(object sender, DateChangedEventArgs e)
    {
        StartDate = e.NewDate;
    }

    private void OnEndDateSelected(object sender, DateChangedEventArgs e)
    {
        EndDate = e.NewDate;
    }




    //on button press   caluclate cost
    private void OnCalculateCostClicked(object sender, EventArgs e)
    {
        int cost = CalculateCost();
        // Display or use the calculated cost
        label_rental_cost.Text = $"${cost.ToString()}"; 
    }



    //Calculate cost of the rental

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DailyRate { get; set; } // This is your daily rental rate

    public int CalculateCost()
    {
        TimeSpan rentalDuration = EndDate - StartDate;
        int totalDays = (int)rentalDuration.TotalDays;

        // If you want to charge for at least one day even if the item is returned the same day

        //get object from equipment picker wheel
        Equipment selectedEquipment = (Equipment)EquipmentPicker.SelectedItem;
        DailyRate = selectedEquipment.Daily_Cost;

        return totalDays * DailyRate;
    }





    private void OnCreateRentalClicked(object sender, EventArgs e)
    {
        SaveRental();
    }


    public void SaveRental()
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
        DatabaseAccess dbAccess = new DatabaseAccess(builder);  //create object of the MSQL string builder'


        Equipment selectedEquipment = (Equipment)EquipmentPicker.SelectedItem;
        Customer selectedCustomer = (Customer)customerPicker.SelectedItem;


        // Get the text from the Entry
        int customer_id = selectedCustomerId;
        int equip_id = selectedEquipmentId;
        //declared publicy above so do not need?
        //DateTime current_date_today = current_date_today;
        DateTime rental_date = StartDate;
        DateTime return_date = EndDate;
        int rental_cost = DailyRate;

        dbAccess.InsertRentalIfNotExists(customer_id, equip_id, current_date_today, rental_date, return_date, rental_cost);



        // CONFIRM RENTAL HAS BEEN SAVED
        Display_Rental_Save.Text = $"{selectedCustomer.FirstName} rental of equipment {selectedEquipment.Equip_Name} has been Saved!";
    }



    private int selectedCustomerId = -1; // Variable to store selected Customer's ID
    //When Customer is selected in picker , select 
    private void CustomerPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Check if the picker selection is valid
        if (customerPicker.SelectedIndex != -1)
        {
            //store selected customer Object in variable
            Customer selectedCustomer = (Customer)customerPicker.SelectedItem;
            // Store the Customer ID in the variable
            //Store object variable Customer_num in selectedCustomerId
            selectedCustomerId = selectedCustomer.CustomerId;

        }
    }


    private int selectedEquipmentId = -1; // Variable to store selected Customer's ID
    //When Customer is selected in picker , select 
    private void EquipmentPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Check if the picker selection is valid
        if (customerPicker.SelectedIndex != -1)
        {
            //store selected customer Object in variable
            Equipment selectedEquipment = (Equipment)EquipmentPicker.SelectedItem;
            // Store the Customer ID in the variable
            //Store object variable Customer_num in selectedCustomerId
            selectedEquipmentId = selectedEquipment.EquipmentId;

        }
    }

}