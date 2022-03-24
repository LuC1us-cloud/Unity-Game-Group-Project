using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityDisplay : MonoBehaviour
{
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
        text.text = player.GetComponent<PlayerMovement>().specialAbility.ToString();
    }
}
