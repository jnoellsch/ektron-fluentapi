<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MappingTests.aspx.cs" Inherits="Ektron.SharedSource.Sandbox.MappingTests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater runat="server" ID="rptrContent" ItemType="Ektron.SharedSource.Sandbox.Content">
            <ItemTemplate>
                <div><%# Item.Title %></div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
