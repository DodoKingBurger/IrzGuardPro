using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro
{
    class InvatePage : ContentPage
    {
        public InvatePage() 
        {
            Title = "Invate";
            Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };
            Label label = new Label { Text = "InvatePage" };

            /// переход с обычной странницы назад
            backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
            Content = new StackLayout { Children = { label, backButton } };
        }
    }
}
