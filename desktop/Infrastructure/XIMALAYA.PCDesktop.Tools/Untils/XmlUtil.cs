using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XIMALAYA.PCDesktop.Tools.Untils
{

    public class XmlUtil
    {
        #region 反序列化

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="xml">XML字符串</param> 
        /// <returns></returns> 
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);

                    return xmldes.Deserialize(sr);
                }
            }
            catch 
            {
                return null;
            }
        }
        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type"></param> 
        /// <param name="xml"></param> 
        /// <returns></returns> 
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);

            return xmldes.Deserialize(stream);

        }
        #endregion

        #region 序列化XML文件

        /// <summary> 
        /// 序列化XML文件 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="obj">对象</param> 
        /// <returns></returns> 
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            //创建序列化对象 
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象 
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);

            return sr.ReadToEnd();
        }

        #endregion

        #region 获取对应XML节点的值

        /// <summary> 
        /// 摘要:获取对应XML节点的值 
        /// </summary> 
        /// <param name="stringRoot">XML节点的标记</param> 
        /// <returns>返回获取对应XML节点的值</returns> 

        public static string XmlAnalysis(string stringRoot, string xml)
        {
            if (stringRoot.Equals("") == false)
            {
                try
                {
                    XmlDocument XmlLoad = new XmlDocument();
                    XmlLoad.LoadXml(xml);

                    return XmlLoad.DocumentElement.SelectSingleNode(stringRoot).InnerXml.Trim();
                }
                catch 
                {

                }
            }

            return "";
        }

        #endregion
    }
}
