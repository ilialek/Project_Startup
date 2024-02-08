using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currency : MonoBehaviour
{
    public GameObject firstOfferSelected;
    public GameObject secondOfferSelected;
    public GameObject thirdOfferSelected;
    public GameObject fourthOfferSelected;

    public void SelectFirstOffer()
    {
        firstOfferSelected.SetActive(true);
        secondOfferSelected.SetActive(false);
        thirdOfferSelected.SetActive(false);
        fourthOfferSelected.SetActive(false);
    }

    public void SelectSecondOffer() 
    {
        firstOfferSelected.SetActive(false);
        secondOfferSelected.SetActive(true);
        thirdOfferSelected.SetActive(false);
        fourthOfferSelected.SetActive(false);
    }

    public void SelectThirdOffer() 
    {
        firstOfferSelected.SetActive(false);
        secondOfferSelected.SetActive(false);
        thirdOfferSelected.SetActive(true);
        fourthOfferSelected.SetActive(false);
    }

    public void SelectFourthOffer()
    {
        firstOfferSelected.SetActive(false);
        secondOfferSelected.SetActive(false);
        thirdOfferSelected.SetActive(false);
        fourthOfferSelected.SetActive(true);
    }
}

