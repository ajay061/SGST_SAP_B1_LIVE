<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" EnableEventValidation="false"
    Inherits="SGST.INE.UserManagement.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>

    <script>
        // WRITE THE VALIDATION SCRIPT.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
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
        <section class="content-header">
            <h1>User Management     
            </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">User Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>User ID : </label>
                                <asp:TextBox ID="txtUserID" runat="server" class="form-control"
                                    placeholder="Enter User ID" data-placeholder="User ID" MaxLength="40" autocomplete="off"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>User Name : </label>
                                <asp:TextBox ID="txtUserName"
                                    placeholder="Enter User Name" runat="server" class="form-control"
                                    data-placeholder="User Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Password : </label>
                                <asp:TextBox ID="txtPassword" placeholder="Enter Password" runat="server" class="form-control"
                                    data-placeholder="User Password" TextMode="Password" MaxLength="100" autocomplete="off"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label>Contact No. : </label>
                                <asp:TextBox ID="txtContactNo" runat="server" placeholder="Enter Contact No"
                                    class="form-control" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" MaxLength="15"></asp:TextBox>

                            </div>
                            <asp:CheckBox ID="chkRememberMe" runat="server" />
                            <asp:Label ID="Label1" runat="server" Text="Active"></asp:Label>

                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Department : </label>
                                <asp:DropDownList ID="drpDepartment" runat="server" class="form-control select2" Style="width: 100%;">
                                    <asp:ListItem>--Select Department No--</asp:ListItem>
                                    <asp:ListItem>RM</asp:ListItem>
                                    <asp:ListItem>WIP</asp:ListItem>
                                    <asp:ListItem>FG</asp:ListItem>
                                    <asp:ListItem>PLANT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>User Type : </label>
                                <asp:DropDownList ID="drpUserType" runat="server" class="form-control select2" Style="width: 100%;">
                                    <asp:ListItem>--Select User Type--</asp:ListItem>
                                    <asp:ListItem>ADMIN</asp:ListItem>
                                    <asp:ListItem>STANDARD</asp:ListItem>
                                    <asp:ListItem>SUPERVISOR</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Email ID : </label>
                                <asp:TextBox ID="txtemailid" runat="server" placeholder="Enter Email ID"
                                    class="form-control" MaxLength="50" data-placeholder="Email ID" autocomplete="off"></asp:TextBox>
                            </div>
                            <!-- /.form-group -->
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hidUID" runat="server" />
                <asp:HiddenField ID="hidUpdate" runat="server" />
                <asp:HiddenField ID="hidPassword" runat="server" />
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <b>User ID : 
                        <asp:DropDownList ID="drpUserFilter" runat="server" AutoPostBack="true"  class="form-control select2" OnSelectedIndexChanged="drpUserFilter_SelectedIndexChanged"></asp:DropDownList>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvUserMaster" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridView1_RowCommand"
                                    OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"
                                        FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="SN" DataField="SNO" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Site Code" DataField="SiteCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="User ID" DataField="USERID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="User Type" DataField="USERTYPE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="User Name" DataField="USERNAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Email" DataField="EMAIL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Contact No." DataField="CONTACTNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("SNO")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("SNO")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
