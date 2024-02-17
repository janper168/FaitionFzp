using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator
{
    public static class HightLineUtils
    {

        public static Hashtable keywords = new Hashtable();

        static HightLineUtils()
        {
            keywords.Add("using", "1");
            keywords.Add("public", "1");
            keywords.Add("private", "1");
            keywords.Add("namespace", "1");
            keywords.Add("class", "1");
            keywords.Add("set", "1");
            keywords.Add("get", "1");
            keywords.Add("void", "1");
            keywords.Add("int", "1");
            keywords.Add("string", "1");
            keywords.Add("float", "1");
            keywords.Add("double", "1");
            keywords.Add("for", "1");
            keywords.Add("foreach", "1");
            keywords.Add("in", "1");
            keywords.Add("object", "1");
            keywords.Add("if", "1");
            keywords.Add("else", "1");
            keywords.Add("while", "1");
            keywords.Add("do", "1");
            keywords.Add("partial", "1");
            keywords.Add("switch", "1");
            keywords.Add("case", "1");
            keywords.Add("default", "1");
            keywords.Add("continue", "1");
            keywords.Add("break", "1");
            keywords.Add("return", "1");
            keywords.Add("new", "1");
            keywords.Add("bool", "1");
            keywords.Add("null", "1");
            keywords.Add("false", "1");
            keywords.Add("true", "1");
            keywords.Add("catch", "1");
            keywords.Add("finally", "1");
            keywords.Add("try", "1");
            keywords.Add("enum", "1");
            keywords.Add("extern", "1");
            keywords.Add("inline", "1");
            keywords.Add("char", "1");
            keywords.Add("byte", "1");
            keywords.Add("const", "1");
            keywords.Add("long", "1");
            keywords.Add("protected", "1");
            keywords.Add("short", "1");
            keywords.Add("signed", "1");
            keywords.Add("unsigned", "1");
            keywords.Add("struct", "1");
            keywords.Add("static", "1");
            keywords.Add("this", "1");
            keywords.Add("throw", "1");
            keywords.Add("union", "1");
            keywords.Add("virtual", "1");
            keywords.Add("abstract", "1");
            keywords.Add("event", "1");
            keywords.Add("as", "1");
            keywords.Add("explicit", "1");
            keywords.Add("base", "1");
            keywords.Add("operator", "1");
            keywords.Add("out", "1");
            keywords.Add("params", "1");
            keywords.Add("typeof", "1");
            keywords.Add("uint", "1");
            keywords.Add("ulong", "1");
            keywords.Add("checked", "1");
            keywords.Add("goto", "1");
            keywords.Add("unchecked", "1");
            keywords.Add("readonly", "1");
            keywords.Add("unsafe", "1");
            keywords.Add("implicit", "1");
            keywords.Add("ref", "1");
            keywords.Add("ushort", "1");
            keywords.Add("decimal", "1");
            keywords.Add("sbyte", "1");
            keywords.Add("interface", "1");
            keywords.Add("sealed", "1");
            keywords.Add("volatile", "1");
            keywords.Add("delegate", "1");
            keywords.Add("internal", "1");
            keywords.Add("is", "1");
            keywords.Add("sizeof", "1");
            keywords.Add("lock", "1");
            keywords.Add("stackalloc", "1");
            keywords.Add("var", "1");
            keywords.Add("value", "1");
            keywords.Add("yield", "1");
        }
        public static void HightLineCSharpCdoe(RichTextBox textBox)
        {
            int start = 0;
            int end = textBox.Text.Length;

            foreach (string strKey in keywords.Keys)
            {

                int index = textBox.Find(strKey, start, end, RichTextBoxFinds.WholeWord);
                while (index != -1)
                {
                    textBox.SelectionFont = new Font(textBox.SelectionFont, FontStyle.Bold);
                    textBox.SelectionColor = Color.Blue;
                    start = index + strKey.Length;
                    index = textBox.Find(strKey, start, end, RichTextBoxFinds.WholeWord);
                }
                start = 0;
                end = textBox.Text.Length;
            }
        }

    }
}
