using UnityEngine;
using UnityEditor;

namespace CPX.Utilities {
	public class CopyPathEditor: MonoBehaviour {

		[MenuItem("Assets/Copy Path")]
		static void CopySomething() {
			if(!(Selection.activeObject is SceneAsset)) {
				string path = System.IO.Path.GetFullPath(AssetDatabase.GetAssetPath(Selection.activeObject));
				EditorGUIUtility.systemCopyBuffer = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);
			}
		}
	}
}
