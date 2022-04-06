using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class InspectEntityText : MonoBehaviour
{
    // Start is called before the first frame update
    // a public text-mesh pro element reference
    public TMPro.TextMeshProUGUI textStats;
    public TMPro.TextMeshProUGUI textName;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // find the player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    int health;
    int armor;
    int damage;
    void Update()
    {
        GameObject entity;
        // get the gameObject under the cursor by raycasting
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        // if hit is null or not does not have an entity component, set the entity variable to player
        if (hit.collider is null || !hit.collider.gameObject.GetComponent<Entity>())
        {
            entity = player;
        }
        else
        {
            entity = hit.collider.gameObject;
        }
        // if the new values are different, lerp to them gradually
        int currentHealth = entity.GetComponent<Entity>().CurrentHealth;
        int currentArmor = entity.GetComponent<Entity>().Armor;
        int currentDamage = entity.GetComponent<Entity>().Damage;

        StringBuilder textToDisplay = new StringBuilder($"Health: {currentHealth}\n");
        textToDisplay.Append($"Armor: {currentArmor}\n");
        textToDisplay.Append($"Damage: {currentDamage}\n");
        
        textStats.text = textToDisplay.ToString();
        textName.text = entity.gameObject.name;
    }
}
