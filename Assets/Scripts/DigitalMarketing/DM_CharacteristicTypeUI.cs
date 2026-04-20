using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DM_CharacteristicTypeUI : MonoBehaviour
{
    public delegate void DetermineWinStateFunc();
    public static event DetermineWinStateFunc OnDetermineWinState;
    
    private enum State
    {
        Default,
        HighQuality,
        BudgetFriendly,
        PackageDesign,
        EthicallyMade,
        Convenient,
        EcoFriendly,
        UniqueDesign,
        Familiarity
    }


    [SerializeField] private Button highQualityButton;
    [SerializeField] private Button budgetFriendlyButton;
    [SerializeField] private Button packageDesignButton;
    [SerializeField] private Button ethicallyMadeButton;
    [SerializeField] private Button convenientButton;
    [SerializeField] private Button ecoFriendlyButton;
    [SerializeField] private Button uniqueDesignButton;
    [SerializeField] private Button familiarityButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject infoPopUp;
    [SerializeField] private TextMeshProUGUI infoPopUpText;
    [SerializeField] private Canvas popUpCanvas;
      


    private State state;
    public string chosenCharacteristic = null;


    private void Awake()
    {
        DM_ContentTypeUI.OnSelectedContent += OnSelectedContent;

        highQualityButton.onClick.AddListener(() => { state = State.HighQuality; ShowInfoPopUp();});
        budgetFriendlyButton.onClick.AddListener(() => { state = State.BudgetFriendly; ShowInfoPopUp();});
        packageDesignButton.onClick.AddListener(() => { state = State.PackageDesign; ShowInfoPopUp();});
        ethicallyMadeButton.onClick.AddListener(() => { state = State.EthicallyMade; ShowInfoPopUp();});
        convenientButton.onClick.AddListener(() => { state = State.Convenient; ShowInfoPopUp();});
        ecoFriendlyButton.onClick.AddListener(() => { state = State.EcoFriendly; ShowInfoPopUp();});
        uniqueDesignButton.onClick.AddListener(() => { state = State.UniqueDesign; ShowInfoPopUp();});
        familiarityButton.onClick.AddListener(() => { state = State.Familiarity; ShowInfoPopUp();});

        confirmButton.onClick.AddListener(() => { state = State.Default; ConfirmButtonChoice();});

        state = State.Default;

        HideCharacteristicCanvas();
        HideInfoPopUp();
    }


    public void OnSelectedContent()
    {
        ShowCharacteristicCanvas();
    }


    private void Update()
    {
        if (state != State.Default)
            switch (state)
            {
                case State.HighQuality:
                    infoPopUpText.text = "This characteristic targets those who value high quality.";
                    chosenCharacteristic = "HighQuality";
                    break;
                case State.BudgetFriendly:
                    infoPopUpText.text = "This characteristic targets those who value budget friendly products.";
                    chosenCharacteristic = "BudgetFriendly";
                    break;
                case State.PackageDesign:
                    infoPopUpText.text = "This characteristic targets those who value package design.";
                    chosenCharacteristic = "PackageDesign";
                    break;
                case State.EthicallyMade:
                    infoPopUpText.text = "This characteristic targets those who value ethically made products.";
                    chosenCharacteristic = "EthicallyMade";
                    break;
                case State.Convenient:
                    infoPopUpText.text = "This characteristic targets those who value convenience.";
                    chosenCharacteristic = "Convenient";
                    break;
                case State.EcoFriendly:
                    infoPopUpText.text = "This characteristic targets those who value eco-friendly products.";
                    chosenCharacteristic = "EcoFriendly";
                    break;
                case State.UniqueDesign:
                    infoPopUpText.text = "This characteristic targets those who value unique design.";
                    chosenCharacteristic = "UniqueDesign";
                    break;
                case State.Familiarity:
                    infoPopUpText.text = "This characteristic targets those who value familiarity.";
                    chosenCharacteristic = "Familiarity";
                    break;
            }
    }


    private void ConfirmButtonChoice()
    {
        HideCharacteristicCanvas();

        state = State.Default;
        Debug.Log(chosenCharacteristic);

        OnDetermineWinState?.Invoke();
    }

    
    private void ShowInfoPopUp()
    {
        popUpCanvas.gameObject.SetActive(true);
    }

    private void HideInfoPopUp()
    {
        popUpCanvas.gameObject.SetActive(false);
    }


    private void ShowCharacteristicCanvas()
    {
        gameObject.SetActive(true);
        Debug.Log("showCharacter");
    }
    
    private void HideCharacteristicCanvas()
    {
        gameObject.SetActive(false);
    }

}
