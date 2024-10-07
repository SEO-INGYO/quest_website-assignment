using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Code_Ranking_UserPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Send_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(connectionString);

        HttpCookie cookie = Request.Cookies["Login_MainPage"];
        Label UserID = (Label)Master.FindControl("UserID");
        UserID.Text = cookie["UserID"];

        string nickname = "";

        string searchSQL = "SELECT *";
        searchSQL += "FROM [Life].[dbo].[User] ";
        searchSQL += "WHERE [UserName] = @UserID ";

        SqlCommand searchcmd = new SqlCommand(searchSQL, conn);

        searchcmd.Parameters.AddWithValue("@UserID", UserID.Text);

        SqlDataReader searchrd;

        try
        {
            conn.Open();
            searchrd = searchcmd.ExecuteReader();

            searchrd.Read();

            nickname = searchrd["NickName"].ToString();

            searchrd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = error.Message;
        }
        finally
        {
            conn.Close();
        }

        string insertSQL = "INSERT INTO [Life].[dbo].[Forum] ";
        insertSQL += "([UserID],[Nickname],[Forum]) ";
        insertSQL += "VALUES(@userid, @usernickname, @userforum) ";

        SqlCommand cmd = new SqlCommand(insertSQL, conn);

        cmd.Parameters.AddWithValue("@userid", UserID.Text);
        cmd.Parameters.AddWithValue("@usernickname", nickname);
        cmd.Parameters.AddWithValue("@userforum", Forum.Text);

        int inserted = 0;

        try
        {
            conn.Open();
            inserted = cmd.ExecuteNonQuery();
        }
        catch (Exception error)
        {
            lblStatus.Text = error.Message;
        }
        finally
        {
            Forum.Text = "";
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }
}