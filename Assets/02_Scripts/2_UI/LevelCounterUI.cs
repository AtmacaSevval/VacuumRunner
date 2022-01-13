using TMPro;
using Twenty.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Twenty.UI
{
    public class LevelCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLevelText;

        private void Start()
        {
            currentLevelText.text = "Level " + (SceneController.uiLevelIndex + 1);
        }
    }
}
