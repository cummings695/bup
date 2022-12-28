using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestUnitPriceApp.Views;

public partial class BatchesDetailPage : ContentPage
{
    public BatchesDetailPage(BatchesDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}