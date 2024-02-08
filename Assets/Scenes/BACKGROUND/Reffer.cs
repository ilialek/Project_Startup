using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reffer : MonoBehaviour
{
    public GameObject refferScreen;

    public GameObject claimScreen;

    public GameObject playScreen;

    public void OpenRefferScreen()
    {
        refferScreen.SetActive(true);
        claimScreen.SetActive(false);
        playScreen.SetActive(false);
    }

    public void OpenClaimScreen()
    {
        claimScreen.SetActive(true);
    }

    public void CloseReffer()
    {
        refferScreen.SetActive(false);
        claimScreen.SetActive(false);
        playScreen.SetActive(true);
    }
}
