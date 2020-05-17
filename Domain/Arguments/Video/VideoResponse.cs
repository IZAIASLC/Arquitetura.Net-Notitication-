using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Arguments.Video
{
    public class  VideoResponse
    {
        public string NomeCanal { get; set; }
        public Guid? IdPlayList { get; set; }
        public string NomePlayList { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Thumbnail { get; set; }
        public string IdVideoYoutube { get; set; }
        public int? OrdemNaPlayList { get; set; }
        public string Url { get; set; }

        public static explicit operator VideoResponse(Entities.Video entidade)
        {
            return new VideoResponse() {
                Descricao = entidade.Descricao,
                Url = string.Concat("https://www.youtube.com/embed/", entidade.IdVideoYoutube),
                NomeCanal = entidade.Canal.Nome,
                IdVideoYoutube = entidade.IdVideoYoutube,
                // Thumbnail = string.Concat("https://img.youtube.com/vi/", entidade.IdVideoYoutube, "/mqdefault.jpg"),
                 Thumbnail = string.Concat("https://localhost:44392/api/v1/Usuario/ExibirImagem?idImagem=", "75F57B5E-12F1-4580-BFB9-54A039FAE55A"),
                
                Titulo = entidade.Titulo,
                IdPlayList = entidade.PlayList !=null? entidade.PlayList.Id:Guid.Empty,
                NomePlayList = entidade.PlayList != null ? entidade.PlayList.Nome:null,
                OrdemNaPlayList= entidade.OrdemNaPlayList 
            };
        }
    }
}
