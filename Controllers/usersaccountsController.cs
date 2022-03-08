#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Data;
using BookStoreProject.Models;
using Microsoft.Data.SqlClient;

namespace BookStoreProject.Controllers
{
    public class usersaccountsController : Controller
    {
        private readonly BookStoreProjectContext _context;
        

        public usersaccountsController(BookStoreProjectContext context)
        {
            _context = context;
        }

        // GET: usersaccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.usersaccounts.ToListAsync());
        }


        // GET: usersaccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usersaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,email,fullName,region")] usersaccounts usersaccounts)
        {
          usersaccounts.role = "customer"; 
          _context.Add(usersaccounts);
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(login));
          
        }





            // GET: usersaccounts/Edit/5
            public async Task<IActionResult> Edit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            return RedirectToAction(nameof(I));


            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _context.book.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

        }

        // POST: usersaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,email,fullName,region")] usersaccounts usersaccounts)
        {
            if (id != usersaccounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersaccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersaccountsExists(usersaccounts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usersaccounts);
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Lenovo\\OneDrive\\Documents\\bookStoredb.mdf; Integrated Security = True; Connect Timeout = 30");
            string sql;
            sql = "SELECT * FROM usersaccounts where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
          
                string id = Convert.ToString((int)reader["Id"]);
                string fullName = (String)reader["fullName"];
                string region = (String)reader["region"];
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("userid", id);
              
                HttpContext.Session.SetString("fullName", fullName);
                HttpContext.Session.SetString("region",region);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("catalogue", "books");

                else
                    return RedirectToAction("Index", "books");

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }




        private bool usersaccountsExists(int id)
        {
            return _context.usersaccounts.Any(e => e.Id == id);
        }
    }
}
