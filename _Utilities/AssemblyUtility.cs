using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Library
{
    /// <summary>Assembly オブジェクトのユーティリティクラス。
    /// </summary>
    [DebuggerStepThrough]
    public static class AssemblyUtility
    {
        /// <summary>指定したアセンブリの type を定義している名前空間にあるリソースの内容を文字列として取得する。
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetManifestResourceString(this Assembly assembly, Type type, string name)
        {
            using (var stream = assembly.GetManifestResourceStream(type, name))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
