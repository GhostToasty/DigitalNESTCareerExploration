using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DM_GameLogic : MonoBehaviour
{
    [SerializeField] DM_ContentTypeUI contentTypeUI;
    [SerializeField] DM_CharacteristicTypeUI characteristicTypeUI;
    [SerializeField] DM_CharacteristicListSO characteristicListSO;
    [SerializeField] DM_ContentListSO contentListSO;

    [SerializeField] public static List<DM_ContentSO> audienceContentList = new List<DM_ContentSO>();
    [SerializeField] public static List<DM_CharacteristicSO> audienceCharacteristicList = new List<DM_CharacteristicSO>();

    private string getChosenContent;
    private string getChosenCharacteristic;
    private string winningContent;
    private string winningCharacteristic;


    private void Awake()
    {
        DM_CharacteristicTypeUI.OnDetermineWinState += OnDetermineWinState;
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
        Debug.Log(audienceContentList[0].contentName);
        
        //randomizes which three characteristics the audience prefers 
        for (int i = 0; i < 3;)
        {
            DM_CharacteristicSO audienceCharacteristic = characteristicListSO.characteristicSOList[Random.Range(0, characteristicListSO.characteristicSOList.Count)];
            
            if (!CheckAudienceCharacteristicDuplicates(audienceCharacteristic))
            {
                audienceCharacteristicList.Add(audienceCharacteristic);
                Debug.Log(audienceCharacteristicList[i].characteristicName);
                i++;
            }
        }

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
    
   
    private bool DetermineContentWin()
    {
        //checks if the selected content matches the audience's preference 
        getChosenContent = contentTypeUI.chosenContent;
        for (int i = 0; i < audienceContentList.Count; i++)
        {
            if (audienceContentList[i].contentName == getChosenContent)
            {
                return true;
            }
        }
        return false;
    }

    
    private bool DetermineCharacteristicWin()
    {
        //checks if the selected characteristic matches with the audience's preference 
        getChosenCharacteristic = characteristicTypeUI.chosenCharacteristic;
        for (int i = 0; i < audienceCharacteristicList.Count; i++)
        {
            if (audienceCharacteristicList[i].characteristicName == getChosenCharacteristic)
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
}
