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
    float timePast = 0;
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
        int currentHealth = 0;
        int currentArmor = 0;
        int currentDamage = 0;
        if (entity.GetComponent<Entity>().Armor != armor || entity.GetComponent<Entity>().CurrentHealth != health || entity.GetComponent<Entity>().Damage != damage)
        {
            currentHealth = (int)Mathf.LerpUnclamped(health, entity.GetComponent<Entity>().CurrentHealth, timePast);
            currentArmor = (int)Mathf.LerpAngle(armor, entity.GetComponent<Entity>().Armor, timePast);
            currentDamage = (int)Mathf.Lerp(damage, entity.GetComponent<Entity>().Damage, timePast);
            timePast += Time.deltaTime * 10;
        }
        // if all values are equal, health, armor, damage to the current values
        else
        {
            health = currentHealth;
            armor = currentArmor;
            damage = currentDamage;
            timePast = 0;
        }
        
        StringBuilder textToDisplay = new StringBuilder($"Health: {currentHealth}\n");
        textToDisplay.Append($"Armor: {currentArmor}\n");
        textToDisplay.Append($"Damage: {currentDamage}\n");
        
        textStats.text = textToDisplay.ToString();
        textName.text = entity.gameObject.name;
    }

    // lerp to a new value gradually
    IEnumerator LerpTo(string text, string newText)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            textStats.text = Mathf.Lerp(float.Parse(text), float.Parse(newText), i).ToString();
            yield return null;
        }
    }
}
