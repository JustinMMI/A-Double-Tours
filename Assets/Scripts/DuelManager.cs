using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DuelManager : MonoBehaviour
{
    public List<PionSelector> duelPions;
    private int lastEventIndex = -1;

    [Header("Consequences Settings")]

    public string[] consequences =
    {
    };

    private int GetRandomConsequenceIndex()
    {
        int randomIndex = Random.Range(0, consequences.Length);

        while (randomIndex == lastEventIndex)
        {
            randomIndex = Random.Range(0, consequences.Length);
        }

        lastEventIndex = randomIndex;
        return randomIndex;
    }

    public void OnConfirmDuel()
    {
        Debug.Log("1. Le bouton Start Duel a bien été cliqué !");

        List<string> selectedForDuel = new List<string>();

        foreach (PionSelector p in duelPions)
        {
            if (p.isSelected) selectedForDuel.Add(p.pionName);
        }

        Debug.Log("2. Nombre de pions sélectionnés : " + selectedForDuel.Count);

        if (selectedForDuel.Count != 2) return;

        int playersInGameCount = PlayerPrefs.GetInt("PlayerCount", 0);

        Debug.Log("3. Nombre de joueurs enregistrés au début de la partie : " + playersInGameCount);


        List<string> validPlayers = new List<string>();

        for (int i = 0; i < playersInGameCount; i++)
        {
            validPlayers.Add(PlayerPrefs.GetString("Player_" + i));
        }

        foreach (string name in selectedForDuel)
        {
            bool playerFound = false;

            foreach (string validName in validPlayers)
            {
                if (name.ToLower().Trim() == validName.ToLower().Trim())
                {
                    playerFound = true;
                    break;
                }
            }

            if (!playerFound)
            {
                Debug.Log("STOP : " + name + " n'est pas dans la partie ! (Vérifie l'orthographe)");
                return;
            }
        }

        string winner = selectedForDuel[Random.Range(0, 2)];

        PlayerPrefs.SetString("DuelWinner", winner);
        PlayerPrefs.SetInt("FromDuel", 1);
        PlayerPrefs.SetInt("ConsequenceIndex", GetRandomConsequenceIndex());

        SceneManager.LoadScene("GameScene");
    }
}