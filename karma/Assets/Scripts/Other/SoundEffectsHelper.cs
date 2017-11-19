using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Création d'effets sonores en toute simplicité
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
    public AudioClip buttonSelectedSound;
    public AudioClip buttonSelectSound;
    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public void MakeButtonSelectedSound()
    {
        MakeSound(buttonSelectedSound);
    }

    public void MakeButtonSelectSound()
    {
        MakeSound(buttonSelectSound);
    }



    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundEffectsHelper!");
        }
        Instance = this;
    }
    

    /// <summary>
    /// Lance la lecture d'un son
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        if(originalClip)
            AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }

    public void MakeSound2(AudioClip originalClip, float volume)
    {
        if (originalClip)
            AudioSource.PlayClipAtPoint(originalClip, transform.position, volume);
    }
}
