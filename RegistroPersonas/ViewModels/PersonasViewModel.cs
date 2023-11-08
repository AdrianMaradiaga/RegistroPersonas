using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using RegistroPersonas.DataAccess;
using RegistroPersonas.DTOs;
using RegistroPersonas.Utilities;
using RegistroPersonas.Models;

namespace RegistroPersonas.ViewModels
{
    public partial class PersonasViewModel : ObservableObject, IQueryAttributable
    {
        private readonly PersonaDbContext _dbContext;
        [ObservableProperty]
        private PersonaDTO personaDto = new PersonaDTO();

        [ObservableProperty]
        private string pageTitle;

        private int idPersona;

        [ObservableProperty]
        private bool loadingIsVisible = false;

        public PersonasViewModel(PersonaDbContext context)
        {
            _dbContext = context;   
            
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            idPersona = id;

            if(idPersona == 0)
            {
                PageTitle = "Nueva Persona";
            }
            else
            {
                PageTitle = "Editar Persona";
                LoadingIsVisible = true;

                await Task.Run(async () =>
                {
                    var found = await _dbContext.Personas.FirstAsync(e => e.ID == idPersona);
                    PersonaDto.Id = found.ID;
                    PersonaDto.Nombre = found.Nombre;
                    PersonaDto.Apellido = found.Apellido;
                    PersonaDto.Edad = found.Edad;
                    PersonaDto.Correo = found.Correo;
                    PersonaDto.Direccion = found.Direccion;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingIsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            LoadingIsVisible = true;
            PersonasMessage message = new PersonasMessage();

            await Task.Run(async () =>
            {
                if(idPersona == 0)
                {
                    var tablePersona = new Personas
                    {
                        Nombre = PersonaDto.Nombre,
                        Apellido = PersonaDto.Apellido,
                        Edad = PersonaDto.Edad,
                        Correo = PersonaDto.Correo,
                        Direccion = PersonaDto.Direccion
                    };

                    _dbContext.Personas.Add(tablePersona);
                    await _dbContext.SaveChangesAsync();

                    PersonaDto.Id = tablePersona.ID;
                    message = new PersonasMessage()
                    {
                        isAdd = true,
                        personaDto = PersonaDto
                    };
                }
                else
                {
                    var found = await _dbContext.Personas.FirstAsync(e => e.ID == idPersona);
                    found.Nombre = PersonaDto.Nombre;
                    found.Apellido = PersonaDto.Apellido;
                    found.Edad = PersonaDto.Edad;
                    found.Correo = PersonaDto.Correo;
                    found.Direccion = PersonaDto.Direccion;

                    await _dbContext.SaveChangesAsync();

                    message = new PersonasMessage()
                    {
                        isAdd = false,
                        personaDto = PersonaDto
                    };
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingIsVisible = false;
                    WeakReferenceMessenger.Default.Send(new PersonasMessaging(message));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }

    }
}
