using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public List<RectTransform> players;
    public List<GameObject> kills;
    public List<GameObject> wins;
    public List<GameObject> lines;
    public int numberOfPlayers;
    public GameObject endGame;
    public GameObject endGameText;
    public GameObject[] killIcon;

    public string[] names = { "Player 1", "Player 2", "Player 3", "Player 4" };
    public Color[] colors = { new Color(60, 228, 60), new Color(255, 52, 33), new Color(33, 181, 255), new Color(255, 217, 7) };

    void Awake()
    {
        players[0].gameObject.SetActive(false);
        players[1].gameObject.SetActive(false);
        players[2].gameObject.SetActive(false);
        players[3].gameObject.SetActive(false);
        numberOfPlayers = playerManager.playerHandlers.Length;

        switch (numberOfPlayers)
        {
            case 2:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                break;
            case 3:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                players[2].gameObject.SetActive(true);
                break;                      
            case 4:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);  
                players[2].gameObject.SetActive(true);
                players[3].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }


    public void EndGame()
    {
        endGame.SetActive(true);
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (playerManager.playerStats[i].alive)
            {
                endGameText.GetComponent<TextMeshProUGUI>().text = names[i];
                endGameText.GetComponent<TextMeshProUGUI>().color = colors[i]; 
            }
        }
    }
    public void UIReset()
    {
        endGame.SetActive(false);
    }
    public void UpdateUI()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            wins[i].GetComponent<TextMeshProUGUI>().text = playerManager.playerStats[i].wins.ToString();
            kills[i].GetComponent<TextMeshProUGUI>().text = playerManager.playerStats[i].kils.ToString();
        }
    }
    public void KillIconReset()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            killIcon[i].SetActive(false);
        }
    }
    public void ActivateKillIcon(int id)
    {
         killIcon[id-1].SetActive(true);
    }
}
