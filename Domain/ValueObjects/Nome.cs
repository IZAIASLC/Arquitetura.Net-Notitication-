using Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using prmToolkit.NotificationPattern.Resources;

namespace Domain.ValueObjects
{
    public class Nome : Notifiable
    {
        protected Nome()
        {

        }
        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;

            new AddNotifications<Nome>(this)
                .IfNullOrInvalidLength(x => x.PrimeiroNome, 1, 50, Message.IfNullOrInvalidLength.ToFormat("Primeiro nome",1, 50))
                    .IfNullOrInvalidLength(x => x.UltimoNome, 1, 50, MSG.X0_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Último nome", 1, 50));
        }

        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
    }
}
