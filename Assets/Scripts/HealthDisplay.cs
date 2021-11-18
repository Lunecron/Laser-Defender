using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player player;
    [SerializeField] int maxHealthLength = 4;


    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            healthText.text = ConvertToScore(player.GetComponent<Player>().GetHealth());
        }
        else
        {
            healthText.text = ConvertToScore(0);
        }
    }

    private string ConvertToScore(float health)
    {
        string amountOfZeros = "";
        for (int zeros = maxHealthLength - health.ToString().Length; zeros > 0; zeros--)
        {
            amountOfZeros += "0";
        }
        return amountOfZeros + health.ToString();
    }
}
