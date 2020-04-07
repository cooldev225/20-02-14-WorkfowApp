<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="WPS.Presentation.NewRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" style="padding: 20px;">
     <div class="row">
         <div class="col-lg-12">
            <div class="form-group">
                <div class="col-lg-2">
                    <label>Created By: </label>
                </div>
                <div class="col-lg-10">
                    <input id="username" name="username" value="Anca"/>
                </div>
            </div>
             <div class="form-group">
                <div class="col-lg-2">
                    <label>Email: </label>
                </div>
                <div class="col-lg-10">
                    <input id="emailaddress" name="emailaddress" value="www@example.com"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-2">
                    <label>Title: </label>
                </div>
                <div class="col-lg-10">
                    <input id="ordertitle" name="ordertitle" value="Working Element"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <textarea id="orderdesc" name="orderdesc" placeholder="Description"></textarea>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <asp:Button ID="btnSubmit" runat="server" Text="Request a new element" CssClass="btn btn-primary"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
         </div>
    </div>
    --------------------------------------------Place: Designated--------------------------------------------------
    <div class="row">
         <div class="col-lg-12">
            <div class="form-group">
                <div class="col-lg-2">
                    <label>Designator: </label>
                </div>
                <div class="col-lg-10">
                    <input id="designator" name="designator" value="Designated user"/>
                </div>
            </div>
             <div class="form-group">
                <div class="col-lg-2">
                    <label>Comment: </label>
                </div>
                <div class="col-lg-10">
                    <input id="designatiedcomment" name="designatiedcomment" value="...in designated place"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <asp:Button ID="Button1" runat="server" Text="Create" CssClass="btn btn-primary"
                        OnClick="btnCreate_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Review" CssClass="btn btn-primary"
                        OnClick="btnReview_Click" />
                    <asp:Button ID="Button5" runat="server" Text="Reject" CssClass="btn btn-primary"
                        OnClick="btnRejectFromDesignated_Click" />
                </div>
            </div>
         </div>
    </div>

    --------------------------------------------Place: Reviewed--------------------------------------------------
    <div class="row">
         <div class="col-lg-12">
            <div class="form-group">
                <div class="col-lg-2">
                    <label>Designator: </label>
                </div>
                <div class="col-lg-10">
                    <input id="revieweduser" name="revieweduser" value="Reviewed user"/>
                </div>
            </div>
             <div class="form-group">
                <div class="col-lg-2">
                    <label>Comment: </label>
                </div>
                <div class="col-lg-10">
                    <input id="reviewedcomment" name="reviewedcomment" value="...in reviewed place"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <asp:Button ID="Button3" runat="server" Text="Approve" CssClass="btn btn-primary"
                        OnClick="btnApproveFromReviewed_Click" />
                    <asp:Button ID="Button4" runat="server" Text="Reject" CssClass="btn btn-primary"
                        OnClick="btnRejectFromReviewed_Click" />
                </div>
            </div>
         </div>
    </div>

    --------------------------------------------Place: Approved--------------------------------------------------
    <div class="row">
         <div class="col-lg-12">
            <div class="form-group">
                <div class="col-lg-2">
                    <label>Designator: </label>
                </div>
                <div class="col-lg-10">
                    <input id="approveduser" name="approveduser" value="Approved user"/>
                </div>
            </div>
             <div class="form-group">
                <div class="col-lg-2">
                    <label>Comment: </label>
                </div>
                <div class="col-lg-10">
                    <input id="approvedcomment" name="approvedcomment" value="... in approved place"/>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <asp:Button ID="Button6" runat="server" Text="Assign" CssClass="btn btn-primary"
                        OnClick="btnAssignFromApprove_Click" />
                </div>
            </div>
         </div>
    </div>
</asp:Content>
