<%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/User_MasterPage.master" AutoEventWireup="true" CodeFile="MyPage_UserPage.aspx.cs" Inherits="Main_Code_MyPage_UserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        <asp:Label ID="GoodWord" runat="server" Text="" CssClass="center"></asp:Label>
    </div>
    <br />
    <div>
        Lv&nbsp;<asp:Label ID="Level" runat="server" Text=""></asp:Label>&nbsp;
        Exp&nbsp;<asp:Label ID="PreExp" runat="server" Text=""></asp:Label>&nbsp; / &nbsp;<asp:Label ID="MaxExp" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td>아이디 :</td>
                <td><asp:Label ID="UserID" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td>비밀번호 :</td>
                <td>
                    <asp:TextBox ID="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="비밀번호를 입력해주세요." ToolTip="비밀번호를 입력해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="PasswordRegular" runat="server" ErrorMessage="비밀번호는 4자 이상 26자 이하여야 합니다." ToolTip="비밀번호는 4자 이상 26자 이하여야 합니다." ControlToValidate="Password" ValidationExpression="\w{4,26}" ValidationGroup="Change">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>비밀번호 확인 :</td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="비밀번호를 한번 더 입력해주세요." ToolTip="비밀번호를 한번 더 입력해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="ConfirmPasswordCompare" runat="server" ErrorMessage="비밀번호가 일치하지 않습니다." ToolTip="비밀번호가 일치하지 않습니다." ControlToCompare="Password" ControlToValidate="ConfirmPassword" ValidationGroup="Change">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>이메일 :</td>
                <td>
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="이메일을 입력해주세요." ToolTip="이메일을 입력해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegular" runat="server" ErrorMessage="이메일 형식이 아닙니다." ToolTip="이메일 형식이 아닙니다." ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Change">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>닉네임 :</td>
                <td>
                    <asp:TextBox ID="NickName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NickNameRequired" runat="server" ControlToValidate="NickName" ErrorMessage="닉네임을 입력해주세요." ToolTip="닉네임을 입력해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="NickNameRegular" runat="server" ErrorMessage="닉네임은 한글로 2자 이상 10자 이하여야 합니다." ToolTip="닉네임은 한글로 2자 이상 10자 이하여야 합니다." ControlToValidate="NickName" ValidationExpression="^[가-힣]{2,10}" ValidationGroup="Account">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>보안 질문 :</td>
                <td>
                    <asp:DropDownList ID="Question" runat="server">
                        <asp:listitem Text="보안 질문을 선택해주세요" Value=""></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 동물은?" Value="animal"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 색깔은?" Value="color"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 음식은?" Value="food"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 노래는?" Value="music"></asp:listitem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="보안 질문을 선택해주세요." ToolTip="보안 질문을 선택해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>보안 정답 :</td>
                <td>
                    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="보안 정답을 입력해주세요." ToolTip="보안 정답을 입력해주세요." ValidationGroup="Change">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <asp:Button ID="ChangeInfo" runat="server" Text="계정 정보 변경" Height="50px" Width="130px" OnClick="ChangeInfo_Click" />
        &nbsp;&nbsp;
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

