using UnityEngine;

namespace _Lights.Hands
{
    public class HandTiger : Appps
    {
        public void InitializeRiverOceanWater()
        {
            UniWebView.SetAllowInlinePlay(true);

            var rivvver = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var water in rivvver)
            {
                water.Stop();
            }

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}