using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickListener);
    }
    void OnClickListener(){
        if (item == null) return;
        item.Drop(GameObject.Find("Main Player").transform);

        // remove from player inventory
        GameObject.Find("Main Player").GetComponent<MainPlayer>().inventory.Remove(item);

        // delete parent Gameobject (Prefab slot)
        Destroy(transform.parent.gameObject);
    }
}
