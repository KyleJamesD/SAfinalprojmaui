using SAfinalprojmaui.Pages;




namespace SAfinalprojmaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        private void GoToRentalPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateRental());

        }

        private void GoToEquipmentPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageEquipment());

        }

        private void GoToCustomersPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageCustomers());

        }

        private void GoToPrintReportsPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrintReports());

        }


    }

}
