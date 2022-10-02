using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherCraftDefend {

	public class AudioManager : MonoBehaviour {

		private static AudioSource audioSource;
		private static List<AudioClip> audioClips;
		private static string audioSfXpath = "Audio/SFX/";

		private void Start() {
			audioSource = GetComponent<AudioSource>();
			audioClips = new List<AudioClip>();
			audioClips = UnityEngine.Resources.LoadAll<AudioClip>(audioSfXpath).ToList();

		}

		public static void PlayAudioClip(string audioClipName) {
			audioSource.PlayOneShot(GetAudioClip(audioClipName));
		}

		private static AudioClip GetAudioClip(string audioClipName) {
			return audioClips.First(clip => clip.name == audioClipName);
		}

	}

}