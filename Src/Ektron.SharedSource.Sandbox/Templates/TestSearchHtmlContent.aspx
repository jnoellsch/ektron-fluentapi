<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSearchHtmlContent.aspx.cs" Inherits="Ektron.SharedSource.Sandbox.Templates.TestSearchHtmlContent" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Html Content Test</h1>
            <fieldset>
                <legend>Hardcode</legend>
                <p>Red</p>
            </fieldset>
            <fieldset>
                <legend>Html Content</legend>
                <asp:Literal runat="server" ID="litContent"/>
            </fieldset>
        </div>
    </form>
</body>
</html>
