using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AureliaDBMenu.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        string crlf = "\r\n";

        IConfiguration _config;
        public SampleDataController(IConfiguration config)
        {
            _config = config;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<NavigationRoute> DefaultRoutes()
        {
            IEnumerable<NavigationRoute> routes = Enumerable.Empty<NavigationRoute>();

            var route1 = new NavigationRoute
            {
                Id = 1,
                Route = "scheduler",
                Name = "scheduler",
                Icon = "education",
                ModuleId = "../scheduler/scheduler",
                Nav = true,
                Title = "Scheduler"
            };

            var route2 = new NavigationRoute
            {
                Id = 2,
                Route = "counter",
                Name = "counter",
                Icon = "education",
                ModuleId = "../counter/counter",
                Nav = true,
                Title = "Counter"
            };

            var route3 = new NavigationRoute
            {
                Id = 3,
                Route = "fetch-data",
                Name = "fetchdata",
                Icon = "th-list",
                ModuleId = "../fetchdata/fetchdata",
                Nav = true,
                Title = "Fetch data"
            };

            routes = routes.Concat(new[] { route1 });
            routes = routes.Concat(new[] { route2 });
            routes = routes.Concat(new[] { route3 });

            return routes;
        }

        [HttpGet("[action]")]
        public IEnumerable<NavigationRoute> DbRoutes()
        {
            IEnumerable<NavigationRoute> routes = Enumerable.Empty<NavigationRoute>();

            try
            {

                string connStr = _config.GetSection("Configuration").GetSection("ConnString").Value;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("SELECT Id, ParentId, Route, Name, Icon, ModuleId, Nav, Title FROM dbo.NavigationRoute order by Id", conn);
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "appicon";

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Object dbParentId = reader["ParentId"];
                        int? parentId;

                        if ((dbParentId.GetType().ToString().Equals("System.DBNull")))
                            parentId = null;
                        else
                            parentId = Convert.ToInt16(dbParentId);

                        NavigationRoute route = new NavigationRoute
                        {
                            Id = Convert.ToInt16(reader["Id"].ToString()),
                            ParentId = parentId,
                            Route = reader["Route"].ToString(),
                            Name = reader["Name"].ToString(),
                            Icon = reader["Icon"].ToString(),
                            ModuleId = reader["ModuleId"].ToString(),
                            Nav = (bool)reader["Nav"],
                            Title = reader["Title"].ToString()
                        };

                        routes = routes.Concat(new[] { route });
                    }
                }
            }
            catch (SystemException ex)
            {
                return this.DefaultRoutes();
            }

            return routes;
        }

        [HttpGet("[action]")]
        public IEnumerable<NavigationRoute> TestDbConn()
        {
            IEnumerable<NavigationRoute> routes = Enumerable.Empty<NavigationRoute>();
            try
            {
                string connStr = _config.GetSection("Configuration").GetSection("ConnString").Value;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                }
            }
            catch (SystemException ex)
            {
                var route1 = new NavigationRoute
                {
                    Id = 1,
                    Route = "error",
                    Name = "scheduler",
                    Icon = "education",
                    ModuleId = "../scheduler/scheduler",
                    Nav = true,
                    Title = "Error"
                };

                routes = routes.Concat(new[] { route1 });

                return routes;
            }

            return routes;
        }
       

        [HttpGet("[action]")]
        public IEnumerable<string> WriteRoutesToFile()
        {
            IEnumerable<string> routes = Enumerable.Empty<string>();

            try
            {
                string connStr = _config.GetSection("Configuration").GetSection("ConnString").Value;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("SELECT ModuleId FROM dbo.NavigationRoute WHERE ParentId IS NULL order by Id", conn);
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = "appicon";

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    using (StreamWriter writer = System.IO.File.CreateText(".\\ClientApp\\app\\components\\app\\routes.ts"))
                    {
                        writer.Write(TextBefore());

                        string route = string.Empty;

                        while (reader.Read())
                        {
                            route = "PLATFORM.moduleName('" + reader["ModuleId"].ToString() + "');";
                            writer.WriteLine("        " + route);
                            routes = routes.Concat(new[] { route });
                        }

                        writer.Write(TextAfter());
                    }
                }
            }
            catch (SystemException ex)
            {
                return WriteDefaultRoutesToFile();
            }

            return routes;
        }

        [HttpGet("[action]")]
        public IEnumerable<string> WriteDefaultRoutesToFile()
        {
            IEnumerable<string> routes = Enumerable.Empty<string>();

            using (StreamWriter writer = System.IO.File.CreateText(".\\ClientApp\\app\\components\\app\\routes.ts"))
            {
                writer.Write(TextBefore());
                writer.Write(HardCodedRoutes());
                writer.Write(TextAfter());
            }

            return routes;
        }

        private string TextBefore()
        {
            return
             "import { Aurelia, PLATFORM } from 'aurelia-framework'; " + crlf + ""
           + "import { HttpClient } from 'aurelia-fetch-client';" + crlf + ""
           + "import { inject } from 'aurelia-framework';" + crlf + ""
           + "" + crlf + ""
           + "@inject(HttpClient)" + crlf + ""
           + "export class DbRoutes {" + crlf + ""
           + "    public navroutes: NavigationRoute[];" + crlf + ""
           + "    public routes: string[];" + crlf + ""
           + "    constructor(http: HttpClient) { " + crlf + "";
        }

        private string HardCodedRoutes()
        {
            return
             "     PLATFORM.moduleName('../scheduler/scheduler');" + crlf + ""
           + "     PLATFORM.moduleName('../counter/counter');" + crlf + ""
           + "     PLATFORM.moduleName('../fetchdata/fetchdata');" + crlf + ""
           + "     PLATFORM.moduleName('../newcustomer/newcustomer');" + crlf + ""
           + "     PLATFORM.moduleName('../searchcustomers/searchcustomers'); ";
        }

        private string TextAfter()
        {
            return
             "    }" + crlf + ""
           + "}" + crlf + ""
           + "" + crlf + ""
           + "class NavigationRoute {" + crlf + ""
           + "    id: number;" + crlf + ""
           + "    parentId: any;" + crlf + ""
           + "    route: string;" + crlf + ""
           + "    name: string;" + crlf + ""
           + "    icon: string;" + crlf + ""
           + "    moduleId: string;" + crlf + ""
           + "    nav: boolean;" + crlf + ""
           + "    title: string;" + crlf + ""
           + "};" + crlf + "";
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(this.TemperatureC / 0.5556);
                }
            }
        }

        public class NavigationRoute
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
            public string Route { get; set; }
            public string Name { get; set; }
            public string Icon { get; set; }
            public string ModuleId { get; set; }
            public bool Nav { get; set; }
            public string Title { get; set; }
        }
    }
}
