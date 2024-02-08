using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class EditPlayerReadiness : MonoBehaviour
{
    public static EditPlayerReadiness Instance { get; private set; }

    public event EventHandler OnNameChanged;

    [SerializeField] private TextMeshProUGUI playerReadinessText;

    [SerializeField] private Color normalGreenColor;
    [SerializeField] private Color highlightedGreenColor;
    [SerializeField] private Color normalRedColor;
    [SerializeField] private Color highlightedRedColor;

    private int playerReadiness = 0;

    private void Awake()
    {
        Instance = this;

        //GetComponent<Button>().onClick.AddListener(() => {
        //    UI_InputWindow.Show_Static("Player Name", playerName, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ .,-", 15,
        //    () => {
        //        // Cancel
        //    },
        //    (string newName) => {
        //        playerName = newName;

        //        playerNameText.text = playerName;

        //        OnNameChanged?.Invoke(this, EventArgs.Empty);
        //    });
        //});

        //GetComponent<Button>().onClick.AddListener(() => {

        //    LobbyManager.Instance.UpdatePlayerReadiness();
            
        //});

        //playerNameText.text = playerName;
    }

    private void ChangeReadiness() {

    }
}
