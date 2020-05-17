using Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email()
        {

        }
        public Email(string endereco)
        {
            Endereco = endereco;

            new AddNotifications<Email>(this)
                .IfNotEmail(x => x.Endereco, MSG.X0_INVALIDO.ToFormat("E-mail"));
        }

        public string Endereco { get; private set; }
    }
}
