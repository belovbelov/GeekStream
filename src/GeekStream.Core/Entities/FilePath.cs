using System.ComponentModel.DataAnnotations;


namespace GeekStream.Core.Entities
{
    public class FilePath
    {
        public FilePath()
        {
            FileName = "DefaultProfilePhoto.png";
            FileType = FileType.Avatar;
        }
        public int Id {get;set;}
        [StringLength(255)]
        public string FileName {get;set;}
        public FileType FileType {get;set;}
    }
}