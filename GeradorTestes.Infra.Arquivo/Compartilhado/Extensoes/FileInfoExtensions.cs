using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeradorDeTestes.Infra.Arquivos.Compartilhado.Extensoes
{
    public static class FileInfoExtensions
    {
        public static void Serializar(this FileInfo arquivo, object objeto, bool preservarReferencias = false) // parâmetro default
        {
            arquivo.Directory.Create();

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            if (preservarReferencias)
                options.ReferenceHandler = ReferenceHandler.Preserve;

            byte[] registrosEmBytes = JsonSerializer.SerializeToUtf8Bytes(objeto, options);

            File.WriteAllBytes(arquivo.FullName, registrosEmBytes);
        }

        public static T Deserializar<T>(this FileInfo arquivo, bool preservarReferencias = false)
        {
            byte[] registrosEmBytes = File.ReadAllBytes(arquivo.FullName);

            JsonSerializerOptions options = new JsonSerializerOptions();

            if (preservarReferencias)
                options.ReferenceHandler = ReferenceHandler.Preserve;

            return JsonSerializer.Deserialize<T>(registrosEmBytes, options);
        }
    }
}
