using System.Collections.Generic;
using ComradeVanti.CSharpTools;
using TMPro;
using UnityEngine;

namespace GatherCraftDefend.UI
{

    public class GameOverReasonDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI label;

        private Dictionary<GameOverReason, string> endMessages = new Dictionary<GameOverReason, string> {
            {GameOverReason.StashLost, "You lost your stash!"},
            {GameOverReason.Killed, "You got killed by the zombies!"}
        };


        private void Awake() => DisplayReason();

        private void DisplayReason()
        {
            var message = LoadScene.Parameters.TryGetInt("reason")
                                   .Map(i => (GameOverReason)i)
                                   .Map(reason => endMessages[reason])
                                   .DefaultValue("You died for some reason");
            label.text = message;
        }

    }

}