using System.Collections;
using System.Collections.Generic;
using _Lights.Hands;
using _Lights.UIOcean;
using AppsFlyerSDK;
using DefaultNamespace._Lights.DataBases;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;

namespace _Lights.RiversController
{
    public class RiverLight : MonoBehaviour
    {
        [SerializeField] private HandHand handHand;

        [SerializeField] private StarMoon _idfaCheck;

        [SerializeField] private MountainForest mountainForest;

        private bool OceanRiver = true;
        private NetworkReachability SkyRiver = NetworkReachability.NotReachable;

        private string ForestForest { get; set; }
        private string StarHand;
        private int StarRiver;

        private string TigerLight;

        [SerializeField] private DataBaseRivers _dataBaseRivers;

        private string LightRiver;

        private void Awake()
        {
            OceanHandInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _idfaCheck.ScrutinizeIDFA();
            StartCoroutine(LightMountainID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    ForestMoonConnection();
                    break;
                default:
                    StarTigerStorData();
                    break;
            }
        }

        private void OceanHandInstances()
        {
            switch (OceanRiver)
            {
                case true:
                    OceanRiver = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator LightMountainID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            TigerLight = _idfaCheck.RetrieveAdvertisingID();
            yield return null;
        }

        private void StarTigerStorData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                HandStarLoadStoredData();
            }
            else
            {
                SkyMoonWithDelay();
            }
        }

        private void HandStarLoadStoredData()
        {
            ForestForest = PlayerPrefs.GetString("top", string.Empty);
            StarHand = PlayerPrefs.GetString("top2", string.Empty);
            StarRiver = PlayerPrefs.GetInt("top3", 0);
            MoonMoonImp();
        }

        private void SkyMoonWithDelay()
        {
            Invoke(nameof(SkySkyReceData), 7.4f);
        }

        private void SkySkyReceData()
        {
            if (Application.internetReachability == SkyRiver)
            {
                ForestMoonConnection();
            }
            else
            {
                StartCoroutine(MoonStarFomSrv());
            }
        }


        private IEnumerator MoonStarFomSrv()
        {
            using UnityWebRequest webRequest =
                UnityWebRequest.Get(mountainForest.ConcatenateStrings(_dataBaseRivers.NameRiver));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                ForestMoonConnection();
            }
            else
            {
                TigerSkyResponse(webRequest);
            }
        }

        private void TigerSkyResponse(UnityWebRequest webRequest)
        {
            string tokenConcatenation = mountainForest.ConcatenateStrings(_dataBaseRivers.TokenRiver);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    ForestForest = dataParts[0];
                    StarHand = dataParts[1];
                    StarRiver = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    ForestForest = webRequest.downloadHandler.text;
                }

                MoonMoonImp();
            }
            else
            {
                ForestMoonConnection();
            }
        }

        private void MoonMoonImp()
        {
            handHand.OceanMoon = $"{ForestForest}?idfa={TigerLight}";
            handHand.OceanMoon +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            handHand.LightForest = StarHand;


            MoonHand();
        }

        public void MoonHand()
        {
            handHand.LightLight = StarRiver;
            handHand.gameObject.SetActive(true);
        }

        private void ForestMoonConnection()
        {
            DisableHandOcean();
        }

        private void DisableHandOcean()
        {
            CanvasHelper.FadeCanvasGroup(gameObject, false);
        }

        // Add the rest of your methods as needed...
    }
}