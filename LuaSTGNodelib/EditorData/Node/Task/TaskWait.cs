﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Task
{
    [Serializable, NodeIcon("/LuaSTGNodeLib;component/images/16x16/taskwait.png")]
    [RequireAncestor(typeof(TaskNode), typeof(Data.Function), typeof(Tasker))]
    [LeafNode]
    [RCInvoke(0)]
    public class TaskWait : TreeNode
    {
        [JsonConstructor]
        private TaskWait() : base() { }

        public TaskWait(DocumentData workSpaceData)
            : this(workSpaceData, "60") { }

        public TaskWait(DocumentData workSpaceData, string code)
            : base(workSpaceData) { attributes.Add(new AttrItem("Time", this) { AttrInput = code }); }

        public override IEnumerable<string> ToLua(int spacing)
        {
            string sp = "".PadLeft(spacing * 4);
            yield return sp + "task._Wait(" + Macrolize(0) + ")\n";
        }
        
        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
        }

        public override string ToString()
        {
            return "Wait " + attributes[0].AttrInput + " frame(s)";
        }

        public override object Clone()
        {
            var n = new TaskWait(parentWorkSpace);
            n.DeepCopyFrom(this);
            return n;
        }
    }
}
