using System;
using Microsoft.Maui.Controls;
using IrzGuardPro;

namespace IrzGuardPro.Page;

public partial class InvatePage : ContentPage
{
	public InvatePage()
	{

        InitializeComponent();
        BindingContext = this;
        //Title = "Invate";
        //Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };
        //Label label = new Label { Text = "InvatePage" };

        //// переход с обычной странницы назад
        //backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
        //Content = new StackLayout { Children = { label, backButton } };
    }
    //public InvatePage()
    //{
    //    Title = "Invate";
    //    Button backButton = new Button { Text = "Back", HorizontalOptions = LayoutOptions.Start };
    //    Label label = new Label { Text = "InvatePage" };

    //    // переход с обычной странницы назад
    //    backButton.Clicked += async (o, e) => await Navigation.PopAsync(true);
    //    Content = new StackLayout { Children = { label, backButton } };
    //}
}