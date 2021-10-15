using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace UTJ
{
    namespace RemoteConnect
    {        
            public class Player : MonoBehaviour
            {
                public delegate void RemoteMessageCB(Message remoteMessageBase);
                
                /// <summary>
                /// RemoteMessageを受信した時のCB
                /// </summary>
                protected RemoteMessageCB remoteMessageCB;

                /// <summary>
                ///  From Editor to Player用識別子
                /// </summary>
                protected System.Guid kMsgSendEditorToPlayer;


                /// <summary>
                /// From Player To Editor用識別子
                /// </summary>
                protected System.Guid kMsgSendPlayerToEditor;


                protected bool isConnected
                {
                    get
                    {
                        return mIsConnected;
                    }
                }

                protected bool isRegist
                {
                    get { return mIsRegist; }
                }

                protected bool isEnableLog{
                    get{return mIsEnableLog;}
                    set{mIsEnableLog = value;}
                }

                bool mIsRegist = false;
                bool mIsConnected = false;
                bool mIsEnableLog = false;

                /// <summary>
                /// 
                /// </summary>
                protected virtual void OnEnable()
                {
                    if(isEnableLog){
                        Debug.Log("RemotePlayerBase.OnEnable()");
                        Debug.Log("Register(" + kMsgSendEditorToPlayer + ")");
                    }
                    PlayerConnection.instance.RegisterConnection(ConnectionCB);
                    PlayerConnection.instance.RegisterDisconnection(OnDisconnecteCB);
                    PlayerConnection.instance.Register(kMsgSendEditorToPlayer, OnMessageEvent);
                    mIsRegist = true;
                }


                /// <summary>
                /// 
                /// </summary>
                protected virtual void OnDisable()
                {
                    if(isEnableLog){
                        Debug.Log("RemotePlayerBase.OnDisable()");
                    }

                    if (mIsRegist)
                    {
                        if(isEnableLog){
                            Debug.Log("Unregister(" + kMsgSendEditorToPlayer + ")");
                        }
                        PlayerConnection.instance.Unregister(kMsgSendEditorToPlayer, OnMessageEvent);
                        PlayerConnection.instance.UnregisterConnection(ConnectionCB);
                        PlayerConnection.instance.UnregisterDisconnection(OnDisconnecteCB);
                        mIsRegist = false;
                    }
                }


                /// <summary>
                /// Editorへメッセージを送信する
                /// </summary>
                /// <param name="bytes">シリアライズされたRemoteMessageBaseを継承したObject</param>
                protected void SendRemoteMessage(byte[] bytes)
                {
                    PlayerConnection.instance.Send(kMsgSendPlayerToEditor, bytes);
                }


                private void OnMessageEvent(MessageEventArgs args)
                {
                    var messageBase = new Message();
                    messageBase = Message.Desirialize<Message>(args.data);
                    if(isEnableLog){
                        Debug.Log("RemotePlayer messageId:" + messageBase.messageId);
                    }
                    if (remoteMessageCB != null)
                    {
                        remoteMessageCB(messageBase);
                    }
                }

                private void ConnectionCB(int playerid)
                {
                    mIsConnected = true;
                    if(isEnableLog){
                        Debug.Log("ConnectionCB:" + playerid);
                    }
                }

                private void OnDisconnecteCB(int playerid)
                {
                    if(isEnableLog){
                        Debug.Log("DisconnectionCB:" + playerid);
                    }
                    mIsConnected = false;
                }
            }        
    }
}