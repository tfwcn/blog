using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    /// <summary>
    /// RSA加密算法（验签部分还没加入）
    /// </summary>
    public class RSAHelper
    {
        private RSA rsa;

        /// <summary>
        /// 公钥
        /// </summary>
        public string PubKey { get; private set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivKey { get; private set; }

        public RSAHelper(string privKey, string pubKey)
        {
            this.PrivKey = privKey;
            this.PubKey = pubKey;

        }

        /// <summary>
        /// 读取密钥参数
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public byte[] ReadParaItem(MemoryStream ms)
        {
            if (ms.ReadByte() == 0x02)
            {
                int len = 0;
                byte tmpByte = (byte)ms.ReadByte();
                if (tmpByte == 0x81)//数据
                {
                    byte[] tmpBytes = new byte[1];
                    ms.Read(tmpBytes, 0, 1);
                    //长度
                    len = tmpBytes[0];
                }
                else if (tmpByte == 0x82)//数据
                {
                    byte[] tmpBytes = new byte[2];
                    ms.Read(tmpBytes, 0, 2);
                    //长度
                    len = (tmpBytes[0] << 8) + tmpBytes[1];
                }
                else
                {
                    throw new Exception("异常格式");
                }
                bool isPadding = true;//去掉开头00填充
                List<byte> resultBytes = new List<byte>();
                for (int i = 0; i < len; i++)
                {
                    byte[] tmpBytes = new byte[1];
                    if (ms.Read(tmpBytes, 0, 1) > 0)
                    {
                        if (isPadding && tmpBytes[0] == 0x00)
                        {
                            continue;
                        }
                        else
                        {
                            isPadding = false;
                            resultBytes.AddRange(tmpBytes);
                        }
                    }
                    else
                    {
                        throw new Exception("长度异常");
                    }
                }
                return resultBytes.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析Pkcs1密钥
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RSAParameters CreateRSAParameters(string key)
        {
            RSAParameters parameters = new RSAParameters();
            //解码密钥
            byte[] keyBytes = Convert.FromBase64String(key);
            //判断是否公钥
            bool isPubKey = false;
            if (keyBytes[1] == 0x81 && (keyBytes[4] == 0x81 || keyBytes[4] == 0x82))
            {
                isPubKey = true;
            }
            else if (keyBytes[1] == 0x82 && (keyBytes[5] == 0x81 || keyBytes[5] == 0x82))
            {
                isPubKey = true;
            }
            using (var ms = new MemoryStream(keyBytes))
            {
                int len = 0;
                byte[] headBytes = new byte[2];
                ms.Read(headBytes, 0, 2);
                if (headBytes[0] == 0x30 && headBytes[1] == 0x81)
                {
                    byte[] tmpBytes = new byte[1];
                    ms.Read(tmpBytes, 0, 1);
                    //总长度
                    len = tmpBytes[0];
                }
                else if (headBytes[0] == 0x30 && headBytes[1] == 0x82)
                {
                    byte[] tmpBytes = new byte[2];
                    ms.Read(tmpBytes, 0, 2);
                    //总长度
                    len = (tmpBytes[0] << 8) + tmpBytes[1];
                }
                else
                {
                    throw new Exception("异常开头");
                }
                //私钥参数
                if (isPubKey == false)
                {
                    //版本号,versionBytes[2]==0(标准密钥)，versionBytes[2]==1(含多个参数)
                    byte[] versionBytes = new byte[3];
                    ms.Read(versionBytes, 0, 3);
                }

                //读取Modulus
                parameters.Modulus = ReadParaItem(ms);
                //读取Exponent
                if (ms.ReadByte() == 0x02)
                {
                    byte[] tmpBytes = new byte[1];
                    ms.Read(tmpBytes, 0, 1);
                    //长度
                    len = tmpBytes[0];
                    parameters.Exponent = new byte[len];
                    ms.Read(parameters.Exponent, 0, len);
                }
                //私钥参数
                if (isPubKey == false)
                {
                    //读取D
                    parameters.D = ReadParaItem(ms);
                    //读取P
                    parameters.P = ReadParaItem(ms);
                    //读取Q
                    parameters.Q = ReadParaItem(ms);
                    //读取DP
                    parameters.DP = ReadParaItem(ms);
                    //读取DQ
                    parameters.DQ = ReadParaItem(ms);
                    //读取InverseQ
                    parameters.InverseQ = ReadParaItem(ms);
                }
            }
            return parameters;
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Encrypt(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (var rsa = RSA.Create())
            {
                //解码公钥
                RSAParameters parameters = CreateRSAParameters(PubKey);
                rsa.ImportParameters(parameters);
                byte[] result = rsa.Encrypt(inputBytes, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(result);
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Decrypt(string input)
        {
            byte[] inputBytes = Convert.FromBase64String(input);
            using (var rsa = RSA.Create())
            {
                //解码私钥
                RSAParameters parameters = CreateRSAParameters(PrivKey);
                rsa.ImportParameters(parameters);
                byte[] result = rsa.Decrypt(inputBytes, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(result);
            }
        }
    }
}
