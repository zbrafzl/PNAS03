using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


public static class ExtentionFunctions
{
    public static bool TooBoolean(this object input, bool defaultValue = false)
    {
        bool result = false;
        try
        {
            result = Convert.ToBoolean(input);
        }
        catch (Exception)
        {
            result = defaultValue;
        }
        return result;
    }

    public static string TooString(this object input, string defaultValue = "")
    {
        string result = "";
        try
        {
            result = Convert.ToString(input);
        }
        catch (Exception)
        {
            result = defaultValue;
        }
        return result;
    }

    public static decimal TooDecimal(this object input)
    {
        decimal result = 0;
        try
        {
            result = Convert.ToDecimal(input);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static int TooInt(this object input)
    {
        int result = 0;
        try
        {
            result = Convert.ToInt32(input);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static long TooLong(this object input)
    {
        long result = 0;
        try
        {
            result = Convert.ToInt64(input);
        }
        catch (Exception)
        {
        }
        return result;
    }


    public static short TooShort(this object input)
    {
        short result = 0;
        try
        {
            result = Convert.ToInt16(input);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static string TooDateString(this DateTime input)
    {
        string result = "";
        try
        {
            result = input.Day + "-" + input.Month + "-" + input.Year;
        }
        catch (Exception)
        {
            result = "";
        }
        return result;
    }

    public static DateTime TooDate(this DateTime input)
    {
        DateTime result = DateTime.Now;
        try
        {
            result = Convert.ToDateTime(input);
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static DateTime TooDate(this string input)
    {
        DateTime result = DateTime.Now;
        try
        {
            string[] dts = input.Split('/');
            result = new DateTime(dts[2].TooInt(), dts[1].TooInt(), dts[0].TooInt());
        }
        catch (Exception)
        {
            result = DateTime.Now;
        }
        return result;
    }

    public static DateTime TooDate(this string input, char delimeter)
    {
        DateTime result = DateTime.Now;
        try
        {
            string[] dts = input.Split(delimeter);
            result = new DateTime(dts[2].TooInt(), dts[1].TooInt(), dts[0].TooInt());
        }
        catch (Exception)
        {
        }
        return result;
    }

    public static string TooDateSql(this string input, char delimeter, bool toCurrentDate = false)
    {
        string result = "";
        try
        {
            string[] dts = input.Split(delimeter);
            result = dts[2] + "-" + dts[1] + "-" + dts[0];
        }
        catch (Exception)
        {
            if (toCurrentDate == true)
            {
                DateTime dt = DateTime.Now;
                result = dt.Year + "-" + dt.Month + "-" + dt.Day;
            }
        }
        return result;
    }

    public static string TooDateSql(this string input, bool toCurrentDate = false)
    {
        string result = "";
        try
        {
            string[] dts = input.Split('/');
            result = dts[2] + "-" + dts[1] + "-" + dts[0];
        }
        catch (Exception)
        {
            if (toCurrentDate == true)
            {
                DateTime dt = DateTime.Now;
                result = dt.Year + "-" + dt.Month + "-" + dt.Day;
            }
        }
        return result;
    }

    public static string Encrypt(this string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public static string Decrypt(this string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

}
namespace prp.fn
{
    class GenericFunctions
    {
    }
}
