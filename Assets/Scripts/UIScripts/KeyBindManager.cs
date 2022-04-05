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

    public Dictionary<string, KeyCode> MovementBinds {get; set;}
    public Dictionary<string, KeyCode> ActionBinds {get; private set;}
    private string bindName;
    public GameObject[] keybindButtons;

    // Start is called before the first frame update
    private void Awake() 
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind"); 
    }
    void Start()
    {
        
        MovementBinds = new Dictionary<string, KeyCode>();

        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("MoveUp", KeyCode.W);
        BindKey("MoveDown", KeyCode.S);
        BindKey("MoveLeft", KeyCode.A);
        BindKey("MoveRight", KeyCode.D);

        BindKey("ActivatePower", KeyCode.LeftShift);
        BindKey("RotatePower", KeyCode.R);
        BindKey("OpenInventory", KeyCode.Tab);
        BindKey("Interact", KeyCode.E);
    }
    
    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = MovementBinds;

        if (key.Contains("Power") || key.Contains("Inventory") || key.Contains("Interact"))
        {
            currentDictionary = ActionBinds;
        }
        if (!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key, keyBind);
            KeyBindManager.MyInstance.UpdateKeyText(key,keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            KeyBindManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }
        currentDictionary[key] = keyBind;
        KeyBindManager.MyInstance.UpdateKeyText(key,keyBind);
        bindName = string.Empty;
    }
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x=> x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }
}
