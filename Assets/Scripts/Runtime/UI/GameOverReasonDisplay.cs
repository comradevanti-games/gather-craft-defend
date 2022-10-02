using ComradeVanti.CSharpTools;
using Dev.ComradeVanti.EnumDict;
using TMPro;
using UnityEngine;

namespace GatherCraftDefend.UI
{

    public class GameOverReasonDisplay : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private EnumDict<GameOverReason, string> messages;


        private void Awake() => DisplayReason();

        private void DisplayReason()
        {
            var message = LoadScene.Parameters.TryGetInt("reason")
                                   .Map(i => (GameOverReason)i)
                                   .Map(reason => messages[reason])
                                   .DefaultValue("You died for some reason");
            label.text = message;
        }

    }

}