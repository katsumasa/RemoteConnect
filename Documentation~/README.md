# RemoteConnection

![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/katsumasa/RemoteConnect)

Connecting the Unity Editor and Unity Player.

## Summary

A package that'll make an easy communication between UnityEditor and UnityPlayer.

## Operating Environment

Unity2019.4LTS or higher

## Install

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

## How to use

1. Add Component to GameObject with Class that inherits `UTJ.RemoteConnect.Player`　
    ※Please note that communication with the UnityEditor is possible only if this GameObject is present in the Active Scene of the UnityPlayer.
2. Create a Class that inherits `UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow`
3. Set `kMsgSendEditorToPlayer`and`kMsgSendPlayerToEditor`in the Class that inherits`UTJ.RemoteConnect.Player`,`UTJ.RemoteConnect.Editor`,and `RecmoteConnectEditorWindow` created above. Create the process when data is recieved, it'll add to remoteMessageCB.
4. Put check to Development Build in order to build.
5. Run the built application on the device.
6.Open`UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow`on Unity Editor.
7. Select the device to connect from the pull-down menu `Connect To` on the above Window.

## Script Reference

### UTJ.RemoteConnect.Player

#### Variable

| | Explanation |
|:-|:-|
| remoteMessageCB | Delegate executed when a message from the Editor is received |
| kMsgSendEditorToPlayer | Unique identifier for Player from Editor |
| kMsgSendPlayerToEditor | Unique identifier for Editor from Player |
| isConnected | Connection between Editor/Player (read only) |

#### Function

| | Explanation |
|:-|:-|
| SendRemoteMessage | Send any byte array to UnityEditor |

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
       // Processing when message has been received
   }
}
```

### UTJ.RemoteConnect.Editor.RecmoteConnectEditorWindow

#### Variable

| | Explanation |
|:-|:-|
| remoteMessageCB | Delegate executed when a message from the Editor is received |
| kMsgSendEditorToPlayer | Unique identifier for Player from Editor |
| kMsgSendPlayerToEditor | Unique identifier for Editor from Player |
| isConnected | Connection between Editor/Player (read only) |

#### Function

| | Explanation |
|:-|:-|
| SendRemoteMessage | Send any byte array to UnityEditor |

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
       // Processing when message has been received
    }
}
```

## Other

Appreciate your comments and feedback.

Tha'll be all
