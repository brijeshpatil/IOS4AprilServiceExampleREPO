using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IOS4AprilServiceExample
{
    /// <summary>
    /// Summary description for IOS4AprilService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IOS4AprilService : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AddNewState(string StateName)
        {
            cmd = new SqlCommand("insert into StateTbl values(@SName)", con);
            cmd.Parameters.AddWithValue("@SName", StateName);
            con.Open();
            int a = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();

            if (a != 0)
            {
                return "State Name Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public string UpdateStateName(int StateID, string StateName)
        {
            cmd = new SqlCommand("update StateTbl  set StateName =@SName where StateID=@SID", con);
            cmd.Parameters.AddWithValue("@SName", StateName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            con.Open();
            int a = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();

            if (a != 0)
            {
                return "State Name Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataTable GetAllStates()
        {
            da = new SqlDataAdapter("select * from StateTbl", con);
            dt = new DataTable("StateTbl");
            da.Fill(dt);
            return dt;
        }

        [WebMethod]
        public string AddNewCity(string CityName, int StateID)
        {
            if (StateID != 0)
            {
                cmd = new SqlCommand("insert into CityTbl values(@CName,@SID)", con);
                cmd.Parameters.AddWithValue("@CName", CityName);
                cmd.Parameters.AddWithValue("@SID", StateID);
                con.Open();
                int a = Convert.ToInt16(cmd.ExecuteNonQuery());
                con.Close();

                if (a != 0)
                {
                    return "City Name Saved";
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "StateID Can Not be ZERO";
            }
        }

        [WebMethod]
        public string UpdateCity(int CityID, string CityName, int StateID)
        {
            if (StateID != 0)
            {
                cmd = new SqlCommand("update CityTbl set CityName=@CName,FkStateID=@SID where CityID=@CID", con);
                cmd.Parameters.AddWithValue("@CName", CityName);
                cmd.Parameters.AddWithValue("@SID", StateID);
                cmd.Parameters.AddWithValue("@CID", CityID);
                con.Open();
                int a = Convert.ToInt16(cmd.ExecuteNonQuery());
                con.Close();

                if (a != 0)
                {
                    return "City Name Saved";
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "StateID Can Not be ZERO";
            }
        }

        [WebMethod]
        public DataTable GetAllCities()
        {
            da = new SqlDataAdapter("select * from CityTbl", con);
            dt = new DataTable("CityTbl");
            da.Fill(dt);
            return dt;
        }

        [WebMethod]
        public DataTable GetCitiesByState(int StateID)
        {
            da = new SqlDataAdapter("select * from CityTbl where FkStateID=@SID", con);
            da.SelectCommand.Parameters.AddWithValue("@SID", StateID);
            dt = new DataTable("CityTbl");
            da.Fill(dt);
            return dt;
        }

        [WebMethod]
        public string AddNewUser(string FirstName, string LastName, int StateID, int CityID, string UserName, string Password)
        {
            cmd = new SqlCommand("insert into UserInfo values(@Fname,@Lname,@SID,@CID,@UName,@Pass)", con);
            cmd.Parameters.AddWithValue("@Fname", FirstName);
            cmd.Parameters.AddWithValue("@Lname", LastName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            cmd.Parameters.AddWithValue("@CID", CityID);
            cmd.Parameters.AddWithValue("@UName", UserName);
            cmd.Parameters.AddWithValue("@Pass", Password);
            con.Open();
            int a = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();

            if (a != 0)
            {
                return "Registered Successfully";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public string UpdateUser(int UserID, string FirstName, string LastName, int StateID, int CityID, string UserName, string Password)
        {
            cmd = new SqlCommand("update UserInfo set fname=@Fname,lname=@Lname,fkstateid=@SID,fkcityid=@CID,uname=@UName,pass=@Pass where userid=@UID", con);
            cmd.Parameters.AddWithValue("@UID", UserID);
            cmd.Parameters.AddWithValue("@Fname", FirstName);
            cmd.Parameters.AddWithValue("@Lname", LastName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            cmd.Parameters.AddWithValue("@CID", CityID);
            cmd.Parameters.AddWithValue("@UName", UserName);
            cmd.Parameters.AddWithValue("@Pass", Password);
            con.Open();
            int a = Convert.ToInt16(cmd.ExecuteNonQuery());
            con.Close();

            if (a != 0)
            {
                return "Updated Successfully";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataTable CheckLoginUser(string UserName, string Password)
        {
            da = new SqlDataAdapter("select * from UserInfo where uname=@UName and pass=@Pass", con);
            da.SelectCommand.Parameters.AddWithValue("@UName", UserName);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Password);
            dt = new DataTable("UserInfo");
            da.Fill(dt);
            return dt;
        }

    }
}
