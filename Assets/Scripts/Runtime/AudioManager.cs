using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherCraftDefend {

	public class AudioManager : MonoBehaviour {

		private static List<AudioClip> audioClips;
		private static string audioSfXpath = "Audio/SFX/";

		private void Start() {
			audioClips = new List<AudioClip>();
			audioClips = UnityEngine.Resources.LoadAll<AudioClip>(audioSfXpath).ToList();
		}

		public void PlayAudioClip(string audioClipName, GameObject source) {
			AudioSource aSrc = source.AddComponent<AudioSource>();
			aSrc.PlayOneShot(GetAudioClip(audioClipName));
			StartCoroutine(RemoveAudioSource(aSrc));
		}

		private static AudioClip GetAudioClip(string audioClipName) {
			return audioClips.First(clip => clip.name == audioClipName);
		}

		private static IEnumerator RemoveAudioSource(AudioSource src) {
			yield return new WaitForSeconds(1);
			
			if(src != null)
				Destroy(src);
		}

	}

}