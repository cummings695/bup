using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestUnitPriceApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BestUnitPriceApp.ViewModels;

public partial class AppTitleViewModel : BaseViewModel
{

    public AppTitleViewModel()
    {

        this.Title = "Restaurant Name Goes Here.";
        WeakReferenceMessenger.Default.Register<SelectedRestaurantChangedMessage>(this, (rec, m) =>
        {
            this.Title = $"{m.Value.Name} - {Shell.Current.CurrentPage.Title}";
        });
    }
}

