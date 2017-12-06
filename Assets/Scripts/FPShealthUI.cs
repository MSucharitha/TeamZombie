using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPShealthUI : MonoBehaviour
{

    [SerializeField]
    GameObject fill;

    [SerializeField]
    GameObject player;

    Image fillImage;
    float currHealth = -1.0f;
    float maxHealth = -1.0f;
    public float healthPercentage;
    bool active = true;

    // Use this for initialization
    void Start()
    {
        ObtainComponents();
    }

    void ObtainComponents()
    {
        fillImage = fill.GetComponent<Image>();
        if (fillImage == null)
        {
            Debug.Log("ERROR! Fill image file not found!");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate canvas towards the player
        transform.LookAt(player.transform.position);
        transform.Rotate(0f, 180f, 0f);

    }

    public void SetHealth(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }

    public void UpdateHealth(float health)
    {
        currHealth = currHealth-health;

        // Guarantee that the health never goes above 1 or below 0
        healthPercentage = Mathf.Clamp01((float)currHealth / (float)maxHealth);
        Debug.Log("Player health is " + health.ToString() + "/" + maxHealth.ToString() + " = " + healthPercentage.ToString() + "%");

        // Update the health bar game object
        fill.transform.localScale = new Vector3(healthPercentage, 1f, 1f);

        // If below a certain threshold, change the color
        fillImage = fill.GetComponent<Image>();
        if (active && fillImage != null)
        {
            if (healthPercentage <= 0.25f)
            {
                fillImage.color = new Color(1f, 0.3f, 0.3f);    // red
            }
            else if (healthPercentage <= 0.5f)
            {
                fillImage.color = new Color(1f, 0.75f, 0.2f);   // orange
            }
            else
            {
                fillImage.color = new Color(0.3f, 1f, 0.3f);    // green
            }
        }

        // Optional: Add Health Bar Text
        // Update the health bar text
    }

    public void Show()
    {
        active = true;
        gameObject.SetActive(true);

        ObtainComponents();
    }
    public void Hide()
    {
        active = false;
        gameObject.SetActive(false);
    }
}
