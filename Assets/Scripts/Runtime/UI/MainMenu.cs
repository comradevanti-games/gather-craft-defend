using UnityEngine;

namespace GatherCraftDefend.UI
{
    public class MainMenu : MonoBehaviour {

        [SerializeField] private GameObject tutorialPanel;

        public void ToggleTutorialPanel() {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }

    }
}
