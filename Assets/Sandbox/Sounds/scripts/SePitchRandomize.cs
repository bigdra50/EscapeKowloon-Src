using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SePitchRandomize : MonoBehaviour
{

    public void PitchRandomize(AudioSource audioSource, float randomRange)
    {
        if (randomRange >= 3) randomRange = 3f;
        if (audioSource.clip != null)
        {
            audioSource.pitch = Random.Range(1f - randomRange, 1f + randomRange);
        }
    }
}
