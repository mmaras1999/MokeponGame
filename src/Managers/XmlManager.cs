using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace MokeponGame.Managers
{
    public class XmlManager
    {
        private static XmlManager instance;
        public static XmlManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new XmlManager();
                return instance;
            }

        }

        //LOAD OBJECT FROM XML FILE
        public T Load<T>(string path)
        {
            T to_load;
            
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                to_load = (T)xml.Deserialize(reader);
            }

            return to_load;
        }

        //SAVE OBJECT TO XML FILE
        public void Save<T>(T to_save, string path)
        {
            using (TextWriter writer = new StreamWriter(path, false))
            {
                XmlSerializer xml = new XmlSerializer(to_save.GetType());
                xml.Serialize(writer, to_save);
            }
        }
    }
}
