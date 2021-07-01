using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private float time;
    public Elevator open;
    private bool hasOpened;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
        if (time >= 5.0f && !hasOpened)
        {
            hasOpened = true; 
            open.DoorsOpening();
            Debug.Log("open");
        }
    }
}
