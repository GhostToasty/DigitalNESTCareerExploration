using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DM_GameLogic : MonoBehaviour
{
    public delegate void SetContentPreferenceFunc(string audienceContentPreference);
    public static event SetContentPreferenceFunc OnSetContentPreference;
    public delegate void SetCharacteristicPreferenceFunc(List<DM_CharacteristicSO> audienceCharacteristicPreferenceList);
    public static event SetCharacteristicPreferenceFunc OnSetCharacteristicPreference;
    // public delegate void ScoreBarUpdateFunc();
    // public static event ScoreBarUpdateFunc OnScoreBarUpdate;

    public enum State
    {
        ContentChoice,
        ContentQuestion,
        CharacteristicChoice,
        CharacteristicQuestion
    }
    private State state;

    
    [SerializeField] private DM_ContentTypeUI contentTypeUI;
    [SerializeField] private DM_CharacteristicTypeUI characteristicTypeUI;
    [SerializeField] private DM_CharacteristicListSO characteristicListSO;
    [SerializeField] private DM_ContentListSO contentListSO;
    [SerializeField] private Image scoreBar;

    public static List<DM_ContentSO> audienceContentList = new List<DM_ContentSO>();
    public static List<DM_CharacteristicSO> audienceCharacteristicList = new List<DM_CharacteristicSO>();

    private DM_ContentSO getChosenContent;
    private DM_CharacteristicSO getChosenCharacteristicSO;
    private DM_ContentSO winningContent;
    private DM_CharacteristicSO winningCharacteristic;


    private void Awake()
    {
        DM_CharacteristicTypeUI.OnDetermineWinState += OnDetermineWinState;
        DM_CharacteristicTypeUI.OnScoreBarUpdate += OnScoreBarUpdate;
        DM_ContentTypeUI.OnScoreBarUpdate += OnScoreBarUpdate;

        scoreBar.fillAmount = 0;
    }


    private void Start()
    {
        RandomAudienceTraits();
        Debug.Log("start logic");
    }
    

    private void RandomAudienceTraits()
    {
        //randomizes which content the audience prefers        
        DM_ContentSO audienceContent = contentListSO.contentSOList[Random.Range(0, contentListSO.contentSOList.Count)];
        audienceContentList.Add(audienceContent);
        OnSetContentPreference?.Invoke(audienceContentList[0].contentPreference);
        Debug.Log(audienceContentList[0].contentName);
        Debug.Log(audienceContentList[0].contentPreference);
        
        //randomizes which three characteristics the audience prefers 
        for (int i = 0; i < 3;)
        {
            DM_CharacteristicSO audienceCharacteristic = characteristicListSO.characteristicSOList[Random.Range(0, characteristicListSO.characteristicSOList.Count)];
            
            if (!CheckAudienceCharacteristicDuplicates(audienceCharacteristic))
            {
                audienceCharacteristicList.Add(audienceCharacteristic);
                // OnSetCharacteristicPreference?.Invoke(audienceCharacteristicList[i].characteristicPreference);
                Debug.Log(audienceCharacteristicList[i].characteristicName);
                // Debug.Log(audienceCharacteristicList[i].characteristicPreference);
                i++;
            }
        }
        OnSetCharacteristicPreference?.Invoke(audienceCharacteristicList);

    }


    private bool CheckAudienceCharacteristicDuplicates(DM_CharacteristicSO audienceCharacteristic)
    {
        //makes sure that the same characteristic isn't added twice 
        for (int i = 0; i < audienceCharacteristicList.Count; i++)
            {
                if (audienceCharacteristicList[i] == audienceCharacteristic)
                    return true;
            }
        return false;
    }
    
   
    private bool DetermineContentQuestionWin()
    {
        Debug.Log("chosen " + contentTypeUI.chosenContentAnswer);
        Debug.Log("correct " + contentTypeUI.correctContentAnswer);
        if (DetermineContentWin())
        {
            if (contentTypeUI.chosenContentAnswer == contentTypeUI.correctContentAnswer)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    
    private bool DetermineCharacteristicQuestionWin()
    {
        Debug.Log("chosen " + characteristicTypeUI.chosenCharacteristicAnswer);
        Debug.Log("correct " + characteristicTypeUI.correctCharacteristicAnswer);
        if (DetermineCharacteristicWin())
        {
            if (characteristicTypeUI.chosenCharacteristicAnswer == characteristicTypeUI.correctCharacteristicAnswer)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    
    
    private bool DetermineContentWin()
    {
        //checks if the selected content matches the audience's preference 
        getChosenContent = contentTypeUI.chosenContentSO;
        for (int i = 0; i < audienceContentList.Count; i++)
        {
            if (audienceContentList[i].contentName == getChosenContent.contentName)
            {
                return true;
            }
        }
        return false;
    }

    
    private bool DetermineCharacteristicWin()
    {
        //checks if the selected characteristic matches with the audience's preference 
        getChosenCharacteristicSO = characteristicTypeUI.chosenCharacteristicSO;
        for (int i = 0; i < audienceCharacteristicList.Count; i++)
        {
            if (audienceCharacteristicList[i].characteristicName == getChosenCharacteristicSO.characteristicName)
            {
                return true;
            }
        }
        return false;
    }


    private void OnDetermineWinState()
    {
        if (DetermineContentWin() && DetermineCharacteristicWin())
        {
            Debug.Log("win both");
        }
        else if (DetermineContentWin())
        {
            Debug.Log("win content");
        }
        else if (DetermineCharacteristicWin())
        {
            Debug.Log("win characteristic");
        }
        else
        {
            Debug.Log("win none");
        }

    }


    private void OnScoreBarUpdate(string checkItem)
    {
        if (checkItem == "ContentChoice")
            if (DetermineContentWin())
                scoreBar.fillAmount += 0.05f;

        if(checkItem == "ContentQAnswer")
             if (DetermineContentQuestionWin())
                scoreBar.fillAmount += 0.05f;

        if (checkItem == "CharChoice")
            if (DetermineCharacteristicWin())
                scoreBar.fillAmount += 0.05f;

        if(checkItem == "CharQAnswer")
             if (DetermineCharacteristicQuestionWin())
                scoreBar.fillAmount += 0.05f;
    }
        
}
