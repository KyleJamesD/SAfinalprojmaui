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
        label_rental_cost.Text = cost.ToString(); // Format as currency, if appropriate
    }



    //Calculate cost of the rental

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DailyRate { get; set; } = 10; // This is your daily rental rate

    public int CalculateCost()
    {
        TimeSpan rentalDuration = EndDate - StartDate;
        int totalDays = (int)rentalDuration.TotalDays;

        // If you want to charge for at least one day even if the item is returned the same day


        return totalDays * DailyRate;
    }






}