using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using dotNSGDX;
using dotNSGDX.Utility;

namespace NTP.Core
{
    public class Map
    {
        public NSGDX core;
        public Device Device { get; set; }

        public Map(IRenderer renderer)
        {
            core = new NSGDX(renderer, 512);
        }

        public void Add()
        {
            if (Device == null) return;
            core.Add(Device);
        }

        public static void ToBinary(Map map, string path)
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, map.core.ToArray());
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                stream.Close();
            }
        }

        public static void FromBinary(Map map, string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                object obj = formatter.Deserialize(stream);
                if (!(obj is IObject[]))
                {
                    throw new FileLoadException("File is incorrect.", path);
                }
                map.core.Add((IObject[])obj);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
