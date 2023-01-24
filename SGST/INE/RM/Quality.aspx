<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Quality.aspx.cs" Inherits="SGST.INE.RM.Quality" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ConfirmOK() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue for OK Status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ConfirmReject() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue for Reject Status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ConfirmScraped() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue for Scraped Status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ConfirmHold() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue for Scraped Status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <style>
        div.scroll {
            width: auto;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
            text-align: center;
        }
    </style>
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-ban"></i></h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-info"></i></h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-warning"></i></h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i></h4>
            </div>
        </div>
        <section class="content-header">
            <h1>RM Quality       </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:CheckBox ID="chkIsMRN" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsMRN_CheckedChanged" Visible="True" />
                                    MRN Quality
                                </label>
                            </div>
                            <div class="form-group">
                                <label>Scan Item Barcode : </label>
                                <asp:TextBox ID="txtReelID" runat="server" class="form-control"
                                    placeholder="Scan Item Barcode" autocomplete="off" OnTextChanged="txtReelID_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Inventory Quantity. : </label>
                                <asp:Label ID="lblInvQuantity" runat="server" Text="0">                                      
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label>Batch No. : </label>
                                <asp:Label ID="lblBatch" runat="server" Text="-">                                      
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label>Item Code : </label>
                                <asp:Label ID="lblItemCode" runat="server" Text="">                                      
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                 <label>MPN : </label>
                                <asp:Label ID="lblMPN" runat="server" Text="">                                      
                                </asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                 <label>Make : </label>
                                <asp:Label ID="lblMake" runat="server" Text="">                                      
                                </asp:Label>
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                 <label>Part Desc : </label>
                                <asp:Label ID="lblPartDesc" runat="server" Text="">                                      
                                </asp:Label>
                                <br />
                                <br />
                                <asp:Panel ID="pn1" runat="server">
                                    <label>
                                        Quality for full batch :
                                    <br />
                                        YES                 
                                    <input type="radio" name="r3" id="rdyes" class="flat-red" runat="server" />
                                    </label>
                                    &nbsp;&nbsp;<label>No:                 
                                    <input type="radio" name="r3" id="rdNo" class="flat-red" runat="server" />
                                    </label>
                                    &nbsp;&nbsp;
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Approved MPN List</h3>
                                    </div>
                                    <div class="row scroll">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="gvmakeData" runat="server"
                                                    class="table table-striped table-bordered table-hover GridPager"
                                                    OnRowCancelingEdit="gvMultiDefect_RowCancelingEdit" OnRowEditing="gvMultiDefect_RowEditing"
                                                    OnRowUpdating="gvMultiDefect_RowUpdating" BorderStyle="Solid"
                                                    AutoGenerateColumns="false">
                                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MAKE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MAKE" runat="server" Text='<%#Eval("MAKE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MPN">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MPN" runat="server" Text='<%#Eval("MPN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Quality Parameter List</h3>
                                    </div>
                                    <div class="row scroll">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="gvMultiDefect" runat="server"
                                                    class="table table-striped table-bordered table-hover GridPager"
                                                    OnRowCancelingEdit="gvMultiDefect_RowCancelingEdit" OnRowEditing="gvMultiDefect_RowEditing"
                                                    OnRowUpdating="gvMultiDefect_RowUpdating" BorderStyle="Solid"
                                                    AutoGenerateColumns="false">
                                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Desc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ITEMDESC" runat="server" Text='<%#Eval("ITEM_DESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zone">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZONE" runat="server" Text='<%#Eval("ZONE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Specification">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SPECIFICATION" runat="server" Text='<%#Eval("SPECIFICATION") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="METHODS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="METHODS" runat="server" Text='<%#Eval("METHOD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T1">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtt1" runat="server" Text='<%#Eval("T1") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("T1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T2">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtt2" runat="server" Text='<%#Bind("T2") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("T2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T3">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtT3" runat="server" Text='<%#Bind("T3") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("T3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T4">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtt4" runat="server" Text='<%#Bind("T4") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("T4") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="T5">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtT5" runat="server" Text='<%#Bind("T5") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("T5") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnedit" CommandName="EDIT" runat="server" Text="EDIT" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Button ID="btnupdate" CommandName="Update" runat="server" Text="Update" />
                                                                <asp:Button ID="btnCancel" CommandName="Cancel" runat="server" Text="Cancel" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Is Any Deviation?(If Yes, Enter remarks) : </label>
                                <asp:DropDownList ID="drpResult" runat="server" class="form-control Select2"
                                    autocomplete="off">
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Enter Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control"
                                    placeholder="Scan remarks" autocomplete="off">
                                </asp:TextBox>
                            </div>
                            
                            <div class="col-xs-3">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat" Text="OK"
                                    OnClick="btnOK_Click" OnClientClick="return ConfirmOK()" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reject" OnClick="btnReject_Click" OnClientClick="ConfirmReject()" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnScraped" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Rework" OnClick="btnScraped_Click" OnClientClick="ConfirmScraped()" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnHold" runat="server" class="btn btn-primary btn-block btn-flat" Text="Scraped"
                                    OnClick="btnHold_Click" Visible="true" OnClientClick="ConfirmHold()" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div class="col-xs-3">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>
                        <asp:HiddenField ID="hidBatch" runat="server" />
                        <asp:HiddenField ID="hidPartCode1" runat="server" />
                        <asp:HiddenField ID="hidItemLineNo" runat="server" />
                        <asp:HiddenField ID="hidGRNNo" runat="server" />
                        <asp:HiddenField ID="hidQty" runat="server" />
                        <asp:HiddenField ID="hidCode" runat="server" />
                        <asp:HiddenField ID="hidMake" runat="server" />
                        <asp:HiddenField ID="hidMPN" runat="server" />
                        <asp:HiddenField ID="hidPartDesc" runat="server" />
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
