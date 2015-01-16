<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSearchPageBuilder.aspx.cs" Inherits="Ektron.SharedSource.Sandbox.Templates.TestSearchPageBuilder" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/PageHost.ascx" TagPrefix="CMS" TagName="PageHost" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/DropZone.ascx" TagPrefix="CMS" TagName="DropZone" %>
<%@ Register Assembly="Ektron.Cms.Widget" Namespace="Ektron.Cms.PageBuilder" TagPrefix="CMS" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <CMS:PageHost runat="server" ID="ph"/>
        <div>
            <h1>Page Builder Test</h1>
            <fieldset>
                <legend>Hardcode</legend>
                <p>Blue</p>
            </fieldset>
            <fieldset>
                <legend>Widget</legend>
                <CMS:DropZone runat="server" ID="CenterDropZone"/>
            </fieldset>
        </div>
    </form>
</body>
</html>
