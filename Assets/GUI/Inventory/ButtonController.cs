using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    public Item item;
    private GameObject mainPlayerGameObject;
    private MainPlayer mainPlayer;
    private GameObject playerRightArm;
    private GameObject playerLeftArm;
    private bool placeInLeftArm = true;
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickListener);
        mainPlayer = GameObject.Find("Main Player").GetComponent<MainPlayer>();
        mainPlayerGameObject = GameObject.Find("Main Player");
        playerRightArm = GameObject.Find("Right Arm");
        playerLeftArm = GameObject.Find("Left Arm");
    }
    private void Update()
    {
        if (isSelected)
        {
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
    void OnClickListener()
    {
        if (item == null) return;
        // if leftshift is clicked and the item is not null drop it
        if (Input.GetKey(KeyCode.LeftShift))
        {
            item.Drop(mainPlayerGameObject.transform);
            // remove from player inventory
            mainPlayer.inventory.Remove(item);
            // delete parent Gameobject (Prefab slot)
            Destroy(transform.parent.gameObject);
        }
        else
        {
            isSelected = true;
            mainPlayer.EquipItem(item.gameObject);
        }
    }
}
