using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]private PlayerState playerState;

    // Serialized fields for the different health bar sections
    [SerializeField] private GameObject FullBars;
    [SerializeField] private GameObject Bars1;
    [SerializeField] private GameObject Bars2;
    [SerializeField] private GameObject Bars3;
    [SerializeField] private GameObject Bars4;
    [SerializeField] private GameObject Bars5;
    [SerializeField] private GameObject Bars6;
    [SerializeField] private GameObject BrokenHeart;
    [SerializeField] private GameObject Healthbars;

    private bool healthThreshold = true;
    void Update()
    {
        // Constantly check and update the health bar based on player's health
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Update the visibility of health bar segments based on the player's current health
        if (playerState.playerHealth >= 7)
        {
            FullBars.SetActive(healthThreshold);
        }
        else
        {

            if (playerState.playerHealth == 6)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(true);
                Bars2.SetActive(false);
                Bars3.SetActive(false);
                Bars4.SetActive(false);
                Bars5.SetActive(false);
                Bars6.SetActive(false);
            }
            else if (playerState.playerHealth == 5)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(true);
                Bars3.SetActive(false);
                Bars4.SetActive(false);
                Bars5.SetActive(false);
                Bars6.SetActive(false);
            }
            else if (playerState.playerHealth == 4)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(false);
                Bars3.SetActive(true);
                Bars4.SetActive(false);
                Bars5.SetActive(false);
                Bars6.SetActive(false);
            }
            else if (playerState.playerHealth == 3)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(false);
                Bars3.SetActive(false);
                Bars4.SetActive(true);
                Bars5.SetActive(false);
                Bars6.SetActive(false);
            }
            else if (playerState.playerHealth == 2)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(false);
                Bars3.SetActive(false);
                Bars4.SetActive(false);
                Bars5.SetActive(true);
                Bars6.SetActive(false);
            }
            else if (playerState.playerHealth == 1)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(false);
                Bars3.SetActive(false);
                Bars4.SetActive(false);
                Bars5.SetActive(false);
                Bars6.SetActive(true);
            }
            else if (playerState.playerHealth <= 0)
            {
                FullBars.SetActive(false);
                Bars1.SetActive(false);
                Bars2.SetActive(false);
                Bars3.SetActive(false);
                Bars4.SetActive(false);
                Bars5.SetActive(false);
                Bars6.SetActive(false);
                Healthbars.SetActive(false);
                BrokenHeart.SetActive(true);
            }
        }
    }
}
