using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;

namespace WF_Parser.Core.Habr
{
    class HabrParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {

            var list = new List<string>();
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));
            //var images = document.GetElementsByTagName("img").Where(image => image.TagName!=null&&image.NodeValue!="facebook.com");     позже пофиксить
            // var images = document.QuerySelectorAll("img").Where(image => image.TagName!=null);
            foreach (var item in items)
            {
                list.Add(item.TextContent);
                list.Add(item.GetAttribute("href"));


            }
            
            return list.ToArray();
        }
    }
}
