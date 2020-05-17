using Domain.Entities.Base;
using Domain.Resources;
using Domain.ValueObjects;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using prmToolkit.NotificationPattern.Resources;
using System;

namespace Domain.Entities
{
    public class Usuario:EntityBase
    {
        protected Usuario()
        {

        }
        public Usuario(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            new AddNotifications<Usuario>(this)
              .IfNullOrInvalidLength(x => x.Senha, 3, 32, Message.IfNullOrInvalidLength.ToFormat("Senha", 3, 32));
 

            Senha = Senha.ConvertToMD5();
         
        }

        public Usuario(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;

            new AddNotifications<Usuario>(this)
                .IfNullOrInvalidLength(x => x.Senha, 3, 32, Message.IfNullOrInvalidLength.ToFormat("Senha", 3, 32));
    
         

            Senha = Senha.ConvertToMD5();

            AddNotifications(nome, email);
        }

        public Nome Nome { get; private set; }
        public Email Email { get; set; }
        public string Senha { get; set; }
    }
}
