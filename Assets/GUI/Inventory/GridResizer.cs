using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridResizer : MonoBehaviour
{
    public Transform Content;
    public GameObject Grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get Grid childs count, and set the content vertical size by the formula
        // 60 * (childs count / 5)
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * (Grid.transform.childCount / 5));
    }
}
