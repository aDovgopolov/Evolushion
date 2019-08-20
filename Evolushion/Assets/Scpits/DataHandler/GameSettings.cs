using System.Collections.Generic;
using System.Xml;
using UnityEngine;

//TODO: use dictinary instead of list
public static class GameSettings
{
    public static class general
    {
        public static class food
        {
            public static int start_count;
        }

        public static class field
        {
            public static int width;
            public static int height;
            public static int depth;
        }

        public static class gold
        {
            public static int start_count;
        }
    }

    public class meel
    {
        public string id;
        public string name;


        public meel _prev = null;
        public meel _next = null;

        public static List<meel> items = new List<meel>();

        public meel(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public static meel get(string id, bool showError = true)
        {
            int index = items.FindIndex(x => x.id == id);

            if (index == -1)
            {
                if (showError)
                    Debug.LogError("data::meel: key not found '" + id + "'");
                return null;
            }

            return items[index];
        }
    }

    public class unit
    {
        public string chance;
        public string icon;
        public int ad_x;
        public int gold_amount;
        public string id;


        public unit _prev = null;
        public unit _next = null;

        public static List<unit> items = new List<unit>();

        public unit(string chance, string icon, int ad_x, int gold_amount, string id)
        {
            this.chance = chance;
            this.icon = icon;
            this.ad_x = ad_x;
            this.gold_amount = gold_amount;
            this.id = id;
        }

        public static unit get(string id, bool showError = true)
        {
            int index = items.FindIndex(x => x.id == id);

            if (index == -1)
            {
                if (showError)
                    Debug.LogError("data::unit: key not found '" + id + "'");
                return null;
            }

            return items[index];
        }
    }


    private static XmlNode FindSubNode(XmlNode node, string id)
    {
        foreach (XmlNode item in node.ChildNodes)
        {
            if (item.Attributes["id"].Value == id)
                return item;
        }

        return null;
    }

    public static void init(string xmlfile)
    {
        TextAsset textAsset = (TextAsset) Resources.Load(xmlfile);
        XmlDocument xDoc = new XmlDocument();
        xDoc.LoadXml(textAsset.text);

        {
            XmlNode item = xDoc.DocumentElement.GetElementsByTagName("general")[0];


            general.food.start_count = 0;
            int.TryParse(FindSubNode(item, "food.start_count").Attributes["value"].Value, out general.food.start_count);
            general.field.width = 0;
            int.TryParse(FindSubNode(item, "field.width").Attributes["value"].Value, out general.field.width);
            general.field.height = 0;
            int.TryParse(FindSubNode(item, "field.height").Attributes["value"].Value, out general.field.height);
            general.field.depth = 0;
            int.TryParse(FindSubNode(item, "field.depth").Attributes["value"].Value, out general.field.depth);
            general.gold.start_count = 0;
            int.TryParse(FindSubNode(item, "gold.start_count").Attributes["value"].Value, out general.gold.start_count);
        }

        meel.items.Clear();
        XmlNode meel_node = xDoc.DocumentElement.GetElementsByTagName("meel")[0];

        meel meel_prev = null;

        foreach (XmlNode item in meel_node.ChildNodes)
        {
            string id = item.Attributes["id"].Value;
            string name = item.Attributes["name"].Value;

            if (meel.get(id, false) == null)
            {
                meel current = new meel(id, name);

                if (meel_prev != null)
                {
                    meel_prev._next = current;
                    current._prev = meel_prev;
                }

                meel_prev = current;
                meel.items.Add(current);
            }
            else
                Debug.LogError("data::meel: already contains key '" + id + "'");
        }

        unit.items.Clear();
        XmlNode unit_node = xDoc.DocumentElement.GetElementsByTagName("unit")[0];

        unit unit_prev = null;

        foreach (XmlNode item in unit_node.ChildNodes)
        {
            string chance = item.Attributes["chance"].Value;
            string icon = item.Attributes["icon"].Value;
            int ad_x = 0;
            int.TryParse(item.Attributes["ad_x"].Value, out ad_x);
            int gold_amount = 0;
            int.TryParse(item.Attributes["gold_amount"].Value, out gold_amount);
            string id = item.Attributes["id"].Value;

            if (unit.get(id, false) == null)
            {
                unit current = new unit(chance, icon, ad_x, gold_amount, id);

                if (unit_prev != null)
                {
                    unit_prev._next = current;
                    current._prev = unit_prev;
                }

                unit_prev = current;
                unit.items.Add(current);
            }
            else
                Debug.LogError("data::unit: already contains key '" + id + "'");
        }
        
        GameManager.Instance.SettingsLoaded = true;
    }


    public static void free()
    {
        meel.items.Clear();
        unit.items.Clear();
    }
}