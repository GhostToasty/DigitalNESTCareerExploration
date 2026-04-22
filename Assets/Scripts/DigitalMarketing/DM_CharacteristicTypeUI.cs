using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DM_CharacteristicTypeUI : MonoBehaviour
{
    // [SerializeField] DM_CharacteristicSO characteristicSO;
    [SerializeField] DM_CharacteristicListSO characteristicListSO;
    
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
    [SerializeField] private Button confirmCharButton;
    [SerializeField] private Button confirmQuestionButton;
    [SerializeField] private GameObject infoPopUp;
    [SerializeField] private TextMeshProUGUI infoPopUpText;
    [SerializeField] private Canvas popUpCanvas;
    [SerializeField] private Canvas characteristicButtonCanvas;
    [SerializeField] private Canvas characteristicQuestionCanvas;
    [SerializeField] private TextMeshProUGUI question1ButtonText;
    [SerializeField] private TextMeshProUGUI question2ButtonText;
    [SerializeField] private TextMeshProUGUI question3ButtonText;
    

    private State state;
    public DM_CharacteristicSO chosenCharacteristicSO;



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

        confirmCharButton.onClick.AddListener(() => { state = State.Default; ConfirmCharButtonChoice();});
        confirmQuestionButton.onClick.AddListener(() => { state = State.Default; ConfirmQuestionButtonChoice();});

        state = State.Default;

        HideCharacteristicCanvas();
        HideInfoPopUp();
        HideCharacteristicQuestionCanvas();
        HideCharacteristicButtonCanvas();
    }


    public void OnSelectedContent()
    {
        ShowCharacteristicCanvas();
        ShowCharacteristicButtonCanvas();
    }


    private void Update()
    {
        if (state != State.Default)
            switch (state)
            {
                case State.HighQuality:
                    GetMatchingCharacteristicSO("HighQuality");
                    break;
                case State.BudgetFriendly:
                    GetMatchingCharacteristicSO("BudgetFriendly");
                    break;
                case State.PackageDesign:
                    GetMatchingCharacteristicSO("PackageDesign");
                    break;
                case State.EthicallyMade:
                    GetMatchingCharacteristicSO("EthicallyMade");
                    break;
                case State.Convenient:
                    GetMatchingCharacteristicSO("Convenient");
                    break;
                case State.EcoFriendly:
                    GetMatchingCharacteristicSO("EcoFriendly");
                    break;
                case State.UniqueDesign:
                    GetMatchingCharacteristicSO("UniqueDesign");
                    break;
                case State.Familiarity:
                    GetMatchingCharacteristicSO("Familiarity");
                    break;
            }
    }


    private void ConfirmCharButtonChoice()
    {
        HideCharacteristicButtonCanvas();
        HideInfoPopUp();

        ShowCharacteristicQuestionCanvas();
        GetButtonQuestionCharacteristicSO(chosenCharacteristicSO);
    }


    private void ConfirmQuestionButtonChoice()
    {
        HideCharacteristicCanvas();

        state = State.Default;
        Debug.Log(chosenCharacteristicSO);

        OnDetermineWinState?.Invoke();
    }


    private void GetMatchingCharacteristicSO(string selectedCharacteristic)
    {
        for (int i = 0; i < characteristicListSO.characteristicSOList.Count; i++)
        {
            if (characteristicListSO.characteristicSOList[i].characteristicName == selectedCharacteristic)
            {
                chosenCharacteristicSO = characteristicListSO.characteristicSOList[i];
                infoPopUpText.text = characteristicListSO.characteristicSOList[i].characteristicDescription;
            }
        }
    }


    private void GetButtonQuestionCharacteristicSO(DM_CharacteristicSO chosenCharacteristicSO)
    {
        question1ButtonText.text = chosenCharacteristicSO.characteristicQuestion1;
        question2ButtonText.text = chosenCharacteristicSO.characteristicQuestion2;
        question3ButtonText.text = chosenCharacteristicSO.characteristicQuestion3;
    }



    //showing and hiding different canvases 
    
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


    private void ShowCharacteristicButtonCanvas()
    {
        characteristicButtonCanvas.gameObject.SetActive(true);
    }

    private void HideCharacteristicButtonCanvas()
    {
        characteristicButtonCanvas.gameObject.SetActive(false);
    }


    private void ShowCharacteristicQuestionCanvas()
    {
        characteristicQuestionCanvas.gameObject.SetActive(true);
    }

    private void HideCharacteristicQuestionCanvas()
    {
        characteristicQuestionCanvas.gameObject.SetActive(false);
    }

}
