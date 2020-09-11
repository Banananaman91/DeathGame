using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Cutscene
{
    public class CutsceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject _cutscenePanel;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private Text _text;
        [SerializeField] private String _winLoseText;
        [SerializeField] private Color _textColor;

        public void SetText()
        {
            _text.color = _textColor;
            _text.text = _winLoseText;
        }

        public void PlayVideo(VideoClip video)
        {
            _videoPlayer.clip = video;
            if (!_cutscenePanel.activeSelf) _cutscenePanel.SetActive(true);
            _videoPlayer.Play();
            StartCoroutine(Playing());
        }

        private IEnumerator Playing()
        {
            while (_videoPlayer.isPlaying)
            {
                yield return null;
            }
            SetText();
        }
    }
}
