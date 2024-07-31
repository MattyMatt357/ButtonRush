using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelTransition : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

       /* public void LevelChange(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        } */

        public IEnumerator LevelChange(string scene, GameObject loadScreen, Slider loadingBar)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            loadScreen.SetActive(true);
            
            while (!operation.isDone)
            {
                float loadProgress = Mathf.Clamp01(operation.progress/0.9f);
                loadingBar.value = loadProgress;
                yield return null;
            }
           
        }
    }

