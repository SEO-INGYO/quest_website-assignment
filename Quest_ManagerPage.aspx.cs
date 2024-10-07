using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Code_Quest_ManagerPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillAllQuestList();
        }
    }

    private void FillAllQuestList()
    {
        QuestList.Items.Clear();

        string selectSQL = "SELECT [Name], [Description] ";
        selectSQL += "FROM [dbo].[Quest] ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);
        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ListItem item = new ListItem();
                item.Text = rd["Name"].ToString();
                item.Value = rd["Description"].ToString();
                QuestList.Items.Add(item);
            }

            rd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = "데이터베이스 읽기 오류<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void QuestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        string selectSQL = "SELECT * FROM [Life].[dbo].[Quest] ";
        selectSQL += "WHERE [Name] = @questname ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@questname", QuestList.SelectedValue);

        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();
            rd.Read();

            QuestName.Text = rd["Name"].ToString();
            QuestDes.Text = rd["Description"].ToString();
            QuestLevel.Text = rd["Lv"].ToString();
            QuestExp.Text = rd["Exp"].ToString();

            rd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = "데이터베이스 읽기 오류<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void CreateQuest_Click(object sender, EventArgs e)
    {
        Page.Validate();

        string insertSQL = "INSERT INTO [dbo].[Quest]";
        insertSQL += "([Name],[Description],[Lv],[Exp],[Time],[State])";
        insertSQL += "VALUES(@questname, @questdes, @questlevel, @questexp, @questtimer,'없음')";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(insertSQL, conn);

        cmd.Parameters.AddWithValue("@questname", QuestName.Text);
        cmd.Parameters.AddWithValue("@questdes", QuestDes.Text);
        cmd.Parameters.AddWithValue("@questlevel", QuestLevel.Text);
        cmd.Parameters.AddWithValue("@questexp", QuestExp.Text);

        int inserted = 0;

        try
        {
            conn.Open();
            inserted = cmd.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 생성 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 생성 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();

            QuestName.Text = "";
            QuestDes.Text = "";
            QuestLevel.Text = "";
            QuestExp.Text = "";
        }
    }

    protected void DeleteQuest_Click(object sender, EventArgs e)
    {
       string updateSQL = "DELETE FROM [dbo].[Quest] ";
       updateSQL += "WHERE [Name] = @questname";

       SqlConnection conn = new SqlConnection(connectionString);
       SqlCommand cmd = new SqlCommand(updateSQL, conn);

       cmd.Parameters.AddWithValue("@questname", QuestName.Text);

       int deleted = 0;

       try
       {
           conn.Open();
           deleted = cmd.ExecuteNonQuery();
           lblStatus.Text = deleted.ToString() + " 개의 레코드가 삭제되었습니다.";

            QuestName.Text = "";
            QuestDes.Text = "";
            QuestLevel.Text = "";
            QuestExp.Text = "";
       }
       catch (Exception error)
       {
           lblStatus.Text = "데이터를 삭제하는 동안 오류가 발생했습니다.<br />";
           lblStatus.Text += error.Message;
       }
       finally
       {
           conn.Close();
       }

       if (deleted > 0)
       {
            FillAllQuestList();
       }
    }

    protected void ChangeQuest_Click(object sender, EventArgs e)
    {
        string updateSQL = "UPDATE [dbo].[Quest] SET [Name] = @questname, ";
        updateSQL += "[Description] = @questdes, [Lv] = @questlevel, ";
        updateSQL += "[Exp] = @questexp, [Time] = @questtimer WHERE [Name] = @questname";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@questname", QuestName.Text);
        cmd.Parameters.AddWithValue("@questdes", QuestDes.Text);
        cmd.Parameters.AddWithValue("@questlevel", QuestLevel.Text);
        cmd.Parameters.AddWithValue("@questexp", QuestExp.Text);

        try
        {
            conn.Open();
            lblStatus.Text = "퀘스트 정보 변경 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 정보 변경 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void ClearInformation_Click(object sender, EventArgs e)
    {
        QuestName.Text = "";
        QuestDes.Text = "";
        QuestLevel.Text = "";
        QuestExp.Text = "";

        Response.Redirect(Request.RawUrl);
    }
}