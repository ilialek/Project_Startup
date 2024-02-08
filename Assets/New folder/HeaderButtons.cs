using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HeaderButtons : MonoBehaviour
{
    //public Button myButton;
    //public Color selectedTextColor = Color.black;

    public TMP_Text buttonText;

    public GameObject playPanel;
    public GameObject storePanel;
    public GameObject battlePassPanel;
    public GameObject currencyPanel;

    void Start()
    {
        //buttonText = myButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowPlayPanel()
    {
        playPanel.SetActive(true);
        storePanel.SetActive(false);
        battlePassPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ShowStorePanel() 
    {
        playPanel.SetActive(false);
        storePanel.SetActive(true);
        battlePassPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ShowBattlePassPanel()
    {
        playPanel.SetActive(false);
        storePanel.SetActive(false);
        battlePassPanel.SetActive(true);
        currencyPanel.SetActive(false);
    }

    public void ShowCurrencyPanel()
    {
        playPanel.SetActive(false);
        storePanel.SetActive(false);
        battlePassPanel.SetActive(false);
        currencyPanel.SetActive(true);
    }
}
