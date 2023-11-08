using RegistroPersonas.ViewModels;
namespace RegistroPersonas.Views;

public partial class PersonaPage : ContentPage
{
	public PersonaPage(PersonasViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}