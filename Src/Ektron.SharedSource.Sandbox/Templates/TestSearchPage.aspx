<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSearchPage.aspx.cs" Inherits="Ektron.SharedSource.Sandbox.Templates.TestSearchPage" %>
<%@ Import Namespace="Ektron.Cms.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Search Test</h1>
        <fieldset>
            <legend>Findings</legend>
            <ul>
                <li>Hardcoded text (i.e., text in the markup) on Page Builder pages <em>will</em> be indexed and searched.</li>
                <li>Hardcoded text on Smart Form pages and Html Content pages <em>will not</em> be indexed and searched.</li>
                <li>Grouped fields are indexed and searched just like any other Smart Form field. Just keep in mind that the inner text of the XML element is what get indexed and searched.</li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>Input</legend>
            <p>
                <asp:Panel runat="server" ID="pnlInput" DefaultButton="btnSearch">
                    <asp:Label runat="server" AssociatedControlID="txtQuery">Query:</asp:Label>
                    <asp:TextBox runat="server" ID="txtQuery"/>
                    <asp:Button runat="server" ID="btnSearch" Text="Search"/>
                </asp:Panel>
            </p>
        </fieldset>
        <fieldset>
            <legend>Output</legend>
            <asp:ListView runat="server" ID="lvResults" ItemType="Ektron.Cms.Search.SearchResultData">
                <ItemTemplate>
                    <p>
                        <a href="<%# Item.GetValue(SearchContentProperty.QuickLink) %>" target="_blank">
                            <strong>
                                <%# Item.GetValue(SearchContentProperty.Title) %>
                            </strong>
                        </a>
                        (<%# Item.GetValue(SearchContentProperty.Id) %>)
                        <br/>
                        <em>
                            <%# Item.GetValue(SearchContentProperty.QuickLink) %>
                        </em>
                        <br/>
                        <%# Item.GetValue(SearchContentProperty.HighlightedSummary) %>
                    </p>
                </ItemTemplate>
            </asp:ListView>
        </fieldset>
    </div>
    </form>
</body>
</html>
