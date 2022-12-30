using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BestUnitPriceApp.Common.Messages;

    internal class SelectedRestaurantChangedMessage : ValueChangedMessage<Restaurant>
    {
        public SelectedRestaurantChangedMessage(Restaurant value) : base(value)
        {
        }
    }

