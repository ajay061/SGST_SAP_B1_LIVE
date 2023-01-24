<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ToolMaster.aspx.cs"
    Inherits="SGST.INE.WIP.ToolMaster" EnableEventValidation="false" %>

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
            if (iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do want to update the printed qty")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
                    <h3 class="box-title">Tool Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Tool Id : </label>
                                <asp:TextBox ID="txttoolid" placeholder="Enter Tool ID" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">
                                   
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Description : </label>
                                <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Make : </label>
                                <asp:TextBox ID="txtmake" runat="server" placeholder="Enter Make"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Model No. : </label>
                                <asp:TextBox ID="txtModelNo" runat="server" placeholder="Enter Model No."
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Equipment SR No. : </label>
                                <asp:TextBox ID="txtEquipSRNo" runat="server" placeholder="Enter Equipment Sr No."
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Usage Range : </label>
                                <asp:TextBox ID="txtUsageRange" runat="server" placeholder="Enter Usage Range"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Accuracy : </label>
                                <asp:TextBox ID="txtAccuracy" runat="server" placeholder="Enter Accuracy"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Calibration Date : </label>
                                <asp:TextBox ID="txtCalibrationDate" runat="server" placeholder="Enter Calibration Date"
                                    autocomplete="off" class="form-control" data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Alert Date : </label>
                                <asp:TextBox ID="txtAlertDate" runat="server" placeholder="Enter Alert Date"
                                    autocomplete="off" class="form-control" data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Type : </label>
                                <asp:DropDownList ID="ddltype" runat="server" class="form-control select2" Style="width: 100%;">
                                    <asp:ListItem Text="" Value="0">--Select Type--</asp:ListItem>
                                    <asp:ListItem Text="Stencil - XF331 TCI" Value="1">Stencil</asp:ListItem>
                                    <asp:ListItem Text="Squeeze Front" Value="2">Squeeze</asp:ListItem>
                                    <asp:ListItem Text="Squeeze Front" Value="3">WAVE</asp:ListItem>
                                    <asp:ListItem Text="ICT Fixture - XF331 TCI" Value="4">Others</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Quantity : </label>
                                <asp:TextBox ID="txtqty" runat="server" placeholder="Enter Qty" class="form-control"
                                    onkeypress="javascript:return isNumber(event)" Style="width: 100%;">
                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary btn-block btn-flat"
                                        OnClick="btnPrint_Click" OnClientClick="Confirm()" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Select File</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-primary btn-block btn-flat" />
                                    <br />
                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload Details" class="btn btn-primary btn-block btn-flat" />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hidUID" runat="server" />
                <asp:HiddenField ID="hidUpdate" runat="server" />
                <asp:HiddenField ID="hidToolID" runat="server" />
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <div class="col-md-4">
                        <b>Tool ID : 
                        <asp:DropDownList ID="drpToolFilter" runat="server" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="drpToolFilter_SelectedIndexChanged"></asp:DropDownList>
                        </b>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvToolId" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvToolId_RowCommand" OnPageIndexChanging="gvToolId_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px"
                                            HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Tool Id" DataField="TOOL_ID"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Description" DataField="DESCRIPTION"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                         <asp:BoundField HeaderText="Make" DataField="Make"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                         <asp:BoundField HeaderText="Model No" DataField="ModelNo"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Equipment SR No" DataField="EqipSRNo"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Usage Range" DataField="UsageRange"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Accuracy" DataField="Accuracy"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Calibration Date" DataField="CalibrationDate"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="AlertDate" DataField="AlertDate"
                                            ItemStyle-Width="300px" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Type" DataField="TYPE"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px" />
                                        <asp:BoundField HeaderText="Qty" DataField="QTY"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px" />
                                        <asp:BoundField HeaderText="Used Qty" DataField="USED_QTY"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px" />




                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("TOOL_ID")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("TOOL_ID")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
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
