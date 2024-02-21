using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Move,
        Jump,
        Hit
    }

    public bool isPlay;

    public float startJumpPower;
    public float jumpPower;
    public bool isGround;

    private Rigidbody2D rigid;
    private Animator anim;
    private PlayerEvents playerEvent;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        startJumpPower = 4;
        jumpPower = 1;
        isGround = true;
        anim.SetInteger("State", (int)State.Move);
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") == true
            && isPlay == true
            && anim.GetInteger("State").Equals((int) State.Hit) == false)
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
            anim.SetInteger("State", (int) State.Hit);
            playerEvent.NotifyHit();
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
            anim.SetInteger("State", (int) State.Move);
            playerEvent.NotifyLanded();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground") == true)
        {
            if (isGround == false)
                return;

            isGround = false;
            anim.SetInteger("State", (int) State.Jump);
            playerEvent.NotifyJumped();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") == true
            && isPlay == true 
            && isGround == true
            && anim.GetInteger("State").Equals((int)State.Hit) == false)
        {
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }
    }

    public bool Initialize(out PlayerEvents playerEvent)
    {
        playerEvent = new PlayerEvents();
        this.playerEvent = playerEvent;

        return true;
    }
}
