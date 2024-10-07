<%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/User_MasterPage.master" AutoEventWireup="true" CodeFile="Quest_UserPage.aspx.cs" Inherits="Main_Code_Quest_UserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        현재 수행중인 퀘스트 [ <asp:Label ID="QuestProgress" runat="server" Text="수행중인 퀘스트 없음 "></asp:Label>&nbsp;]<br /><br />
        퀘스트 이름 : <asp:Label ID="ProgressQuestName" runat="server" Text=""></asp:Label><br />
        퀘스트 내용 : <asp:Label ID="ProgressQuestDescription" runat="server" Text=""></asp:Label><br />
        퀘스트 제한 레벨 : <asp:Label ID="ProgressQuestLv" runat="server" Text=""></asp:Label><br />
        퀘스트 완료 경험치 : <asp:Label ID="ProgressQuestExp" runat="server" Text=""></asp:Label><br />
        <br /><br />
        <asp:Button ID="QuestAbandon" runat="server" Text="퀘스트 포기" Height="50px" Width="120px" OnClick="QuestAbandon_Click"/>
        <asp:Button ID="QuestComplete" runat="server" Text="퀘스트 완료" Height="50px" Width="120px" OnClick="QuestComplete_Click"/><br /><br />
    </div><br />
    <div>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LifeConnectionString_Quest %>" SelectCommand="SELECT * FROM [Quest] WHERE NOT [Name] = '없음'"></asp:SqlDataSource>
        퀘스트 목록 : <asp:DropDownList ID="QuestList" runat="server" DataSourceID="SqlDataSource" DataTextField="Name" DataValueField="Name" Height="40px" Width="150px" OnSelectedIndexChanged="QuestList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br /><br />
    </div>
    <div>
        퀘스트 이름 : <asp:Label ID="QuestName" runat="server" Text=""></asp:Label><br />
        퀘스트 내용 : <asp:Label ID="QuestDescription" runat="server" Text=""></asp:Label><br />
        퀘스트 제한 레벨 : <asp:Label ID="QuestLv" runat="server" Text=""></asp:Label><br />
        퀘스트 완료 경험치 : <asp:Label ID="QuestExp" runat="server" Text=""></asp:Label><br />
    </div><br />
    <div>
        <asp:Button ID="QuestAccept" runat="server" Text="퀘스트 수락" Height="50px" Width="120px" OnClick="QuestAccept_Click"/><br />
    </div><br /><br />
    <div>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

