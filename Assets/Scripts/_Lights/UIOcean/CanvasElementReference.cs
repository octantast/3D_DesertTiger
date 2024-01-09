using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Lights.UIOcean
{
    public class CanvasElementReference : MonoBehaviour
    {
        private Transform textTransform;

        public void ShowAndScale()
        {
            CanvasHelper.FadeCanvasGroup(gameObject, true);
            ScaleElement();
        }

        private void ScaleElement()
        {
            textTransform.localScale = new Vector3(1, 0.8f, 1);
            textTransform.DOScale(Vector3.one, 0.5f);
        }

        private void Awake()
        {
            textTransform = GetComponentInChildren<TMP_Text>().transform;
        }

        private void DisableCanvas()
        {
            CanvasHelper.FadeCanvasGroup(gameObject, false);
            FindObjectOfType<CanvasHelper>().ActivateNextCanvasElement();
        }
    }
}