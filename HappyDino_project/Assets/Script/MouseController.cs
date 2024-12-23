using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float jetpackForce = 75;
    private Rigidbody2D playerRigidbody;
    public float fowardMovementSpeed = 3;
    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator mouseAnimator;

    private bool isDead = false;
    private uint coins = 0;
    public Button restartButton;
    public Text coinsCollectedLabel;
    public AudioClip coinCollectSound;

    void CollectCoin(Collider2D coinCollider){
        coins++;
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }
    void OnTriggerEnter2D(Collider2D collider){
        // HitByLaser(collider);

        if(collider.gameObject.CompareTag("Coins")){
            CollectCoin(collider);
        }
        else{
            HitByLaser(collider);
        }
    }
    void HitByLaser(Collider2D laserCollider){
        isDead = true;
    }

    public void RestartGame(){
        SceneManager.LoadScene("RocketMouse");
    }

    void UpdateGroundedStatus(){
    isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
    mouseAnimator.SetBool("isGrounded", isGrounded);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        mouseAnimator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        bool jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive && !isDead;

        if(jetpackActive){
            playerRigidbody.AddForce(new Vector2(0, jetpackForce));
        }

        if(!isDead){
            Vector2 newVelocity = playerRigidbody.velocity;
            newVelocity.x = fowardMovementSpeed;
            playerRigidbody.velocity = newVelocity;
        }
        if(isDead){
            restartButton.gameObject.SetActive(true);
        }
        UpdateGroundedStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
