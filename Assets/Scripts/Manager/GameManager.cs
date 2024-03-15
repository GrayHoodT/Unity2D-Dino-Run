using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;

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
        titlePanel = GameObject.Find(Defines.TITLE_PANEL_NAME);
        gameEndPanel = GameObject.Find(Defines.GAME_END_PANEL_NAME);

        tapToStartBtn = GameObject.Find(Defines.TAP_TO_START_BUTTON_NAME).GetComponent<Button>();
        tapToStartBtn.onClick.AddListener(() => audioEvent.NotifyPlaySFX(Enums.SFXClipType.UIClick));

        gameResetBtn = GameObject.Find(Defines.GAME_RESET_BUTTON_NAME).GetComponent<Button>();
        gameResetBtn.onClick.AddListener(() => audioEvent.NotifyPlaySFX(Enums.SFXClipType.UIClick));

        cameraController = GameObject.Find(Defines.MAIN_CAMERA_NAME).GetComponent<CameraController>();
        cameraController.Initialize(out cameraEvent);

        playerController = GameObject.Find(Defines.PLAYER_NAME).GetComponent<PlayerController>();
        playerController.Initialize(out playerEvent);

        audioController = GameObject.Find(Defines.AUDIO_CONTROLLER_NAME).GetComponent<AudioController>();
        audioController.Initialize(out audioEvent);
        
        audioEvent.NotifyPlayBGM();
    }

    private void Start()
    {
        gameEndPanel.SetActive(false);

        tapToStartBtn.onClick.AddListener(StartGame);
        gameResetBtn.onClick.AddListener(ResetGame);

        InputSystem.onAnyButtonPress
            .CallOnce((ctrl) => { StartGame(); });

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

    public void EndGame()
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

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
