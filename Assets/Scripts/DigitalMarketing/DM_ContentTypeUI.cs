using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


public class DM_ContentTypeUI : MonoBehaviour
{
    // [SerializeField] DM_ContentSO contentSO;
    [SerializeField] DM_ContentListSO contentListSO;
    public delegate void SelectedContentFunc();
    public static event SelectedContentFunc OnSelectedContent;
    public delegate void ScoreBarUpdateFunc(string checkItem);
    public static event ScoreBarUpdateFunc OnScoreBarUpdate;

    public DM_ContentSO chosenContentSO;
    public Button correctContentAnswer;
    public Button chosenContentAnswer;
    
    private enum State
    {
        Default,
        Email,
        Graphic,
        Video,
        Blog,
        // ClickableAd,
        InfluencerOutreach,
        SEO,
        // VideoAd
    }
    private State state;

    
    [SerializeField] private Button organicContentButton;
    [SerializeField] private Button paidContentButton;
    [SerializeField] private Button emailButton;
    [SerializeField] private Button graphicButton;
    [SerializeField] private Button videoButton;
    [SerializeField] private Button blogButton;
    [SerializeField] private Button clickableAdButton;
    [SerializeField] private Button influencerOutreachButton;
    [SerializeField] private Button seoButton;
    [SerializeField] private Button videoAdButton;
    [SerializeField] private Button confirmContentButton;
    [SerializeField] private Button confirmQuestionButton;
    [SerializeField] private GameObject infoPopUp;
    [SerializeField] private TextMeshProUGUI infoPopUpText;
    [SerializeField] private Canvas organicButtonsCanvas;
    [SerializeField] private Canvas paidButtonsCanvas;
    [SerializeField] private Canvas contentButtonsCanvas;
    [SerializeField] private Canvas contentPopUpCanvas;
    [SerializeField] private Canvas contentQuestionCanvas;
    [SerializeField] private Button question1Button;
    [SerializeField] private Button question2Button;
    [SerializeField] private Button question3Button;
    [SerializeField] private TextMeshProUGUI question1ButtonText;
    [SerializeField] private TextMeshProUGUI question2ButtonText;
    [SerializeField] private TextMeshProUGUI question3ButtonText;
    


    private void Awake()
    {
        // organicContentButton.onClick.AddListener(() =>
        // {
        //     HideOrganicPaidButtons();
        //     ShowOrganicOptions();
        // });

        // paidContentButton.onClick.AddListener(() =>
        // {
        //     HideOrganicPaidButtons();
        //     ShowPaidOptions();
        // });

        emailButton.onClick.AddListener(() => { state = State.Email; ShowInfoPopUp();});
        graphicButton.onClick.AddListener(() => { state = State.Graphic; ShowInfoPopUp();});
        videoButton.onClick.AddListener(() => { state = State.Video; ShowInfoPopUp();});
        blogButton.onClick.AddListener(() => { state = State.Blog; ShowInfoPopUp();});
        // clickableAdButton.onClick.AddListener(() => { state = State.ClickableAd; ShowInfoPopUp();});
        influencerOutreachButton.onClick.AddListener(() => { state = State.InfluencerOutreach; ShowInfoPopUp();});
        seoButton.onClick.AddListener(() => { state = State.SEO; ShowInfoPopUp();});
        // videoAdButton.onClick.AddListener(() => { state = State.VideoAd; ShowInfoPopUp();});

        question1Button.onClick.AddListener(() => { GetButtonChoosen(question1Button); ShowConfirmQuestionButton();});
        question2Button.onClick.AddListener(() => { GetButtonChoosen(question2Button); ShowConfirmQuestionButton();});
        question3Button.onClick.AddListener(() => { GetButtonChoosen(question3Button); ShowConfirmQuestionButton();});

        confirmContentButton.onClick.AddListener(() => { state = State.Default; ConfirmContentButtonChoice();});
        confirmQuestionButton.onClick.AddListener(() => { state = State.Default; ConfirmQuestionButtonChoice();});

        state = State.Default;

        ShowContentCanvas();
        // HideOrganicOptions();
        // HidePaidOptions();
        HideInfoPopUp();
        HideContentQuestionCanvas();
        HideContentButtonsCanvas();
        HideConfirmQuestionButton();
    }

    private void Start()
    {
        // ShowOrganicPaidButtons();
        ShowContentButtonsCanvas();
    }


