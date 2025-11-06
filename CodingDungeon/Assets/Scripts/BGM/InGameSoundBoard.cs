using System.Collections.Generic;
using UnityEngine;



public class InGameSoundBoard : MonoBehaviour
{
    public List<AudioClip> clips;

    public void SFXPlay(int i)
    {
        SoundManager.Instance.SFXPlay("SFX", clips[i]);
    }
}
