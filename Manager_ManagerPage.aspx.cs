using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

public partial class Main_Code_Manager_ManagerPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string selectSQL = "SELECT *";
            selectSQL += "FROM [Life].[dbo].[User] ";

            HttpCookie cookie = Request.Cookies["Login_MainPage"];

            UserName.Text = cookie["UserID"];

            selectSQL += "WHERE [UserName] = @UserID";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, conn);

            cmd.Parameters.AddWithValue("@UserID", UserName.Text);

            SqlDataReader rd;

            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();

                rd.Read();

                Password.Text = rd["Password"].ToString();
                ConfirmPassword.Text = rd["Password"].ToString();
                Email.Text = rd["Email"].ToString();
                NickName.Text = rd["NickName"].ToString();
                Question.Text = rd["Question"].ToString();
                Answer.Text = rd["Answer"].ToString();

                rd.Close();
            }
            catch (Exception error)
            {
                lblStatus.Text = "데이터베이스 오류.<br>";
                lblStatus.Text += error.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    protected void ChangeAccount_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid == true)
        {
            string updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Password] = @userpw, [Email] = @usermail, [Question] = @userquestion, [Answer] = @useranswer ";
            updateSQL += "WHERE [UserName] = @userID ";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, conn);

            cmd.Parameters.AddWithValue("@userpw", Password.Text);
            cmd.Parameters.AddWithValue("@usermail", Email.Text);
            cmd.Parameters.AddWithValue("@userquestion", Question.Text);
            cmd.Parameters.AddWithValue("@useranswer", Answer.Text);
            cmd.Parameters.AddWithValue("@userID", UserName.Text);

            int updated = 0;

            try
            {
                conn.Open();
                updated = cmd.ExecuteNonQuery();
                lblStatus.Text = "계정 정보 변경 완료";
            }
            catch (Exception error)
            {
                lblStatus.Text = "계정 정보 변경 실패<br />";
                lblStatus.Text += error.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            lblStatus.Text = "계정 정보 변경 실패<br />";
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void DeleteForum_Click(object sender, EventArgs e)
    {
        string deleteSQL = "DELETE FROM [Life].[dbo].[Forum] ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(deleteSQL, conn);

        int deleted = 0;

        try
        {
            conn.Open();
            deleted = cmd.ExecuteNonQuery();
            lblStatus.Text = "게시판 초기화 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "게시판 초기화 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }
}