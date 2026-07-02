using MAUI101.Maui.Models;
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

	void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Pet? current = e.CurrentSelection.FirstOrDefault() as Pet ?? null;

		if (current == null)
			return;

		// Navigate to the details page, passing the ID as a query parameter.
		var navigationParameter = new Dictionary<string, object>
		{
			{ "Pet", current } // Can pass strings, IDs, or complex objects
		};
		Shell.Current.GoToAsync($"{nameof(AdoptionFormPage)}", navigationParameter);
	}

}