using System;
using TMPro;
using UnityEngine;

public class LevelInterations : MonoBehaviour
{
    private TextMeshProUGUI coinsText;
    private int coins = 0;

    private void Awake()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // check for the left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.CompareTag("Brick"))
                {
                    Destroy(hit.transform.gameObject);
                }

                else if (hit.transform.CompareTag("Question"))
                {
                    coins++;
                    coinsText.text = "x" + (coins < 10 ? "0" + coins : coins);
                }
            }
        }
    }
}
