# RemoteConnection

![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/katsumasa/RemoteConnect)

Connecting the Unity Editor and Unity Player.

## 概要

UnityEditorとUnityPlayerで簡単に通信を行う為のパッケージです。

## 動作環境

Unity2019.4LTS以降

## インストール

### using Git

```:console
git clone https://github.com/katsumasa/RemoteConnect.git
```

### using Unity Package Manager

![image](https://user-images.githubusercontent.com/29646672/136918028-7236dbf2-2b47-4ea2-9390-61ea57b5e107.png)

1. Click the add  button in the status bar.
2. The options for adding packages appear.
3. Select Add package from git URL from the add menu. A text box and an Add button appear.
4. Enter https://github.com/katsumasa/RemoteConnect.git in the text box and click Add.

[Click here for details.](https://docs.unity3d.com/2019.4/Documentation/Manual/upm-ui-giturl.html)

## 使い方

1. `UTJ.RemoteConnect.Player`　を継承したClassをGameObjectにAddComponentします。
    ※このGameObjectがUnityPlayerのActiveなSceneに存在する場合にのみUnityEditorとの通信が可能であることに注意して下さい。
2. `UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow`を継承したClassを作成します。
3. 上記で作成した`UTJ.RemoteConnect.Player`・`UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow`を継承したClassに`kMsgSendEditorToPlayer`と`kMsgSendPlayerToEditor`を設定し、データを受信した時の処理を作成しremoteMessageCBへ追加します。
4. Development　Buildにチェックを入れてビルドを行います。
5. ビルドしたアプリケーションをデバイス上で実行します。
6. Unity Editor上で`UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow`を開きます。
7. 上記Window上でプルダウンメニュー`Connect To`から接続するデバイスを選択します。

## スクリプトリファレンス

### UTJ.RemoteConnect.Player

#### 変数

| | 説明 |
|:-|:-|
| remoteMessageCB | Editorからのメッセージを受信した時に実行されるデリゲート |
| kMsgSendEditorToPlayer | EditorからPlayer用固有識別子 |
| kMsgSendPlayerToEditor | PlayerからEditor用固有識別子 |
| isConnected | Editor/Player間の接続が行われているか(read only) |

#### 関数

| | 説明 |
|:-|:-|
| SendRemoteMessage | 任意のバイト配列をUnityEditorへ送信します |

#### Sample

```:cs
public class SamplePlayer : UTJ.RemoteConnect.Player
{
  protected override void OnEnable()
  {
    kMsgSendEditorToPlayer = new System.Guid("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
    kMsgSendPlayerToEditor = new System.Guid("yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
    remoteMessageCB = MessageReciveCB;
    base.OnEnable();
  }

   void MessageReciveCB(UTJ.RemoteConnect.Message remoteMessageBase)
   {
       // 何かメッセージを受信した時の処理
   }
}
```

### UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow

#### 変数

| | 説明 |
|:-|:-|
| remoteMessageCB | Editorからのメッセージを受信した時に実行されるデリゲート |
| kMsgSendEditorToPlayer | EditorからPlayer用固有識別子 |
| kMsgSendPlayerToEditor | PlayerからEditor用固有識別子 |
| isConnected | Editor/Player間の接続が行われているか(read only) |

#### 関数

| | 説明 |
|:-|:-|
| SendRemoteMessage | 任意のバイト配列をUnityEditorへ送信します |

#### Sample

```:cs
public class Sample SampleEditorWindow : RemoteConnectEditorWindow
{
    [MenuItem("Window/Sample")]
    static void OpenWindow()
    {
        var window = (SampleEditorWindow)EditorWindow.GetWindow(typeof(SampleEditorWindow));        
    }

    protected override void OnEnable()
    {
        kMsgSendEditorToPlayer = new System.Guid("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        kMsgSendPlayerToEditor = new System.Guid("yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
        remoteMessageCB = MessageReciveCB;
        base.OnEnable();
    }

    void MessageReciveCB(UTJ.RemoteConnect.Message remoteMessageBase)
    {
       // 何かメッセージを受信した時の処理
    }
}
```

## その他

フィードバック・コメントお待ちしております。

以上
