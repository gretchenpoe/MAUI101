using MAUI101.Maui.ViewModels;

namespace MAUI101.Maui.Pages;

public partial class AdoptionDetailsPage : ContentPage
{
	public AdoptionDetailsPage(AdoptionDetailsViewModel viewModel)
	{
		InitializeComponent();
		 BindingContext = viewModel;
	}

	
}