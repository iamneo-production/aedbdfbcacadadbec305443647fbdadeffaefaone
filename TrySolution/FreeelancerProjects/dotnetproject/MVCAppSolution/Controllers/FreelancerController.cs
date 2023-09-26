using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Services;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    public class FreelancerController : Controller
    {
        private readonly IFreelancerService _FreelancerService;

        public FreelancerController(IFreelancerService FreelancerService)
        {
            _FreelancerService = FreelancerService;

        }

        public IActionResult AddFreelancer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFreelancer(Freelancer freelancer)
        {
            try
            {
                if (freelancer == null)
                {
                    return BadRequest("Invalid Freelancer data");
                }

                var success = _FreelancerService.AddFreelancer(freelancer);

                if (success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Failed to add the Freelancer. Please try again.");
                return View(freelancer);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error response or another appropriate response
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                return View(freelancer);
            }
        }

        public IActionResult Index()
        {
            try
            {
                var listFreelancers = _FreelancerService.GetAllFreelancers();
                return View("Index",listFreelancers);
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }

       public IActionResult Delete(int id)
        {
            try
            {
                var success = _FreelancerService.DeleteFreelancer(id);

                if (success)
                {
                    // Check if the deletion was successful and return a view or a redirect
                    return RedirectToAction("Index"); // Redirect to the list of Freelancers, for example
                }
                else
                {
                    // Handle other error cases
                    return View("Error"); // Assuming you have an "Error" view
                }
            }
            catch (Exception ex)
            {
                // Log or print the exception to get more details
                Console.WriteLine("Exception: " + ex.Message);

                // Return an error view or another appropriate response
                return View("Error"); // Assuming you have an "Error" view
            }
        }
    }
}
