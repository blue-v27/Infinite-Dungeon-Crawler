using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip dashSound, shootSound, hitSound, gameOverSound, enemyDamage, enemydes, skinSwitch;
    static AudioSource audioSrc;
    void Start()
    {
        dashSound = Resources.Load<AudioClip>("dashSound");
        shootSound = Resources.Load<AudioClip>("shootSound");
        hitSound = Resources.Load<AudioClip>("hiitSound");
        gameOverSound = Resources.Load<AudioClip>("gameOverSound");
        enemyDamage = Resources.Load<AudioClip>("enemyDamage");
        enemydes = Resources.Load<AudioClip>("enemydes");
        skinSwitch = Resources.Load<AudioClip>("powerUpSound");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
            switch (clip)
            {
                case "dashSound":
                    audioSrc.PlayOneShot(dashSound);
                    break;
                case "shootSound":
                    audioSrc.PlayOneShot(shootSound);
                    break;
                case "hiitSound":
                    audioSrc.PlayOneShot(hitSound);
                    break;
                case "gameOverSound":
                    audioSrc.PlayOneShot(gameOverSound);
                    break;
                case "enemyDamage":
                    audioSrc.PlayOneShot(enemyDamage);
                    break;
                case "enemydes":
                    audioSrc.PlayOneShot(enemydes);
                    break;
                case "skinWitch":
                    audioSrc.PlayOneShot(skinSwitch);
                    break;
        }
    }
}
