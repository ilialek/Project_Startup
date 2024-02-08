using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    public GameObject packSelected;
    public GameObject skinSelected;
    public GameObject keyboardSelected;
    public GameObject emote1Selected;
    public GameObject emote2Selected;

    public UnityEngine.UI.Image rulleteSelector;
    public UnityEngine.UI.Image shopSelector;

    public Sprite normal;
    public Sprite selected;

    public GameObject itemsShop;
    public GameObject rulette;

    private void Start()
    {
        shopSelector.sprite = selected;
    }

    public void OpenRulette()
    {
        itemsShop.SetActive(false);
        rulette.SetActive(true);
        rulleteSelector.sprite = selected;
        shopSelector.sprite = normal;
    }

    public void OpenItemShop()
    {
        itemsShop.SetActive(true);
        rulette.SetActive(false);
        rulleteSelector.sprite = normal;
        shopSelector.sprite = selected;
    }

    public void SelectPack()
    {
        packSelected.SetActive(true);
        skinSelected.SetActive(false);
        keyboardSelected.SetActive(false);
        emote1Selected.SetActive(false);
        emote2Selected.SetActive(false);
    }

    public void SelectSkin() 
    {
        packSelected.SetActive(false);
        skinSelected.SetActive(true);
        keyboardSelected.SetActive(false);
        emote1Selected.SetActive(false);
        emote2Selected.SetActive(false);
    }

    public void SelectKeyboard()
    {
        packSelected.SetActive(false);
        skinSelected.SetActive(false);
        keyboardSelected.SetActive(true);
        emote1Selected.SetActive(false);
        emote2Selected.SetActive(false);
    }

    public void SelectEmote1()
    {
        packSelected.SetActive(false);
        skinSelected.SetActive(false);
        keyboardSelected.SetActive(false);
        emote1Selected.SetActive(true);
        emote2Selected.SetActive(false);
    }

    public void SelectEmote2() 
    {
        packSelected.SetActive(false);
        skinSelected.SetActive(false);
        keyboardSelected.SetActive(false);
        emote1Selected.SetActive(false);
        emote2Selected.SetActive(true);
    }
}
