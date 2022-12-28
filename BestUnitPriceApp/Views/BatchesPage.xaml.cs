using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestUnitPriceApp.Views;

public partial class BatchesPage : ContentPage
{
    BatchesViewModel ViewModel;
    
    public BatchesPage(BatchesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }
    
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel.LoadDataAsync();
    }
}