using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyOnlineStore.Utilities
{
    public static class Utils
    {
        public static void ExtractSaveResource(string filename, string location)
        {
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    var full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }

                }
            }
        }
        public static void GetByteArrayFromPath(out byte[] source, string path)
        {
            ExtractSaveResource(path, FileSystem.CacheDirectory);
            Stream str = new StreamReader(Path.Combine(FileSystem.CacheDirectory, path)).BaseStream;
            MemoryStream ms = new MemoryStream();
            str.CopyTo(ms);
            source = ms.ToArray();
        }

        public static async Task<byte[]> GetPhoto(byte[] productImageSource)
        {
            MediaFile mediaFile;
            MemoryStream memoryStream;
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Full, CompressionQuality = 85 });

                if (mediaFile != null)
                {
                    memoryStream = new MemoryStream();
                    mediaFile.GetStream().CopyTo(memoryStream);
                    productImageSource = memoryStream.ToArray();
                }
            }
            else
            {
                //--------------------------------------
                //
                // TODO: Inject PopupView and display 
                //       message of permission.
                //
                //--------------------------------------
            }

            return productImageSource;
        }


        public static async Task<byte[]> TakePhoto(byte[] productImageSource)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            MemoryStream memoryStream = new MemoryStream();
            Image PhotoImage = new Image();
            if (photo != null)
            {
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                photo.GetStream().CopyTo(memoryStream);
                productImageSource = memoryStream.ToArray();
            }

            return productImageSource;
        }
    }
    
}
