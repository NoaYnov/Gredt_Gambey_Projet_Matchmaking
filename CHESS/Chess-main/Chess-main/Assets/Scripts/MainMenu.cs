using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_Dropdown ddTime;
    public TMP_Dropdown ddLevel;
    public TMP_Dropdown ddIASide;
    public Button btn;
    public Text feedbackText;
    private byte maxPlayersPerRoom = 4;
    private bool isConnecting;
    private string gameVersion = "1";

    /// <summary>
    /// Launch game 1vs1 mode
    /// </summary>
    public void PlayGame()
    {
        if (ddTime.value == 0)
        {
            PieceManager.whiteTime = 60;
            PieceManager.blackTime = 60;
        }
        if (ddTime.value == 1)
        {
            PieceManager.whiteTime = 300;
            PieceManager.blackTime = 300;
        }
        if (ddTime.value == 2)
        {
            PieceManager.whiteTime = 900;
            PieceManager.blackTime = 900;
        }
        if (ddTime.value == 3)
        {
            PieceManager.whiteTime = 3600;
            PieceManager.blackTime = 3600;
        }

        PieceManager.IAmode = false;
        Connect();
    }

    /// <summary>
    /// Launch game player vs AI mode
    /// </summary>
    public void PlayIA()
    {
        PieceManager.IAmode = true;

        if (ddIASide.value == 0)
            PieceManager.isIAWithe = false;
        if (ddIASide.value == 1)
            PieceManager.isIAWithe = true;

        IA.level = IA.IA_Level[ddLevel.value];

        SceneManager.LoadScene(1); // Game
    }

    /// <summary>
    /// Exit application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }


    public void Connect()
    {
        feedbackText.text = "";

        isConnecting = true;

        btn.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            LogFeedback("Connecting...");

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }
    void LogFeedback(string message)
    {
        // we do not assume there is a feedbackText defined.
        if (feedbackText == null)
        {
            return;
        }

        // add new messages as a new line and at the bottom of the log.
        feedbackText.text += System.Environment.NewLine + message;
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
        Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");

        // #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.

        isConnecting = false;
        btn.interactable = true;
    }
    public override void OnJoinedRoom()
    {
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

        // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.AutomaticallySyncScene to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");

            // #Critical
            // Load the Room Level. 
            PhotonNetwork.LoadLevel("GameScene");

        }
    }
}
