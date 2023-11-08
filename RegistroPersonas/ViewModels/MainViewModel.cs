using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using RegistroPersonas.DataAccess;
using RegistroPersonas.DTOs;
using RegistroPersonas.Utilities;
using RegistroPersonas.Models;
using System.Collections.ObjectModel;
using RegistroPersonas.Views;

namespace RegistroPersonas.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly PersonaDbContext _dbContext;
        [ObservableProperty]
        private ObservableCollection<PersonaDTO> listPersonas = new ObservableCollection<PersonaDTO>();

        public MainViewModel(PersonaDbContext context)
        {
            _dbContext = context;

            MainThread.BeginInvokeOnMainThread(new Action(async () => await Obtener()));

            WeakReferenceMessenger.Default.Register<PersonasMessaging>(this, (r, m) =>
            {
                PersonaMessageReceivedd(m.Value);
            });
        }

        public async Task Obtener()
        {
            var lista = await _dbContext.Personas.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListPersonas.Add(new PersonaDTO
                    {
                        Id = item.ID,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        Edad = item.Edad,
                        Correo = item.Correo,
                        Direccion = item.Direccion,
                    });
                }
            }
        }

        private void PersonaMessageReceivedd(PersonasMessage personaMensaje)
        {
            var personaDto = personaMensaje.personaDto;

            if (personaMensaje.isAdd)
            {
                ListPersonas.Add(personaDto);
            }
            else
            {
                var encontrado = ListPersonas
                    .First(e => e.Id == personaDto.Id);

                encontrado.Nombre = personaDto.Nombre;
                encontrado.Apellido = personaDto.Apellido;
                encontrado.Edad = personaDto.Edad;
                encontrado.Correo = personaDto.Correo;
                encontrado.Direccion = personaDto.Direccion;
            }
        }

        [RelayCommand]
        private async Task Crear()
        {
            var uri = $"{nameof(PersonaPage)}?id=0";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Editar(PersonaDTO personaDto)
        {
            var uri = $"{nameof(PersonaPage)}?id={personaDto.Id}";
            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task Eliminar(PersonaDTO personaDto)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea eliminar la persona?", "Si", "No");

            if (answer)
            {
                var encontrado = await _dbContext.Personas
                    .FirstAsync(e => e.ID == personaDto.Id);

                _dbContext.Personas.Remove(encontrado);
                await _dbContext.SaveChangesAsync();
                ListPersonas.Remove(personaDto);
            }
        }
    }
}
