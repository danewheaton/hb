using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] stepSounds;
    AudioSource source;

    public float pitchMin;
    public float pitchMax;

    float volume;
    const float bottomOfCatacombs = -9;
    float volumeYPosDifferential;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0;
    }

    void Update()
    {
        if ((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) && !source.isPlaying)
        {
            source.clip = stepSounds[Random.Range(0, stepSounds.Length)];
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.Play();
        }

        float volumeRatio = 1 / source.volume;
        float distanceRatio = transform.position.y / bottomOfCatacombs;

        volume = Mathf.Abs((volumeRatio * distanceRatio) / 100);

        if (transform.position.y < -1 && volume > 0)
        {
            source.volume = Mathf.Lerp(source.volume, Mathf.Abs(transform.position.y) / 20, Time.deltaTime);
        }
    }
}
