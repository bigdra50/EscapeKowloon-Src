using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    public new GameObject obj;
    [SerializeField] float intensity;
    [SerializeField] float delay;
    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.localPosition = generateRandomV3();
    }

    Vector3 generateRandomV3()
    {
        float timeForNose = (Time.time - delay) * speed;
        return new Vector3(Mathf.PerlinNoise(0, timeForNose)*intensity, Mathf.PerlinNoise(timeForNose, 0)*intensity, 0);
    }
}
