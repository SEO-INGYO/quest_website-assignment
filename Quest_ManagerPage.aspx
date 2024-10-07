<%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/Manager_MasterPage.master" AutoEventWireup="true" CodeFile="Quest_ManagerPage.aspx.cs" Inherits="Main_Code_Quest_ManagerPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LifeConnectionString %>" SelectCommand="SELECT * FROM [Quest] WHERE NOT [Name] = '없음'"></asp:SqlDataSource>
        현재 선택 중인 퀘스트 : <asp:DropDownList ID="QuestList" runat="server" DataSourceID="SqlDataSource" DataTextField="Name" DataValueField="Name" OnSelectedIndexChanged="QuestList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        &nbsp;<asp:Button ID="ClearInformation" runat="server" Text="초기화" OnClick="ClearInformation_Click" />
    </div>
    <br />
    <div class="center">
        <asp:Button ID="CreateQuest" runat="server" Text="퀘스트 생성" OnClick="CreateQuest_Click" />
        <asp:Button ID="DeleteQuest" runat="server" Text="퀘스트 삭제" OnClick="DeleteQuest_Click" />
        <asp:Button ID="ChangeQuest" runat="server" Text="퀘스트 변경" OnClick="ChangeQuest_Click" />
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td>퀘스트 이름 :</td>
                <td>
                    <asp:TextBox ID="QuestName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>퀘스트 내용 :</td>
                <td>
                    <asp:TextBox ID="QuestDes" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>퀘스트 제한 레벨 :</td>
                <td>
                    <asp:TextBox ID="QuestLevel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>퀘스트 완료 경험치 :</td>
                <td>
                    <asp:TextBox ID="QuestExp" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table><br />
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>

