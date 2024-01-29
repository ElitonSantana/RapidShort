using RapidShort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidShort.Domain.Services.Interfaces
{
    public interface IUrlService
    {
        List<Urls> GetUrlTop5();
        List<Urls> Get();
        Urls GetById(int Id);
        bool Create(UrlRequest url);
        bool Update(Urls url);
        bool Delete(int Id);
        Urls GetByShortUrl(string ShortUrl);
        bool ValidateShortUrl(string ShortUrl);
    }
}
