using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectEntityText : MonoBehaviour
{
    // Start is called before the first frame update
    // a public text-mesh pro element reference
    public TMPro.TextMeshProUGUI text;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // find the player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
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

        string textToDisplay = $"{entity.gameObject.name}\n";
        textToDisplay += "Health: " + entity.GetComponent<Entity>().CurrentHealth.ToString() + "\n";
        textToDisplay += "Armor: " + entity.GetComponent<Entity>().Armor.ToString() + "\n";
        textToDisplay += "Damage: " + entity.GetComponent<Entity>().Damage.ToString() + "\n";
        
        text.text = textToDisplay;
    }
}
