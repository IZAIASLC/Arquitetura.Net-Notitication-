using Domain.Entities.Base;
using Domain.Enums;
using Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;

namespace Domain.Entities
{
    public class PlayList:EntityBase
    {
        protected PlayList()
        {
                
        }
        public PlayList(string nome, Usuario usuario )
        {
            Usuario = usuario;
            Nome = nome;
           

            new AddNotifications<PlayList>(this)
                .IfNullOrInvalidLength(x => x.Nome, 2, 100, MSG.A_X0_DEVE_SER_MAIOR_OU_IGUAL_A_X1.ToFormat("2", "100"));

            AddNotifications(usuario);
        }

        public Usuario Usuario { get; private set; }
        public string Nome { get; private set; }
        public EnumStatus Status { get; private set; }
    }
}
