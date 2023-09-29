using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
    private AudioSource[] sources;
    private int audioLevel = 0;
    
    private void Awake()
    {
        sources = GetComponents<AudioSource>();
    }

    public void TriggerAudio(bool isEnd)
    {
      var source = sources[audioLevel];

      if(!isEnd) {
        StartCoroutine(Fade(source, 2f, 1f));
        audioLevel++;
      } else {
        for(var i = 0; i < sources.Length - 1; i++) {
          StartCoroutine(Fade(sources[i], 1.5f, 0f));
        }
        source.Play();
      }
    }
    public IEnumerator Fade(AudioSource audioSource, float duration = 2f, float targetVolume = 0.5f)
    {
        float currentTime = 0f;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}