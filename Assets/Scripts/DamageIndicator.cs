using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public float timeToLive = 3f;
    private float timeAlive = 0;
    public float minDistance = 0.5f;
    public float maxDistance = 3f;
    public float minSize = 0.5f;
    public float maxSize = 3f;

    private Vector2 iniPos;
    private Vector2 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        float distance = Random.Range(minDistance, maxDistance);
        float size = Random.Range(minSize, maxSize);
        // change the objects scale by multiplying it with the size
        transform.localScale = transform.localScale * size;
        iniPos = transform.position;
        targetPos = iniPos + new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * distance;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        float fraction = timeToLive / 2f;

        if (timeAlive >= timeToLive)
        {
            Destroy(gameObject);
        }
        else if (timeAlive > fraction)
        {
            text.color = Color.Lerp(text.color, Color.clear, (timeAlive - fraction) / (timeToLive - fraction));
        }
        transform.position = Vector2.Lerp(iniPos, targetPos, Mathf.Sin(timeAlive / timeToLive));
        transform.localScale = Vector2.Lerp(Vector2.one, Vector2.zero, Mathf.Sin(timeAlive / timeToLive));
    }
    public void SetDamageText(int damage)
    {
        text.text = "-" + damage.ToString();
    }
}
