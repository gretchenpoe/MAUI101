using MAUI101.Maui.Models;
using MAUI101.Maui.Services;
using MAUI101.Maui.ViewModels;

namespace MAUI101.Maui.Pages;

public partial class AdoptionListPage : ContentPage
{
	private readonly AdoptionListViewModel _viewModel;

	public AdoptionListPage(AdoptionListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		

		await _viewModel.LoadDataCommand.ExecuteAsync(null);
	}


	void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Pet? current = e.CurrentSelection.FirstOrDefault() as Pet ?? null;

		if (current == null)
			return;

		// Navigate to the details page, passing the pet data
		var navigationParameter = new Dictionary<string, object>
		{
			{ "Pet", current }
		};
		Shell.Current.GoToAsync($"{nameof(AdoptionFormPage)}", navigationParameter);
	}
}