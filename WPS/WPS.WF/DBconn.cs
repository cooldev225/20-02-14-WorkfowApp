using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WPS.WF.Model;

namespace WPS.Presentation
{
    public class DBconn
    {
        static string _initDBSql= "CREATE TABLE Agent (Id INT PRIMARY KEY identity(1, 1),Name nvarchar(max) not null,Email nvarchar(max) not null,Roles nvarchar(max) not null);" +
                           "CREATE TABLE Element (Id INT PRIMARY KEY identity(1, 1),OrderTitle nvarchar(max) not null,OrderDesc nvarchar(max) not null,RequestedBy int not null,LastState nvarchar(max) not null,RequestDate nvarchar(max) not null,TerminationDate nvarchar(max) null);" +
                           "CREATE TABLE StateLog (Id INT PRIMARY KEY identity(1, 1),ElementId int not null,TriggerId nvarchar(max) not null,TriggeredBy int not null,TriggeredDate nvarchar(max) not null,SourceState nvarchar(max) not null,DestinationState nvarchar(max) not null,Comment nvarchar(max) null);";
        static string _connString = "Server=DESKTOP-465IRHT;Database=StatelessDemo;Trusted_Connection=True;MultipleActiveResultSets=true;";
            //ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;
        public static object excuteQuery(string sql) {
            object res = "";
            SqlConnection myConnection = new SqlConnection(_connString);
            myConnection.Open();
            try{new SqlCommand(_initDBSql, myConnection).ExecuteNonQuery();}catch (Exception) { }
            try{
                SqlDataReader reader=new SqlCommand(sql, myConnection).ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    res=reader.GetValue(0);
                }

            }catch (Exception e) { Console.WriteLine(e.ToString()); }
            myConnection.Close();
            return res;
        }

        public static int insertAgent(string user)
        {
            return insertAgent(user,"");
        }

        public static int insertAgent(string user, string email)
        {
            SqlConnection myConnection = new SqlConnection(_connString);
            myConnection.Open();
            try { new SqlCommand(_initDBSql, myConnection).ExecuteNonQuery(); } catch (Exception e) { Console.WriteLine(e.ToString()); }
            int id = 0;
            try
            {
                SqlCommand cmd = new SqlCommand($"select count(*) from agent where name='{user}'", myConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.GetInt32(0) == 0) new SqlCommand($"insert into agent (name,email,roles) values('{user}','{email}','assign');", myConnection).ExecuteNonQuery();
                cmd = new SqlCommand($"select * from agent where name='{user}'", myConnection);
                reader = cmd.ExecuteReader();
                reader.Read();
                id = reader.GetInt32(0);
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            myConnection.Close();
            return id;
        }

        public static List<WorkElement> GetStateless(int eid) {
            List<WorkElement> res = new List<WorkElement>();
            SqlConnection myConnection = new SqlConnection(_connString);
            myConnection.Open();
            try { new SqlCommand(_initDBSql, myConnection).ExecuteNonQuery(); } catch (Exception e) { Console.WriteLine(e.ToString()); }
            try
            {
                string sql = $"select a.*,c.name,d.name as requestedname,d.email as requestedmail,b.id as bid,b.ordertitle,b.orderdesc,laststate,requestdate,terminationdate from StateLog a join element b on a.ElementId=b.Id join agent c on a.triggeredby=c.id join agent d on b.requestedby=d.id " + (eid > 0 ? $"where b.id = '{eid}'" : "") + " order by b.RequestDate,a.TriggeredDate";
                SqlCommand cmd = new SqlCommand(sql,myConnection);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows) {
                    var id = -1;
                    WorkElement e = null;
                    List<Transition> t=null;
                    while (r.Read()) {
                        if (id != int.Parse(r["bid"].ToString())) {
                            if (id > -1) {
                                e.Transition = t;
                                res.Add(e);
                            }
                            id = int.Parse(r["bid"].ToString());
                            t = new List<Transition>();
                            e = new WorkElement
                            {
                                Id = id,
                                OrderTitle = r["ordertitle"].ToString(),
                                OrderDescription = r["orderdesc"].ToString(),
                                RequestedBy= r["requestedname"].ToString(),
                                EmailAddress= r["requestedmail"].ToString(),
                                LastState = r["laststate"].ToString(),
                                Transition = t
                            };
                            if (!r["requestdate"].ToString().Equals("")) e.RequestDate = DateTime.Parse(r["requestdate"].ToString());
                            if (!r["terminationdate"].ToString().Equals("")) e.TerminationDate = DateTime.Parse(r["terminationdate"].ToString());
                        }
                        t.Add(new Transition { 
                            ElementId=id,
                            SourceState = r["sourcestate"].ToString(),
                            DestnationState = r["destinationstate"].ToString(),
                            TriggeredBy = r["name"].ToString(),
                            TriggeredDate = DateTime.Parse(r["triggereddate"].ToString()),
                            Comment = r["comment"].ToString()
                        });
                    }
                    if (e != null) res.Add(e);
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            myConnection.Close();
            return res;
        }
    }
}