using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBL.Helper
{
    public static class UploadFile
    {
        private static string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/uploads/";
        public static string UploadFiles(IFormFile file, string FolderName )
        {
            try { 
                string uploads = FolderPath + FolderName;
                string newPath = Path.Combine(uploads, file.FileName);
                using (var Stream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }
                return file.FileName;
            }
            catch ( Exception ex)
            {
                return ex.Message;
            }

        }

        public static bool RemoveFile(string FolderName, string FileName)
        {
            try
            {

                string FullPath = FolderPath + FolderName + "/" + FileName;

                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                }

                

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        }

   }

