using UnityEngine;

namespace Sounds
{
    public class GlobalAudioSettings : MonoBehaviour
    {
        /// <summary>
        /// how many sounds or audio in game
        /// </summary>
        [SerializeField] private Sound[] AudioSources;
        
        private void Awake()
        {
            foreach (Sound s in AudioSources)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.playOnAwake = s._playOnStart;
            }
        }

        private void Start()
        {
            foreach (Sound s in AudioSources)
            {
                if(s._playOnStart) PlaySound(s.audioID);
            }
        }

        public void PlaySoundOnce(int ID)
        {
            Sound s = null;
            foreach (var sound in AudioSources)
            {
                if (sound.audioID == ID) s = sound;
            }
            if (s == null)
            {
                Debug.LogError("Sound " + ID + "Not Found");
                return;
            }
            if (!s.source.isPlaying)
            {
                s.source.PlayOneShot(s.clip);
            }
        
        }
    
        public void PlaySound(int ID)
        {
            Sound s = null;
            foreach (var sound in AudioSources)
            {
                if (sound.audioID == ID) s = sound;
            }
            if (s == null)
            {
                Debug.LogError("Sound " + ID + "Not Found");
                return;
            }
            s.source.Play();
        }

        public void StopPlayingSound()
        {
            foreach (var s in AudioSources)
            {
                s.source.Stop();
            }
        }
    }
}
