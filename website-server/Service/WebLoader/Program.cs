
using DataManager;
using Model.Server;
using Model.Server.Args;
using OpenQA.Selenium.Chrome;
using System;

namespace WebLoader
{
    class Program
    {
        static ServerDataManager serverDataManager = new ServerDataManager("http://localhost:5000/");
        static void Main(string[] args)
        {
            var startUrl = "http://news.baidu.com/";
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--headless");//开启无gui模式
            op.AddArguments("--no-sandbox");//停用沙箱以在Linux中正常运行
            ChromeDriver cd = new ChromeDriver(Environment.CurrentDirectory, op, TimeSpan.FromSeconds(180));
            cd.Navigate().GoToUrl(startUrl);
            var elements = cd.FindElementById("pane-news").FindElements(OpenQA.Selenium.By.TagName("a"));
            foreach (var e in elements)
            {
                string link = e.GetProperty("href");
                string title = e.Text;
                Console.WriteLine(link + " " + title);
                var result = serverDataManager.News.GetModel(new NewsGetModelRequest() { Link = link });
                if (result.Code == ServerResponseType.空数据)
                {
                    serverDataManager.News.Add(new Model.Server.Models.NewsModel() { Title = title, Link = link });
                    Console.WriteLine("插入新数据");
                }
            }
            cd.Quit();
            Console.Read();
        }
    }
}
