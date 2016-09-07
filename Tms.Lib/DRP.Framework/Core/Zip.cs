using System;
using ICSharpCode.SharpZipLib.Zip.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace DRP.Framework.Core
{
    /// <summary>
    /// 压缩、解压
    /// 依赖<see cref="ICSharpCode.SharpZipLib.dll"/>
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] pBytes)
        {
            MemoryStream mMemory = new MemoryStream();
            Deflater mDeflater = new Deflater(Deflater.BEST_COMPRESSION);
            using (DeflaterOutputStream mStream = new DeflaterOutputStream(mMemory, mDeflater, 131072))
            {
                mStream.Write(pBytes, 0, pBytes.Length);
                mStream.Close();
                return mMemory.ToArray();
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static byte[] DeCompress(byte[] pBytes)
        {
            InflaterInputStream mStream = new InflaterInputStream(new MemoryStream(pBytes));
            using (MemoryStream mMemory = new MemoryStream())
            {
                Int32 mSize;
                byte[] mWriteData = new byte[4096];
                while (true)
                {
                    mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
                    if (mSize > 0)
                    {
                        mMemory.Write(mWriteData, 0, mSize);
                    }
                    else
                    {
                        break;
                    }
                }
                mStream.Close();
                return mMemory.ToArray();
            }
        }
    }
}
