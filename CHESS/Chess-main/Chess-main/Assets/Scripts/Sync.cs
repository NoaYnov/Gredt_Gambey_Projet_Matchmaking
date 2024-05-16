using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviourPunCallbacks
{
    void start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
}
