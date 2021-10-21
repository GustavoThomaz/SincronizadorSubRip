using Microsoft.AspNetCore.Mvc;
using Sincronizador_SubRip_Library.Service;
using Sincronizador_SubRip_Library.Model.ArquivoSincronizar;

namespace Sincronizador_SubRip.Controllers.SincronizadorController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SincronizadorController
    {
        [HttpPut]
        public string Put([FromForm] ArquivoSincronizar arquivoSincronizar )
        {
            //Chamada para sincronização de arquivo .srt
            ArquivoSincronizarService arquivoSincronizarService = new ArquivoSincronizarService();
            return arquivoSincronizarService.sincronizarArquivo(arquivoSincronizar.arquivoSubRip, arquivoSincronizar.tempoSegundos);
        }
    }
}
