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
    
    private enum State
    {
        Default,
        Email,
        Graphic,
        Video,
        Text,
        ClickableAd,
        InfluencerOutreach,
        SEO,
        VideoAd
    }

    
    [SerializeField] private Button organicContentButton;
    [SerializeField] private Button paidContentButton;
    [SerializeField] private Button emailButton;
    [SerializeField] private Button graphicButton;
    [SerializeField] private Button videoButton;
    [SerializeField] private Button textButton;
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
    [SerializeField] private Canvas popUpCanvas;
    [SerializeField] private Canvas contentQuestionCanvas;
    [SerializeField] private TextMeshProUGUI question1ButtonText;
    [SerializeField] private TextMeshProUGUI question2ButtonText;
    [SerializeField] private TextMeshProUGUI question3ButtonText;
      

    private State state;
    public DM_ContentSO chosenContentSO;


    private void Awake()
    {
        organicContentButton.onClick.AddListener(() =>
        {
            HideOrganicPaidButtons();
            ShowOrganicOptions();
        });

        paidContentButton.onClick.AddListener(() =>
        {
            HideOrganicPaidButtons();
            ShowPaidOptions();
        });

        emailButton.onClick.AddListener(() => { state = State.Email; ShowInfoPopUp();});
        graphicButton.onClick.AddListener(() => { state = State.Graphic; ShowInfoPopUp();});
        videoButton.onClick.AddListener(() => { state = State.Video; ShowInfoPopUp();});
        textButton.onClick.AddListener(() => { state = State.Text; ShowInfoPopUp();});
        clickableAdButton.onClick.AddListener(() => { state = State.ClickableAd; ShowInfoPopUp();});
        influencerOutreachButton.onClick.AddListener(() => { state = State.InfluencerOutreach; ShowInfoPopUp();});
        seoButton.onClick.AddListener(() => { state = State.SEO; ShowInfoPopUp();});
        videoAdButton.onClick.AddListener(() => { state = State.VideoAd; ShowInfoPopUp();});

        confirmContentButton.onClick.AddListener(() => { state = State.Default; ConfirmContentButtonChoice();});
        confirmQuestionButton.onClick.AddListener(() => { state = State.Default; ConfirmQuestionButtonChoice();});

        state = State.Default;

        ShowContentCanvas();
        HideOrganicOptions();
        HidePaidOptions();
        HideInfoPopUp();
        HideContentQuestionCanvas();
    }

    private void Start()
    {
        ShowOrganicPaidButtons();
    }


    private void Update()
    {
        if (state != State.Default)
            switch (state)
            {
                case State.Email:
                    infoPopUpText.text = "This is an email";
                    GetMatchingContentSO("Email");
                    break;
                case State.Graphic:
                    infoPopUpText.text = "This is an graphic";
                    GetMatchingContentSO("Graphic");
                    break;
                case State.Video:
                    infoPopUpText.text = "This is a video";
                    GetMatchingContentSO("Video");
                    break;
                case State.Text:
                    infoPopUpText.text = "This is a text";
                    GetMatchingContentSO("Text");
                    break;
                case State.ClickableAd:
                    infoPopUpText.text = "This is a clickable ad";
                    GetMatchingContentSO("ClickableAd");
                    break;
                case State.InfluencerOutreach:
                    infoPopUpText.text = "This is influencer outreach";
                    GetMatchingContentSO("InfluencerOutreach");
                    break;
                case State.SEO:
                    infoPopUpText.text = "This is SEO";
                    GetMatchingContentSO("SEO");
                    break;
                case State.VideoAd:
                    infoPopUpText.text = "This is a video ad";
                    GetMatchingContentSO("VideoAd");
                    break;
            }
    }


     private void ConfirmContentButtonChoice()
    {
        HidePaidOptions();
        HideOrganicOptions();
        HideInfoPopUp();

        ShowContentQuestionCanvas();
        GetButtonQuestionContentSO(chosenContentSO);
    }


    private void ConfirmQuestionButtonChoice()
    {
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


    private void GetButtonQuestionContentSO(DM_ContentSO chosenCotentSO)
    {
        question1ButtonText.text = chosenCotentSO.contentQuestion1;
        question2ButtonText.text = chosenCotentSO.contentQuestion2;
        question3ButtonText.text = chosenCotentSO.contentQuestion3;
    }
    
    
    
    private void ShowOrganicPaidButtons()
    {
        organicContentButton.gameObject.SetActive(true);
        paidContentButton.gameObject.SetActive(true);
    }

    private void HideOrganicPaidButtons()
    {
        organicContentButton.gameObject.SetActive(false);
        paidContentButton.gameObject.SetActive(false);
    }
    
    
    private void ShowOrganicOptions()
    {
        organicButtonsCanvas.gameObject.SetActive(true);
    }

    private void HideOrganicOptions()
    {
        organicButtonsCanvas.gameObject.SetActive(false);
    }


    private void ShowPaidOptions()
    {
        paidButtonsCanvas.gameObject.SetActive(true);
    }

    private void HidePaidOptions()
    {
        paidButtonsCanvas.gameObject.SetActive(false);
    }


    private void ShowInfoPopUp()
    {
        popUpCanvas.gameObject.SetActive(true);
    }

    private void HideInfoPopUp()
    {
        popUpCanvas.gameObject.SetActive(false);
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

}
