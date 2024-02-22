using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI.
    private GameObject titlePanel;
    private GameObject gameEndPanel;

    // Button.
    private Button tapToStartBtn;
    private Button gameResetBtn;

    // Camera.
    private CameraController cameraController;
    private CameraEvents cameraEvent;

    // Player.
    private PlayerController playerController;
    private PlayerEvents playerEvent;

    // Audio.
    private AudioController audioController;
    private AudioEvents audioEvent;

    // Environment.
    public Scroller[] scrollers;

    // Obstacle.
    public Scroller obstacleGroup;

    private void Awake()
    {
        titlePanel = GameObject.Find("Title Panel");
        gameEndPanel = GameObject.Find("Game End Panel");

        tapToStartBtn = GameObject.Find("Tap To Start Button").GetComponent<Button>();
        tapToStartBtn.onClick.AddListener(() => audioEvent.NotifyPlaySFX(Enums.SFXClipType.UIClick));

        gameResetBtn = GameObject.Find("Game Reset Button").GetComponent<Button>();
        gameResetBtn.onClick.AddListener(() => audioEvent.NotifyPlaySFX(Enums.SFXClipType.UIClick));

        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cameraController.Initialize(out cameraEvent);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Initialize(out playerEvent);

        audioController = GameObject.Find("Audio Controller").GetComponent<AudioController>();
        audioController.Initialize(out audioEvent);
        
        audioEvent.NotifyPlayBGM();
    }

    private void Start()
    {
        gameEndPanel.SetActive(false);

        tapToStartBtn.onClick.AddListener(StartGame);
        gameResetBtn.onClick.AddListener(ResetGame);

        playerEvent.Jumped += () => audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerJump);
        playerEvent.Landed += () => audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerLand);
        playerEvent.Hit += EndGame;
    }

    public void StartGame()
    {
        titlePanel.SetActive(false);
        playerController.Enable();
        cameraEvent.NotifyMove(Defines.CAMERA_POSITION_X_PLAY);
        audioEvent.NotifyPlayBGM();
        audioEvent.NotifyPlaySFX(Enums.SFXClipType.GameStart);
        
        obstacleGroup.isPlay = true;
    }

    public void PauseGame()
    {
        playerController.Disable();
    }

    private void EndGame()
    {
        gameEndPanel.SetActive(true);
        playerController.Disable();
        cameraEvent.NotifyPunch();
        audioEvent.NotifyStopBGM();
        audioEvent.NotifyPlaySFX(Enums.SFXClipType.GameEnd);
        audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerHit);

        obstacleGroup.isPlay = false;

        foreach (var scroller in scrollers)
            scroller.isPlay = false;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
