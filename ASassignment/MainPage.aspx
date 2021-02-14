<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ASassignment.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Welcome"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Comment your enquiry"></asp:Label>
            <br />
            <asp:TextBox ID="TBEnquiry" runat="server" Height="62px" Width="158px"></asp:TextBox>
            <br />
            <asp:Label ID="LBEnquiry" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit Enquiry" />
            <br />
            <asp:Button ID="LogoutBTN" runat="server" OnClick="LogoutBTN_Click" Text="Logout" />
        </div>
    </form>
</body>
</html>
