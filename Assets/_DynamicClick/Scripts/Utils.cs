using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Helpers
{
    /**
     * 常用工具类
     */
    public class Utils : MonoBehaviour
    {
        public static Utils initialized = null;

        private GameObject consoleText;

        private void Awake()
        {
            if (initialized == null)
            {
                initialized = this;
            }
            else if (initialized != this)
            {
                Destroy(initialized);
            }

        }

        /// <summary>
        /// 显示消息，并在1秒后自动关闭
        /// </summary>
        /// <param name="message"></param>
        public void _ShowAndroidToastMessage(string message)
        {
            _ShowAndroidToastMessage(message, true);
        }

        /// <summary>
        /// 显示消息，手动设置，1秒后是否自动关闭
        /// </summary>
        /// <param name="message"></param>
        /// <param name="autoHideStatus"></param>
        public void _ShowAndroidToastMessage(string message, bool autoHideStatus)
        {
            showGameObject(consoleText);
            consoleText.GetComponent<Text>().text = message;
            if (autoHideStatus)
            {
                StartCoroutine(autoHide(consoleText));
            }
        }

        public void _ShowAndroidToastMessage(string message, GameObject gameObject, bool autoHideStatus)
        {
            showGameObject(gameObject);
            gameObject.GetComponent<Text>().text = message;
            if (autoHideStatus)
            {
                StartCoroutine(autoHide(gameObject));
            }
        }

        /// <summary>
        /// 1秒后自动隐藏 GameObject
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        IEnumerator autoHide(GameObject gameObject)
        {
            yield return new WaitForSeconds(1);
            hideGameObject(gameObject);
        }

        /// <summary>
        /// 隐藏game object通用方法
        /// </summary>
        /// <param name="gameObject"></param>
        public void hideGameObject(GameObject gameObject)
        {
            if (gameObject.active)
                gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示GameObject通用方法
        /// </summary>
        /// <param name="gameObject"></param>
        public void showGameObject(GameObject gameObject)
        {
            if (!gameObject.active)
                gameObject.SetActive(true);
        }

        /// <summary>
        /// 显示GameObject，并且可以添加点击事件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="call"></param>
        public void showAndClickGameObject(GameObject gameObject, UnityAction call)
        {
            if (!gameObject.active)
            {
                gameObject.SetActive(true);
            }
            
            addClick(gameObject, call);
        }

        /// <summary>
        /// 隐藏关闭按钮
        /// </summary>
        /// <param name="closeButton"></param>
        public void hideCloseButton(GameObject closeButton)
        {
            closeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            Utils.initialized.hideGameObject(closeButton);
        }

        /// <summary>
        /// 赋予GameObject对象onClick事件托管
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="call"></param>
        public void addClick(GameObject gameObject, UnityAction call)
        {
            if (gameObject.GetComponent<Button>() == null)
            {
                gameObject.AddComponent<Button>();
            }

            gameObject.GetComponent<Button>().onClick.AddListener(call);
        }

        /// <summary>
        /// 设置GameObject是显示还是隐藏
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool setActive(GameObject gameObject, bool active)
        {
            if (gameObject.active != active)
            {
                gameObject.SetActive(active);
                return true;
            }

            return false;
        }

        /// <summary>
        /// GameObject添加Component的通用方法
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        public void addComponent<T>(GameObject gameObject) where T : Component
        {
            T t = gameObject.GetComponent<T>();
            if (t == null)
            {
                gameObject.AddComponent<T>();
            }
        }

        /// <summary>
        /// 根据gameObject和传入的参数解析，对象的子对象
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public GameObject parseGameObjectList(GameObject gameObject, params string[] names)
        {
            if (gameObject == null)
            {
                Debug.Log("gameObject is null.");
                return null;
            }

            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                int count = gameObject.transform.childCount;
                for (int j = 0; j < count; j++)
                {
                    string gameObjectName = gameObject.transform.GetChild(j).gameObject.name;
                    if (gameObjectName.Equals(name))
                    {
                        gameObject = gameObject.transform.GetChild(j).gameObject;
                        break;
                    }
                }
            }

            return gameObject;
        }

        /// <summary>
        /// 重写复制对象方法
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public GameObject instantiate(GameObject gameObject)
        {
            GameObject newGameObject = Instantiate(gameObject);
            newGameObject.name += newGameObject.GetHashCode();
            newGameObject.transform.parent = gameObject.transform.parent;
            newGameObject.transform.position = gameObject.transform.position;
            return newGameObject;
        }

        /// <summary>
        /// image reset 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="imageURL"></param>
        public void setImage(Image image, string imageURL)
        {
            StartCoroutine(loadImage(image, imageURL));
        }

        IEnumerator loadImage(Image image, string imageURL)
        {
            WWW www = new WWW(imageURL);
            yield return www;
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }

        /// <summary>
        /// 设置文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public void setText(Text text, string value)
        {
            text.text = value;
        }
        
        public float x = 0, y = 0, z = 0;
        public int count = 0, startNum = 0;

        /// <summary>
        /// 评论零星算法初始化，刷新时也可以调用此方法
        /// </summary>
        public void fragmentaryCommentInit()
        {
            x = 0;
            y = 0;
            z = 0;
            count = 0;
        }
        /// <summary>
        /// 评论零星算法,计算位置
        /// </summary>
        /// <returns></returns>
        public Vector3 fragmentaryComment()
        {
            if (count != 0)
            {
                float randomResult = Random.Range(startNum, 2);
                if (randomResult == 0) //X
                {
                    x++;
                    startNum = 1;
                }
                else //Y
                {
                    y++;
                    //如果位置下移，水平距离，向左或者向右移动0.5个单位
                    if (Random.Range(0, 2) == 0)
                    {
                        x += 0.5f;
                    }
                    else
                    {
                        x -= 0.5f;
                    }

                    startNum = 0;
                }

                /*else //Z
                {
                    z++;
                }*/
            }

            count++;
            return Vector3.right * 90 * x + Vector3.forward * 50 * z + Vector3.down * 25 * y + Vector3.forward * 100;
        }
        
    }
}