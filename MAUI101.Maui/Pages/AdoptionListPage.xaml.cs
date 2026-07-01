using MAUI101.Maui.Services;

namespace MAUI101.Maui.Pages;

public partial class AdoptionListPage : ContentPage
{
	IPetService _petService;

	public AdoptionListPage(IPetService petService)
	{
		InitializeComponent();
		_petService = petService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		collectionView.ItemsSource = await _petService.GetPetsAsync();
	}

}