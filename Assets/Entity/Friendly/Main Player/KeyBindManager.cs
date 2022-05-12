using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }

    private string bindName;
    public GameObject[] keybindButtons;

    void Start()
    {

        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");

        BindKey("MoveUp", KeyCode.W);
        BindKey("MoveDown", KeyCode.S);
        BindKey("MoveLeft", KeyCode.A);
        BindKey("MoveRight", KeyCode.D);

        BindKey("ActivatePower", KeyCode.LeftShift);
        BindKey("RotatePower", KeyCode.R);
        BindKey("OpenInventory", KeyCode.Tab);
        BindKey("Interact", KeyCode.E);
        this.gameObject.SetActive(false);
    }

    public void BindKey(string key, KeyCode keyBind)
    {

        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, keyBind.ToString());
        }

        else if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, keyBind.ToString());
            KeyBindManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }
        KeyBindManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }
    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }
    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }
}
