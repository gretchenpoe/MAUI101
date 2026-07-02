using MAUI101.Maui.ViewModels;

namespace MAUI101.Maui.Pages;

public partial class AdoptionFormPage : ContentPage
{
	public AdoptionFormPage(AdoptionFormViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	
}