<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSearchSmartForm.aspx.cs" Inherits="Ektron.SharedSource.Sandbox.Templates.TestSearchSmartForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Smart Form Test</h1>
            <fieldset>
                <legend>Hardcode</legend>
                <p>Yellow</p>
            </fieldset>
            <fieldset>
                <legend>Smart Form</legend>
                <p><asp:Literal runat="server" ID="litColor1"/></p>
                <p><asp:Literal runat="server" ID="litColor2"/></p>
                <fieldset>
                    <legend>Secondary Colors</legend>
                    <asp:ListView runat="server" ID="lvSecondaryColors" ItemType="Ektron.SharedSource.Sandbox.Models.SearchTestModel.SecondaryColor" ItemPlaceholderID="plc">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder runat="server" ID="plc"/>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <span style="color: <%# Item.ColorName %>;">
                                    <%# Item.ColorName %>
                                </span>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </fieldset>
            </fieldset>
        </div>
    </form>
</body>
</html>
