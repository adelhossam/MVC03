using System.Net;

namespace Company.G03.PL.Helpers
{
    public static class DecumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            //1- Get Folder Path 
            //string FolderPath = Directory.GetCurrentDirectory() + @"wwwroot\files" + FolderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            //2- File Name Must Be Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
             
            //3- Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            //4- Save File as Stream 
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream); // To Save It on Server file must be put in FileStream 

            return fileName; // That Name Will Stor in DataBase
        }
        public static void Delete(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if (File.Exists(filePath)) 
            {
                File.Delete(filePath); 
            }
        }
    }
}
