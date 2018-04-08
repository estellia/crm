using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Base
{
    public class AliPayConfig
    {
        static AliPayConfig()
        {
            MD5_Key_Royalty = "3ev1hpmp2ndi9lwok7z24uac8lll6dp3";
            MD5_Key_SoundWave = "ai1ce2jkwkmd3bddy97z0xnz3lxqk731";
            InputCharset = "utf-8";
            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            Partner = "2088701598244705";
            Partner_Royalty = "2088011289712913";
            Partner_SoundWave = "2088201565141845";
            //卖家账户名称
            Seller_Account_Name = "account@jitmarketing.cn";
            Seller_Account_Name_Royalty = "zhifubao@weixun.co";

            //支付宝的公钥
            //如果签名方式设置为“0001”时，请设置该参数
            RSA_PublicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCidRWLiJfzFuKYNYyoW27s70e9yStuiE1Et4aQ qUGL4Z4/vG6p8sJ3JALUTQwZmHwY7GeTqA+n2nUSqpFQLfqUeTBS5IDnxR+5DqL5lOaCDDDw3Uxn CnBdBcfuiwjsEXaDv4sqi/So6tmQlVgi9wFlRc87uM6/kL+WxkhkenIgMwIDAQAB";
            RSA_PublicKey_Royalty = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCPnN+aKYGCd9Eoy2uzfs6hf9F09+9bIUcdJnn/O9Pu2wpdPz+VcVr2gyesMwK+nAjkpdvjjiXRwYfl6mxd3SNhqUaJYEUsWKQwNhLPYWUEtrraVKRPy41Vbm64LJ1CRUXC7ORx2/1BK0ip/KQyaIzzb5m5Q0ry3k0ockcBp6PawwIDAQAB";

            //商户的私钥
            //如果签名方式设置为“0001”时，请设置该参数
            RSA_PrivateKey = @"MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALEtRLU0wUHcmn8seP8Du8P6jBe57/igqtqwKoPU8t6Zxr/U8pNB+ORj4uTLblvT+KdYCR1TIbkvaZWkHaD/dlcVw+xM8dWqvM86Hf+NB7y7st1mzN9xVhkVwr3sc/2B4J3jOQMpEkN19yejf3KCo779zYdldWi6Kk9hpQeYYNqdAgMBAAECgYEAhsoLlVe3FqX/m3R38HokpKm9XmeEWr/Qe2K+VWDyC8stWs9kZAcylH4xJSJmqNGQP69H79lItJuPVdpu+AahPccs+DBt/moUCdsP5IZphk4gNVTJDPxzqO2mPiGJH+gjK0u/DhiOQUKUcOMjTgEkE+U5d0ftjVxm+WT5WiLS3AECQQDXky9sJmgQ2fU+iYnuGTQDs8NXghUgdwd6RjyTf1lOjlBMnZEUhdtDJ4tQe9X9CjgHwrZXF7iOlRV0mIt7FZQdAkEA0mbBwlCYJVluDFSJORzdZmpllZcfRncjQ3vepNvdYgppXJhLNqaLAbSTHXBbIsTTsiVUOi4ipA8crf4FJ9CYgQJAQlVR9E9lGjpXEmU0AgXTUYhRBW5Lne/CZ0eRgDlhe6Ci6NBbQhtmOqXCYoOYdwJb91dc0DPGYGlTbss5sCgVqQJALGWceylQgYkWbKml7xRFL6hB2UfzRIY9Pa80surmExsJUo2cSWLpMCnvZSXhRTvtQ8kWtdQoYSADOD/CzLz6gQJAKixONE4XyXercbnLKUXcMZ6NKpEaoS5eaTAkyzpHMxWmNV1S5k1pViKq8wMhY/GarOEIOEcTbEtYv32zeOvHpw==";
            RSA_PrivateKey_Royalty = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAO/cxqSMduqJ2mWTwbFRN4HMcvgnDW/Oc+UDPeO4rCNLc5jkYdlKHetuviohoaSMGOrQJYnqpsx75Fy2lEvkJjcGBz1kL77EybsEv1L79p2jef6j5sDsH2ChaJ3Awh30p1TYdfo3i1GGAdykvItbMU9itZUiNs0m6rqS5K7cKZPXAgMBAAECgYEAjp/vef6P4ywvMcEnJkGNyN+B6W6HPdk77ov77AFuUdpWlS4PxL2ehtSlvLWcwRQQ6Ob1u0lM/0AX7M0f5vR1h5GXpvHe7JnxJ76iif6SvW7G3MceSOkTnIinYNf2S1xkRnIW67WNTVDVlJL9wldsI6TxaQrOMjtuGa8Y0nMqZJECQQD6VgCMGyIcWXJ9v1r8ploNyPm8mU9QcRsvVEe1uGcTqMHNslVNATR3aS5lxp56yBTn7BQpkLJ7KSqcm+n+k0OPAkEA9Uob4DHKLyukesQUIgwbcwegH6ddUCgHmaqxCFf3Tmpda7tLwRLoTEG7oxTfBvujdqXpSm3FoHzOmxeWYz9nOQJBANPi3V25TZrvPtAemnXEm+6VEITIwvBUe+0IihXOujhSm49uhXLDNVRpC6OLhPJpzgAruzkfR2KlinK6KUmX/hMCQAX8E+gJbvRtrSqtpAwcnYLV+csr6zPsdhsCtiUM+GS6ZaMeQ7/nNTG/HNPiy3pBI4DelW2SdhLvWJ8iGTI8tskCQDM5u00IIDIJfjaQ+JA9Nvcv4Y6TSJ9bOa+b/vLHLW/H77T4z7iz+vax8RzIoNKtEnIb+ZYfsaMLJd6NqTPzoLc=";

            RoyaltyUrl = "https://mapi.alipay.com/gateway.do?";
            OffLineUrl = "https://mapi.alipay.com/gateway.do?";
            WapTradeUrl = "http://wappaygw.alipay.com/service/rest.htm?";
        }
        /// <summary>
        /// RSA公钥,分润
        /// </summary>
        public static readonly string RSA_PublicKey_Royalty;

        /// <summary>
        /// RSA公钥
        /// </summary>
        public static readonly string RSA_PublicKey;

        /// <summary>
        /// RSA私钥,分润
        /// </summary>
        public static readonly string RSA_PrivateKey_Royalty;

        /// <summary>
        /// RSA私钥
        /// </summary>
        public static readonly string RSA_PrivateKey;

        /// <summary>
        /// MD5密钥,分润
        /// </summary>
        public static readonly string MD5_Key_Royalty;

        /// <summary>
        /// MD5密钥,声波
        /// </summary>
        public static readonly string MD5_Key_SoundWave;

        /// <summary>
        /// 编码
        /// </summary>
        public static readonly string InputCharset;
        /// <summary>
        /// 合作身份者ID,分润
        /// </summary>
        public static readonly string Partner_Royalty;
        /// <summary>
        /// 合作身份者ID,无分润
        /// </summary>
        public static readonly string Partner;
        /// <summary>
        /// 合作身份者ID,声波
        /// </summary>
        public static readonly string Partner_SoundWave;
        /// <summary>
        /// 卖家账户名称,分润
        /// </summary>
        public static readonly string Seller_Account_Name_Royalty;
        /// <summary>
        /// 卖家账户名称
        /// </summary>
        public static readonly string Seller_Account_Name;

        /// <summary>
        /// 分润URL
        /// </summary>
        public static readonly string RoyaltyUrl;

        /// <summary>
        /// 线下支付(面支付)
        /// </summary>
        public static readonly string OffLineUrl;

        /// <summary>
        /// 支付URL
        /// </summary>
        public static readonly string WapTradeUrl;

    }
}
