using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Media.Effects;
using System.Windows;

namespace passwrkk.Model;

internal class Encrypter
{
    private static byte[] GetKey(string password, int keySize)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            Array.Resize(ref hash, keySize);
            return hash;
        }
    }

    public static byte[] EncryptString(string password, string plainText)
    {
        using(Aes aes = Aes.Create())
        {
            byte[] key = GetKey(password, aes.KeySize/8);
            aes.Key = key;
            aes.GenerateIV();

            using(MemoryStream ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);
                using (CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)) 
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }
                return ms.ToArray();
            }
        }
    }

    public static void SaveToFile(string filePath, string password, string plainText)
    {
        var data = EncryptString(password, plainText);
        File.WriteAllBytes(filePath, data);
    }

    public static string ReadDataFromFile(string filePath, string password)
    {
        var bytes = File.ReadAllBytes(filePath);
        return DecryptString(bytes, password);
    }

        public static string DecryptString(byte[] cipherText, string password)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = GetKey(password, aes.KeySize / 8);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    byte[] iv = new byte[aes.BlockSize / 8];
                    ms.Read(iv, 0, iv.Length);
                    aes.Key = key;
                    aes.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }

