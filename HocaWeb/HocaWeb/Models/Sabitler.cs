using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;

namespace HocaWeb.Models
{
    public class Sabitler
    {

        public static string Sifreolustur(string text)
        {
            string password = "kanlitokak77314520";
            DESEncrypt sifrele = new DESEncrypt();
            string sifrelenmispasword = sifrele.EncryptString(text, password);
            return sifrelenmispasword;
        }
        public static string SifreCöz(string text)
        {
            string password = "kanlitokak77314520";
            DESEncrypt sifrele = new DESEncrypt();
          string cozulmuspasword = sifrele.DecryptString(text, password);
          return cozulmuspasword;
        }
        public List<Sabit> SABITLER
        {
            get
            {
                List<Sabit> Sbt = new List<Sabit>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_sabitler())
                    {
                        Sabit Eleman = new Sabit();
                        Eleman.Text_id = item.h_ıd;
                        Eleman.Text_1 = item.h1_txt1;
                        Eleman.Text_2 = item.h1_txt2;
                        Eleman.Text_3 = item.h1_txt3;
                        Eleman.Text_4 = item.h2_txt1;
                        Eleman.Text_5 = item.h2_txt2;
                        Eleman.Text_6 = item.h2_txt3;
                        Eleman.Text_7 = item.c1_txt1;
                        Eleman.Text_8 = item.c1_txt2;
                        Eleman.Text_9 = item.c1_txt3;
                        Eleman.Text_10 = item.c1_txt4;
                        Eleman.Text_11= item.c1_txt5;
                        Eleman.Text_12 = item.c1_txt6;
                        Eleman.Text_13 = item.c2_txt1;
                        Eleman.Text_14 = item.c2_txt2;
                        Eleman.Text_15 = item.c3_txt1;
                        Sbt.Add(Eleman);
                    }
                }
                return Sbt;
            }
        }

        public List<Resim> RESİM
        {
            get
            {
                List<Resim> rsm = new List<Resim>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_homeBig_Image())
                    {
                        Resim Eleman = new Resim();
                        Eleman.Res_id = item.H_img_id;
                        Eleman.Res_adi = item.H_img_name;
                        
                        Eleman.Res_title = item.H_img_title;
                        Eleman.Res_url = item.H_img_url;
                     
                        rsm.Add(Eleman);
                    }
                }
                return rsm;
            }
        }
        public List<User_Pasword> UserPasword
        {
            get
            {
                List<User_Pasword> u_p= new List<User_Pasword>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_kullanici_bilgi_getir())
                    {
                        User_Pasword Eleman = new User_Pasword();
                        Eleman.U_P_id = item.kullanici_id;
                        Eleman.username = item.kullanici_adi;
                        Eleman.pasword = item.kullanici_sifre;


                        u_p.Add(Eleman);
                    }
                }
                return u_p;
            }
        }
    }

    public class Sabit
    {
        public int Text_id { get; set; }
        public string Text_1 { get; set; }
        public string Text_2 { get; set; }
        public string Text_3 { get; set; }
        public string Text_4 { get; set; }
        public string Text_5 { get; set; }
        public string Text_6 { get; set; }
        public string Text_7 { get; set; }
        public string Text_8 { get; set; }
        public string Text_9 { get; set; }
        public string Text_10 { get; set; }
        public string Text_11 { get; set; }
        public string Text_12 { get; set; }
        public string Text_13 { get; set; }
        public string Text_14 { get; set; }
        public string Text_15 { get; set; }
    }
    public class Resim
    {
        public int Res_id { get; set; }
        public string Res_adi { get; set; }
        public string Res_title { get; set; }
        public string Res_url { get; set; }
      
    }
    public class User_Pasword
    {
        public int U_P_id { get; set; }
        public string username { get; set; }
        public string pasword { get; set; }
        

    }
    //---------------------------------Sifrele---------------------------------
    class DESEncrypt
    {
        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }

        public string EncryptString(string plainText, string password)
        {
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);
            MemoryStream myStream = new MemoryStream();

            TripleDES des = CreateDES(password);
            CryptoStream cryptoStream = new CryptoStream(myStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(myStream.ToArray());
        }

        public string DecryptString(string encryptedText, string password)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);
            MemoryStream myStream = new MemoryStream();

            TripleDES des = CreateDES(password);
            CryptoStream decryptStream = new CryptoStream(myStream, des.CreateDecryptor(), CryptoStreamMode.Write);
            decryptStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
            decryptStream.FlushFinalBlock();

            return Encoding.Unicode.GetString(myStream.ToArray());
        }
    }

}