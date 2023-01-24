<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WIPFGRouting.aspx.cs" Inherits="SGST.INE.WIP.FGRouting" ClientIDMode="Static" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this record?"))
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#chkISSFG").click(function () {
                if ($(this).is(":checked")) {
                    $("#SFGQty").removeAttr("disabled");
                } else {
                    $("#SFGQty").attr("disabled", "disabled");
                }
            });
        });
    </script>
     <script type="text/javascript">
         function RemoveSpecialChar(SFGQty) {
             if (SFGQty.value != '' && SFGQty.value.match(/^[\w ]+$/) == null) {
                 SFGQty.value = SFGQty.value.replace(/[\W]/g, '');
                 SFGQty.value = SFGQty.value.replace(/G/g, '');
                 
             }
         }
     </script>
     <script>
         
         function isNumber(evt) {
             var iKeyCode = (evt.which) ? evt.which : evt.keyCode
             if (iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                 return false;
             return true;
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
            <h1 id="head1" runat="server">FG ROUTE MASTER
            </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Create FG Route</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>FG Item Code</label>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlFgItemCode" runat="server"
                                    class="form-control select2" Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFgItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="ItemCode">Route Name</label>
                                <div>
                                    <asp:DropDownList ID="ddlRoute" runat="server"
                                        class="form-control" Style="width: 100%;" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        OR
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtRouteName" runat="server"
                                    class="form-control" Style="width: 100%;" AutoPostBack="true"
                                    OnTextChanged="txtRouteName_TextChanged">                                    
                                </asp:TextBox>
                                <asp:Button ID="btnGetRecord" runat="server" Text="get" class="btn btn-primary btn-block btn-flat" OnClick="btnGetRecord_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>Line ID</label>
                                <asp:DropDownList ID="ddlLineId" runat="server"
                                    class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLineId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label for="ItemCode">Machine Name</label>
                                <asp:DropDownList ID="ddlMachineid" runat="server"
                                    class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMachineid_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>Program ID</label>
                                <asp:DropDownList ID="ddlProfileid" runat="server" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>Sequence</label>
                                <asp:DropDownList ID="ddlSequence" runat="server" class="form-control select2" Style="width: 100%;">
                                    <asp:ListItem Text="--Select a Sequence--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>Rework Sequence</label>
                                <asp:DropDownList ID="ddlReworkSequence" runat="server" class="form-control select2" Style="width: 100%;">
                                    <asp:ListItem Text="--Select a Rework Sequence--" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group ">
                                <asp:CheckBox ID="chkIsEnable" runat="server" class="form-control" Text="    IS ENABLE" />
                            </div>
                            <div class="form-group ">
                                <asp:CheckBox ID="chkISSFG" runat="server" class="form-control" Text="    IS SFG" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>SFG Item Code</label>
                                <asp:TextBox ID="txtSFGItemCode" runat="server" class="form-control" autocomplete="off"
                                    Style="width: 100%;" MaxLength="50">                                    
                                </asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-6">
                            <div class="form-group ">
                                <asp:CheckBox ID="ckhOutScanReq" runat="server" class="form-control" Text="  Out Scan Required" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="chkISLotCreate" runat="server" class="form-control" Text="  Lot Create Here" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="chkIsAutoSampledPic" runat="server" class="form-control" Text="xRay Auto Sampled Allowed" />
                            </div>
                        </div>
                       
                       </div>
                     <div class="row">
                         <div class="col-md-6">
                            <div class="form-group ">
                                <label>SFG Qty</label>
                                <asp:TextBox ID="SFGQty" runat="server" class="form-control" autocomplete="off"
                                    Style="width: 100%;" MaxLength="50" disabled="disabled"  onkeypress="javascript:return isNumber(event)"
                                    onkeydown="return (event.keyCode!=13)" >                                    
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>TMO Part Code</label>
                                <asp:TextBox ID="txtTMOPartCode" runat="server" class="form-control"
                                    Style="width: 100%;" MaxLength="50" autocomplete="off">                                    
                                </asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group col-md-12">
                            <div class="col-xs-4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-primary btn-block btn-flat" OnClick="btnDelete_Click" Visible="false" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnDownloadData" runat="server" Text="Download" class="btn btn-primary btn-block btn-flat" OnClick="btnDownloadData_Click" Visible="true" />
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
                <div class="col-md-12">
                    <div class="row scroll">
                        <%-- <div class="box-body">--%>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="GV_Routing" runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                        class="table table-striped table-bordered table-hover GridPager"
                                        OnRowCommand="GV_Routing_RowCommand" OnPageIndexChanging="GV_Routing_PageIndexChanging">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                        <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                        <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                        <Columns>
                                            <asp:BoundField HeaderText="Sr.No" DataField="ID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Line ID" DataField="LINEID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="FG item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Program ID" DataField="PROFILE_ID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="SEQ" DataField="SEQ" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Update Out Time" DataField="UPDATE_OUT_TIME" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Rework Seq" DataField="REWORK_SEQ" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Machine Name" DataField="MACHINENAME" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="IS SFG" DataField="IS_SFG" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="SFG Item Code" DataField="SFG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                           
                                            <asp:BoundField HeaderText="ENABLE" DataField="ENABLE" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Out Scan Required" DataField="OUT_SCAN_REQ" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Lot Create Here" DataField="IS_LOTCREATE" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Route Name" DataField="ROUTE_NAME" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="TMO Part Code" DataField="TMO_PARTCODE" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                             <asp:BoundField HeaderText="xRay Auto Sampled Allowed" DataField="ISAUTOXRAYSAMPLING" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                             <asp:BoundField HeaderText="SFGQty" DataField="SFGQty" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                        CommandArgument='<%#Eval("ID")%>' Visible="true">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                        CommandArgument="<%# Container.DataItemIndex %>" OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <%-- </div>--%>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hidUID" runat="server" />
            <asp:HiddenField ID="hidUpdate" runat="server" />
        </section>
    </div>
</asp:Content>
