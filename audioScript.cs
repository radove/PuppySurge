using UnityEngine;
using System.Collections;

public class audioScript : MonoBehaviour
{
	public AudioClip[] soundtrack;
    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
		if (!audioSource.playOnAwake)
		{
            audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audioSource.Play();
        }
        StartCoroutine(CheckMusic());
    }

    IEnumerator CheckMusic()
    {
        while (true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundtrack[Random.Range(0, soundtrack.Length)];
                audioSource.Play();
            }
            yield return StartCoroutine(Wait(2f));
        }
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}