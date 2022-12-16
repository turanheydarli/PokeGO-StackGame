using Code.Scripts.Classes;
using UnityEngine;

namespace Code.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = ESDataManager.Instance.gameData.soundLevel;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
            }
        }

        private void Start()
        {
            Play("BackgroundMusic");
        }

        public void Play(string soundName)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
                return;
            //s.source.Play();
            // For completely play all sounds without cutting some last of sounds
            s.source.PlayOneShot(s.clip);
        }

        public void Stop(string soundName)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
                return;
            //s.source.Play();
            // For completely play all sounds without cutting some last of sounds
            s.source.Stop();
        }

        public void ChangeVolume(float volume)
        {
            foreach (var sound in sounds)
            {
                sound.source.volume = volume;
                ESDataManager.Instance.gameData.soundLevel = volume;
                ESDataManager.Instance.Save();
            }
        }
    }
}