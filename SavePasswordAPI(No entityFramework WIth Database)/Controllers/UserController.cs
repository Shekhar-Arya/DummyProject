using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using SavePasswordAPI_No_entityFramework_WIth_Database_.Models;

namespace SavePasswordAPI_No_entityFramework_WIth_Database_.Controllers
{
    public class UserController : Controller
    {

        //string connectionString = @"Data Source=.;Initial Catalog=Employee;User ID=postgres;Password=shekhar";
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From Employee", connection))
                {
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dbEmp);
                    connection.Close();
                }


            }
            return View(dbEmp);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        // GET: User/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Insert Into employee (website,username,password) Values(@Website,@Username,@Password)", connection))
                {
                    
                    //cmd.Parameters.AddWithValue("@Id", user.id);
                    cmd.Parameters.AddWithValue("@Website", user.website);
                    cmd.Parameters.AddWithValue("@Username", user.username);
                    cmd.Parameters.AddWithValue("@Password", user.password);

                    cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();
                    
                }

            }
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User();
            DataTable dt = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select * From employee Where id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dt);
                    //cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();

                }

            }
            if (dt.Rows.Count==1)
            {
                user.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                user.website = dt.Rows[0][1].ToString();
                user.username = dt.Rows[0][2].ToString();
                user.password = dt.Rows[0][3].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Update employee Set website = @Website, username = @Username, password = @Password Where id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", user.id);
                    cmd.Parameters.AddWithValue("@Website", user.website);
                    cmd.Parameters.AddWithValue("@Username", user.username);
                    cmd.Parameters.AddWithValue("@Password", user.password);

                    cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();

                }

            }

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Delete from employee where id = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();

                }

            }
            return RedirectToAction("Index");
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
