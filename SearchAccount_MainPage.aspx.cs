using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;


public partial class Main_Code_SearchAccount_MainPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SearchAccount_Click(object sender, EventArgs e)
    {
        if (IsAuthenticated(Email.Text, Question.Text, Answer.Text))
        {
            

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("master@aspnet.com", "관리자", System.Text.Encoding.Default);

            Random num = new Random();

            string updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Password] = @Password ";
            updateSQL += "WHERE [Email] = @Email ";

            string newpass = num.Next(100000, 999999).ToString();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, conn);

            cmd.Parameters.AddWithValue("@Email", Email.Text);
            cmd.Parameters.AddWithValue("@Password", newpass);

            int updated = 0;

            try
            {
                conn.Open();
                updated = cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                SearchLabel.Text = "데이터베이스 오류<br/>";
                SearchLabel.Text += error.Message;
            }
            finally
            {
                conn.Close();
            }

            string userid = "";

            string selectSQL = "SELECT * ";
            selectSQL += "FROM [Life].[dbo].[User] ";
            selectSQL += "WHERE [Email] = @Email ";

            SqlCommand cmd2 = new SqlCommand(selectSQL, conn);

            cmd2.Parameters.AddWithValue("@Email", Email.Text);

            SqlDataReader rd;

            try
            {
                conn.Open();
                rd = cmd2.ExecuteReader();

                rd.Read();

                userid = rd["UserName"].ToString();

                rd.Close();
            }
            catch (Exception error)
            {
                SearchLabel.Text = "데이터베이스 오류<br/>";
                SearchLabel.Text += error.Message;
            }
            finally
            {
                conn.Close();
            }

            mail.Subject = "생활의 달인 비밀번호 재설정";

            mail.Body = "아이디 " + userid + " 의 " + "임시 비밀번호는 " + newpass + " 입니다.";

            mail.SubjectEncoding = System.Text.Encoding.Default;
            mail.BodyEncoding = System.Text.Encoding.Default;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("127.0.0.1");

            mail.To.Add(Email.Text);

            smtp.Send(mail);

            this.SearchLabel.Text = "입력하신 이메일로 비밀번호 재설정 메일을 보냈습니다.";
        }
        else
        {
            this.SearchLabel.Text = "계정 정보를 다시 확인해주세요";
        }
    }

    bool IsAuthenticated(string Email, string Question, string Answer)
    {
        SqlConnection conn = new SqlConnection(connectionString);

        string selectSQL = "SELECT count(Email) ";
        selectSQL += "FROM [Life].[dbo].[User] ";
        selectSQL += "WHERE [Email] = @Email ";
        selectSQL += "AND [Question] = @Question ";
        selectSQL += "AND [Answer] = @Answer ";

        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@Email", Email);
        cmd.Parameters.AddWithValue("@Question", Question);
        cmd.Parameters.AddWithValue("@Answer", Answer);

        conn.Open();
        int count = (int)cmd.ExecuteScalar();
        conn.Close();

        return count > 0;
    }
}