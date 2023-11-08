using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RegistroPersonas.Utilities
{
    public class PersonasMessaging : ValueChangedMessage<PersonasMessage>
    {
        public PersonasMessaging(PersonasMessage value):base(value) { 
            
        } 
    }
}
