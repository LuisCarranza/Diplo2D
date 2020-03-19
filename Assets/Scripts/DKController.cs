using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DKController : MonoBehaviour
{
    public float dkSpeed;
    public float dkJumpForce;
    public int countItem;
    // Audio
    public AudioClip[] sfx;
    private AudioSource dkSource;

    Touch touch;
    private SpriteRenderer dkSprite;
    private Rigidbody2D rbDK;
    private Vector2 swipeVector;
    private Vector2 startTouchPosition;
    private bool Ground;
    private int countDamage;
    private Animator dkAnimator;
    private int nextLevel;

    void Start()
    {
        dkSprite = GetComponent<SpriteRenderer>();
        dkAnimator = GetComponent<Animator>();
        rbDK = GetComponent<Rigidbody2D>();
        dkSource = GetComponent<AudioSource>();
        Ground = false;
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        // PlayerProfiler.instance.LoadData();

        // if (PlayerPrefs.HasKey("Items"))
        //     countItem = PlayerPrefs.GetInt("Items");
        // else
        //     countItem = 0;
    }

    void Update()
    {
#if UNITY_STANDALONE
        dkAnimator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        rbDK.velocity = new Vector2(dkSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), rbDK.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !Ground)
        {
            rbDK.AddForce(Vector2.up * dkJumpForce, ForceMode2D.Impulse);
            dkAnimator.SetTrigger("Jump");
            // Ground = true;
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            // switch (touch.phase)
            // {
            //     case TouchPhase.Began:
            //         startTouchPosition = touch.position;
            //         break;
            //     case TouchPhase.Stationary:
            //         if (touch.position.x > Screen.width * 0.5f)
            //         {
            //             dkAnimator.SetFloat("Speed", 1);
            //             rbDK.velocity = new Vector2(dkSpeed * Time.deltaTime * Vector2.right.x, rbDK.velocity.y);
            //             dkSprite.flipX = false;
            //         }
            //         if (touch.position.x < Screen.width * 0.5f)
            //         {
            //             dkAnimator.SetFloat("Speed", 1);
            //             rbDK.velocity = new Vector2(-dkSpeed * Time.deltaTime * Vector2.right.x, rbDK.velocity.y);
            //             dkSprite.flipX = true;
            //         }
            //         break;
            //     case TouchPhase.Ended:
            //         swipeVector = startTouchPosition - touch.position;
            //         if (Ground == true)
            //         {
            //             // print("Swipe Value: " + swipeVector);
            //             if (Mathf.Abs(swipeVector.x) < 1.0f)
            //             {
            //                 rbDK.AddForce(Vector2.up * dkJumpForce, ForceMode2D.Impulse);
            //                 dkAnimator.SetTrigger("Jump");
            //                 dkSource.PlayOneShot(sfx[0]);
            //                 Ground = false;
            //             }
            //         }
            //         dkAnimator.SetFloat("Speed", 0);
            //         break;
            // }
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x > Screen.width * 0.5f)
                {
                    dkAnimator.SetFloat("Speed", 1);
                    rbDK.velocity = new Vector2(dkSpeed * Time.deltaTime * Vector2.right.x, rbDK.velocity.y);
                    dkSprite.flipX = false;
                }
                if (touch.position.x < Screen.width * 0.5f)
                {
                    dkAnimator.SetFloat("Speed", 1);
                    rbDK.velocity = new Vector2(-dkSpeed * Time.deltaTime * Vector2.right.x, rbDK.velocity.y);
                    dkSprite.flipX = true;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                dkAnimator.SetFloat("Speed", 0);
                if (Ground == true)
                {
                    swipeVector = startTouchPosition - touch.position;
                    // print("Swipe Value: " + swipeVector);
                    if (Mathf.Abs(swipeVector.x) < 1.0f)
                    {
                        rbDK.AddForce(Vector2.up * dkJumpForce, ForceMode2D.Impulse);
                        dkAnimator.SetTrigger("Jump");
                        dkSource.PlayOneShot(sfx[0]);
                        Ground = false;
                    }
                }
            }
        }
#endif

        if (DataLoader.instance.currentPlayer.lives <= 0)
        {
            DataLoader.instance.currentPlayer.items = 0;
            DataLoader.instance.currentPlayer.lives = 3;
            DataLoader.instance.currentPlayer.lastLevel = 1;
            DataLoader.instance.WriteData();
            SceneManager.LoadScene(0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            Ground = true;
            // print("Can Jump");
        }
        if (col.transform.tag == "Barrel")
        {
            dkAnimator.SetTrigger("Damage");
            DataLoader.instance.currentPlayer.lives--;
            // countDamage++;
            dkSource.PlayOneShot(sfx[2]);

            RewardedVideo.instance.ShowVideoForLifes();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            DataLoader.instance.currentPlayer.items++;
            // countItem++;
            dkSource.PlayOneShot(sfx[1]);

            // PlayerProfiler.instance.itemsCount = countItem;
            // PlayerProfiler.instance.SaveData();

            // PlayerPrefs.SetInt("Items", countItem);
        }

        if (col.tag == "Checkpoint")
        {
            Debug.Log("Reached Checkpoint!");
            DataLoader.instance.currentPlayer.lastLevel = nextLevel;
            DataLoader.instance.WriteData();
            SceneManager.LoadScene(nextLevel);
        }
    }
}
