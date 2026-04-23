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
    public delegate void ScoreBarUpdateFunc(string checkItem);
    public static event ScoreBarUpdateFunc OnScoreBarUpdate;

    public DM_CharacteristicSO chosenCharacteristicSO;
    public Button correctCharacteristicAnswer;
    public Button chosenCharacteristicAnswer;
    
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
    private State state;


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
    [SerializeField] private Button question1Button;
    [SerializeField] private Button question2Button;
    [SerializeField] private Button question3Button;
    [SerializeField] private TextMeshProUGUI question1ButtonText;
    [SerializeField] private TextMeshProUGUI question2ButtonText;
    [SerializeField] private TextMeshProUGUI question3ButtonText;



    private void Awake()
    {
        DM_ContentTypeUI.OnSelectedContent += OnSelectedContent;
        // DM_GameLogic.OnScoreBarUpdate += OnScoreBarUpdate;

        highQualityButton.onClick.AddListener(() => { state = State.HighQuality; ShowInfoPopUp();});
        budgetFriendlyButton.onClick.AddListener(() => { state = State.BudgetFriendly; ShowInfoPopUp();});
        packageDesignButton.onClick.AddListener(() => { state = State.PackageDesign; ShowInfoPopUp();});
        ethicallyMadeButton.onClick.AddListener(() => { state = State.EthicallyMade; ShowInfoPopUp();});
        convenientButton.onClick.AddListener(() => { state = State.Convenient; ShowInfoPopUp();});
        ecoFriendlyButton.onClick.AddListener(() => { state = State.EcoFriendly; ShowInfoPopUp();});
        uniqueDesignButton.onClick.AddListener(() => { state = State.UniqueDesign; ShowInfoPopUp();});
        familiarityButton.onClick.AddListener(() => { state = State.Familiarity; ShowInfoPopUp();});

        question1Button.onClick.AddListener(() => { GetButtonChoosen(question1Button); ShowConfirmQuestionButton();});
        question2Button.onClick.AddListener(() => { GetButtonChoosen(question2Button); ShowConfirmQuestionButton();});
        question3Button.onClick.AddListener(() => { GetButtonChoosen(question3Button); ShowConfirmQuestionButton();});

        confirmCharButton.onClick.AddListener(() => { state = State.Default; ConfirmCharButtonChoice();});
        confirmQuestionButton.onClick.AddListener(() => { state = State.Default; ConfirmQuestionButtonChoice();});

        state = State.Default;

        HideCharacteristicCanvas();
        HideInfoPopUp();
        HideCharacteristicQuestionCanvas();
        HideCharacteristicButtonCanvas();
        HideConfirmQuestionButton();
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
                case State.BudgetFriendly:
                    GetMatchingCharacteristicSO("BudgetFriendly");
                    correctCharacteristicAnswer = question1Button;
                    break;
                case State.Convenient:
                    GetMatchingCharacteristicSO("Convenient");
                    correctCharacteristicAnswer = question2Button;
                    break;
                case State.EcoFriendly:
                    GetMatchingCharacteristicSO("EcoFriendly");
                    correctCharacteristicAnswer = question3Button;
                    break;
                case State.EthicallyMade:
                    GetMatchingCharacteristicSO("EthicallyMade");
                    correctCharacteristicAnswer = question1Button;
                    break;
                case State.Familiarity:
                    GetMatchingCharacteristicSO("Familiarity");
                    correctCharacteristicAnswer = question2Button;
                    break;
                case State.HighQuality:
                    GetMatchingCharacteristicSO("HighQuality");
                    correctCharacteristicAnswer = question3Button;
                    break;
                case State.PackageDesign:
                    GetMatchingCharacteristicSO("PackageDesign");
                    correctCharacteristicAnswer = question1Button;
                    break;
                case State.UniqueDesign:
                    GetMatchingCharacteristicSO("UniqueDesign");
                    correctCharacteristicAnswer = question2Button;
                    break;   
            }
    }


    private void ConfirmCharButtonChoice()
    {
        OnScoreBarUpdate?.Invoke("CharChoice");
        
        HideCharacteristicButtonCanvas();
        HideInfoPopUp();

        ShowCharacteristicQuestionCanvas();
        GetButtonQuestionCharacteristicSO(chosenCharacteristicSO);
    }


    private void ConfirmQuestionButtonChoice()
    {
        OnScoreBarUpdate?.Invoke("CharQAnswer");
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

    private void GetButtonChoosen(Button buttonAnswer)
    {
        chosenCharacteristicAnswer = buttonAnswer;
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


     private void ShowConfirmQuestionButton()
    {
        confirmQuestionButton.gameObject.SetActive(true);
    }

    private void HideConfirmQuestionButton()
    {
        confirmQuestionButton.gameObject.SetActive(false);
    }

}
