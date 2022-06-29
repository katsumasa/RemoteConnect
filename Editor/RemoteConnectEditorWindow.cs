
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
#if UNITY_2020_1_OR_NEWER
using ConnectionUtility = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUIUtility;
using ConnectionGUILayout = UnityEditor.Networking.PlayerConnection.PlayerConnectionGUILayout;
using UnityEngine.Networking.PlayerConnection;
#else
using ConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
using ConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
using UnityEngine.Experimental.Networking.PlayerConnection;
#endif




namespace UTJ
{
    namespace RemoteConnect
    {
        namespace Editor
        {
            public class RemoteConnectEditorWindow : EditorWindow
            {
                public delegate void RemoteMessageCB(UTJ.RemoteConnect.Message remoteMessageBase);
                public delegate void EventMessageCB(byte[] bytes);
                
                
                /// <summary>
                /// RemoteMessageを受信した時のCB
                /// </summary>
                protected RemoteMessageCB remoteMessageCB;

                protected EventMessageCB eventMessageCB;


                /// <summary>
                ///  From Editor to Player用識別子
                /// </summary>
                protected System.Guid kMsgSendEditorToPlayer;


                /// <summary>
                /// From Player To Editor用識別子
                /// </summary>
                protected System.Guid kMsgSendPlayerToEditor;


                IConnectionState m_connectionState;


                protected virtual void OnEnable()
                {
                    if (EditorConnection.instance == null)
                    {
                        EditorConnection.instance.Initialize();
                    }
                    EditorConnection.instance.Register(kMsgSendPlayerToEditor, OnMessageEvent);

#if UNITY_2020_1_OR_NEWER
                    m_connectionState = ConnectionUtility.GetConnectionState(this);
#else
            m_connectionState = ConnectionUtility.GetAttachToPlayerState(this);
#endif

                }


                protected virtual void OnDisable()
                {
                    EditorConnection.instance.Unregister(kMsgSendPlayerToEditor, OnMessageEvent);
                    m_connectionState.Dispose();
                }


                /// <summary>
                /// 接続先を選択する為のGUI
                /// </summary>
                protected void ConnectionTargetSelectionDropdown()
                {
                    var contents = new GUIContent("Connect To");
                    var v2 = EditorStyles.label.CalcSize(contents);
                    UnityEditor.EditorGUILayout.LabelField(contents, GUILayout.Width(v2.x));
                    if (m_connectionState != null)
                    {
#if UNITY_2020_1_OR_NEWER
                        ConnectionGUILayout.ConnectionTargetSelectionDropdown(m_connectionState, EditorStyles.toolbarDropDown);
#else
                        ConnectionGUILayout.AttachToPlayerDropdown(m_connectionState, EditorStyles.toolbarDropDown);
#endif
                    }
                }


                protected void SendRemoteMessage(byte[] bytes)
                {
                    EditorConnection.instance.Send(kMsgSendEditorToPlayer, bytes);
                }


                private void OnMessageEvent(UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
                {
                    if (eventMessageCB != null)
                    {
                        eventMessageCB(args.data);
                    }

                    if (remoteMessageCB != null)
                    {
                        var messageBase = new RemoteConnect.Message();
                        messageBase = RemoteConnect.Message.Desirialize<RemoteConnect.Message>(args.data);
                        remoteMessageCB(messageBase);
                    }                    
                }
            }
        }
    }
}
#endif