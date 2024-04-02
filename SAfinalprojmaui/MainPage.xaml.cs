using SAfinalprojmaui.Pages;




namespace SAfinalprojmaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
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
