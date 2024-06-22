using BankSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankSolution.Controllers
{
    public class HomeController : Controller
    {
        readonly AccountDetailsModel _accountDetailsModel = new AccountDetailsModel
        {
            AccountNumber = 1001,
            AccountHolderName = "Example Name",
            CurrentBalance = 5000,

        };

        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank");
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            return Json(_accountDetailsModel);
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("docs.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber}")]
        public IActionResult GetCurrentBalanceOfAccountNumer()
        {
            if (Request.RouteValues["accountNumber"] == null)
            {
                return BadRequest("Invalid account number");
            }

            if (int.TryParse(Request.RouteValues["accountNumber"].ToString(), out int accountNumber))
            {
                if (_accountDetailsModel.AccountNumber == accountNumber)
                {
                    return Content(_accountDetailsModel.CurrentBalance.ToString());
                }

                return BadRequest("Account Number should be 1001");
            }

            return BadRequest("Invalid account number");
        }

        [Route("/get-current-balance/")]
        public IActionResult GetCurrentBalance()
        {
            return BadRequest("Account Number should be supplied");
        }
    }
}
