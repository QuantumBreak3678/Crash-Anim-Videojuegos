using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuperPunch : MonoBehaviour
{
    [SerializeField] private ParticleSystem superPunch;
    [SerializeField] private ParticleSystem NucleoVFX;
    [SerializeField] private VisualEffect finalLaser;
    public void PlayPunch()
    {
        superPunch.Play();
    }
    public void PlayLaser()
    {
        finalLaser.Play();
        NucleoVFX.Play();
    }
}