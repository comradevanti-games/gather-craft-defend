using UnityEngine;
using UnityEngine.SceneManagement;

namespace GatherCraftDefend
{

    public class LoadScene : MonoBehaviour
    {

        public void WithName(string sceneName) =>
            SceneManager.LoadScene(sceneName);

    }

}