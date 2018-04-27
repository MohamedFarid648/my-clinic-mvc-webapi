using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TryClinic.Models;
using System.Web.Routing;
using Newtonsoft.Json.Converters;

namespace TryClinic.Controllers
{

    public class EmployeeWebAPIController : Controller
    {
        HttpClient client = new HttpClient();
        static List<EmployeeFromWebAPI> Employees = new List<EmployeeFromWebAPI>();
        static List<DepartmentFromWebAPI> Departments = new List<DepartmentFromWebAPI>();

        protected override void Initialize(RequestContext requestContext)
        {
            client.BaseAddress = new Uri("http://localhost:53643/");
            if(Departments!=null && Departments.Count == 0)
            {


                GetDepartments();
            }

            base.Initialize(requestContext);
        }

        // GET: EmployeeWebAPI
        public ActionResult Index(string Name, string Salary,string HireDate,string DepartmentId)
        {
            //client.BaseAddress = new Uri("http://localhost:53643/");

            

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.GetAsync("api/Emps/Emps").Result;//If Response is success

                if (result.IsSuccessStatusCode)
                {

                    var JsonData = result.Content.ReadAsStringAsync().Result;//Get Data as Json

                    JsonSerializerSettings JsonSettings = new JsonSerializerSettings();

                    JsonSettings.DateFormatString = "dd-MM-yyyy";

                    // var format = "dd-MM-yyyy"; // your datetime format
                    //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

                    Employees = JsonConvert.DeserializeObject<List<EmployeeFromWebAPI>>(JsonData, JsonSettings);//Deserilize it

                
            }
            DateTime d; decimal s; int i;

            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Salary) && !String.IsNullOrEmpty(HireDate) && !String.IsNullOrEmpty(DepartmentId))
            {


                if (DateTime.TryParse(HireDate, out d) && decimal.TryParse(Salary, out s) && Int32.TryParse(DepartmentId, out i))
                    Employees = Employees.Where(e => e.Name.Equals(Name) && e.Salary == s && e.DepartmentId == i).ToList();
                else
                    ViewBag.Message = $"{Salary} Should be number ,and {HireDate} Should be Date please  ^_^";

            }


            else if (!String.IsNullOrEmpty(Name))
            {
                Employees = Employees.Where(e => e.Name.Equals(Name)).ToList();
            }