    private void Update()
    {
        if (state != State.Default)
            switch (state)
            {
                case State.Blog:
                    infoPopUpText.text = "This is a blog";
                    GetMatchingContentSO("Blog");
                    correctContentAnswer = question2Button;
                    break;
                case State.Email:
                    infoPopUpText.text = "This is an email";
                    GetMatchingContentSO("Email");
                    correctContentAnswer = question3Button;
                    break;
                case State.Graphic:
                    infoPopUpText.text = "This is an graphic";
                    GetMatchingContentSO("Graphic");
                    correctContentAnswer = question1Button;
                    break;
                case State.InfluencerOutreach:
                    infoPopUpText.text = "This is influencer outreach";
                    GetMatchingContentSO("InfluencerOutreach");
                    correctContentAnswer = question2Button;
                    break;
                case State.SEO:
                    infoPopUpText.text = "This is SEO";
                    GetMatchingContentSO("SEO");
                    correctContentAnswer = question3Button;
                    break;
                case State.Video:
                    infoPopUpText.text = "This is a video";
                    GetMatchingContentSO("Video");
                    correctContentAnswer = question1Button;
                    break;
                // case State.ClickableAd:
                //     infoPopUpText.text = "This is a clickable ad";
                //     GetMatchingContentSO("ClickableAd");
                //     break;
                // case State.VideoAd:
                //     infoPopUpText.text = "This is a video ad";
                //     GetMatchingContentSO("VideoAd");
                //     break;
            }
    }


     private void ConfirmContentButtonChoice()
    {
        OnScoreBarUpdate?.Invoke("ContentChoice");
        
        // HidePaidOptions();
        // HideOrganicOptions();
        HideInfoPopUp();
        HideContentButtonsCanvas();

        ShowContentQuestionCanvas();
        GetButtonQuestionContentSO(chosenContentSO);
    }


    private void ConfirmQuestionButtonChoice()
    {
        OnScoreBarUpdate?.Invoke("ContentQAnswer");
        HideContentCanvas();

        state = State.Default;
        Debug.Log(chosenContentSO);

        OnSelectedContent?.Invoke();
    }


    private void GetMatchingContentSO(string selectedContent)
    {
        for (int i = 0; i < contentListSO.contentSOList.Count; i++)
        {
            if (contentListSO.contentSOList[i].contentName == selectedContent)
            {
                chosenContentSO = contentListSO.contentSOList[i];
                infoPopUpText.text = contentListSO.contentSOList[i].contentDescription;
            }
        }
    }


    private void GetButtonQuestionContentSO(DM_ContentSO chosenContentSO)
    {
        question1ButtonText.text = chosenContentSO.contentQuestion1;
        question2ButtonText.text = chosenContentSO.contentQuestion2;
        question3ButtonText.text = chosenContentSO.contentQuestion3;
    }


    private void GetButtonChoosen(Button buttonAnswer)
    {
        chosenContentAnswer = buttonAnswer;
    }
    
    
    
    // private void ShowOrganicPaidButtons()
    // {
    //     organicContentButton.gameObject.SetActive(true);
    //     paidContentButton.gameObject.SetActive(true);
    // }

    // private void HideOrganicPaidButtons()
    // {
    //     organicContentButton.gameObject.SetActive(false);
    //     paidContentButton.gameObject.SetActive(false);
    // }
    
    
    // private void ShowOrganicOptions()
    // {
    //     organicButtonsCanvas.gameObject.SetActive(true);
    // }

    // private void HideOrganicOptions()
    // {
    //     organicButtonsCanvas.gameObject.SetActive(false);
    // }


    // private void ShowPaidOptions()
    // {
    //     paidButtonsCanvas.gameObject.SetActive(true);
    // }

    // private void HidePaidOptions()
    // {
    //     paidButtonsCanvas.gameObject.SetActive(false);
    // }


    private void ShowInfoPopUp()
    {
        contentPopUpCanvas.gameObject.SetActive(true);
    }

    private void HideInfoPopUp()
    {
        contentPopUpCanvas.gameObject.SetActive(false);
    }


    private void ShowContentCanvas()
    {
        gameObject.SetActive(true);
    }
    
    private void HideContentCanvas()
    {
        gameObject.SetActive(false);
    }


    private void ShowContentQuestionCanvas()
    {
        contentQuestionCanvas.gameObject.SetActive(true);
    }

    private void HideContentQuestionCanvas()
    {
        contentQuestionCanvas.gameObject.SetActive(false);
    }


    private void ShowContentButtonsCanvas()
    {
        contentButtonsCanvas.gameObject.SetActive(true);
    }

    private void HideContentButtonsCanvas()
    {
        contentButtonsCanvas.gameObject.SetActive(false);
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
