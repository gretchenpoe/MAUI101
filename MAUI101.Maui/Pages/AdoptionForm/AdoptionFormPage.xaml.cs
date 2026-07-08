using MAUI101.Maui.ViewModels;

namespace MAUI101.Maui.Pages;

public partial class AdoptionFormPage : ContentPage
{
	private readonly AdoptionFormViewModel _viewModel;
	public AdoptionFormPage(AdoptionFormViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		
		// Loading pet data if needed
		await _viewModel.LoadPetDataCommand.ExecuteAsync(null);

		form.IsVisible = true;
	}

	
}