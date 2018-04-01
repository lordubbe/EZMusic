using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZMusic
{
    public class EZMMusicPieceTreeViewItem : EZMTreeViewItem
    {
        public EZMMusicPiece MusicPiece;

        public override Texture2D icon
        {
            get
            {
                return EditorGUIUtility.FindTexture("GameObject Icon");
            }
            set
            {
                base.icon = value;
            }
        }

        public override Type[] GetChildOptions()
        {
            return new Type[] { typeof(EZMMusicSegment) };
        }
    }
}
