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
        <p>
            Hardcoded text (i.e., text in the markup) <em>will</em> be indexed for PageBuilder pages. 
            It <em>will not</em> be indexed on SmartForm and HtmlContent pages.
        </p>
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
                        <a href="<%# Item.GetValue(SearchContentProperty.QuickLink) %>">
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
