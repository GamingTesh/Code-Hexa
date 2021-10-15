using System.Collections.Generic;
using System;
using UnityEngine;

namespace ambiens.archtoolkit.atmaterials.texturestructure
{
    [Serializable]
    public class JsonSources
    {
        public List<TextureSource> sources = new List<TextureSource>();
    }
    [Serializable]
    public class TextureSource
    {
        public string name = "";
        public string img;
        public List<Categories> categories = new List<Categories>();
    }
    [Serializable]
    public class Categories
    {
        public string url;
        public string name;

        public List<ATMaterialItem> materials = new List<ATMaterialItem>();

    }
    [Serializable]
    public class ATMaterialItem
    {
        public string url;
        public string img;
        public string name;
        public List<string> labels = new List<string>();
        
        public List<DownloadsItem> downloads = new List<DownloadsItem>();
    }


    [Serializable]
    public class DownloadsItem
    {
        public string type;
        public string url;
        public List<string> set;
        public string localPath;
        public Material materialLoaded;
    }
}