            else if (!String.IsNullOrEmpty(HireDate))
            {


                if (DateTime.TryParse(HireDate, out d))
                    Employees = Employees.Where(e => e.HireDate.Equals(d)).ToList();

            }
            else if (!String.IsNullOrEmpty(DepartmentId))
            {
                if(int.TryParse(DepartmentId,out i))
                    Employees = Employees.Where(e => e.DepartmentId==i).ToList();
  

            }
            else if (!String.IsNullOrEmpty(Salary))
            {
                if (decimal.TryParse(Salary, out s))
                    Employees = Employees.Where(e => e.Salary == s).ToList();


            }

          
            else if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Salary))
            {
               
                if (decimal.TryParse(Salary, out s))
                    Employees = Employees.Where(e => e.Name.Equals(Name) && e.Salary==s).ToList();
                else
                    ViewBag.Message = $"{Salary} Should be number please  ^_^";

            }
           
           

            ViewBag.DepartmentId = new SelectList(Departments, "Id", "Name");


            


            return View(Employees);
        }

        // GET: EmployeeWebAPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeWebAPI/Create
        public ActionResult Create()
        {
                ViewBag.DepartmentId = new SelectList(Departments, "Id", "Name");

                return View();
        }

        private void GetDepartments()
        {


            //client.BaseAddress = new Uri("http://localhost:53643/");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //Get Response Result
            var responseResult = client.GetAsync("Depts/All").Result;
            if (responseResult.IsSuccessStatusCode)
            {

                var DeptsJson = responseResult.Content.ReadAsStringAsync().Result;
                /*
                 		DeptsJson	"[{\"$id\":\"1\",\"Employees\":[{\"$id\":\"2\",\"Department\":{\"$ref\":\"1\"},\"Id\":1,\"Name\":\"Ali\",\"Salary\":2000.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"3\",\"Department\":{\"$ref\":\"1\"},\"Id\":3,\"Name\":\"Mostafa\",\"Salary\":7000.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"4\",\"Department\":{\"$ref\":\"1\"},\"Id\":7,\"Name\":\"Mohamed\",\"Salary\":1000000.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"5\",\"Department\":{\"$ref\":\"1\"},\"Id\":8,\"Name\":\"Mohamed\",\"Salary\":1000000.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"6\",\"Department\":{\"$ref\":\"1\"},\"Id\":1002,\"Name\":\"Mohamed\",\"Salary\":458.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"7\",\"Department\":{\"$ref\":\"1\"},\"Id\":1003,\"Name\":\"Mohamed\",\"Salary\":458.00,\"HireDate\":null,\"DepartmentId\":1},{\"$id\":\"8\",\"Department\":{\"$ref\":\"1\"},\"Id\":1005,\"Name\":\"Abdullah\",\"Salary\":10024587.00,\"HireDate\":\"09-05-1950\",\"DepartmentId\":1}],\"Id\":1,\"Name\":\"SD\",\"Location\":\"Cairo\"},{\"$id\":\"9\",\"Employees\":[{\"$id\":\"10\",\"Department\":{\"$ref\":\"9\"},\"Id\":2,\"Name\":\"Ahmed\",\"Salary\":8000.00,\"HireDate\":null,\"DepartmentId\":2},{\"$id\":\"11\",\"Department\":{\"$ref\":\"9\"},\"Id\":4,\"Name\":\"Amr\",\"Salary\":7000.00,\"HireDate\":null,\"DepartmentId\":2},{\"$id\":\"12\",\"Department\":{\"$ref\":\"9\"},\"Id\":1004,\"Name\":\"Maged\",\"Salary\":5897.00,\"HireDate\":\"09-05-2000\",\"DepartmentId\":2}],\"Id\":2,\"Name\":\"Gaming\",\"Location\":\"Alex\"},{\"$id\":\"13\",\"Employees\":[{\"$id\":\"14\",\"Department\":{\"$ref\":\"13\"},\"Id\":5,\"Name\":\"Khalid\",\"Salary\":6000.00,\"HireDate\":null,\"DepartmentId\":3},{\"$id\":\"15\",\"Department\":{\"$ref\":\"13\"},\"Id\":6,\"Name\":\"Hussien\",\"Salary\":7000.00,\"HireDate\":null,\"DepartmentId\":3}],\"Id\":3,\"Name\":\"DataScience\",\"Location\":\"Mansoura\"}]"	string

                 */
                JsonSerializerSettings JsonSettings = new JsonSerializerSettings();

                JsonSettings.DateFormatString = "dd-MM-yyyy";
                Departments = JsonConvert.DeserializeObject<List<DepartmentFromWebAPI>>(DeptsJson, JsonSettings);
            }

        }
        // POST: EmployeeWebAPI/Create
        [HttpPost]
        public ActionResult Create(EmployeeFromWebAPI emp)//FormCollection collection)
        {
            /*
             
             
             
             Microsoft.AspNet.WebApi.Client
             This package adds support for formatting and content negotiation to System.Net.Http. It includes support for JSON, XML, and form URL encoded data.

             */

            if (ModelState.IsValid)
            {

                try
                {
                    // TODO: Add insert logic here
                     client.DefaultRequestHeaders.Clear();
                     client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                     string d = emp.HireDate.Value.ToString("dd-MM-yyyy");


                    //emp.HireDate = DateTime.Parse(d);//Exception:{"String was not recognized as a valid DateTime."}

                    var ResponseResult = client.PostAsJsonAsync<EmployeeFromWebAPI>("api/Emps/Emps", emp).Result;
                   //var ResponseResult = client.PostAsJsonAsync<EmployeeFromWebAPI>($"api/Emps/Emps/Add/Name={emp.Name}/Salary={emp.Salary}/Department={emp.DepartmentId}/HDate={d}", emp).Result;
                    /*
                     Error: StatusCode: 406, ReasonPhrase: 'Not Acceptable', Version: 1.1, Content: System.Net.Http.StreamContent, Headers: { Pragma: no-cache X-SourceFiles: =?UTF-8?B?QzpcVXNlcnNcTW9oYW1lZCBBYmR1bGxhaFxEb2N1bWVudHNcVmlzdWFsIFN0dWRpbyAyMDE3XFByb2plY3RzXFdlYkFQSVxXZWJBUEkxXFdlYkFQSTFcYXBpXEVtcHNcRW1wc1xBZGRcTmFtZT10cnk0XFNhbGFyeT00MjAyXERlcGFydG1lbnQ9M1xIRGF0ZT0yNC0wMi0yMDAw?= Cache-Control: no-cache Date: Sat, 24 Mar 2018 20:47:06 GMT Server: Microsoft-IIS/10.0 X-AspNet-Version: 4.0.30319 X-Powered-By: ASP.NET Content-Length: 137 Content-Type: application/json; charset=utf-8 Expires: -1 }

                     
                     
                     */
                    if (ResponseResult.IsSuccessStatusCode)
                    {

                        ViewBag.Message = $"Employee {emp.Name} has already added successfully  ^_^";
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.Message = $"Error: {ResponseResult.ToString()}";


                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error: {ex.Message}  ^_^";

                }

            }

            ViewBag.DepartmentId = new SelectList(Departments, "Id", "Name");

            return View(emp);

        }

        // GET: EmployeeWebAPI/Edit/5
        public ActionResult Edit(int id)
        {/*
            
             /*
    "MessageDetail": "The parameters dictionary contains a null entry for parameter 'id' of non-nullable type 'System.Int32' for method 'System.Web.Http.IHttpActionResult PutEmployee(Int32, WebAPI1.Models.Employee)' in 'WebAPI1.Controllers.EmployeeController'. An optional parameter must be a reference type, a nullable type, or be declared as an optional parameter."
http://localhost:53643/api/Emps/Emps/Edit/Name=ALiNew/Salary=456/Department=2/HDate=05-09-2017

**/

            
            return View();
        }

        // POST: EmployeeWebAPI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeWebAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeWebAPI/Delete/5
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
