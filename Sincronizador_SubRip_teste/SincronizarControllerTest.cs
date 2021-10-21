using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Sincronizador_SubRip.Controllers.SincronizadorController;
using System.IO;
using Sincronizador_SubRip_Library.Model.ArquivoSincronizar;
namespace Sincronizador_SubRip_teste
{
    public class Tests
    {
        SincronizadorController sincronizadorController = new SincronizadorController();
        ArquivoSincronizar arquivoSincronizar = new ArquivoSincronizar();
       
        [SetUp]
        public void Setup()
        {
            var arquivo = new Mock<IFormFile>();

            var memoria = new MemoryStream();
            var arquivoSubRip = new StreamWriter(memoria);
            arquivoSubRip.Write("1\r\n00:07:15,128 --> 00:07:21,876\r\nOlá!\r\n\r\n2\r\n00:07:23,123 --> 00:07:29,001\r\nTudo bem?\r\n\r\n3\r\n 00:07:31,032 --> 00:07:35,153\r\nFim.");
            arquivoSubRip.Flush();
            memoria.Position = 0;
            arquivo.Setup(_ => _.OpenReadStream()).Returns(memoria);
            arquivo.Setup(_ => _.FileName).Returns("teste.srt");
            arquivo.Setup(_ => _.Length).Returns(memoria.Length);

            arquivoSincronizar.arquivoSubRip = arquivo.Object;
            arquivoSincronizar.tempoSegundos = 3;
        }

        [Test]
        public void Test()
        {
            Assert.IsTrue(File.Exists(sincronizadorController.Put(arquivoSincronizar)));
        }
    }
}