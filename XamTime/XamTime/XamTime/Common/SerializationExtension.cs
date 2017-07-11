using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XamTime
{

    public static class SerializationExtension
    {
        /// <summary>
        /// Serialize with default XmlWriterSettings
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this object obj)
        {
            return Serialize(obj, new XmlWriterSettings());
        }
        /// <summary>
        /// Serialize object using XmlSerializer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(this object obj, XmlWriterSettings settings)
        {
            if (obj == null)
                return string.Empty;

            var strBldr = new System.Text.StringBuilder();
            var serializer = new XmlSerializer(obj.GetType());

            using (var writer = XmlWriter.Create(strBldr, settings))
            {
                serializer.Serialize(writer, obj);
            }
            return strBldr.ToString();
        }

        /// <summary>
        /// Deserialize using default XmlReaderSettings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml) where T : class
        {            
            return Deserialize<T>(xml, new XmlReaderSettings() );
        }
        /// <summary>
        /// Deserialize the given xml to an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml, XmlReaderSettings settings) where T : class
        {
            return typeof(T).Deserialize(xml, settings) as T;
        }
        /// <summary>
        /// Deserialize the given xml to an object 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="xml"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static object Deserialize(this Type t, string xml, XmlReaderSettings settings)
        {
            object obj = null;
            try
            {
                var serializer = new XmlSerializer(t);

                using (var reader = new StringReader(xml))
                {
                    using (var xmlReader = XmlReader.Create(reader, settings))
                    {
                        obj = serializer.Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return obj;
        }
    }
}
