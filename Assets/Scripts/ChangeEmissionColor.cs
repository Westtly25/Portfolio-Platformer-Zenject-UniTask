using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChangeEmissionColor : MonoBehaviour
    {
        [ColorUsage(true, true)] [SerializeField]
        private Color color;

        private SpriteRenderer sprite;
        private static readonly int EmissionColor = Shader.PropertyToID("_Color");

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        [ContextMenu("Change color")]
        public void Change() =>
            sprite.material.SetColor(EmissionColor, color);
    }
}