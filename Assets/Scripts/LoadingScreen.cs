using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image _imageToFill;

        [SerializeField] private float _fillDuration;

        private void Start()
        {
            // Set the initial fill value to 0
            _imageToFill.fillAmount = 0f;

            // Use DOTween to animate the fill value to 1 over the specified duration
            _imageToFill.DOFillAmount(1f, _fillDuration)
                .SetEase(Ease.Linear) // You can choose different easing options
                .OnComplete(() =>
                {
                    Debug.Log("Fill animation completed");
                    SceneManager.LoadScene("MainMenu");
                }); // Optional callback on completion
        }
    }
}