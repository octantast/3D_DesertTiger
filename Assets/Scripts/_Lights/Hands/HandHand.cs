using UnityEngine;

namespace _Lights.Hands
{
    public class HandHand : MonoBehaviour
    {
        public HandTiger _handTiger;

        public void OnEnable()
        {
            _handTiger.InitializeRiverOceanWater();
        }


        public string LightForest;

        public string OceanMoon
        {
            get => MoonLight;
            set => MoonLight = value;
        }

        [HideInInspector] public int LightLight = 70;

        private string MoonLight;
        private UniWebView RiverMountain;
        private GameObject RiverSky;

        private void Start()
        {
            LightSky();
            RiverForest(MoonLight);
            HideRiverOcean();
        }

        private void LightSky()
        {
            SkyOcean();

            switch (OceanMoon)
            {
                case "0":
                    RiverMountain.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    RiverMountain.SetShowToolbar(false);
                    break;
            }

            RiverMountain.Frame = new Rect(0, LightLight, Screen.width, Screen.height - LightLight);

            // Other setup logic...

            RiverMountain.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void SkyOcean()
        {
            RiverMountain = GetComponent<UniWebView>();
            if (RiverMountain == null)
            {
                RiverMountain = gameObject.AddComponent<UniWebView>();
            }

            RiverMountain.OnShouldClose += _ => false;

            // Other initialization logic...
        }

        private void RiverForest(string riverMountainSTR)
        {
            print((riverMountainSTR));
            if (!string.IsNullOrEmpty(riverMountainSTR))
            {
                RiverMountain.Load(riverMountainSTR);
            }
        }

        private void HideRiverOcean()
        {
            if (RiverSky != null)
            {
                RiverSky.SetActive(false);
            }
        }
    }
}