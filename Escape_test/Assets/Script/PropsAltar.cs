using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;
    public GameObject chiken;
    public bool gameEnd = false;

        private Color curColor;
        private Color targetColor;
        private float timeAfter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player") {
            gameEnd = true;

            targetColor = new Color(1, 1, 1, 1);

        }
    }
    private void Start()
    {
        timeAfter = 0;
    }

    private void Update()
        {
        if(gameEnd)
            timeAfter += Time.deltaTime;
        if (timeAfter > 3)
            chiken.SetActive(true);

        curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
