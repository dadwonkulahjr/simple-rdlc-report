using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace EmployeeRDLCReport.WebUI.Pages.Admin.Employee.Report
{
    public class IndexModel : PageModel
    {
        private const string _connectionString = "Data Source=.;Initial Catalog=Simple1;Integrated Security=True";
        private readonly IWebHostEnvironment _webHostEnvironment;
        private DataTable _dt = new DataTable("MyTable");
        public IndexModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public void OnGet() { }


        public IActionResult OnPost()
        {
            //ConnectToSQLServerAndFetchData();
            string minType = "";
            string path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptEmployee.rdlc";
            int extention = 1;
            Dictionary<string, string> paramenter = new();
            paramenter.Add("rp1", "Hello, World!");

            LocalReport report = new(path);
            report.AddDataSource("dsEmployee", Employee.GetEmployees());
            var result = report.Execute(RenderType.Pdf, extention, paramenter, minType);
            return File(result.MainStream, "application/pdf");

        }



        private void ConnectToSQLServerAndFetchData()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * FROM Test", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                _dt.Columns.Add("Id");
                _dt.Columns.Add("Name");
                _dt.Columns.Add("Email");
                _dt.Columns.Add("Address");
                _dt.Columns.Add("Gender");

                DataRow dataRow;


                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        dataRow = _dt.NewRow();
                        dataRow["Id"] = reader["Id"];
                        dataRow["Name"] = reader["Name"];
                        dataRow["Email"] = reader["Email"];
                        dataRow["Address"] = reader["Address"];
                        dataRow["Gender"] = reader["Gender"];

                        _dt.Rows.Add(dataRow);
                    }
                }
                reader.Close();
                da.Fill(_dt);


            }
        }

       
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        internal static IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new()
                {
                    Id = 1,
                    Name = "iamtuse",
                    Email = "iamtuse@iamtuse.com",
                    Gender = "Male",
                    Address = "Caldwell"
                },
                new()
                {
                    Id = 2,
                    Name = "Leo Max",
                    Email = "leo@iamtuse.com",
                    Gender = "Male",
                    Address = "Old Road"
                },
                new()
                {
                    Id = 3,
                    Name = "Precious K Wonkulah",
                    Email = "wonkulap@iamtuse.com",
                    Gender = "Female",
                    Address = "Caldwell"
                },
            };
        }
    }
}
