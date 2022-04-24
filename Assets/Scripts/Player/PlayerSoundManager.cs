using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    public AudioClip primarySfx;
    public AudioClip secondarySfx;

    Dictionary<AttackData.BasicAttackType, AudioClip> GetSfXLookup() {
        return new Dictionary<AttackData.BasicAttackType, AudioClip> {
            { AttackData.BasicAttackType.Kick, secondarySfx },
            { AttackData.BasicAttackType.LeftPunch, secondarySfx },
            { AttackData.BasicAttackType.RightPunch, secondarySfx },
            { AttackData.BasicAttackType.DoublePunch, primarySfx },
            { AttackData.BasicAttackType.Super, primarySfx }
        };
    }

    public void PlaySound(AttackData.BasicAttackType attackType) {
        AudioClip clip;
        bool found = GetSfXLookup().TryGetValue(attackType, out clip);
        if (found) {
            AudioSource source = GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
		}
	}
}
