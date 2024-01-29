using Microsoft.AspNetCore.Mvc;
using RapidShort.Domain.Entities;
using RapidShort.Domain.Services.Interfaces;
using System.Web;

namespace RapidShort.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("Top5")]
        public IActionResult GetUrlTop5()
        {
            var urls = _urlService.GetUrlTop5();
            return Ok(urls);
        }

        [HttpGet]
        public IActionResult GetUrls()
        {
            var urls = _urlService.Get();

            return Ok(urls);
        }

        [HttpGet("{id}")]
        public IActionResult GetUrl(int id)
        {
            if (id == 0)
                return BadRequest("Id inválido!");

            var url = _urlService.GetById(id);
            if (url == null)
                return NotFound();

            return Ok(url);
        }

        [HttpPost]
        public IActionResult CreateUrl(UrlRequest url)
        {
            var result = _urlService.Create(url);
            if (result)
                return Created();
            else
                return BadRequest("Ocorreu um erro para cadastrar uma nova URL. Tente novamente !");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUrl(int id, Urls url)
        {
            if (id != url.Id)
                return BadRequest("Id inválido!");

            var result = _urlService.Update(url);

            if (result)
                return Ok();
            else
                return BadRequest("Ocorreu um erro para atualizar dados da URL. Tente novamente !");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUrl(int id)
        {
            var result = _urlService.Delete(id);

            if (result)
                return Ok();
            else
                return BadRequest("Ocorreu um erro para remover a URL. Tente novamente !");
        }

        [HttpGet("Access/{shortUrl}")]
        public IActionResult AccesUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return BadRequest("Url inválida.");
            shortUrl = HttpUtility.UrlDecode(shortUrl);

            var url = _urlService.GetByShortUrl(shortUrl);
            if (url == null)
                return NotFound();

            return Ok(url.Url);
        }

        [HttpGet("Validate/{shortUrl}")]
        public IActionResult ValidateUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
                return BadRequest("Url inválida.");

            shortUrl = HttpUtility.UrlDecode(shortUrl);

            var validate = _urlService.ValidateShortUrl(shortUrl);
            if (validate)
                return Ok();
            else
                return BadRequest();
        }

    }
}
