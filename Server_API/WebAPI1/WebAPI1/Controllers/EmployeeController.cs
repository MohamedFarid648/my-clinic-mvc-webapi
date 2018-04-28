using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [RoutePrefix("api/Emps")]
    //[Display(Name ="Emps")]
    public class EmployeeController : ApiController
    {
        MyDBContext db = new MyDBContext();


        [Route("Emps")]
        public List<Employee> GetAll()
        {
            /*
             
             why?
             exec sp_executesql N'SELECT 
            [Extent1].[Id] AS [Id], 
            [Extent1].[Name] AS [Name], 
            [Extent1].[Salary] AS [Salary], 
            [Extent1].[HireDate] AS [HireDate], 
            [Extent1].[DepartmentId] AS [DepartmentId]
            FROM [dbo].[Employees] AS [Extent1]
            WHERE [Extent1].[DepartmentId] = @EntityKeyValue1',N'@EntityKeyValue1 int',@EntityKeyValue1=3
             
             */
            return db.Employees.ToList();

        }

        [Route("Emps/{name}/details")]
        public Employee GetDetails(string name)
        {
            var result = db.Employees.FirstOrDefault(e => e.Name == name);
            //if (result == null)
            // return BadRequest("H");
            return result;

        }

        /*[Route("Emps/{id}/details")]
        public Employee GetDetails(int id)
        {
            var result = db.Employees.FirstOrDefault(e => e.Id == id);
            //if (result == null)
            // return BadRequest("H");
            return result;

        }
        */
        [Route("Emps/{hiredate}")]
        public Employee GetDetailsByHireDate(DateTime hiredate)
        {
            var result = db.Employees.FirstOrDefault(e => e.HireDate == hiredate);
            return result;

        }


        [Route("~/Depts/{id}/Employees")]
        public List<Employee> GetDetailsByDepartment(int id)
        {


            var result = db.Employees.Where(e => e.DepartmentId == id).ToList();
            return result;

        }

        [Route("~/Depts/All")]
        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();

        }

        [Route("Emps")]///HDate={hireDate}
        public HttpResponseMessage PostEmployee(Employee emp)//[FromUri] 
        {
            HttpResponseMessage msg = new HttpResponseMessage()
            {
                Content = new StringContent(""),
                ReasonPhrase = "Exception on Saving Data on DB"
            };

            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);

            else
            {
                try
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Added ^_^");

                    // return RedirectToRoute("Emps", new { });
                } catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
                }
            }
        }



        [Route("Emps/Add/Name={Name}/Salary={Salary}/Department={DepartmentId}/HDate={HireDate}")]///HDate={hireDate}//dd-MM-yyyy
        //ex:http://localhost:53643/api/Emps/Emps/Add/Name=Maged/Salary=5897/Department=2/HDate=05-09-2000
        public HttpResponseMessage PostEmployeeURI([FromUri]Employee emp)//[FromUri] 
        {
            HttpResponseMessage msg = new HttpResponseMessage()
            {
                Content = new StringContent(""),
                ReasonPhrase = "Exception on Saving Data on DB"
            };

            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);

            else
            {
                try
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Added ^_^");

                    // return RedirectToRoute("Emps", new { });
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
                }
            }
        }


        [Route("Emps/Edit/Name={Name}/Salary={Salary}/Department={DepartmentId}/HDate={HireDate}")]///HDate={hireDate}

        public IHttpActionResult PutEmployee([FromBody]int id, [FromUri]Employee emp)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }
            Employee MyEmp = db.Employees.Find(id);
            if (id == 0 || emp == null || MyEmp == null)
            {

                return NotFound();

            }

            //db.Entry(emp).State = EntityState.Modified;
            MyEmp.DepartmentId = emp.DepartmentId;
            MyEmp.HireDate = emp.HireDate;
            MyEmp.Name = emp.Name;
            MyEmp.Salary = emp.Salary;
            try
            {
                db.SaveChanges();
                /*

               exec sp_executesql N'UPDATE [dbo].[Employees]
                SET [Name] = @0, [Salary] = @1, [HireDate] = @2, [DepartmentId] = @3
                WHERE ([Id] = @4)
                ',N'@0 nvarchar(10),@1 decimal(18,2),@2 datetime2(7),@3 int,@4 int',@0=N'ALiNew',@1=456.00,@2='2017-09-05 00:00:00',@3=2,@4=1      

               */
                return Ok(MyEmp);

                // return RedirectToRoute("Emps", new { });

            }
            catch (Exception ex)
            {

                HttpResponseMessage msg = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Message.ToString()),
                    ReasonPhrase = "Exception on Saving Data on DB"
                };

                throw new HttpResponseException(msg);
            }
            // throw new Exception(ex.Message);
            //return BadRequest(ex.Message);
        }





        [Route("Emps/Edit/{id}")]

    public IHttpActionResult PutEmployeeById(int id,Employee emp)
    {

        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);

        }
        Employee MyEmp = db.Employees.Find(id);
        if (id == 0 || emp == null || MyEmp == null)
        {

            return NotFound();

        }

            //db.Entry(emp).State = EntityState.Modified;
            MyEmp.DepartmentId = emp.DepartmentId;
            MyEmp.HireDate = emp.HireDate;
            MyEmp.Name = emp.Name;
            MyEmp.Salary = emp.Salary;
        try
        {
            db.SaveChanges();
                /*

               exec sp_executesql N'UPDATE [dbo].[Employees]
    SET [Name] = @0, [Salary] = @1, [HireDate] = @2, [DepartmentId] = @3
    WHERE ([Id] = @4)
    ',N'@0 nvarchar(10),@1 decimal(18,2),@2 datetime2(7),@3 int,@4 int',@0=N'ALiNew',@1=456.00,@2='2017-09-05 00:00:00',@3=2,@4=1      

               */
                return Ok(MyEmp);

            // return RedirectToRoute("Emps", new { });

        }
        catch (Exception ex)
        {

            HttpResponseMessage msg = new HttpResponseMessage()
            {
                Content = new StringContent(ex.Message.ToString()),
                ReasonPhrase = "Exception on Saving Data on DB"
            };

            throw new HttpResponseException(msg);
        }
        // throw new Exception(ex.Message);
        //return BadRequest(ex.Message);
    }


}
    }

