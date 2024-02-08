using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class HeaderButtons : MonoBehaviour
{
    public TMP_Text buttonText;
    public TMP_Text characterName;
    public TMP_Text characterDescription;
    public TMP_Text characterOrEmote;

    public UnityEngine.UI.Image selectorCharacterButton;
    public UnityEngine.UI.Image selectorEmoteButton;
    public UnityEngine.UI.Image guyIcon;
    public UnityEngine.UI.Image ladyIcon;

    public GameObject playPanel;
    public GameObject changeRoomPanel;
    public GameObject gameModesPanel;
    public GameObject battlePassPanel;
    public GameObject shopPanel;
    public GameObject currencyPanel;

    public GameObject guyIconBorder;
    public GameObject guyCharacer;

    public GameObject ladyIconBorder;
    public GameObject ladyCharacer;

    public Sprite yellowButton;
    public Sprite grayButton;

    public Sprite guyEmoteIcon;
    public Sprite guyEmoteIcon2;
    public Sprite ladyEmoteIcon;

    public Sprite guyNormalIcon;
    public Sprite ladyNormalIcon;



    void Start()
    {
        guyIconBorder.SetActive(true);
        guyCharacer.SetActive(true);

        ladyIconBorder.SetActive(false);
        ladyCharacer.SetActive(false);
    }

    public void ShowPlayPanel()
    {
        playPanel.SetActive(true);
        changeRoomPanel.SetActive(false);
        gameModesPanel.SetActive(false);
        battlePassPanel.SetActive(false);
        shopPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ShowStorePanel() 
    {
        playPanel.SetActive(false);
        changeRoomPanel.SetActive(false);
        gameModesPanel.SetActive(false);
        battlePassPanel.SetActive(false);
        shopPanel.SetActive(true);
        currencyPanel.SetActive(false);
    }

    public void ShowBattlePassPanel()
    {
        playPanel.SetActive(false);
        changeRoomPanel.SetActive(false);
        gameModesPanel.SetActive(false);
        battlePassPanel.SetActive(true);
        shopPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ShowCurrencyPanel()
    {
        playPanel.SetActive(false);
        changeRoomPanel.SetActive(false);
        gameModesPanel.SetActive(false);
        battlePassPanel.SetActive(false);
        shopPanel.SetActive(false);
        currencyPanel.SetActive(true);
    }

    public void ShowGameModesPanel()
    {
        playPanel.SetActive(false);
        changeRoomPanel.SetActive(false);
        gameModesPanel.SetActive(true);
        battlePassPanel.SetActive(false);
        shopPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ShowChangeRoomPanel()
    {
        playPanel.SetActive(false);
        changeRoomPanel.SetActive(true);
        gameModesPanel.SetActive(false);
        battlePassPanel.SetActive(false);
        shopPanel.SetActive(false);
        currencyPanel.SetActive(false);
    }

    public void ClickOnGuyIcon()
    {
        guyIconBorder.SetActive(true);
        guyCharacer.SetActive(true) ;

        ladyIconBorder.SetActive(false);
        ladyCharacer.SetActive(false) ;

        characterName.text = "THE ANALYST";
        characterDescription.text = "Sharp, avvt and ready to dissect the competition, the Analyst skin brings a data-driven edgeto the office battlefield.";
    }

    public void ClickOnLadyIcon()
    {
        ladyIconBorder.SetActive(true);
        ladyCharacer.SetActive(true) ;

        guyIconBorder.SetActive(false) ;
        guyCharacer.SetActive(false);

        characterName.text = "THE MANAGER";
        characterDescription.text = "Command the office battlefield in style with this sleek and authoritive skin, exuding confidence and leadership.";
    }

    public void ClickOnCharacters()
    {
        selectorCharacterButton.sprite = yellowButton;
        selectorEmoteButton.sprite = grayButton;

        guyIcon.sprite = guyNormalIcon;
        ladyIcon.sprite = ladyNormalIcon;

        characterName.text = "THE ANALYST";
        characterDescription.text = "Sharp, avvt and ready to dissect the competition, the Analyst skin brings a data-driven edgeto the office battlefield.";
        characterOrEmote.text = "CHARACTER";
    }   
    
    public void ClickOnEmotes()
    {
        selectorCharacterButton.sprite = grayButton;
        selectorEmoteButton.sprite = yellowButton;

        guyIcon.sprite = guyEmoteIcon;
        ladyIcon.sprite = guyEmoteIcon2;

        characterName.text = "ALPHA WAVE";
        characterDescription.text = "Send a friendly greeting with the Alpha Wave emote, making waves and spreading positivity across the office arena.";
        characterOrEmote.text = "EMOTE";
    }

    public void ClickOnEmote2()
    {
        characterName.text = "THOUGHT TWIST";
        characterDescription.text = "Get those gears turning with the Thought Twist emote, a quick gesture to show you're bewing up some briliant ideas.";
    }
}


