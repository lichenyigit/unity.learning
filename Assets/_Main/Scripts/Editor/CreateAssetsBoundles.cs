using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
* 将文件转为.assetbundle文件
* @author lichenyi
* 2018-08-09 14:36:58
*/
public class CreateAssets : MonoBehaviour
{
    [MenuItem("AssetBundle/CreateAssetBundles")]
    static void CreateAssetBundleThemelves()
    {
        //获取要打包的对象（在Project视图中选中的.obj或者.fbx对象）  
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string outPath = "Assets/_Main/AssetsBundles/";
        checkPath(outPath);
        //遍历选中的对象  
        foreach (Object obj in selects)
        {
            string targetPath = outPath + obj.name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android))
            {
                Debug.Log(obj.name + "is packed successfully!");
            }
            else
            {
                Debug.Log(obj.name + "is packed failly!");
            }
        }

        //刷新编辑器（不写的话要手动刷新,否则打包的资源不能及时在Project视图内显示）  
        AssetDatabase.Refresh();
    }

    private static void checkPath(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
            print("文件夹不存在,创建");
        }
    }
}