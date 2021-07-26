using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    public class KeywordService
    {
        private readonly IKeywordRepository _keywordRepository;

        public KeywordService(IKeywordRepository keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }

        public async IEnumerable<Keyword> SaveKeywordsAsync(string keywordsString, Article article)
        {
            List<Keyword> keywords = new List<Keyword>();
            foreach (var word in keywordsString.Split(" "))
            {
                var keyword = new Keyword
                {
                    Word = word,
                    Article = article
                };
                keywords.Add(keyword);
                await _keywordRepository.SaveAsync(keyword);
            }
            return keywords;
        }
    }
}