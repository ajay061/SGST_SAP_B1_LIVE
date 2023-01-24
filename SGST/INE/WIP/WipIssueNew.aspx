<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WipIssueNew.aspx.cs"
    Inherits="SGST.INE.WIP.WipIssueNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue with new qty?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
            <h1>MATERIAL ISSUANCE</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Line ID : </label>
                                <asp:DropDownList ID="drpLineID" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Scan Machine ID/Work Station ID : </label>
                                <asp:TextBox ID="txtScanMachineID" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35"
                                    OnTextChanged="txtScanMachineID_TextChanged" autocomplete="off" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Machine Name/Work Station Name : </label>
                                <asp:Label ID="lblMachineName" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                                <label>Model Name/ Process Name : </label>
                                <asp:Label ID="lblModelNo" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Program ID : </label>
                                <asp:DropDownList ID="drpProgramID" runat="server" class="form-control"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpProgramID_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary btn-block btn-flat" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnGetDetails" runat="server" Text="GetDetails" OnClick="btnGetDetails_Click" class="btn btn-primary btn-block btn-flat" />
                            </div>
                            <div class="col-xs-4">
                                <asp:CheckBox ID="chkAutoRefrsh" runat="server" AutoPostBack="true" Text="Auto Refresh" Checked="false"
                                    class="btn-block btn-flat" OnCheckedChanged="chkAutoRefrsh_CheckedChanged" />
                                <%-- for every three minute--%>
                                <asp:ScriptManager ID ="sc1" runat="server"></asp:ScriptManager>
                                <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick" Enabled="false">
                                </asp:Timer> 
                            </div>
                            <br />
                            <br />
                            <asp:RadioButton ID="rdBIN" GroupName="Group1" Text="BIN" value="B" runat="server" Checked="true" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdTool" GroupName="Group1" Text="TOOL" value="N" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdFeeder" GroupName="Group1" Text="FEEDER" value="F" runat="server"
                                OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            <br />
                            <br />
                        </div>
                    </div>
                    <div class="row scroll">
                        <div class="col-md-12">
                            <asp:GridView ID="gvProfileMaster" runat="server"
                                CssClass="display"
                                AutoGenerateColumns="false">
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
                                    <asp:BoundField HeaderText="ID" DataField="SR NO." Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                    <asp:BoundField HeaderText="Program ID" DataField="PROGRAM_ID" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                    <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Feeder Type" DataField="FeederNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" Visible="false" />
                                    <asp:BoundField HeaderText="Feeder Location" DataField="FeederLoc" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Tool ID" DataField="ToolID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Scanned Barcode" DataField="RM_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Scanned Tool" DataField="ToolID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                    <asp:BoundField HeaderText="Material Qty" DataField="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Scan Bin : </label>
                            <asp:TextBox ID="txtBin" runat="server" class="form-control" Style="width: 100%;"
                                OnTextChanged="txtBin_TextChanged" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Tool/Fixture/Jig ID : </label>
                            <asp:TextBox ID="txtToolID" runat="server"
                                class="form-control" Style="width: 100%;" Height="35"
                                OnTextChanged="txtToolID_TextChanged" autocomplete="off" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Feeder Location : </label>
                            <asp:TextBox ID="txtFeederLocation" runat="server"
                                class="form-control" Style="width: 100%;" Height="35"
                                OnTextChanged="txtFeederLocation_TextChanged" AutoPostBack="true" autocomplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group" runat="server" visible="false">
                            <label>Feeder ID : </label>
                            <asp:TextBox ID="txtFeederID" runat="server"
                                class="form-control" Style="width: 100%;" Height="35"
                                OnTextChanged="txtFeederID_TextChanged" autocomplete="off" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Scan barcode : </label>
                            <asp:TextBox ID="txtRmBarcode" runat="server" class="form-control" Style="width: 100%;" AutoPostBack="true"
                                autocomplete="off" OnTextChanged="txtRmBarcode_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group" runat="server" visible="false">
                            <label>Material Qty :</label>
                            <asp:Label ID="lblMatQty" runat="server" Style="width: 100%;" Text="0"></asp:Label>
                            <br />
                            <label>Required Qty :</label>
                            <asp:TextBox ID="txtAllocatedQty" runat="server" class="form-control" Style="width: 100%;"
                                onkeypress="javascript:return isNumber(event)" AutoPostBack="true"
                                MaxLength="10" OnTextChanged="txtAllocatedQty_TextChanged"
                                autocomplete="off"></asp:TextBox>
                            <br />
                            <label>Excess Qty :</label>
                            <asp:Label ID="lblExcessQty" runat="server" class="form-control" Style="width: 100%;"
                                onkeypress="javascript:return isNumber(event)"
                                maxlength="10"
                                autocomplete="off"></asp:Label>
                            <br />
                            <div class="col-xs-4">
                                <asp:Button ID="btnUpdateQty" runat="server" Text="Allocate"
                                    OnClick="btnUpdateQty_Click" class="btn btn-primary btn-block btn-flat"
                                    OnClientClick="Confirm()" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <asp:HiddenField ID="HidLINEID" runat="server" />
                    <asp:HiddenField ID="HIDMACHINEID" runat="server" />
                    <asp:HiddenField ID="hidProfileID" runat="server" />
                    <asp:HiddenField ID="hidBatchNO" runat="server" />
                    <asp:HiddenField ID="hidQuantity" runat="server" />
                    <asp:HiddenField ID="hidbin" runat="server" />
                    <asp:HiddenField ID="hidmpid" runat="server" />
                    <asp:HiddenField ID="hidInvoice" runat="server" />
                    <asp:HiddenField ID="hidQty" runat="server" />
                    <asp:HiddenField ID="hidRemQty" runat="server" />
                    <asp:HiddenField ID="hidExpiryDate" runat="server" />
                    <asp:HiddenField ID="hiddiscription" runat="server" />
                    <asp:HiddenField ID="hidMFGDate" runat="server" />
                    <asp:HiddenField ID="hidFGItem" runat="server" />
                    <asp:HiddenField ID="hidPartCode" runat="server" />
                    <asp:HiddenField ID="hidfeederlocation" runat="server" />
                    <asp:HiddenField ID="hidbarcode" runat="server" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>

