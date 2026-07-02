using MAUI101.Maui.Models;
using MAUI101.Maui.Services;

namespace MAUI101.Maui.Pages;

public partial class AdoptionFormListPage : ContentPage
{
	IAdoptionFormService _adoptionFormService;

	public AdoptionFormListPage(IAdoptionFormService adoptionFormService)
	{
		InitializeComponent();
		_adoptionFormService = adoptionFormService;
	}


	protected async override void OnAppearing()
	{
		base.OnAppearing();
		collectionView.ItemsSource = await _adoptionFormService.GetAllAdoptionForms();
	}

	void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		AdoptionForm? current = e.CurrentSelection.FirstOrDefault() as AdoptionForm ?? null;

		if (current == null)
			return;

		// Navigate to the details page, passing the adoptionform
		var navigationParameter = new Dictionary<string, object>
		{
			{ "AdoptionForm", current } 
		};
		Shell.Current.GoToAsync($"{nameof(AdoptionFormPage)}", navigationParameter);
	}
}