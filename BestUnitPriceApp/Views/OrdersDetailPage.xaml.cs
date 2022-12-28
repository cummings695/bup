using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestUnitPriceApp.Views;

public partial class OrdersDetailPage : ContentPage
{
    private readonly OrdersDetailViewModel _viewModel;
    public OrdersDetailPage(OrdersDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
    
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        this.Title = _viewModel.Order.Vendor.Name;
    }
}