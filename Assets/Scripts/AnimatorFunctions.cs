using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    
    [SerializeField] private AudioClip punchAudio;
    [SerializeField] private AudioClip stepAudio;
    [SerializeField] private ParticleSystem dustParticleSystem;

    public void PlayPunch(float volume)
    {
        GameManager.Instance.PlaySFX(punchAudio, volume);
    }

    public void PlayStep(float volume)
    {
        GameManager.Instance.PlaySFX(stepAudio, volume);
    }

    public void EmitDustStep()
    {
        dustParticleSystem.Emit(10);
    }

}
