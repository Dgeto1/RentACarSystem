using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class UserCarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserCars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserCar.Include(u => u.Car).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserCars/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCar = await _context.UserCar
                .Include(u => u.Car)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userCar == null)
            {
                return NotFound();
            }

            return View(userCar);
        }

        // GET: UserCars/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CarId,UserName,CarBrand,CarModel,RentDate,ReturnDate")] UserCar userCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", userCar.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCar.UserId);
            return View(userCar);
        }

        // GET: UserCars/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCar = await _context.UserCar.FindAsync(id);
            if (userCar == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", userCar.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCar.UserId);
            return View(userCar);
        }

        // POST: UserCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,CarId,UserName,CarBrand,CarModel,RentDate,ReturnDate")] UserCar userCar)
        {
            if (id != userCar.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCarExists(userCar.UserId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", userCar.CarId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCar.UserId);
            return View(userCar);
        }

        // GET: UserCars/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCar = await _context.UserCar
                .Include(u => u.Car)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userCar == null)
            {
                return NotFound();
            }

            return View(userCar);
        }

        // POST: UserCars/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userCar = await _context.UserCar.FindAsync(id);
            _context.UserCar.Remove(userCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCarExists(string id)
        {
            return _context.UserCar.Any(e => e.UserId == id);
        }
    }
}
