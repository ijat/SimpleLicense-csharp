using ijat.my.SimpleLicense.Models;
using System;

namespace ijat.my.SimpleLicense.Interfaces
{
    public interface ISimpleLicense
    {
        /// <summary>
        /// Generate a new license for your client software. NOTE: Client software should never uses this method.
        /// </summary>
        /// <param name="licenseFile">Path to license file</param>
        /// <param name="expiryDate">License expiry date</param>
        /// <param name="name">(Optional) Name for this license</param>
        /// <param name="otherMetadata">(Optional) Other metadata to be included as string</param>
        void CreateNewLicense(String licenseFile, DateTime expiryDate, String name = null, String otherMetadata = null);

        /// <summary>
        /// Generate a new license from MyLicense object
        /// </summary>
        /// <param name="licenseFile">Path to license file</param>
        /// <param name="license">MyLicense object</param>
        void CreateNewLicense(String licenseFile, MyLicense license);

        /// <summary>
        /// Verify license file with expiration.
        /// </summary>
        /// <param name="licenseFile"></param>
        /// <returns></returns>
        bool VerifyLicense(string licenseFile);

        /// <summary>
        /// Verify license file with expiration
        /// </summary>
        /// <param name="license">MyLicense object</param>
        /// <seealso cref="GetLicense(string)"/>
        /// <returns></returns>
        bool VerifyLicense(MyLicense license);

        /// <summary>
        /// Read license data from file
        /// </summary>
        /// <param name="licenseFile">Path to client license file</param>
        /// <returns></returns>
        MyLicense GetLicense(String licenseFile);
    }
}
