using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppDepartmentManager.Serivices
{
    public class ImageService
    {
        public string SaveImageFromBase64(string imgB64, int id)
        {
            if (!string.IsNullOrEmpty(imgB64))
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), id + ".tmp");

                byte[] data = Convert.FromBase64String(imgB64);

                File.WriteAllBytes(filePath, data);

                return filePath;
            }
            else
            {
                return "";
            }
        }

        public ImageSource ConvertImageFromBase64ToImageSource(string imgBase64)
        {
            if (!string.IsNullOrEmpty(imgBase64))
            {
                return ImageSource.FromStream(() =>
                    new MemoryStream(System.Convert.FromBase64String(imgBase64))
                );
            }
            else
            {
                return null;
            }
        }

        public async Task<string> ConvertImageFileToBase64(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileStream stream = File.Open(filePath, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                return System.Convert.ToBase64String(bytes);
            }
            else
            {
                return null;
            }
        }
    }
}
