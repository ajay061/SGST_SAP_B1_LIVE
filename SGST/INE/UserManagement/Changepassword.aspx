<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="SGST.INE.UserManagement.Changepassword" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-ban"></i>Alert!</h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-info"></i>Alert!</h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-warning"></i>Alert!</h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i>Alert!</h4>
            </div>
        </div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>User Management
            </h1>

        </section>

        <!-- Main content -->
        <section class="content">

            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Change Password</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>User ID : </label>
                                <asp:TextBox ID="txtUserId" runat="server" class="form-control" placeholder="Enter user id"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>New Password : </label>
                                <asp:TextBox ID="txtNewPassword" runat="server" class="form-control"
                                    placeholder="Enter Password" MaxLength="40" TextMode="Password"
                                    TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnSave" runat="server" Text="Save"
                                    class="btn btn-primary btn-block btn-flat" TabIndex="4"
                                    OnClick="btnSave_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset"
                                    class="btn btn-primary btn-block btn-flat" TabIndex="7" OnClick="btnReset_Click" />
                            </div>
                            <!-- /.form-group -->
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">                      
                            <div class="form-group">
                                <label>Confirm Password : </label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control"
                                    placeholder="Enter new password" TabIndex="2" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <asp:HiddenField ID="hidUID" runat="server" />
                    <asp:HiddenField ID="hidUpdate" runat="server" />
                    <asp:HiddenField ID="hidUserType" runat="server" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>
