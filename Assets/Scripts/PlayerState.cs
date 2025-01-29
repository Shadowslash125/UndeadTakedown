using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] public int playerHealth = 7;
    [SerializeField] public int maxHealth = 7;

    [SerializeField] private Healthbar healthBar;
    private ThirdPersonController thirdPerson;
    private ThirdPersonShooterController thirdShooter;
    private EnemyAiTutorial enemyAiTutorial;
    private PlayerCombat playerCombat;

    // Rigidbody and colliders for death effects
    //private Rigidbody myRigidbody;
    private CapsuleCollider myBodyCollider;

    // State variables for death behavior
    public bool isAlive = true;
    private Animator animator;
    public GameObject gameOverScreen;
    public GameObject MenuBackground;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth; 
        //healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        healthBar =  GetComponent<Healthbar>();
        thirdPerson = GetComponent<ThirdPersonController>();
        thirdShooter = GetComponent<ThirdPersonShooterController>();
        enemyAiTutorial = GetComponent<EnemyAiTutorial>();
        playerCombat = GetComponent<PlayerCombat>();
        //myRigidbody = GetComponent<Rigidbody>(); // Assuming 2D, you can change this to 3D if needed
        myBodyCollider = GetComponent<CapsuleCollider>(); 
        //myFeetCollider = GetComponent<Collider>(); // You can assign a specific foot collider if required
    }

    void Update()
    {
        // If the player is dead, prevent any further movement
        if (!isAlive)
        {
            return;
        }
    }

    // Method to reduce player's health
    public void TakeDamage(int amount)
    {
        playerHealth = playerHealth - amount;
        //healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0 && isAlive)
        {
            Die(); // Trigger death when health is zero or less
        }
    }

    // Method to handle player's death
    public void Die()
    {
        Debug.Log("You have died!");
        AudioManager.instance.PlayEventSound("Die");
        isAlive = false;
        animator.SetTrigger("Dying");
        playerHealth = 0;
        // Disable movement and set up the death animation
        //myAnimator.SetTrigger("isDying"); // Play death animation trigger
        // Apply a slight force or freeze the player after death
        //myRigidbody.velocity = deathForce;
        myBodyCollider.enabled = false; // Disable body collision
        //myFeetCollider.enabled = false; // Disable feet collision
        //myRigidbody.gravityScale = 0; // Optional: remove gravity after death
        //myRigidbody.constraints = RigidbodyConstraints.FreezeAll; // Freeze all physics movement
        playerCombat.enabled = false;
        thirdShooter.enabled = false;
        thirdPerson.enabled = false;
        this.enabled = false; // Disable PlayerHealth script (so no more updates happen)
        //gameOver.GameOver(); // Call GameOver function
    }
    public void GameOver()
    {
        MenuBackground.SetActive(true);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
