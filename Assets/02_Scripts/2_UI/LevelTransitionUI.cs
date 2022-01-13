using Sirenix.OdinInspector;
using TMPro;
using Twenty.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Twenty.UI
{
    public class LevelTransitionUI : MonoBehaviour
    {
        [Header("Level End")]
        [SerializeField] private GameObject winPanel = null;
        [SerializeField] private GameObject losePanel = null;

        [Header("Buttons")]
        [Required("Button is required")][SerializeField] private Button nextButton = null;
        [Required("Button is required")][SerializeField] private Button retryButton = null;

        [Header("WinMultiplierText")]
        [SerializeField] private TextMeshProUGUI multiplierText;

        private void Awake()
        {
            if (nextButton != null)
                nextButton.onClick.AddListener(() => NextLevel());
            if (retryButton != null)
                retryButton.onClick.AddListener(() => Retry());
        }

        private void NextLevel()
        {
            StartCoroutine(SceneController.NextLevel());
            nextButton.interactable = false;
        }

        private void Retry()
        {
            StartCoroutine(SceneController.CurrentLevel());
            retryButton.interactable = false;
        }

        private void ShowWinUI()
        {
            winPanel.SetActive(true);
        }

        private void ShowLoseUI()
        {
            losePanel.SetActive(true);
        }


        private void OnEnable()
        {
            GameManager.onLevelSuccess += ShowWinUI;
            GameManager.onLevelFail += ShowLoseUI;
        }

        private void OnDisable()
        {
            GameManager.onLevelSuccess -= ShowWinUI;
            GameManager.onLevelFail -= ShowLoseUI;
        }
    }
}
