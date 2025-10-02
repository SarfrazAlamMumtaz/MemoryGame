using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    public class SoundSystem : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource sfxSource;

        [SerializeField] private AudioClip flipSound;
        [SerializeField] private AudioClip matchSound;
        [SerializeField] private AudioClip mismatchSound;
        [SerializeField] private AudioClip gameOverSound;

        public void PlayFlip()
        {
            PlaySound(flipSound);
        }

        public void PlayMatch()
        {
            PlaySound(matchSound);
        }

        public void PlayMismatch()
        {
            PlaySound(mismatchSound);
        }

        public void PlayGameOver()
        {
            PlaySound(gameOverSound);
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip);
        }

    }
}

