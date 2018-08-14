<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Attribute
    {
        public String tag;
        public String value;
        public Attribute(String t, String v)
        {
            tag = t;
            value = v;
        }

        public String GetTagCode()
        {
            if (tag == "family")
            {
                return "1";
            }
            else if (tag == "person")
            {
                return "2";
            }
            else if (tag == "firstName")
            {
                return "3";
            }
            else if (tag == "lastName")
            {
                return "4";
            }
            else if (tag == "state")
            {
                return "5";
            }
            return "--";
        }
    }

    public class Element
    {
        public List<Attribute> attributes;
        public List<Element> children;
        public String name;
        public String value;

        public Element(String n)
        {
            name = n;
            attributes = new List<Attribute>();
            children = new List<Element>();
        }

        public Element(String n, String v)
        {
            name = n;
            value = v;
            attributes = new List<Attribute>();
            children = new List<Element>();
        }

        public String GetNameCode()
        {
            if (name == "family")
            {
                return "1";
            }
            else if (name == "person")
            {
                return "2";
            }
            else if (name == "firstName")
            {
                return "3";
            }
            else if (name == "lastName")
            {
                return "4";
            }
            else if (name == "state")
            {
                return "5";
            }
            return "--";
        }

        public void Insert(Attribute attribute)
        {
            attributes.Add(attribute);
        }

        public void Insert(Element child)
        {
            children.Add(child);
        }
    }

    public static void Encode(String v, StringBuilder sb)
    {
        v = v.Replace("0", "\\0");
        sb.Append(v);
        sb.Append(" ");
    }

    public static void EncodeEnd(StringBuilder sb)
    {
        sb.Append("0");
        sb.Append(" ");
    }

    public static void Encode(Attribute attr, StringBuilder sb)
    {
        Encode(attr.GetTagCode(), sb);
        Encode(attr.value, sb);
    }

    public static void Encode(Element root, StringBuilder sb)
    {
        Encode(root.GetNameCode(), sb);
        foreach (Attribute a in root.attributes)
        {
            Encode(a, sb);
        }
        EncodeEnd(sb);
        if (root.value != null && root.value != "")
        {
            Encode(root.value, sb);
        }
        else
        {
            foreach (Element e in root.children)
            {
                Encode(e, sb);
            }
        }
        EncodeEnd(sb);
    }

    public static String EncodeToString(Element root)
    {
        StringBuilder sb = new StringBuilder();
        Encode(root, sb);
        return sb.ToString();
    }

    public static void Main(String[] args)
    {
        Element root = new Element("family");
        Attribute a1 = new Attribute("lastName", "mcdowell");
        Attribute a2 = new Attribute("state", "CA");
        root.Insert(a1);
        root.Insert(a2);

        Element child = new Element("person", "Some Message");
        Attribute a3 = new Attribute("firstName", "Gayle");
        child.Insert(a3);

        root.Insert(child);

        String s = EncodeToString(root);
        Console.WriteLine(s);
    }
}
