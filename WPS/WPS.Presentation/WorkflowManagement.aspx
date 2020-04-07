<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkflowManagement.aspx.cs" Inherits="WPS.Presentation.WorkflowManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br><br>
    
    <label>Work Element: </label>
    <asp:DropDownList id="Select1" name="Select1" OnSelectedIndexChanged="Select1_SelectedIndexChanged" runat="server" AutoPostBack="True" >
        <asp:ListItem Value="-1"> ---ALL--- </asp:ListItem>
    </asp:DropDownList>


    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <% if (_stateless_id < 1){%>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Order Title</th>
                            <th>Last State</th>
                            <th>Request Date</th>
                            <th>Termination Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        <% foreach (var stateless in statelesses)
                            { %>
                        <tr>
                            <td><%=stateless.Id%></td>
                            <td><%=stateless.OrderTitle%></td>
                            <td><%=stateless.LastState%></td>
                            <td><%=stateless.RequestDate%></td>
                            <td><%=stateless.TerminationDate%></td>
                        </tr>
                        <% }%>
                    </tbody>
                </table>
                <% }else{ %>
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Old State</th>
                            <th>New State</th>
                            <th>Start DateTime</th>
                            <th>End DateTime</th>
                            <th>Comppleted By</th>
                        </tr>
                    </thead>
                    <tbody>
                        <% int i = 0;var prevtime=DateTime.Now; foreach (var transition in _transitions){
                                if (i == 0) prevtime = transition.TriggeredDate; i++; %>
                        <tr>
                            <td><%=i%></td>
                            <td><%=transition.SourceState%></td>
                            <td><%=transition.DestnationState%></td>
                            <td><%=prevtime%></td>
                            <td><%=transition.TriggeredDate%></td>
                            <td><%=transition.TriggeredBy%></td>
                        </tr>
                        <% prevtime = transition.TriggeredDate;}%>
                    </tbody>
                </table>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
