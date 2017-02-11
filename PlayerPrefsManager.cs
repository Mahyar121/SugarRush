using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";

	public static void SetMasterVolume(float volume)
    {
        // allows the volume to range between 0.0 to 1.0
        if (volume >= 0f && volume <= 1f) { PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume); }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }


    
}
