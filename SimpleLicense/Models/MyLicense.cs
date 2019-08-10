using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace ijat.my.SimpleLicense.Models
{
    [Serializable]
    public class MyLicense
    {

        [Serializable]
        public class data
        {
            public data()
            {
                this.Id = Guid.NewGuid();
            }

            public Guid Id;
            public String Name;
            public DateTime Created;
            public DateTime Expiry;
            public String ExtraData;
        }

        public data Data;
        public RSAParameters Key;
        public byte[] Signature;

        public MyLicense()
        {
            this.Data = new data();
        }

        public byte[] getData
        {
            get
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    formatter.Serialize(ms, this.Data);
                    return ms.ToArray();
                }
            }
        }

        public data getDataFromBytes(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj as data;
            }
        }

        public static MyLicense Create()
        {
            return new MyLicense();
        }
    }
}
