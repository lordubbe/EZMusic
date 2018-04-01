using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZMusic
{
    public class EZMMusicTrackTreeViewItem : EZMTreeViewItem
    {
        public override Texture2D icon
        {
            get
            {
                return EditorGUIUtility.FindTexture("PlayButton");
            }
            set
            {
                base.icon = value;
            }
        }

        public override Type[] GetChildOptions()
        {
            return null;
        }
    }
}
