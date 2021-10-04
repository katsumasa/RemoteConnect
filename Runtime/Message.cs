using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace UTJ
{
    namespace RemoteConnect
    {
        /// <summary>
        /// RemoteConnectionで送受信するMessageの基底Class
        /// </summary>
        [System.Serializable]
        public class Message
        {
            // メッセージ識別子
            [SerializeField] int m_messageId;

            public int messageId
            {
                get { return m_messageId; }
            }


            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="messageId">メッセージ識別子</param>
            public Message(int messageId = -1)
            {
                m_messageId = messageId;
            }


            /// <summary>
            /// byte配列に変換する
            /// </summary>
            /// <returns>byte配列</returns>
            public byte[] ToBytes()
            {
                return Serialize(this);
            }


            /// <summary>
            /// objectをbyte配列へシリアライズする
            /// </summary>
            /// <param name="obj">object</param>
            /// <returns>byte配列</returns>
            public static byte[] Serialize(object obj)
            {
                using (var ms = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }


            /// <summary>
            /// byte配列からobjectへデシリアライズする
            /// </summary>
            /// <typeparam name="T">Type</typeparam>
            /// <param name="srcs">byte配列</param>
            /// <returns>object</returns>
            public static T Desirialize<T>(byte[] srcs)
            {
                using (var ms = new MemoryStream(srcs))
                {
                    var bf = new BinaryFormatter();
                    return (T)bf.Deserialize(ms);
                }
            }
        }
    }
}