using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncher
{
    [Serializable()]
    public class FlatImage
    {
        public FlatImage(System.Drawing.Image image, string key)
        {
            _image = image;
            _key = key == null ? "" : key;
        }
        public FlatImage()
        {
            _image = null;
            _key = "";
        }
        public System.Drawing.Image _image { get; set; }
        public string _key { get; set; }
    }
    public class SerializableImageList
    {
        public SerializableImageList()
        {
        }
        public static bool Save(ImageList imageList, FileStream stream, bool closeBeforeExit = false)
        {
            List<FlatImage> fis = new List<FlatImage>();
            for (int index = 0; index < imageList.Images.Count; index++)
                fis.Add(new FlatImage(imageList.Images[index], imageList.Images.Keys[index]));
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, fis);
            if (closeBeforeExit)
                stream.Close();
            return true;
        }
        public static bool Save(ImageList imageList, string filePath)
        {
            FileStream stream = File.OpenWrite(filePath);
            return Save(imageList, stream, true);
        }
        public static ImageList Load(FileStream stream, int imgSize, bool doMultiThread, bool closeBeforeExit = false, string filePath = "") // filePath is only use for error reporting...
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(imgSize, imgSize);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                List<FlatImage> ilc = formatter.Deserialize(stream) as List<FlatImage>;
                for (int index = 0; index < ilc.Count; index++)
                {
                    System.Drawing.Image i = ilc[index]._image;
                    string key = ilc[index]._key;
                    imageList.Images.Add(key as string, i);
                }
                if (closeBeforeExit)
                    stream.Close();
                return imageList;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Load ImageList exception thrown \"{ex.Message}\" for file {filePath}!");
                Form_Main.DbErrorLogging("SerializableImageList.Load", $"Load ImageList exception thrown \"{ex.Message}\"!", ex.StackTrace, $"Input Arg(filePath={filePath}, imgSize={imgSize}, doMultiThread={doMultiThread}, closeBeforeExit={closeBeforeExit})");
            }
            if (closeBeforeExit)
                stream.Close();
            return null;
        }
        public static ImageList Load(string filePath, int imgSize, bool doMultiThread = false)
        {
            FileStream stream = File.OpenRead(filePath);
            return Load(stream, imgSize, doMultiThread, true, filePath);
        }
    }
}
