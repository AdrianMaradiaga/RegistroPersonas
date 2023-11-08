using CommunityToolkit.Mvvm.ComponentModel;
namespace RegistroPersonas.DTOs
{
    public partial class PersonaDTO : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string nombre;
        [ObservableProperty]
        public string apellido;
        [ObservableProperty]
        public int edad;
        [ObservableProperty]
        public string correo;
        [ObservableProperty]
        public string direccion;
    }
}
