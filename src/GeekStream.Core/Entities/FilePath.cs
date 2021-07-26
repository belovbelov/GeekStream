using System.ComponentModel.DataAnnotations;


namespace GeekStream.Core.Entities
{
    public class FilePath
    {
        public int FilePathId {get;set;}
        [StringLength(255)]
        public string FileName {get;set;}
        public FileType FileType {get;set;}

        public ApplicationUser User { get; set; }
    }
}