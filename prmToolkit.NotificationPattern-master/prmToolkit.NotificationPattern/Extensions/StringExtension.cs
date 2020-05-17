using System.Text;

namespace prmToolkit.NotificationPattern.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// ToFormat tem a mesma finalidade do string.Format, podendo setar novos valores em uma string de forma dinamica.
        /// </summary>
        /// <param name="mensagem">Mensagem que contém as paravras reservadas {0} para subistituição</param>
        /// <param name="parametros">Parametros que serão usados para sobrescrever a string</param>
        /// <returns></returns>
        public static string ToFormat(this string mensagem, params object[] parametros)
        {
            return string.Format(mensagem, parametros);
        }

        /// <summary>
        /// ConvertToMD5 tem a finalidade de criptografar uma string no formato MD5
        /// </summary>
        /// <param name="text">O texto a ser criptografado</param>
        /// <returns></returns>
        public static string ConvertToMD5(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var password = (text += "|2d-cc-abc");

            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach(var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();        
        }
    }
}
