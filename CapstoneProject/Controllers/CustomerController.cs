﻿using Capstone.DataAccess.Data;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    public class CustomerController : Controller
    {
        // Access ApplicationDbContext which is the sql database
        private readonly ApplicationDbContext _database;
        public CustomerController(ApplicationDbContext database)
        {
            _database = database;
        }
        public IActionResult Index()
        {
            // Store list of all customers from table
            List<Customer> customerList = _database.Customers.ToList();
            return View(customerList);
        }
        // Action to go to new path/page, goes to Customer/Create URL path
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost] // The page "Create" uses a post method to pass information
        public IActionResult Create(Customer newCustomer)
        {
            if(ModelState.IsValid)
            {
                // This executes the insert SQL statement
                _database.Customers.Add(newCustomer);
                // Changes to database only applied after SaveChanges
                _database.SaveChanges();
                // TempData is a key-value pair that is useable on the next page render, only once
                TempData["success"] = "Customer created.";
                // Redirect back to Customer/Index, can also be used to redirect to another controller
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Returns object or null
            Customer? customer = _database.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost] 
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _database.Customers.Update(customer);
                _database.SaveChanges();
                TempData["success"] = "Customer updated.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Returns object or null
            Customer? customer = _database.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _database.Customers.Remove(customer);
            _database.SaveChanges();
            TempData["success"] = "Customer deleted.";
            return RedirectToAction("Index");
        }
    }
}
