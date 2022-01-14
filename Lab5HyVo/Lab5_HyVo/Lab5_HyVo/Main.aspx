<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Lab5_HyVo.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddlImport" runat="server" AutoPostBack="True" Height="30px" Width="200px" OnSelectedIndexChanged="ddlImport_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        Movie Title: 
        <asp:Label ID="lblMovie" runat="server" Text=""></asp:Label>
        <br />
        Year Released:
        <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
        <br />
        Review Score:
        <asp:Label ID="lblScore" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
