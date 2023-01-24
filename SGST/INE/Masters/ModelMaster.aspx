<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelMaster.aspx.cs"
    MasterPageFile="~/Main.Master" Inherits="SGST.INE.Masters.ModelMaster" %>

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
            <h1>MASTER</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">FG Model Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server"
                                    autocomplete="off" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Model Name : </label>
                                <asp:TextBox ID="txtModelName" runat="server" placeholder="Enter Model Name"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="40">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Model Type : </label>
                                <asp:TextBox ID="txtModelType" runat="server" placeholder="Enter Model Type"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>EAN : </label>
                                <asp:TextBox ID="txtEAN" runat="server"
                                    onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" MaxLength="20" placeholder="Enter EAN" class="form-control" Style="width: 100%;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>MRP : </label>
                                <asp:TextBox ID="txtMRP" runat="server" placeholder="Enter MRP"
                                    autocomplete="off" class="form-control" Style="width: 100%;" onkeypress="javascript:return isNumber(event)" MaxLength="6">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>SW Version : </label>
                                <asp:TextBox ID="txtSWVersion" runat="server"
                                    placeholder="Enter SW VERSION"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="80">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Device Vendor : </label>
                                <asp:TextBox ID="txtVendor" runat="server"
                                    placeholder="Enter Vendor"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="80">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Warranty in Days : </label>
                                <asp:TextBox ID="txtWarentyInDaya" runat="server"
                                    placeholder="Enter Pack Size" TextMode="Number" Text="1"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="80">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>GB Wt : </label>
                                <asp:TextBox ID="txtWt" runat="server"
                                    placeholder="Enter Wt" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Tol Plus : </label>
                                <asp:TextBox ID="txtTolPlus" runat="server"
                                    placeholder="Enter Tolerance Plus" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Tol Minus : </label>
                                <asp:TextBox ID="txtTolMinus" runat="server"
                                    placeholder="Enter Tolerence Minus" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Carton Wt : </label>
                                <asp:TextBox ID="txtCartonWt" runat="server"
                                    placeholder="Enter Wt" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Carton Tol Plus : </label>
                                <asp:TextBox ID="txtCartonTolPlus" runat="server"
                                    placeholder="Enter Tolerance Plus" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Carton Tol Minus : </label>
                                <asp:TextBox ID="txtCartonTolMinus" runat="server"
                                    placeholder="Enter Tolerence Minus" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Empty Carton WT : </label>
                                <asp:TextBox ID="txtEmptyCartonWT" runat="server"
                                    placeholder="Enter Empty Carton WT" Text="0" onkeypress="javascript:return isNumber(event)"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Wave Tool Validate : </label>
                                <asp:CheckBox ID="chkWaveToolValidate" runat="server" Text="Wave Tool Validate"
                                    class="form-control" Style="width: 100%;" MaxLength="10"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ASN Mac2 Shows : </label>
                                <asp:CheckBox ID="chkASNMac2" runat="server" Text="ASN Mac2 Shows"
                                    class="form-control" Style="width: 100%;" MaxLength="10"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>No OF Mac Address : </label>
                                <asp:TextBox ID="txtMacAddress" runat="server"
                                    class="form-control" Style="width: 100%;" Text="1" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Report Location : </label>
                                <asp:TextBox ID="txtReportLocation" runat="server"
                                    class="form-control" Style="width: 100%;" Text="" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>PIN No : </label>
                                <asp:TextBox ID="txtPINNO" runat="server"
                                    class="form-control" Style="width: 100%;" Text="" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Report Lot No. : </label>
                                <asp:TextBox ID="txtReportLotNo" runat="server"
                                    class="form-control" Style="width: 100%;" Text="" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>HW Version : </label>
                                <asp:TextBox ID="txtHWVersion" runat="server"
                                    class="form-control" Style="width: 100%;" Text="" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>ASN Model No. : </label>
                                <asp:TextBox ID="txtASnModelNo" runat="server"
                                    class="form-control" Style="width: 100%;" Text="" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Duplicate Columns Name during SN Upload </label>
                                <asp:DropDownList ID="drpColumnName" runat="server"
                                    class="form-control" Style="width: 100%;">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>GRPONSN</asp:ListItem>
                                    <asp:ListItem>KEY_PART_NO</asp:ListItem>
                                    <asp:ListItem>Wifi_MAC</asp:ListItem>
                                    <asp:ListItem>Wireless_SSID</asp:ListItem>
                                    <asp:ListItem>Pre_password</asp:ListItem>
                                    <asp:ListItem>ACS_DATA</asp:ListItem>
                                    <asp:ListItem>HDCP_FILE_NAME</asp:ListItem>
                                    <asp:ListItem>COL1</asp:ListItem>
                                    <asp:ListItem>COL2</asp:ListItem>
                                    <asp:ListItem>COL3</asp:ListItem>
                                    <asp:ListItem>COL4</asp:ListItem>
                                    <asp:ListItem>COL5</asp:ListItem>
                                    <asp:ListItem>COL6</asp:ListItem>
                                    <asp:ListItem>COL7</asp:ListItem>
                                    <asp:ListItem>COL8</asp:ListItem>
                                    <asp:ListItem>COL9</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary btn-block btn-flat" OnClick="btnAdd_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" style="width: 100%; overflow: scroll">
                                <asp:GridView ID="gvDuplicateColumn" runat="server" ShowHeader="true"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" OnRowCommand="gvDuplicateColumn_RowCommand">
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Duplicate Column" DataField="duplicateColumn" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("duplicateColumn")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
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
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlModelName" runat="server"
                                        class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlModelName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:HiddenField ID="hidModelID" runat="server" />
                            <div class="form-group" style="width: 100%; overflow: scroll">
                                <asp:GridView ID="gvModel" runat="server" ShowHeader="true"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvModel_PageIndexChanging" OnRowCommand="gvModel_RowCommand">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Site Code" DataField="SITECODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="FG Item Code" DataField="BOM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Model Name" DataField="MODEL_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" SortExpression="MODEL_CODE" />
                                        <asp:BoundField HeaderText="Model Type" DataField="MODEL_DESC" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="SW Version" DataField="SWVERSION" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="EAN Code" DataField="EAN_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="MRP" DataField="MRP" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Vendor Device" DataField="VENDOR_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Warrenty In Days" DataField="WARENTYINDAYS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="GBWT" DataField="GrossWT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Tol Plus" DataField="TolPlus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Tol Minus" DataField="TolMinus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Carton Wt" DataField="Carton_wt" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Carton Tol Plus" DataField="CTolPlus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Catton Tol Minus" DataField="CTolMinus" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Carton Wt(Empty Box)" DataField="ECARTONWT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Wave Tool Validate" DataField="WAVE_TOOL_VALIDATE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="ASN Mac2 Shows" DataField="IsASNMac2Required" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="No Of mac Address" DataField="MACADDRESSCOUNT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Report Location" DataField="REPORT_LOCATION" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Pin No" DataField="PIN_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Report Lot No" DataField="REPORT_LOT_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="HW version" DataField="HWVERSION" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="ASN Model No" DataField="ASN_MODEL_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("MODEL_CODE")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("MODEL_CODE")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
