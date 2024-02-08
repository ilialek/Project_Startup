using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModes : MonoBehaviour
{
    public UnityEngine.UI.Image solo;
    public UnityEngine.UI.Image duo;
    public UnityEngine.UI.Image trio;
    public UnityEngine.UI.Image squad;

    public GameObject soloBorder;
    public GameObject duoBorder;
    public GameObject trioBorder;
    public GameObject squadBorder;

    public Sprite selected;
    public Sprite normal;
    public Sprite hover;

    public void SelectSolo()
    {
        solo.sprite = selected;
        duo.sprite = normal;
        trio.sprite = normal;
        squad.sprite = normal;
        
        soloBorder.SetActive(true);
        duoBorder.SetActive(false);
        trioBorder.SetActive(false);
        squadBorder.SetActive(false);
    }

    public void SelectDuo()
    {
        solo.sprite = normal;
        duo.sprite = selected;
        trio.sprite = normal;
        squad.sprite = normal;

        soloBorder.SetActive(false);
        duoBorder.SetActive(true);
        trioBorder.SetActive(false);
        squadBorder.SetActive(false);
    }

    public void SelectTrio()
    {
        solo.sprite = normal;
        duo.sprite = normal;
        trio.sprite = selected;
        squad.sprite = normal;

        soloBorder.SetActive(false);
        duoBorder.SetActive(false);
        trioBorder.SetActive(true);
        squadBorder.SetActive(false);
    }

    public void SelectSquad()
    {
        solo.sprite = normal;
        duo.sprite = normal;
        trio.sprite = normal;
        squad.sprite = selected;

        soloBorder.SetActive(false);
        duoBorder.SetActive(false);
        trioBorder.SetActive(false);
        squadBorder.SetActive(true);
    }
}
