using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FileOperator
    {
        public static bool WriteFile(string FilePath, string Content)
        {
            return WriteFile(FilePath, Content, FileMode.Create);
        }

        public static bool WriteFile(string FilePath, string Content, FileMode FileMode)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                }

                using (FileStream fs = new FileStream(FilePath, FileMode))
                {
                    StreamWriter writer = new StreamWriter(fs);

                    if (FileMode == FileMode.Append)//追加写入
                    {
                        writer.WriteLine(Content);
                    }
                    else
                    {
                        //开始写入
                        writer.Write(Content);
                    }

                    //清空缓冲区
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
