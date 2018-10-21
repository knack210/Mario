using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float speed;
    public float jumpforce;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private int coins;
    public Text coinText;
    public Text winText;

    // private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    //audio stuff
    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip stompClip;
    public AudioClip defeatClip;
    public AudioClip victoryClip;
    //private float volLowRange = .5f;
    //private float volHighRange = 1.0f;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coins = 0;
        setCoinText();
        winText.text = "";
    }

    void Awake()
    {

       source = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    
    // Update is called once per frame
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");

        //Vector2 movement = new Vector2(moveHorizontal, 0);

       // rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);



        //stuff I added to flip my character
        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {
               // rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
               rb2d.velocity = Vector2.up * jumpforce;


                // Audio stuff
                //float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(jumpClip);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coins = coins + 1;
            setCoinText();
            
        }

        else if (other.gameObject.CompareTag("CoinBlock"))
        {
            if (other.GetComponent<CoinBox>().coinNumber >= 0)
            {
                coins = coins + 1;
                setCoinText();
            }
        }

        else if (other.gameObject.CompareTag("Goomba"))
        {
            if (transform.position.y > other.transform.position.y)
            {
                other.gameObject.SetActive(false);
                source.PlayOneShot(stompClip);
            }

            else
            {
                source.PlayOneShot(defeatClip);
                Thread.Sleep(1000);
                this.gameObject.SetActive(false);
                
            }
        }

        else if (other.gameObject.CompareTag("Victory"))
        {
            winText.text = "You win!";
            source.PlayOneShot(victoryClip);
        }
    }

    void setCoinText()
    {
        coinText.text = "Coins: " + coins.ToString();

        if (coins > 0)
        {
            source.PlayOneShot(coinClip);
        }
    }
}
