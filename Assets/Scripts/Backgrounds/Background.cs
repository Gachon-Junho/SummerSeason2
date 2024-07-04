using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Backgrounds
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isRightMove;
        [SerializeField] private SpriteRenderer[] bg;

        private const float bg_width = 20.48f;

        private void Start()
        {
            StartCoroutine(updateBackground());
        }

        private void Update()
        {
        }

        private IEnumerator updateBackground()
        {
            while (true)
            {
                var outOfScreen = bg.Where(b => b.transform.position.x < -21).FirstOrDefault();
                
                if (outOfScreen != null)
                    outOfScreen.transform.position = new Vector3(bg.Max(b => b.transform.position.x) + bg_width, 0, 0);
                
                for (int i = 0; i < bg.Length; i++)
                {
                    bg[i].transform.Translate(isRightMove ? 1 : -1 * speed, 0, 0);
                    bg[i].color = Color.HSVToRGB(GameStateCache.Round / 100f, 0.5f, 0.9f);
                }

                yield return null;
            }
        }
    }
}