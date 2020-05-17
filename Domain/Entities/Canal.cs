using Domain.Entities.Base;
using Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;

namespace Domain.Entities
{
    public class Canal:EntityBase
    {
        protected Canal()
        {       
        }
        public Canal(string nome, string urlLogo, Usuario usuario)
        {
            Nome = nome;
            UrlLogo = urlLogo;
            Usuario = usuario;

            new AddNotifications<Canal>(this)
                .IfNullOrInvalidLength(x => x.Nome, 2, 50, MSG.A_X0_DEVE_SER_MAIOR_OU_IGUAL_A_X1.ToFormat("2", "50"))
                .IfNullOrInvalidLength(x => x.Nome, 4, 200, MSG.A_X0_DEVE_SER_MAIOR_OU_IGUAL_A_X1.ToFormat("4", "200"));

        }

        public string Nome { get; private set; }
        public string UrlLogo { get; private set; }
        public  Usuario Usuario { get; private set; }
    }
}
