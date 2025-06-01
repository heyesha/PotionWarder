using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    public AudioClip[] splashSounds;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (splashSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, splashSounds.Length);
                audioSource.PlayOneShot(splashSounds[randomIndex]);
            }
        }
    }
}
