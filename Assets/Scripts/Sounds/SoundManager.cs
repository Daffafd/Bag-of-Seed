using PrimeTween;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgm;
        
        [SerializeField]
        private AudioSource[] _audioSources;

        [SerializeField]
        private SfxPool _sfxPool;
        public static SoundManager Instance { get; set; }

      
        private float _bgmVolume = 1, _sfxVolume = 1;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            
            }
            else
            {
                Instance = this;
            }
        }


     
        public void SetBGMVolume(float volume)
        {
            _bgmVolume = volume;

            _bgm.volume = volume;

            _bgm.mute = volume == 0;
        }
        
        public void SetSfxVolume(float volume)
        {
            _sfxVolume = volume;
        }
        
        public AudioSource PlaySfx(string id, Vector2 location, float volume = 1, float pitch = 1, float range = 25)
        {
            foreach (var audioSource in _audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.maxDistance = range;
                    audioSource.transform.position = location;
                    audioSource.clip = _sfxPool.GetFromPool(id);
                    audioSource.pitch = pitch;
                    audioSource.PlayOneShot(audioSource.clip, Mathf.Min(volume, _sfxVolume));
                    
                    //Debug.Log($"Playing SFX: {id}");

                    return audioSource;
                }
            }

            return null;
        }
        
        public AudioSource PlaySfxLoop(string id, int loopTime, Vector2 location, float volume = 1, float pitch = 1, float range = 25)
        {
            foreach (var audioSource in _audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.maxDistance = range;
                    audioSource.transform.position = location;
                    audioSource.clip = _sfxPool.GetFromPool(id);
                    audioSource.pitch = pitch;
                    audioSource.PlayOneShot(audioSource.clip, Mathf.Min(volume, _sfxVolume));

                    if (loopTime > 0)
                    {
                        Tween.Delay(audioSource.clip.length,
                            () => { PlaySfxLoop(id, loopTime - 1, location, volume, pitch, range); });
                    }

                    return audioSource;
                }
            }

            return null;
        }
 
    }
}

