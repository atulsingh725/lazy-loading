using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using test_app.Models;

namespace test_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int? page)
        {
            page=page ?? 0;
            var _conString = "Data Source=DESKTOP-KPGRRF1\\MSSQLSERVER01;Integrated Security=True;Initial Catalog=test;";
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd =new SqlCommand("sp_pegination", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@page",page),
                new SqlParameter("@record",20)
            };
            cmd.Parameters.AddRange(param);
            SqlDataAdapter ada=new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            ada.Fill(dt);
            return View(dt);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}