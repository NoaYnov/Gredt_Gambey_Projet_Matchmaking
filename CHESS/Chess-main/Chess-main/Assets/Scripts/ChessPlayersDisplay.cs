using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChessPlayersDisplay : MonoBehaviour
{
    private string url = "http://127.0.0.1/bdd.php";

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

                PlayerData[] players = JsonHelper.FromJsonArray<PlayerData>(jsonResponse);

                if (players == null)
                {
                    Debug.LogError("Deserialization returned null.");
                }
                else
                {
                    foreach (PlayerData player in players)
                    {
                        Debug.Log("ID: " + player.id);
                        Debug.Log("First Name: " + player.first_name);
                        Debug.Log("Last Name: " + player.last_name);
                        Debug.Log("Elo: " + player.elo);
                        Debug.Log("Nationality: " + player.nationality);
                        Debug.Log("Is In Game: " + player.is_in_game);
                    }
                }
            }
        }
    }

}

public class PlayerData
{
    public int id;
    public string first_name;
    public string last_name;
    public int elo;
    public string nationality;
    public bool is_in_game;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        return JsonUtility.FromJson<Wrapper<T>>(json).Items;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static T[] FromJsonArray<T>(string json)
{
    string newJson = "{ \"Items\": " + json + "}";
    Debug.Log("Transformed JSON: " + newJson);
    return FromJson<T>(newJson);
}
}
