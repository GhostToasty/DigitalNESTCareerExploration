using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class DM_AudiencePersonaUI : MonoBehaviour
{

    // [SerializeField] private TextMeshProUGUI characteristicNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI contentPreferenceText;
    [SerializeField] private TextMeshProUGUI characteristic1PreferenceText;
    [SerializeField] private TextMeshProUGUI characteristic2PreferenceText;
    [SerializeField] private TextMeshProUGUI characteristic3PreferenceText;

    

    private void Awake()
    {
        DM_GameLogic.OnSetContentPreference += OnSetContentPreference;
        DM_GameLogic.OnSetCharacteristicPreference += OnSetCharacteristicPreference;
    }

    
    private void Start()
    {
        SetCharacteristicVisual();
    }



    public void SetCharacteristicVisual()
    {  
        foreach (Transform child in iconContainer)
        {
            //skips icon template so it isn't destroyed 
            if (child == iconTemplate) continue;
            
            //destroys all other children objects so they can be replaced 
            Destroy(child.gameObject);
        }

        foreach (DM_CharacteristicSO characteristicSO in DM_GameLogic.audienceCharacteristicList)
        {
            //creates and reveals characteristic item icon
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = characteristicSO.sprite;
            // characteristicNameText.text = characteristicSO.characteristicName;

            Debug.Log("set visual");
            Debug.Log(characteristicSO.characteristicName);
        }
    }


    public void OnSetContentPreference(string audienceContentPreference)
    {
        contentPreferenceText.text = audienceContentPreference;
    }

    public void OnSetCharacteristicPreference(List<DM_CharacteristicSO> audienceCharacteristicPreferenceList)
    {
        for(int i = 0; i < 3; i++)
        {
            if(i == 0)
                characteristic1PreferenceText.text = audienceCharacteristicPreferenceList[0].characteristicPreference;
            else if(i == 1)
                characteristic2PreferenceText.text = audienceCharacteristicPreferenceList[1].characteristicPreference;
            else if(i == 2)
                characteristic3PreferenceText.text = audienceCharacteristicPreferenceList[2].characteristicPreference;
        }
    }
}
