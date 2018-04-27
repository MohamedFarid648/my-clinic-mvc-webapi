/*1.  Tables in Code First
             https://social.msdn.microsoft.com/Forums/en-US/aba0fb67-d290-475c-a639-075424096b29/linq-joining-3-tables?forum=linqtosql
			 https://stackoverflow.com/questions/41933985/how-to-join-3-tables-with-linq
             3 tables join
             string clientName = "Client Name";

                using (var context = new DataClasses1DataContext())
                {
                    var query = from ot in context.OTs
                                join v in context.Vehicles on ot.vehicle_id equals v.id
                                join c in context.Clients on v.client_id equals c.id
                                where c.Name == clientName
                                select new {ot, c};

                    foreach (var q in query)
                    {
                        Console.WriteLine("OT ID = {0} Customer Name = {1}", q.ot.id, q.c.Name );
                    }
                }
             */


/*2.Users in role
 https://forums.asp.net/t/2033844.aspx?get+all+user+in+a+role
 */


/*3.Add Extension Method

 https://stackoverflow.com/questions/6981853/asp-net-mvc3-razor-display-actionlink-based-on-user-role
 */



/*
 Handle Errors:
  <!--<customErrors mode="On" defaultRedirect="~/Error/Index">
     <error redirect="~/Error/NotFound" statusCode="404" />
   </customErrors>-->

 */