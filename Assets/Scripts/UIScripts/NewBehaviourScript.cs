 using UnityEngine;  
 using System.Collections;  
 using UnityEngine.EventSystems;  
 using UnityEngine.UI;
 public class MenuButton : MonoBehaviour {
 
	public Button yourButton;
    public Text thetext;

    public Gradient grad1;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
	}
    public void OnPointerDown(){
		
	}
 }
