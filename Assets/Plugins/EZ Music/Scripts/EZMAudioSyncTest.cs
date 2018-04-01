using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZMusic
{
    public class EZMAudioSyncTest : MonoBehaviour
    {
        public AudioClip FirstSong;
        public AudioClip SecondSong;

        [Header("INFO")]
        public float TimeSamples;

        private EZMObjectPool<AudioSource> pool = new EZMObjectPool<AudioSource>();
        private AudioSource currentAudio;

        private void Start()
        {
            pool.Initialize(10, transform);

            currentAudio = InitializeElement(FirstSong, 0);
            currentAudio.Play();
        }

        private void FixedUpdate()
        {
            TimeSamples = currentAudio.timeSamples;
        }

        private void ChangeSong()
        {
            Debug.Log("Switching tracks...");
            AudioClip targetClip;

            targetClip = currentAudio.clip == FirstSong ? SecondSong : FirstSong;
            StartCoroutine(FadeMusic(currentAudio, 0f, 2f, () => {
                currentAudio.Stop();
                currentAudio.gameObject.name = "Pooled Object";
            }));

            var nextElement = InitializeElement(targetClip, currentAudio.timeSamples);
            nextElement.volume = 0f;
            nextElement.Play();
            StartCoroutine(FadeMusic(nextElement, 1f, 2f, () =>
            {
                pool.ReleaseElement(currentAudio);
                currentAudio = nextElement;
            }));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeSong();
            }
        }

        private AudioSource InitializeElement(AudioClip clip, int samples)
        {
            AudioSource a = pool.GetElement();
            a.clip = clip;
            a.timeSamples = samples;

            a.gameObject.name = clip.name;
            return a;
        }

        private IEnumerator FadeMusic(AudioSource audioSource, float targetVolume, float time, System.Action fadeDoneAction = null)
        {
            float currentVolume = audioSource.volume;
            audioSource.gameObject.name += "(Fading volume to '" + targetVolume + "')";
            for (float t = 0f; t < 1f; t += Time.deltaTime / time)
            {
                //TODO: Curve support
                float v = Mathf.Lerp(currentVolume, targetVolume, t);
                audioSource.volume = v;
                yield return null;
            }
            audioSource.volume = targetVolume;

            fadeDoneAction?.Invoke();
        }
    }
}
