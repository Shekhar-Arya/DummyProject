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
        public static int temp = 0;

        //string connectionString = @"Data Source=.;Initial Catalog=Employee;User ID=postgres;Password=shekhar";
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From users", connection))
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
        public ActionResult Details(int uid)
        {
            temp = uid;
            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From employee where uid = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", uid);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dbEmp);
                    connection.Close();
                }


            }
            return View(dbEmp);
        }

        [HttpGet]
        // GET: User/Create
        public ActionResult Create()
        {
            return View(new Users());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(Users u)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Insert Into users (name) Values(@Name)", connection))
                {
                    cmd.Parameters.AddWithValue("@Name",u.name);

                    cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();
                    
                }

            }
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int uid)
        {
            Users user = new Users();
            DataTable dt = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select * From users Where uid = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", uid);
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
                user.uid = Convert.ToInt32(dt.Rows[0][0].ToString());
                user.name = dt.Rows[0][1].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Users user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Update users Set name = @Name Where uid = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", user.uid);
                    cmd.Parameters.AddWithValue("@Name", user.name);
                    cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();

                }

            }

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int uid)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Delete from users where uid = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", uid);

                    cmd.ExecuteNonQuery();

                }

            }
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Delete from employee where uid = @Id", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", uid);

                    cmd.ExecuteNonQuery();

                }

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        // GET: User/Create
        public ActionResult Add()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Add(User u)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Insert Into employee (uid,website,username,password) Values(@Id,@Website,@Username,@Password)", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", temp);
                    cmd.Parameters.AddWithValue("@Website", u.website);
                    cmd.Parameters.AddWithValue("@Username", u.username);
                    cmd.Parameters.AddWithValue("@Password", u.password);

                    cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();

                }

            }
            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From employee where uid = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", temp);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dbEmp);
                    connection.Close();
                }
            }
            return View("Details",dbEmp);

        }

        // GET: User/Edit/5
        public ActionResult Change(int id)
        {
            User user = new User();
            DataTable dt = new DataTable();
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select * From employee Where id = @Id AND uid = @Uid", connection))
                {

                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Uid", temp);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dt);
                    //cmd.ExecuteNonQuery();
                    //cmd.Prepare();
                    //cmd.ExecuteScalar();

                }

            }
            if (dt.Rows.Count == 1)
            {
                user.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                user.website = dt.Rows[0][2].ToString();
                user.username = dt.Rows[0][3].ToString();
                user.password = dt.Rows[0][4].ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Change(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Update employee Set website = @Website, username = @Username, password = @Password Where id = @Id And uid = @Uid", connection))
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

            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From employee where uid = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", temp);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dbEmp);
                    connection.Close();
                }
            }
            return View("Details", dbEmp);
        }

        // GET: User/Delete/5
        public ActionResult Remove(int id)
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
            DataTable dbEmp = new DataTable();
            using (var connection = new NpgsqlConnection("User ID=postgres;Password=shekhar;Host=localhost;Database=Employee;Port=5432"))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("Select*From employee where uid = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", temp);
                    cmd.Prepare();
                    NpgsqlDataAdapter Da = new NpgsqlDataAdapter(cmd);
                    Da.Fill(dbEmp);
                    connection.Close();
                }
            }
            return View("Details", dbEmp);
        }
    }
    
}
