using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using RapidShort.Domain.Entities;
using RapidShort.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RapidShort.Domain.Services
{
    public class UrlService : IUrlService
    {
        #region :: jsonData ::
        private static string jsonData = @"[{
    ""id"": ""23094"",
    ""hits"": 5,
    ""url"": ""http://globo.com"",
    ""shortUrl"": ""http://chr.dc/9dtr4""
},
{
    ""id"": ""76291"",
    ""hits"": 4,
    ""url"": ""http://google.com"",
    ""shortUrl"": ""http://chr.dc/aUx71""
},
{
    ""id"": ""66761"",
    ""hits"": 7,
    ""url"": ""http://terra.com.br"",
    ""shortUrl"": ""http://chr.dc/u9jh3""
},
{
    ""id"": ""70001"",
    ""hits"": 1,
    ""url"": ""http://facebook.com"",
    ""shortUrl"": ""http://chr.dc/qy61p""
},
{
    ""id"": ""21220"",
    ""hits"": 2,
    ""url"": ""http://diariocatarinense.com.br"",
    ""shortUrl"": ""http://chr.dc/87itr""
},
{
    ""id"": ""10743"",
    ""hits"": 0,
    ""url"": ""http://uol.com.br"",
    ""shortUrl"": ""http://chr.dc/y81xc""
},
{
    ""id"": ""19122"",
    ""hits"": 2,
    ""url"": ""http://chaordic.com.br"",
    ""shortUrl"": ""http://chr.dc/qy5k9""
},
{
    ""id"": ""55324"",
    ""hits"": 4,
    ""url"": ""http://youtube.com"",
    ""shortUrl"": ""http://chr.dc/1w5tg""
},
{
    ""id"": ""70931"",
    ""hits"": 5,
    ""url"": ""http://twitter.com"",
    ""shortUrl"": ""http://chr.dc/7tmv1""
},
{
    ""id"": ""87112"",
    ""hits"": 2,
    ""url"": ""http://bing.com"",
    ""shortUrl"": ""http://chr.dc/9opw2""
}]
";
        #endregion

        private readonly IRepository<Urls> _urlRepository;
        private readonly string _BaseUrl;
        private ObjectCache cache = MemoryCache.Default;

        private readonly List<Urls> _Urls = JsonConvert.DeserializeObject<List<Urls>>(jsonData);
        public UrlService(IRepository<Urls> urlRepository, IOptions<AppSettings> BaseUrl)
        {
            _urlRepository = urlRepository;
            _BaseUrl = BaseUrl.Value.BaseUrl;
        }

        #region :: Private Methods ::
        private string GenerateShortUrl(string baseUrl)
        {
            Guid guid = Guid.NewGuid();
            string guidHex = guid.ToString("N");
            return $"{baseUrl}/{guidHex.Substring(0, 10)}";
        }

        private void SendToCache(string url)
        {
            cache.Add("url", url, null);
        }

        private bool UrlExists(string url)
        {
            var urls = _urlRepository.Get();
            if (urls != null && urls.Any())
                return urls.Exists(x => x.Url == url);

            return false;
        }
        private void UpdateHits(Urls url)
        {
            url.Hits++;
            _urlRepository.Update(url);
        }
        #endregion

        public bool Create(UrlRequest url)
        {
            try
            {
                Urls entity = new Urls { Url = url.Url };
                entity.ShortURL = GenerateShortUrl(_BaseUrl);

                bool urlExists = UrlExists(url.Url);

                _urlRepository.Create(entity);

                if (urlExists)
                    SendToCache(url.Url);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro in {nameof(Create)} {JsonConvert.SerializeObject(url)} ex {ex.Message}");
                throw;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var entity = _urlRepository.GetById(Id);
                if (entity != null)
                {
                    _urlRepository.Delete(Id);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Urls> Get()
        {
            var response = _urlRepository.Get();
            return response?.OrderByDescending(x => x.Hits).ToList();
        }

        public Urls GetById(int Id)
        {
            try
            {
                return _urlRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Update(Urls url)
        {
            try
            {
                var entity = _urlRepository.GetById(url.Id);
                if (entity != null)
                {
                    _urlRepository.Update(url);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Urls GetByShortUrl(string ShortUrl)
        {
            try
            {
                var response = _urlRepository.Get().Where(x => x.ShortURL == ShortUrl).ToList();
                if (response != null && response.Any())
                {
                    UpdateHits(response.First());
                    return response.First();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public bool ValidateShortUrl(string ShortUrl)
        {
            try
            {
                var entity = _urlRepository.Get().Where(x => x.ShortURL == ShortUrl).ToList();
                if (entity != null && entity.Any())
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Urls> GetUrlTop5()
        {
            var response = _Urls.OrderByDescending(x => x.Hits).Take(5).ToList();
            return response;
        }
    }
}
