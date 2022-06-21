using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Button button;
    public Stopwatch stopwatch;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(resetTimers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetTimers(){
		stopwatch.reset();
	}
}
