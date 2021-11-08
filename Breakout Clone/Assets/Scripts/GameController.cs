using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerInfo
{
    public int Score;
    public GameObject Ball;
    public GameObject Paddle;
    public Text ScoreBoard;
}

public class GameController : MonoBehaviour
{

    public List<PlayerInfo> Players = new List<PlayerInfo>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GamePrep()
    {
        for (int i = 0; i < Players.Count; i++) //Using for to get the index
        {
            if (Players[i].Ball != null)
            {
                Players[i].Ball.GetComponent<BallMovement>().PlayerIndex = i;
            }
        }
        
    }

    void GameUpdate()
    {
        foreach (PlayerInfo player in Players)
        {
            if (player.ScoreBoard != null)
            {
                player.ScoreBoard.text = player.Score.ToString();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameUpdate();
    }
}
