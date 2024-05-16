﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public Board board;
    public PieceManager pieceManager;
    

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        board.Create();
        pieceManager.Setup(board);
    }

    public void BackMenu()
    {
        if (PieceManager.IAmode)
            pieceManager.stockfish.Close();
        SceneManager.LoadScene(0); // Menu
    }

    public void Reload()
    {
        pieceManager.ResetGame();
    }

    public void Reverse()
    {
        board.transform.localRotation *= Quaternion.Euler(180, 180, 0);
        foreach (List<Cell> row in board.allCells)
        {
            foreach (Cell boardCell in row)
            {
                if(boardCell.currentPiece != null)
                    boardCell.currentPiece.PlaceInit(boardCell);
            }
        }
    }
}

