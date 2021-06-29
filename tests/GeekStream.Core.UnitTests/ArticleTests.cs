using GeekStream.Core.Entities;
using System;
using Xunit;

namespace GeekStream.Core.UnitTests
{
    public class ArticleTests
    {
        [Fact]
        public void Constructor_NullTitle_ThrowsException()
        {
            string title = null;
            string content = "id faucibus nisl tincidunt eget";

            Assert.Throws<ArgumentException>(() =>
            {
                var article = new Article(title, content);
            });
        }
        [Fact]
        public void Constructor_NullContent_ThrowsException()
        {
            string title = "Lorem ipsum 2?";
            string content = null;

            Assert.Throws<ArgumentException>(() =>
            {
                var article = new Article(title, content);
            });
        }
    }
}
