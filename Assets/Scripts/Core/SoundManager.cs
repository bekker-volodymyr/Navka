using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Legacy MB will need
    //public static SoundManager instance;

    //[SerializeField] private AudioSource audioSourcePrefab;

    //private void Awake()
    //{
    //    if(instance == null) instance = this;
    //    else
    //    {
    //        Destroy(this);
    //        return;
    //    }
    //}

    //public void PlaySoundClip(AudioClip clip, Transform spawnTransform, float volume)
    //{
    //    AudioSource source = Instantiate(audioSourcePrefab, spawnTransform);

    //    source.clip = clip;
    //    source.volume = volume;
    //    source.Play();
    //    float clipLength = source.clip.length;
    //    Destroy(source, clipLength);
    //}

    //public void PlayRandomSoundClip(List<AudioClip> clips, Transform spawnTransform, float volume)
    //{
    //    int index = Random.Range(0, clips.Count);

    //    AudioSource source = Instantiate(audioSourcePrefab, spawnTransform);

    //    source.clip = clips[index];
    //    source.volume = volume;
    //    source.Play();
    //    float clipLength = source.clip.length;
    //    Destroy(source, clipLength);
    //}
    #endregion

    public void OnMasterVolumeChanged(float volume)
    {

    }

    public void OnSfxVolumeChanged(float volume) 
    {
        
    }

    public void OnMusicVolumeChanged(float volume)
    {

    }
}
