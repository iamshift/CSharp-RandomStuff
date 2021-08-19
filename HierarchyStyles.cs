using UnityEditor;
using UnityEngine;

namespace JoPires
{
    [InitializeOnLoad]
    public static class HierarchyStyles
    {
        private static GUIStyle BlackText;
        private static GUIStyle BlackTextInactive;
        private static GUIStyle WhiteText;
        private static GUIStyle WhiteTextInactive;

        private static GUIStyle TextColor;

        static HierarchyStyles()
        {
            EditorApplication.hierarchyWindowItemOnGUI += ApplyStyles;
        }

        private static void initStyle()
        {
            BlackText = new GUIStyle("PreOverlayLabel") {normal = {textColor = Color.black}};
            BlackTextInactive = new GUIStyle("PreOverlayLabel") {normal = {textColor = new Color(0, 0, 0, .5F)}};
            WhiteText = new GUIStyle("PreOverlayLabel") {normal = {textColor = Color.white}};
            WhiteTextInactive = new GUIStyle("PreOverlayLabel") {normal = {textColor = new Color(1, 1, 1, .5F)}};

            TextColor = new GUIStyle("PreOverlayLabel");
        }

        public static void ApplyStyles(int instanceID, Rect selectionRect)
        {
            if (BlackText is null) initStyle();

            if (!(EditorUtility.InstanceIDToObject(instanceID) is GameObject go)) return;
            if (!go.name.StartsWith("_")) return;

            var symbol = go.name.Substring(0, 2);
            var boxColor = GetColorFromSymbol(symbol);
            var name = go.name.Replace(symbol, "");

            if (name.StartsWith("_"))
            {
                symbol = name.Substring(0, 2);
                var nameColor = GetColorFromSymbol(symbol);
                TextColor.normal.textColor = new Color(nameColor.r, nameColor.g, nameColor.b,
                    go.activeInHierarchy ? 1.0F : 0.5F);

                name = name.Replace(symbol, "");
            }

            EditorGUI.DrawRect(selectionRect, boxColor);
            EditorGUI.LabelField(selectionRect, name.ToUpperInvariant(), TextColor);
        }

        private static Color GetColorFromSymbol(string symbol)
        {
            switch (symbol)
            {
                case "_w":
                    return Color.white;
                case "_b":
                    return Color.black;
                case "_c":
                    return Color.cyan;
                case "_g":
                    return Color.green;
                case "_m":
                    return Color.magenta;
                case "_r":
                    return Color.red;
                case "_y":
                    return Color.yellow;
                case "_B":
                    return Color.blue;
                case "_G":
                    return Color.gray;
                default:
                    return Color.clear;
            }
        }
    }
}
