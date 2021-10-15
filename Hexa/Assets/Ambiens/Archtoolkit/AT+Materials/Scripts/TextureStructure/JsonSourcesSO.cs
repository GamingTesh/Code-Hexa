using System.Collections.Generic;
using UnityEngine;

namespace ambiens.archtoolkit.atmaterials.texturestructure
{
    [System.Serializable]
    public class JsonSourcesSO : ScriptableObject
    {
        public JsonSources jsonSources;

        public List<ATMaterialItem> Search(string search)
        {
            if (jsonSources == null) return new List<ATMaterialItem>();
            if (search.Length > 2)
            {
                var toreturn = new List<ATMaterialItem>();

                search = search.ToLower();

                foreach(var s in this.jsonSources.sources)
                {
                    foreach (var c in s.categories)
                    {
                        if (c.name.ToLower().Contains(search))
                        {
                            toreturn.AddRange(c.materials);
                        }
                        else
                        {
                            foreach (var m in c.materials)
                            {
                                foreach(var t in m.labels)
                                {
                                    if (t.ToLower().Contains(search)&&!toreturn.Contains(m))
                                    {
                                        toreturn.Add(m);
                                    }
                                }
                            }
                        }
                    }
                }
                return toreturn;

            }
            else return new List<ATMaterialItem>();

        }

    }
}