namespace BestUnitPriceApp.Views;

public partial class ItemsPage : ContentPage
{
    ItemsViewModel ViewModel;

    public ItemsPage(ItemsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel.LoadZonesAsync();
        await ViewModel.LoadDataAsync();
    }

    private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            ((ItemsViewModel)BindingContext).LoadDataAsync();
        }
    }
}
