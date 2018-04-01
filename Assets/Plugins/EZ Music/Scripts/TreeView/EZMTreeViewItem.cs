using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

public abstract class EZMTreeViewItem : TreeViewItem
{
    public abstract System.Type[] GetChildOptions();
}