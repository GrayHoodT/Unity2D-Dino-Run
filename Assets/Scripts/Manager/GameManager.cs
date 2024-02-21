using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController cameraController;
    private CameraEvents cameraEvent;

    private AudioController audioController;
    private AudioEvents audioEvent;

    private PlayerController playerController;
    private PlayerEvents playerEvent;

    private void Awake()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        cameraController.Initialize(out cameraEvent);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.Initialize(out playerEvent);

        audioController = GameObject.Find("Audio Controller").GetComponent<AudioController>();
        audioController.Initialize(out audioEvent);
    }

    private void Start()
    {
        playerEvent.Hit += cameraEvent.NotifyPunched;
    }

    public void GameStart()
    {
        playerController.isPlay = true;
        cameraEvent.NotifyMoved();
    }

    public void GamePause()
    {
        playerController.isPlay = false;
    }

    private void GameEnd()
    {
        playerController.isPlay = false;
    }
}
