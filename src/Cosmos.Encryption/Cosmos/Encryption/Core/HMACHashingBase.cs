﻿using System.Security.Cryptography;
using System.Text;
using Cosmos.Encryption.Core.Internals.Extensions;
using Cosmos.Optionals;

namespace Cosmos.Encryption.Core
{
    /// <summary>
    /// Abstrace HMACHashingBase encryption.
    /// Reference: Seay Xu
    ///     https://github.com/godsharp/GodSharp.Encryption/blob/master/src/GodSharp.Shared/Encryption/Hash/HMACHashingBase/HMAC.cs
    /// Editor: AlexLEWIS
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class HMACHashingBase
    {
        /// <summary>
        /// HMACHashingBase encryption algoritm core.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Security.Cryptography.HMACHashingBase"/> sub-class.</typeparam>
        /// <param name="data">The string to be encrypted,not null.</param>
        /// <param name="key">Encryption key,not null.</param>
        /// <param name="encoding">The <see cref="T:System.Text.Encoding"/>,default is Encoding.UTF8.</param>
        /// <returns>The encrypted string.</returns>
        protected static string Encrypt<T>(string data, string key, Encoding encoding = null) where T : KeyedHashAlgorithm, new()
        {
            Checker.Data(data);
            Checker.Key(key);

            encoding = encoding.SafeValue();

            using (var hash = new T())
            {
                hash.Key = encoding.GetBytes(key);
                return hash.ComputeHash(encoding.GetBytes(data)).ToHexString();
            }
        }
    }
}