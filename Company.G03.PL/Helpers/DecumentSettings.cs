using System.Net;

namespace Company.G03.PL.Helpers
{
    public static class DecumentSettings
    {
        public static string UploadFileCompletedEventArgs(IFormFile file , string FolderName)
        {
            //1- Get Folder Path 
            //string FolderPath = Directory.GetCurrentDirectory() + @"wwwroot\files" + FolderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName);

            //2- File Name Must Be Unique
            string fileName = $"{Guid.NewGuid}{file.Name}";

            //3- Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            //4- Save File as Stream 
            var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream); // To Save It on Server file must be put in FileStream 

            return fileName; // That Name Will Stor in DataBase
        }
    }
}
