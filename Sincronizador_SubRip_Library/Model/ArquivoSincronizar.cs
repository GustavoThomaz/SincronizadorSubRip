using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sincronizador_SubRip_Library.Model.ArquivoSincronizar
{
    public class ArquivoSincronizar
    {
        [FromForm(Name = "tempoSegundos")]
        public int tempoSegundos { get; set; }

        [Required]
        public IFormFile arquivoSubRip { get; set; }

        

    }
}
