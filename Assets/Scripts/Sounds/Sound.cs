using UnityEngine;

namespace Sounds
{
    /// <summary>
    /// One line to give the library's name and an idea of what it does
    /// </summary>

    [System.Serializable]
    public class Sound
    {
        /// <summary>
        /// name of the audio
        /// </summary>
        public string audioName;
        
        /// <summary>
        /// audio ID clip
        /// </summary>
        public int audioID;

        /// <summary>
        /// the clip of the audio 
        /// </summary>
        public AudioClip clip;

        /// <summary>
        /// audio volume
        /// </summary>
        [Range(0f, 1f)] public float volume;
        
        /// <summary>
        /// audio pitch
        /// </summary>
        [Range(.1f, 3f)] public float pitch;

        /// <summary>
        /// should the audio clip looping 
        /// </summary>
        public bool loop;

        public bool _playOnStart;
    
        /// <summary>
        /// audio sources
        /// </summary>
        private AudioSource Source;

        public AudioSource source
        {
            get => Source;
            set => Source = value;
        }
    }
}
