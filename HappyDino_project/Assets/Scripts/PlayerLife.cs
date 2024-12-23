using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private ParticleSystem explosionEffect;
    public float currentHealth { get; private set; }
    private bool dead;
    [SerializeField] private float startHealth;

    void Start()
    {
        currentHealth = startHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void TakeDmg(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, 5);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            if (dead == false)
            {
                Die();
                dead = true;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDmg(1);
        }
    }
    public void Die()
    {
        deathSoundEffect.Play();
        explosionEffect.Play();
        rb.velocity = Vector2.zero;
        rb.simulated = false;

        transform.localScale = Vector3.zero;
        //anim.SetTrigger("death");
        Invoke(nameof(RestartLevel), 2f);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
