using UnityEngine;
using UnityEngine.UI;

public class DM_ScreenSwitch : MonoBehaviour
{
    public delegate void OnRestartGameLoopFunc();
    public static event OnRestartGameLoopFunc OnRestartGameLoop;

    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Canvas replayCanvas;
    [SerializeField] private Button startButton;
    [SerializeField] private Button replayButton;

    
    private void Awake()
    {
        DM_CharacteristicTypeUI.OnReplayScreen += OnReplayScreen;
        DM_GameLogic.OnStartScreen += OnStartScreen;
        
        startButton.onClick.AddListener(() => { ClickStartButton(); });
        replayButton.onClick.AddListener(() => { ClickReplayButton(); });
    }

    
    private void Start()
    {
        ShowStartScreen();
        HideReplayScreen();
    }


    public void OnReplayScreen()
    {
        ShowReplayScreen();
    }

    public void OnStartScreen()
    {
        ShowStartScreen();
    }


    private void ClickStartButton()
    {
        HideStartScreen();
        OnRestartGameLoop?.Invoke();
    }


    private void ClickReplayButton()
    {
        HideReplayScreen();
        OnRestartGameLoop?.Invoke();
    }


    //show and hiding functions 

    private void ShowStartScreen()
    {
        startCanvas.gameObject.SetActive(true);
    }

    private void HideStartScreen()
    {
        startCanvas.gameObject.SetActive(false);
    }


    private void ShowReplayScreen()
    {
        replayCanvas.gameObject.SetActive(true);
    }

    private void HideReplayScreen()
    {
        replayCanvas.gameObject.SetActive(false);
    }
}
