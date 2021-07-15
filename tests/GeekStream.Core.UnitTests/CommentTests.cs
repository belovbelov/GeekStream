using System;
using GeekStream.Core.Entities;
using Xunit;

namespace GeekStream.Core.UnitTests
{
    public class CommentTests
    {
        [Fact]
        public void Constructor_NullContent_ThrowsException()
        {
            string name = "Anton";
            string content = null;
            int articleId = 5;
            Assert.Throws<ArgumentException>(() =>
            {
                 var article = new Comment(name, content, articleId);
            });
        }
    }
}
