using RegistroPersonas.ViewModels;
namespace RegistroPersonas
{
    public partial class MainPage : ContentPage
    {


        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}