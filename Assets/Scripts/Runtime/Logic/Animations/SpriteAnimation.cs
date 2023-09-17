using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField, Range(1, 30)]
        private int frameRate = 10;
        [SerializeField]
        private UnityEvent<string> onComplete;
        [SerializeField]
        private AnimationClip[] clips;

        private SpriteRenderer renderer;

        private float secPerFrame;
        private float nextFrameTime;
        private int currentFrame;
        private bool isPlaying = true;

        private int currentClip;

        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            secPerFrame = 1f / frameRate;

            StartAnimation();
        }

        private void OnBecameVisible() =>
            enabled = isPlaying;

        private void OnBecameInvisible() =>
            enabled = false;

        public void SetClip(string clipName)
        {
            for (var i = 0; i < clips.Length; i++)
            {
                if (clips[i].Name == clipName)
                {
                    currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = isPlaying = false;
        }

        private void StartAnimation()
        {
            nextFrameTime = Time.time;
            enabled = isPlaying = true;
            currentFrame = 0;
        }

        private void OnEnable()
        {
            nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (nextFrameTime > Time.time) return;

            var clip = clips[currentClip];
            if (currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    currentFrame = 0;
                }
                else
                {
                    enabled = isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        currentFrame = 0;
                        currentClip = (int) Mathf.Repeat(currentClip + 1, clips.Length);
                    }
                }

                return;
            }

            renderer.sprite = clip.Sprites[currentFrame];

            nextFrameTime += secPerFrame;
            currentFrame++;
        }
    }
}