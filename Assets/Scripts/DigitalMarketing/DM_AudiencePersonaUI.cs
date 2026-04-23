using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DM_AudiencePersonaUI : MonoBehaviour
{

    // [SerializeField] private TextMeshProUGUI characteristicNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI contentPreferenceText;
    

    private void Awake()
    {
        DM_GameLogic.OnSetContentPreference += OnSetContentPreference;
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
}
