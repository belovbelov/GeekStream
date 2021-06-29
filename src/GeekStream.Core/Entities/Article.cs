using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GeekStream.Core.Entities
{
    public class Article
    {
        public Article(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(nameof(title));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(nameof(content));
            }

            Title = title;
            Content = content;
        }

        public int ArticleID 
        { get; set; }

        public string Title 
        { get; set; }

        public string Content 
        { get; set; }

        public string ShortDescribtion 
        { get; set; }
        
        public DateTime PostedOn 
        { get; set; }

        public IList<string> Keywords 
        { get; set; }

        public Category Category 
        { get; set; }

        //TODO Добавить информацию об Авторе И Рейтинг статьи
        //public КЛАСС_ЮЗЕРА Author
        //{ get; set; }
    }
}
