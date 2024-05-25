using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class ChessPlayersDisplay : MonoBehaviour
{
    private string url = "http://127.0.0.1/bdd.php";
    public GameObject textPrefab;
    public Transform contentTransform;
    public TextMeshProUGUI textComponent;

    void Start()
    {
        StartCoroutine(GetPlayerData());
    }

    IEnumerator GetPlayerData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);

                List<PlayerData> players = DataParser.ParsePlayerData(jsonResponse);

                if (players == null)
                {
                    Debug.LogError("Deserialization returned null.");
                }
                else
                {
                    string playerInfo = "";
                    foreach (PlayerData player in players)
                    {
                        playerInfo += $"Player: {player.first_name} {player.last_name}, ELO: {player.elo}\n\n";
                    }

                    textComponent.text = playerInfo;
                }
            }
        }
    }

}

[System.Serializable]
public class PlayerData
{
    public int id;
    public string first_name;
    public string last_name;
    public int elo;
    public string nationality;
    public bool is_in_game;
}
[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> players;
}
public class DataParser
{
    public static List<PlayerData> ParsePlayerData(string json)
    {
        PlayerDataList playerDataList = JsonUtility.FromJson<PlayerDataList>("{\"players\":" + json + "}");
        return playerDataList.players;
    }
}