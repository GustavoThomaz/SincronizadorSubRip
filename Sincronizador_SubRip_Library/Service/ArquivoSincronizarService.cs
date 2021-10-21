using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sincronizador_SubRip_Library.Service
{
    public class ArquivoSincronizarService
    {

        public string sincronizarArquivo(IFormFile arquivoSubRip, int tempoSegundos)
        {

            try
            {
                if (!((tempoSegundos != null) && arquivoSubRip.FileName.EndsWith(".srt")))
                    throw new Exception("Informações insuficiente para realizar a sincronização");

                string diretorioOutput = Path.Combine(Environment.CurrentDirectory, "Output");

                if (!Directory.Exists(diretorioOutput))
                    Directory.CreateDirectory(diretorioOutput);

                string arquivoOutput = Path.Combine(diretorioOutput, $"ArquivoSincronizado_{arquivoSubRip.FileName.Replace(".srt", "", StringComparison.InvariantCultureIgnoreCase)}_{DateTime.Now.Ticks}.srt");

                using (StreamReader reader = new StreamReader(arquivoSubRip.OpenReadStream(), Encoding.Default))
                {
                    using (StreamWriter escritaArquivoOutput = new StreamWriter(arquivoOutput, false, Encoding.Default))
                    {
                        string linha;

                        while ((linha = reader.ReadLine()) != null)
                        {

                            if (string.IsNullOrEmpty(linha))
                            {
                                escritaArquivoOutput.Write("\r\n");
                                continue;
                            }

                            if (linha.Contains("-->"))
                            {
                                string[] linhaTempo = linha.Split("-->");
                                string TempoInicio = linhaTempo[0].Trim();
                                string TempoFim = linhaTempo[1].Trim();

                                escritaArquivoOutput.Write(String.Format(
                                        "{0:HH\\:mm\\:ss\\,fff} --> {1:HH\\:mm\\:ss\\,fff}\r\n",
                                        DateTime.Parse(TempoInicio.Replace(",", ".")).AddSeconds(tempoSegundos),
                                        DateTime.Parse(TempoFim.Replace(",", ".")).AddSeconds(tempoSegundos)));

                                continue;
                            }

                            escritaArquivoOutput.Write($"{linha}\r\n");
                        }
                    }
                }
                return arquivoOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
          