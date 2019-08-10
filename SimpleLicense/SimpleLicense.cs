using ijat.my.SimpleLicense.Misc;
using ijat.my.SimpleLicense.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using ijat.my.SimpleLicense.Models;
using ijat.my.Misc;

namespace ijat.my.SimpleLicense
{
    public class SimpleLicense : ISimpleLicense
    {
        private RSA _rsa;

        public void CreateNewLicense(string licenseFile, DateTime expiryDate, String name = null, String otherMetadata = null)
        {
            MyLicense thisLicense = new MyLicense();
            thisLicense.Data.Created = DateTime.UtcNow;
            thisLicense.Data.Expiry = expiryDate;
            thisLicense.Data.ExtraData = otherMetadata;
            thisLicense.Data.Name = name;
            CreateNewLicense(licenseFile, thisLicense);
        }

        public void CreateNewLicense(string licenseFile, MyLicense license)
        {
            license.Signature = _rsa.SignData(license.getData, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            license.Key = _rsa.ExportParameters(false);
            File.WriteAllText(licenseFile, LicenseExtensions.LicenseToXmlString(license));
        }

        public bool VerifyLicense(string licenseFile)
        {
            return VerifyLicense(GetLicense(licenseFile));
        }

        public bool VerifyLicense(MyLicense license)
        {
            RSA rsa = RSA.Create();
            rsa.ImportParameters(license.Key);

            if (rsa.VerifyData(license.getData, license.Signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1))
            {
                if (license.Data.Expiry < DateTime.Now) return false;
                return true;
            }

            return false;
        }


        public MyLicense GetLicense(String licenseFile)
        {
            string xmlString = File.ReadAllText(licenseFile);
            return LicenseExtensions.XmlStringToLicense(xmlString);
        }

        public SimpleLicense()
        {
            _rsa = RSA.Create();
        }

        /// <summary>Create (if not exists) or load private key for licensing. Should not be used by client software</summary>
        /// <example>
        /// <code>
        /// SimpleLicense myLicenseProvider = SimpleLicense.InitFile("main.key");
        /// </code>
        /// </example>
        /// <param name="keyFilePath">File path to the key file. If not exists, a new key file will be created.</param>
        public static SimpleLicense Init(String keyFilePath)
        {
            try
            {
                SimpleLicense license = null;
                string xmlString = String.Empty;

                if (!File.Exists(keyFilePath))
                    CreateInitFile(keyFilePath);
                
                xmlString = File.ReadAllText(keyFilePath);
                license = new SimpleLicense();
                RSAKeyExtensions.FromXmlString(license._rsa, xmlString);

                return license;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void CreateInitFile(String filePath)
        {
            RSA rsa = RSA.Create();
            string xmlString = RSAKeyExtensions.ToXmlString(rsa, true);
            File.WriteAllText(filePath, xmlString);
        }
    }
}
