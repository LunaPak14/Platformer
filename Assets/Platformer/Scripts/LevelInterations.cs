using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelInterations : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI pointText;
    private int coins = 0;
    private int points = 0;

    private void Awake()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
    }

    private void changePoints()
    {
        pointText.text = "Points \n x" + points;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            points += 100;
            Destroy(collision.gameObject);
            changePoints();
        }

        if (collision.gameObject.CompareTag("Question"))
        {
            points += 100;
            coins++;
            changePoints();
            coinsText.text = "x" + (coins < 10 ? "0" + coins : coins);
        }
    }
}
