namespace ChanYee.Util
{
    using UnityEngine;
    using UnityEngine.UI;
    using Object = UnityEngine.Object;

    public class BaseController<T> : MonoBehaviour where T : Object
    {
        /// <summary>
        /// 单例
        /// </summary>
        private static T _Instance;

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    var manipulationSystems = FindObjectsOfType<T>();
                    if (manipulationSystems.Length > 0)
                    {
                        _Instance = manipulationSystems[0];
                    }
                    else
                    {
                        Debug.LogError("No instance of ManipulationSystem exists in the scene.");
                    }
                }

                return _Instance;
            }
        }

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        protected GameObject parseGameObjectList(GameObject _gameObject, params string[] names)
        {
            GameObject _tempGameObject = _gameObject;
            int length = names.Length;
            if (_gameObject == null)
            {
                Debug.Log("gameObject is null.");
                return null;
            }

            for (int i = 0; i < length; i++)
            {
                string name = names[i];
                for (int j = 0; j < _tempGameObject.transform.childCount; j++)
                {
                    GameObject _tempG = _tempGameObject.transform.GetChild(j).gameObject;
                    string gameObjectName = _tempG.name;
                    if (gameObjectName.Equals(name))
                    {
                        _tempGameObject = _tempG;
                        break;
                    }
                }
            }

            if (_tempGameObject.name.Equals(names[length - 1]))
            {
                return _tempGameObject;
            }

            return null;
        }

        protected GameObject parseGameObjectListByStringArr(GameObject _gameObject, string[] names)
        {
            GameObject _tempGameObject = _gameObject;
            int length = names.Length;
            if (_gameObject == null)
            {
                Debug.Log("gameObject is null.");
                return null;
            }

            for (int i = 0; i < length; i++)
            {
                string name = names[i];
                for (int j = 0; j < _tempGameObject.transform.childCount; j++)
                {
                    GameObject _tempG = _tempGameObject.transform.GetChild(j).gameObject;
                    string gameObjectName = _tempG.name;
                    if (gameObjectName.Equals(name))
                    {
                        _tempGameObject = _tempG;
                        break;
                    }
                }
            }

            if (_tempGameObject.name.Equals(names[length - 1]))
            {
                return _tempGameObject;
            }

            return null;
        }
        
        /// <summary>
        /// 加载图片为Texture
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected Texture LoadTexture(string name)
        {
            string path = "Image/";
            Texture texture = Resources.Load<Texture>(path + name);
            return texture;
        }

        /// <summary>
        /// 加载图片为Sprite
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected Sprite LoadSprite(string name)
        {
            string path = "Sprite/";
            Sprite texture = Resources.Load<Sprite>(path + name);
            return texture;
        }

        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        protected void SetImage(Image image, string name)
        {
            Sprite _sprite = LoadSprite(name);
            image.sprite = _sprite;
        }

        /// <summary>
        /// text 设置文字
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="value"></param>
        protected void SetText(Text _text, string value)
        {
            _text.text = value;
        }

        /// <summary>
        /// 按钮设置文字
        /// </summary>
        /// <param name="button"></param>
        /// <param name="text"></param>
        protected void SetButtonText(GameObject button, string text)
        {
            GameObject _text = parseGameObjectList(button, "Text");
            _text.GetComponent<Text>().text = text;
        }

        protected void ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}