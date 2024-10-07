using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Code_MyPage_UserPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            string selectSQL = "SELECT *";
            selectSQL += "FROM [Life].[dbo].[User] ";

            HttpCookie cookie = Request.Cookies["Login_MainPage"];

            UserID.Text = cookie["UserID"];

            selectSQL += "WHERE [UserName] = @UserID";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, conn);

            cmd.Parameters.AddWithValue("@UserID", UserID.Text);

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
                Level.Text = rd["Lv"].ToString();
                PreExp.Text = rd["Exp"].ToString();

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

            string slv = Level.Text;
            int ilv;
            int mxexp;

            try
            {
                ilv = int.Parse(slv);
                mxexp = ilv * ilv * 100 + 100;

                MaxExp.Text = mxexp.ToString();
            }
            catch(FormatException)
            {

            }
        }
        Random rand = new Random();

        string[] word = { "오늘도 좋은 하루 보내세요", "너무 무리하지 마세요", "좋은 일만 가득하세요" };

        int i;

        int word_length = word.Length;

        for (i = 0; i < word_length; i++)
        {
            int randindex = rand.Next(word_length);
            GoodWord.Text = word[randindex];

        }
    }

    protected void ChangeInfo_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid == true)
        {
            string updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Password] = @userpw, [Email] = @usermail, [NickName] = @usernick, [Question] = @userquestion, [Answer] = @useranswer ";
            updateSQL += "WHERE [UserName] = @userID ";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, conn);

            cmd.Parameters.AddWithValue("@userpw", Password.Text);
            cmd.Parameters.AddWithValue("@usermail", Email.Text);
            cmd.Parameters.AddWithValue("@usernick", NickName.Text);
            cmd.Parameters.AddWithValue("@userquestion", Question.Text);
            cmd.Parameters.AddWithValue("@useranswer", Answer.Text);
            cmd.Parameters.AddWithValue("@userID", UserID.Text);

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
}