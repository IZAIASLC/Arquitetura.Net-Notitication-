using Domain.Entities.Base;
using Domain.Enums;
using prmToolkit.NotificationPattern;
using System;

namespace Domain.Entities
{
    public class Video:EntityBase
    {
        protected Video()
        {

        }
        public Video(Canal canal, PlayList playList, string titulo, string descricao, string tags, int? ordemNaPlayList, string idVideoYoutube, Usuario usuarioSugeriu )
        {
            Canal = canal;
            PlayList = playList;
            Titulo = titulo;
            Descricao = descricao;
            Tags = tags;
            OrdemNaPlayList = ordemNaPlayList;
            IdVideoYoutube = idVideoYoutube;
            UsuarioSugeriu = usuarioSugeriu;
            Status = EnumStatus.EmAnalise;

            new AddNotifications<Video>(this)
                .IfNullOrInvalidLength(x => x.Titulo, 1, 200, "O campo título é obrigatório e deve conter entre x e x ")
                .IfNullOrInvalidLength(x => x.Descricao, 1, 200, "O campo descricao é obrigatório e deve conter entre x e x ")
                .IfNullOrInvalidLength(x => x.Tags, 1, 50, "O campo tags é obrigatório e deve conter entre x e x ")
                .IfNullOrInvalidLength(x => x.IdVideoYoutube, 1, 50, "O campo idyoutube é obrigatório e deve conter entre x e x ");

            AddNotifications(canal);

            if(playList !=null)
            {
                AddNotifications(playList);
            }

        }

        public Canal Canal { get; private set; }
        public PlayList PlayList { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Tags { get; private set; }
        public int? OrdemNaPlayList { get; private set; }
        public string IdVideoYoutube { get; private set; }
        public Usuario UsuarioSugeriu { get; private set; }
        public EnumStatus Status { get;   private set; }
    }
}
