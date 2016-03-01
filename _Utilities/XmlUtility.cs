using System.Text;
namespace Library
{
    /// <summary>XML ユーティリティ
    /// </summary>
    public static class XmlUtility
    {
        /// <summary>シリアライズしたデータを書き込む。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        public static void Serialize<T>(T obj, string filePath)
        {
            using (var sw = new System.IO.StreamWriter(filePath, false, new UTF8Encoding(false)))
                new System.Xml.Serialization.XmlSerializer(typeof(T)).Serialize(sw, obj);
        }

        /// <summary>逆シリアライズする。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath)
        {
            var returnValue = default(T);
            using (var sr = new System.IO.StreamReader(filePath, new UTF8Encoding(false)))
                returnValue = (T)new System.Xml.Serialization.XmlSerializer(typeof(T)).Deserialize(sr);
            return returnValue;
        }
    }
}


// ↓使い方。
//class MainClass
//{
//    public static void Main()
//    {
//        var sample = new SampleClass() { Message = "test" };

//        // 書き込み（シリアル化
//        sample.Save();

//        // 読み込み（逆シリアル化
//        var sample2 = SampleClass.Load();
//    }
//}

//public class SampleClass
//{
//    public int Number;
//    public string Message;
//    public static readonly string FilePath = @".\sampleclass.xml";

//    public static SampleClass Load()
//    {
//        return XmlUtility.Deserialize<SampleClass>(FilePath);
//    }

//    public void Save()
//    {
//        XmlUtility.Serialize<SampleClass>(this, FilePath);
//    }
//}