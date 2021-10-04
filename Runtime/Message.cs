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
        /// RemoteConnection�ő���M����Message�̊��Class
        /// </summary>
        [System.Serializable]
        public class Message
        {
            // ���b�Z�[�W���ʎq
            [SerializeField] int m_messageId;

            public int messageId
            {
                get { return m_messageId; }
            }


            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            /// <param name="messageId">���b�Z�[�W���ʎq</param>
            public Message(int messageId = -1)
            {
                m_messageId = messageId;
            }


            /// <summary>
            /// byte�z��ɕϊ�����
            /// </summary>
            /// <returns>byte�z��</returns>
            public byte[] ToBytes()
            {
                return Serialize(this);
            }


            /// <summary>
            /// object��byte�z��փV���A���C�Y����
            /// </summary>
            /// <param name="obj">object</param>
            /// <returns>byte�z��</returns>
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
            /// byte�z�񂩂�object�փf�V���A���C�Y����
            /// </summary>
            /// <typeparam name="T">Type</typeparam>
            /// <param name="srcs">byte�z��</param>
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