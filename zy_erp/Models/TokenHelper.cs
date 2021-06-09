using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zy_erp.Models
{
    public class TokenHelper
    {
        /// <summary>
        /// jwt私钥
        /// </summary>
        private const string SecretKey = "jiangxilong";

        /// <summary>
        /// 创建新token
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">payload 为 null。</exception>
        public static string GenerateToken(Dictionary<string, object> payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            var tokenInfo = new TokenInfo();

            payload.Add("iss", tokenInfo.iss);
            payload.Add("iat", tokenInfo.iat);
            payload.Add("exp", tokenInfo.exp);
            payload.Add("aud", tokenInfo.aud);
            payload.Add("sub", tokenInfo.sub);
            payload.Add("jti", tokenInfo.jti);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, SecretKey);
            return token;
        }

        /// <summary>
        /// 解码token
        /// </summary>
        /// <param name="strToken">token字符串</param>
        /// <returns>json内容</returns>
        /// <exception cref="ArgumentNullException">strToken 为 null、空和由空白字符组成。</exception>
        public static string GetDecodingToken(string strToken)
        {
            if (string.IsNullOrWhiteSpace(strToken))
                throw new ArgumentNullException(nameof(strToken));

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

            var json = decoder.Decode(strToken, SecretKey, true);
            return json;
        }
    }
}