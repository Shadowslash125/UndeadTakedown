using StarterAssets;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private GameObject Melee;
    private bool toggleWeapon = true;
    private bool isAttacking = false; // To prevent multiple attacks at once

    private void Start()
    {
        animator = GetComponent<Animator>();
        Melee.SetActive(!toggleWeapon); // Melee weapon is inactive initially
    }

    private void Update()
    {
        Fight();
    }

    private void Fight()
    {
        // Only allow attack if player is not aiming and has pressed the shoot button
        if (!starterAssetsInputs.aim && Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttack();
        }
        else
        {
            return;
        }
    }

    private void StartAttack()
    {
        // Set attacking state to prevent retriggering
        isAttacking = true;

        // Activate melee weapon
        Melee.SetActive(toggleWeapon);

        // Trigger attack animation
        animator.SetTrigger("hit1");
    }

    // This method should be called at the end of the attack animation to reset the state
    public void EndAttack()
    {
        // Reset the attacking state
        isAttacking = false;
        // Hide melee weapon after attack
        Melee.SetActive(!toggleWeapon);
    }
}
