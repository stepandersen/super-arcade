using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public StageAudioManager Manager;
    public float LifeSpan = 10f;
    public bool IsEnd = false;

    private void Start() {
      if(LifeSpan > 0)
        StartCoroutine(SelfDestruct());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
          Manager.TriggerAudio(IsEnd);
          Destroy(gameObject);
        }
    }

private IEnumerator SelfDestruct()
{
    yield return new WaitForSeconds(LifeSpan);
    Destroy(gameObject);
}
    
}