using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Playerdata : MonoBehaviour
{
    public string server = "127.0.0.1";
    public string database = "chess_player";
    public string user = "root@localhost";
    public string password = "";
    public string port = "80";

    public Transform contentPanel;
    public GameObject playerItemPrefab;
    public Canvas canvas;

    void Start()
    {
        LoadPlayers();
    }
    public void activeCanvas()
    {
        canvas.gameObject.SetActive(true);
    }
    public void desactiveCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    void LoadPlayers()
    {
        List<Player> players = new List<Player>();
        string connectionString = $"Server={server};Database={database};User ID={user};Password={password};Port={port};";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT first_name, last_name, elo, nationality, is_in_game FROM players";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Player player = new Player
                {
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    Elo = reader.GetInt32("elo"),
                    Nationality = reader.GetString("nationality"),
                    IsInGame = reader.GetBoolean("is_in_game")
                };
                players.Add(player);
            }
        }

        foreach (var player in players)
        {
            GameObject newPlayerItem = Instantiate(playerItemPrefab, contentPanel);
            PlayerItemController controller = newPlayerItem.GetComponent<PlayerItemController>();
            controller.SetPlayerInfo(player);
        }
    }
}

[Serializable]
public class Player
{
    public string FirstName;
    public string LastName;
    public int Elo;
    public string Nationality;
    public bool IsInGame;
}

public class PlayerItemController : MonoBehaviour
{
    public Text firstNameText;
    public Text lastNameText;
    public Text eloText;
    public Text nationalityText;
    public Text isInGameText;

    public void SetPlayerInfo(Player player)
    {
        firstNameText.text = player.FirstName;
        lastNameText.text = player.LastName;
        eloText.text = "Elo: " + player.Elo.ToString();
        nationalityText.text = "Nationality: " + player.Nationality;
        isInGameText.text = player.IsInGame ? "In Game" : "Not In Game";
    }

    
}
