﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData.Document;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LuaSTGEditorSharp.EditorData
{
    public static class EditorSerializer
    {
        public static readonly JsonSerializerSettings DefaultSettings =
            new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };

        public static string SerializeTreeNode(object o)
        {
            return JsonConvert.SerializeObject(o, typeof(TreeNode), DefaultSettings);
        }

        public static object DeserializeTreeNode(string s)
        {
            return JsonConvert.DeserializeObject(s, typeof(TreeNode), DefaultSettings);
        }

        public static string SerializeMetaData(object o)
        {
            return JsonConvert.SerializeObject(o, typeof(AbstractMetaData), DefaultSettings);
        }

        public static object DeserializeMetaData(string s)
        {
            return JsonConvert.DeserializeObject(s, typeof(AbstractMetaData), DefaultSettings);
        }
    }
}