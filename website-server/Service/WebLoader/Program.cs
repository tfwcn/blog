
using Common;
using DataManager;
using Model.Server;
using Model.Server.Args;
using Model.Server.Models;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace WebLoader
{
    class Program
    {
        static ServerDataManager serverDataManager = new ServerDataManager("http://localhost:5000/");
        /// <summary>
        /// 线程列表
        /// </summary>
        static List<Thread> threads = new List<Thread>();

        static void Main(string[] args)
        {
            var resultWebLoader = serverDataManager.WebLoader.GetList(new WebLoaderGetListRequest());
            if (resultWebLoader.Code == ServerResponseType.成功)
            {
                foreach (var item in resultWebLoader.Data.DataList)
                {
                    Thread tmpThread = new Thread((o) =>
                      {
                          var tmpWebLoader = o as WebLoaderModel;
                          var startUrl = tmpWebLoader.Url;
                          ChromeOptions op = new ChromeOptions();
                          op.AddArguments("--headless");//开启无gui模式
                          op.AddArguments("--no-sandbox");//停用沙箱以在Linux中正常运行
                          ChromeDriver cd = new ChromeDriver(Environment.CurrentDirectory, op, TimeSpan.FromSeconds(180));
                          try
                          {
                              while (true)
                              {
                                  try
                                  {
                                      cd.Navigate().GoToUrl(startUrl);
                                      //var resultScript = cd.ExecuteScript(tmpWebLoader.Javascript);
                                      var resultScript = cd.ExecuteScript("let aList=document.querySelectorAll('#pane-news a');let tmplist=[];for(a in aList){ tmplist.push({Link: aList[a].href,Title:aList[a].innerText}); }return tmplist;");
                                      var modelList = JsonHelper.CloneObject<List<NewsModel>>(resultScript);
                                      foreach (var m in modelList)
                                      {
                                          if (m.Link == null || m.Title == null || m.Title.Length < 4)
                                              continue;
                                          Console.WriteLine(m.Link + " " + m.Title);
                                          var result = serverDataManager.News.GetModel(new NewsGetModelRequest() { Link = m.Link });
                                          if (result.Code == ServerResponseType.空数据)
                                          {
                                              serverDataManager.News.Add(m);
                                              Console.WriteLine("插入新数据");
                                          }
                                      }
                                      //var elements = cd.FindElementById("pane-news").FindElements(OpenQA.Selenium.By.TagName("a"));
                                      //foreach (var e in elements)
                                      //{
                                      //    string link = e.GetProperty("href");
                                      //    string title = e.Text;
                                      //    Console.WriteLine(link + " " + title);
                                      //    var result = serverDataManager.News.GetModel(new NewsGetModelRequest() { Link = link });
                                      //    if (result.Code == ServerResponseType.空数据)
                                      //    {
                                      //        serverDataManager.News.Add(new Model.Server.Models.NewsModel() { Title = title, Link = link });
                                      //        Console.WriteLine("插入新数据");
                                      //    }
                                      //}
                                      Thread.Sleep(1000 * 60 * 15);
                                  }
                                  catch (ThreadAbortException)
                                  {
                                      break;
                                  }
                                  catch (Exception ex)
                                  {
                                      Log.LogHelper.WriteErrorLog(typeof(Program), ex);
                                  }
                              }
                          }
                          catch (Exception ex)
                          {
                              Log.LogHelper.WriteErrorLog(typeof(Program), ex);
                          }
                          finally
                          {
                              cd.Quit();
                          }
                      });
                    tmpThread.IsBackground = true;
                    tmpThread.Start(item);
                    threads.Add(tmpThread);
                }
            }
            while (true)
            {
                int stopNum = 0;
                for (int i = 0; i < threads.Count; i++)
                {
                    if (threads[i].ThreadState == ThreadState.Aborted || threads[i].ThreadState == ThreadState.Stopped)
                    {
                        stopNum++;
                    }
                }
                //全部线程停止，则退出服务
                if (stopNum == threads.Count)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
