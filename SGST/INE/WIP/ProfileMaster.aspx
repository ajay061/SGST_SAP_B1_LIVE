<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master"
    EnableEventValidation="false" CodeBehind="ProfileMaster.aspx.cs" Inherits="SGST.INE.WIP.ProfileMaster" %>

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
            <h1>PROGRAM MASTER</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Create Program</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Program Name : </label>
                                <asp:TextBox ID="txtProgramName" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" Height="35" MaxLength="50">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Select Machine/ Work Station ID : </label>
                                    <asp:DropDownList ID="drpMachine" runat="server"
                                        class="form-control select2" Style="width: 100%;" Height="35">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Part Code : </label>
                                <asp:DropDownList ID="drpPartcode" runat="server"
                                    class="form-control select2" Style="width: 100%;"
                                    Height="35">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-lg-6">
                            <div class="form-group">
                                <label>Feeder Type : </label>
                                <asp:TextBox ID="txtFeederNo" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35" autocomplete="off"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Feeder Location : </label>
                                <asp:TextBox ID="txtFeederLocation" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35" autocomplete="off" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>M/C PCB Qty : </label>
                                <asp:TextBox ID="txtMachinePCBQty" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35" autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Feeder/Tool/Fixture/JIG ID : </label>
                                <asp:TextBox ID="txtTool" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35" autocomplete="off"
                                    OnTextChanged="txtTool_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <!-- /.form-group -->
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary btn-block btn-flat" OnClick="btnadd_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                                <br />
                                <br />
                                <div class="col-xs-4">
                                    <asp:Button ID="btnDownloadData" runat="server" Text="Download" class="btn btn-primary btn-block btn-flat" OnClick="btnDownloadData_Click" Visible="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvProfileMaster" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ID" DataField="P_ID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Program ID" DataField="PROGRAM_ID" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <%--<asp:BoundField HeaderText="Feeder Type" DataField="FeederNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />--%>
                                        <asp:BoundField HeaderText="Feeder Location" DataField="FeederLoc" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Feeder/Tool ID" DataField="ToolID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PCB Qty" DataField="MACHINE_PCB_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("ID")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
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
            <!-- /.box-body -->
            <asp:HiddenField ID="hidUID" runat="server" />
            <asp:HiddenField ID="hidUpdate" runat="server" />
            <asp:HiddenField ID="HIDMACHINEID" runat="server" />

            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Edit Program</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Program ID : </label>
                                <asp:DropDownList ID="drpProgramID" runat="server" class="form-control select2" Style="width: 100%;" Height="35">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnedit" runat="server" Text="Edit" class="btn btn-primary btn-block btn-flat" OnClick="btnedit_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4 col-xs-5">
                                    <asp:Button ID="btnDelProfile" runat="server" Text="Delete Program" class="btn btn-primary btn-block btn-flat" OnClick="btnDelProfile_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
