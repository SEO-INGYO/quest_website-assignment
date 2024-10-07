using System;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Main_Code_CreateAccount_MainPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreatAccount_btn_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid == true)
        {
            string insertSQL = "INSERT INTO [Life].[dbo].[User] ";
            insertSQL += "([UserName],[Password],[Email],[NickName],[Question],[Answer],[Lv],[Exp],[Manager],[Quest])";
            insertSQL += "VALUES(@userid, @userpw, @usermail, @usernick, @userquestion, @useranswer, 0, 0, 'False', '없음')";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertSQL, conn);

            cmd.Parameters.AddWithValue("@userid", UserName.Text);
            cmd.Parameters.AddWithValue("@userpw", Password.Text);
            cmd.Parameters.AddWithValue("@usermail", Email.Text);
            cmd.Parameters.AddWithValue("@usernick", NickName.Text);
            cmd.Parameters.AddWithValue("@userquestion", Question.Text);
            cmd.Parameters.AddWithValue("@useranswer", Answer.Text);

            int inserted = 0;

            try
            {
                conn.Open();

                inserted = cmd.ExecuteNonQuery();
                lblStatus.Text = "계정 생성 완료";
            }
            catch (Exception error)
            {
                lblStatus.Text = "계정 생성 실패<br />";
                lblStatus.Text += error.Message;
            }
            finally
            {
                conn.Close();

                UserName.Text = "";
                Password.Text = "";
                ConfirmPassword.Text = "";
                Email.Text = "";
                NickName.Text = "";
                Question.Text = "";
                Answer.Text = "";
            }
        }

        else
        {
            lblStatus.Text = "계정 생성 실패<br />";
        }

    }
}