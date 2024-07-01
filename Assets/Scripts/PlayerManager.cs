using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> players;
    private int currentPlayerIndex = 0;

    void Start()
    {
        SetActivePlayer(currentPlayerIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tuþuna basýnca
        {
            SwitchPlayer();
        }
    }

    void SetActivePlayer(int index)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(i == index);
        }
    }

    void SwitchPlayer()
    {
        players[currentPlayerIndex].GetComponent<CharMovement>().enabled = false; // Mevcut oyuncunun hareketini durdur
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; // Sýradaki oyuncuya geç
        SetActivePlayer(currentPlayerIndex);
        players[currentPlayerIndex].GetComponent<CharMovement>().enabled = true; // Yeni oyuncunun hareketini etkinleþtir
    }
}
