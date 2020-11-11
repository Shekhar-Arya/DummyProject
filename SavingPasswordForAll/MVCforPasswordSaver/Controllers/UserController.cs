using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVCforPasswordSaver.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace MVCforPasswordSaver.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44386/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        static List<User> user = new List<User>(); 
        // GET: User
        [HttpGet]
        public new ActionResult View()
        {
            //HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User").Result;
            return View(user);
        }

        // GET: User/Details/5
        /*public new ActionResult View(String name)
        {
            User i = null;
            foreach (var item in user)
            {
                if (item.name == name)
                {
                    i.name = item.name;
                    break;
                }

            }
            List<User> u = new List<User>();
            u.Add(i);
            return View(u);
        }*/

        // GET: User/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User u)
        {
            User a = new User();
            UserPassword p = new UserPassword();
            a.name = u.name;
            p.website = "www";
            p.username = "sss";
            p.password = "123";
            a.passwordSaver.Add(p);
            user.Add(a);
            
            return RedirectToAction("View");
            
        }

        // GET: User/Edit/5
        [HttpGet]
        public ActionResult Edit(String name)
        {
            User u = new User();
            foreach (var item in user)
            {
                if(item.name == name)
                {
                    u = item;
                    user.Remove(item);
                    break;
                }
            }
            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit( User u)
        {

            user.Add(u);
             return RedirectToAction("View");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
