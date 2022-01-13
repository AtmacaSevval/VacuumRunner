using UnityEngine;
using System.Collections;
using TMPro;

public class ShootableObject : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    private TMP_Text healthTextOfObject;

    private int textHealth;
    private void Awake()
    {
        healthTextOfObject = GetComponentInChildren<TMP_Text>();
    }
    void Start()
    {
        textHealth = currentHealth;
        ChangeHealthText();
    }

    void ChangeHealthText()
    {
        if (healthTextOfObject != null)

            healthTextOfObject.text = textHealth.ToString();

    }
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "bullet")
        {
            textHealth--;

            Scores.AddToScore(1);

            if (textHealth <= 0)

                gameObject.SetActive(false);

            else
            {
                ChangeHealthText();
            }



        }
    }
}





