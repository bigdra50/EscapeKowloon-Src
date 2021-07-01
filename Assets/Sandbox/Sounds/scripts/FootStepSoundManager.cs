using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

public class FootStepSoundManager : MonoBehaviour
{
    public List<AudioClip> FootStepsClip = new List<AudioClip>();
    private AudioSource audioSource;

    [SerializeField] private float stride;
    [SerializeField] private float randomPitchRnage;
    private float walkingDistance;

    private Vector3 originPosition;
    private int clipListSize;
    private int clipListOrder;
    
    // Start is called before the first frame update
    void Start()
    {
        if (stride <= 0f) stride = 0.4f;
        walkingDistance = 0;
        originPosition = this.gameObject.transform.position;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        clipListSize = FootStepsClip.Count;
        clipListOrder=0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = this.gameObject.transform.position;
        if (newPosition.x != originPosition.x || newPosition.z != originPosition.z)
        {
            walkingDistance += math.sqrt(math.pow(newPosition.x - originPosition.x, 2) + math.pow(newPosition.z - originPosition.z, 2));
        }
        if (walkingDistance >= stride)
        {
            PlayFootStepSound(FootStepsClip, randomPitchRnage);
            walkingDistance = 0;
        }
        originPosition = newPosition;
    }

    void PlayFootStepSound(List<AudioClip> audioClipList, float randomRange)
    {
        audioSource.clip = FootStepsClip[clipListOrder];

        new SePitchRandomize().PitchRandomize(audioSource, randomRange);

        audioSource.Play();
        if (clipListOrder < clipListSize-1)
        {
            clipListOrder++;
        }
        else
        {
            clipListOrder = 0;
        }
    }
}
