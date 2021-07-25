using System.ComponentModel.DataAnnotations;


namespace GeekStream.Core.Entities
{
    public class File
    {
            public int FileId { get; set; }
            [StringLength(255)]
            public string FileName { get; set; }
            [StringLength(100)]
            public string ContentType { get; set; }
            public byte[] Content { get; set; }
            public FileType FileType { get; set; }
            public string UserId { get; set; }
            public virtual ApplicationUser User{ get; set; }
    }
}