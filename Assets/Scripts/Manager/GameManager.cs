using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // UI.
    private Button tapToStartBtn;

    // Camera.
    private CameraController cameraController;
    private CameraEvents cameraEvent;

    // Player.
    private PlayerController playerController;
    private PlayerEvents playerEvent;

    // Audio.
    private AudioController audioController;
    private AudioEvents audioEvent;
  
    private void Awake()
    {
        tapToStartBtn = GameObject.Find("Tap To Start Button").GetComponent<Button>();
        tapToStartBtn.onClick.AddListener(GameStart);
        tapToStartBtn.onClick.AddListener(() => audioEvent.NotifyPlaySFX(Enums.SFXClipType.UIClick));

        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cameraController.Initialize(out cameraEvent);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Initialize(out playerEvent);

        audioController = GameObject.Find("Audio Controller").GetComponent<AudioController>();
        audioController.Initialize(out audioEvent);
    }

    private void Start()
    {
        playerEvent.Jumped += () => audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerJump);
        playerEvent.Landed += () => audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerLand);
        playerEvent.Hit += () => audioEvent.NotifyPlaySFX(Enums.SFXClipType.PlayerHit);
        playerEvent.Hit += GameEnd;
    }

    public void GameStart()
    {
        playerController.Enable();
        cameraEvent.NotifyMove();
        audioEvent.NotifyPlaySFX(Enums.SFXClipType.GameStart);
    }

    public void GamePause()
    {
        playerController.Disable();
    }

    private void GameEnd()
    {
        playerController.Disable();
        cameraEvent.NotifyPunch();
        audioEvent.NotifyPlaySFX(Enums.SFXClipType.GameEnd);
    }
}
