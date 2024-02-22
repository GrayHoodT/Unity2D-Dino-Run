using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ���� �÷��� �����ߴ���?
    private bool isPlay;

    // �÷��̾� ������.
    private float startJumpPower;
    private float jumpPower;

    // �÷��̾ ���� �پ� �ִ���?
    private bool isGround;

    // �÷��̾� ������Ʈ ����.
    private Rigidbody2D rigid;
    private Animator anim;

    // Input.
    private Controls controls;
    private bool isJumpStarted;
    private bool isJumpPerformed;

    private PlayerEvents events;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        controls = new Controls();
        controls.Player.Jump.started += OnJumpStarted;
        controls.Player.Jump.performed += OnJumpPerformed;
        controls.Player.Jump.canceled += OnJumpCanceled;
        controls.Enable();
    }

    private void Start()
    {
        startJumpPower = 4;
        jumpPower = 1;
        isGround = true;
        anim.SetInteger("State", (int) Enums.PlayerState.Move);
    }

    private void FixedUpdate()
    {
        if (isPlay == true
            && isJumpPerformed == true
            && anim.GetInteger("State").Equals((int) Enums.PlayerState.Hit) == false)
        {
            jumpPower = Mathf.Lerp(jumpPower, 0, 0.1f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") == true)
        {
            rigid.simulated = false;
            anim.SetInteger("State", (int) Enums.PlayerState.Hit);
            events.NotifyHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            if (isGround == true)
                return;

            jumpPower = 1;
            isGround = true;
            isJumpStarted = false;
            anim.SetInteger("State", (int) Enums.PlayerState.Move);
            events.NotifyLanded();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground") == true)
        {
            if (isGround == false)
                return;

            isGround = false;
            anim.SetInteger("State", (int) Enums.PlayerState.Jump);
            events.NotifyJumped();
        }
    }

    private void Update()
    {
        if (isPlay == true
            && isJumpStarted == true
            && isGround == true
            && anim.GetInteger("State").Equals((int) Enums.PlayerState.Hit) == false)
        {
            isJumpStarted = false;
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }
    }

    public bool Initialize(out PlayerEvents playerEvent)
    {
        playerEvent = new PlayerEvents();
        events = playerEvent;

        return true;
    }

    public void Enable()
    {
        isPlay = true;
        controls.Enable();
    }

    public void Disable()
    {
        isPlay = false;
        controls.Disable();
    }

    #region Callbacks

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        isJumpStarted = true;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        isJumpPerformed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        isJumpStarted = false;
        isJumpPerformed = false;
    }

    #endregion
}
