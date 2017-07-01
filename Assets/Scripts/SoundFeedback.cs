using UnityEngine;
using System.Collections;

public class SoundFeedback : MonoBehaviour
{

	public GameObject musicSpeaker;
	public GameObject sfxSpeaker;

	AudioSource musicSource;
	AudioSource sfxSource;
	AudioSource footstepSource;

	public AudioClip music1;
	public AudioClip warpSound;

	public float distortAmount;
	public float warpVolume;
	public float warpPitchMin;
	public float warpPitchMax;

    GameObject player;
    float volume;
    const float bottomOfCatacombs = -9;
    //float volumeYPosDifferential;
    bool fadeOutMusic;

	void Start ()
	{
		musicSource = musicSpeaker.GetComponent<AudioSource> ();
		sfxSource = sfxSpeaker.GetComponent<AudioSource> ();

		musicSpeaker.GetComponent<AudioDistortionFilter> ().distortionLevel = 0f;

        player = GameObject.FindGameObjectWithTag("Player");
        volume = musicSource.volume;
        //volumeYPosDifferential = volume - player.transform.position.y;
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
			Vwoop ();
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			AddMusicLayer1 ();
		}

		if (Input.GetKeyDown(KeyCode.I))
		{
			musicSpeaker.GetComponent<AudioDistortionFilter> ().distortionLevel = 0;
		}

        float volumeRatio = 1 / musicSource.volume;
        float distanceRatio = player.transform.position.y / bottomOfCatacombs;

        volume = Mathf.Abs((volumeRatio / distanceRatio) / 100);

        if (player.transform.position.y < -1 && volume < 1)
        {
            if (player.transform.position.y < -9 && !fadeOutMusic) StartCoroutine(FadeMusic());
            else musicSource.volume = Mathf.Lerp(musicSource.volume, volume, Time.deltaTime);
        }
	}

	public void Vwoop()
	{
		sfxSource.pitch = (Random.Range (warpPitchMin, warpPitchMax));
		sfxSource.PlayOneShot (warpSound, warpVolume);
	}

	void AddMusicLayer1()
	{
		musicSpeaker.GetComponent<AudioDistortionFilter> ().distortionLevel = distortAmount;
	}

    IEnumerator FadeMusic()
    {
        print("music should fade out");

        fadeOutMusic = true;

        float timer = 3;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        musicSource.volume = 0;
    }

}
