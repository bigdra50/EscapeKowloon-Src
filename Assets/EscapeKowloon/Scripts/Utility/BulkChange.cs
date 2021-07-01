using UnityEngine;
 
#if UNITY_EDITOR
 using System.IO;
 using UnityEditor;
#endif
 
public class BulkChange : MonoBehaviour
{
 
#if UNITY_EDITOR
    [Tooltip("アセット名 ※空の場合は全アセット対象です。")]
    public string AssetsName = "";
 
    // Max Size
    [Tooltip("32/64/128/256/512/1024/2048/4096/8192")]
    public int MaxSize = 512;
 
    // ファイル一覧を取得する(PNG/TIF/JPG)
    string[] getFileList(string path)
    {
        if (!Directory.Exists(path)) return new string[0];
 
        // PNG
        string[] png = System.IO.Directory.GetFiles(
            path, "*.png", System.IO.SearchOption.AllDirectories);
 
        // TIF
        string[] tif = System.IO.Directory.GetFiles(
            path, "*.tif", System.IO.SearchOption.AllDirectories);
 
        // TIFF
        string[] tiff = System.IO.Directory.GetFiles(
             path, "*.tiff", System.IO.SearchOption.AllDirectories);
 
        // JPG
        string[] jpg = System.IO.Directory.GetFiles(
             path, "*.jpg", System.IO.SearchOption.AllDirectories);
 
        // JPEG
        string[] jpeg = System.IO.Directory.GetFiles(
             path, "*.jpeg", System.IO.SearchOption.AllDirectories);
 
        System.Collections.ArrayList list1 = new System.Collections.ArrayList(png);
        System.Collections.ArrayList list2 = new System.Collections.ArrayList(tif);
        System.Collections.ArrayList list3 = new System.Collections.ArrayList(tiff);
        System.Collections.ArrayList list4 = new System.Collections.ArrayList(jpg);
        System.Collections.ArrayList list5 = new System.Collections.ArrayList(jpeg);
 
        list1.AddRange(list2);
        list1.AddRange(list3);
        list1.AddRange(list4);
        list1.AddRange(list5);
 
        string[] files = (string[])list1.ToArray(typeof(string));
 
        // ファイル名を整形する
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace("\\", "/");
            files[i] = files[i].Replace(Application.dataPath, "Assets");
        }
 
        return files;
    }
 
    [ContextMenu("MaxSizeの一覧をバックアップする。")]
    void Mehod1()
    {
        string[] files = getFileList(Application.dataPath + "/");
        if (files.Length == 0)
        {
            Debug.LogWarning("画像ファイルが見つかりませんでした。");
            return;
        }
 
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace("\\", "/");
            files[i] = files[i].Replace(Application.dataPath, "Assets");
 
            TextureImporter Ti = AssetImporter.GetAtPath(files[i]) as TextureImporter;
            files[i] = Ti.maxTextureSize + "," + "\"" + files[i] + "\"";
        }
 
        // Assets/backup_all.txtを作成する
        File.WriteAllText(Application.dataPath + "/backup_all.txt", string.Join("\n", files));
        Debug.Log("Asset/backup_all.txtを作成しました。");
 
        // エディタ側のファイル構成を更新する
        AssetDatabase.Refresh();
    }
 
    [ContextMenu("MaxSizeを一括変更する。")]
    void Method2()
    {
        switch (this.MaxSize) {
            case 32:;break;
            case 64:; break;
            case 128:; break;
            case 256:; break;
            case 512:; break;
            case 1024:; break;
            case 2048:; break;
            case 4096:; break;
            case 8192:; break;
            default:
                Debug.LogWarning("MaxSizeは32/64/128/256/512/1024/2048/4096/8192のいずれかを設定します。");
                return;
        }
 
        string[] files = getFileList(Application.dataPath + "/" + this.AssetsName);
        if (files.Length == 0)
        {
            Debug.LogWarning("画像ファイルが見つかりませんでした。アセット名を確認してください。");
            return;
        }
 
        for (int i = 0; i < files.Length; i++)
        {
            // MaxSizeを設定する
            // ※既にMaxSize以下の画像はそのままとする
            TextureImporter Ti = AssetImporter.GetAtPath(files[i]) as TextureImporter;
 
            if (Ti.maxTextureSize > this.MaxSize)
            {
                Ti.maxTextureSize = this.MaxSize;
 
                // 変更内容をプロジェクトに反映する ※この処理は数分かかります。                
                // (注意)この3種のメソッドの使い方は正しくないかも知れません。
                // (注意)とりあえず、MaxSizeを下げる分には動いているのでこのままにしておきます。
                EditorUtility.SetDirty(Ti);
                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(files[i], ImportAssetOptions.ForceUpdate);
            }
        }
 
        Debug.Log("一括変更を行いました。");
    }
#endif
 
}
