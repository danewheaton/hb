using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class GlassBounce : MonoBehaviour
{
	Transform m_Transform = null;
	Rigidbody m_Rigidbody = null;
	AudioSource m_Audio = null;
	Collider m_Collider = null;

	public List<AudioClip> m_BounceSounds = new List<AudioClip>();

	void Awake()
	{
		m_Transform = transform;
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Audio = GetComponent<AudioSource>();
		m_Collider = GetComponent<Collider>();
		m_Audio.playOnAwake = false;
		m_Audio.dopplerLevel = 0.0f;
	}

	void OnCollisionEnter(Collision collision)
	{
		
		// if collision velocity is sufficient, make a 'hard bounce'
		if (collision.relativeVelocity.magnitude > 2)
		{
			// also, we play a random bounce sound
			if (m_Audio != null && m_BounceSounds.Count > 0)
			{
//				m_Audio.pitch = Time.timeScale;
				m_Audio.PlayOneShot(m_BounceSounds[(int)Random.Range(0, (m_BounceSounds.Count))]);
			}

		}
			
	}
		
}


	