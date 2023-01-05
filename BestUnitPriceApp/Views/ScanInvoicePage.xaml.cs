using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestUnitPriceApp.Views;

public partial class ScanInvoicePage : ContentPage
{
    private readonly ScanInvoiceViewModel _viewModel;
    public ScanInvoicePage(ScanInvoiceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.ScanInvoice();
    }
}