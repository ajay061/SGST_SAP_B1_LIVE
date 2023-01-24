<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WIPFGRollback.aspx.cs"
    Inherits="SGST.INE.WIP.WIPFGRollback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
     <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Repack box?")) {
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
            <h1>FG Box Repacking</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">FG Box Repacking</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scanned Box ID : </label>
                                <asp:TextBox ID="txtBoxID" runat="server" AutoPostBack="true"
                                    class="form-control" Style="width: 100%;" Height="35" autocomplete="off"
                                    OnTextChanged="txtBoxID_TextChanged">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:GridView ID="gvPCBData" class="table table-bordered table-striped" runat="server"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">                                           
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Box ID" DataField="BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Pcb ID" DataField="PCB_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Work Order No" DataField="WORK_ORDER_NO" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                         <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Customer Code" DataField="CUSTOMER_PART_NUMBER" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnRollback" runat="server" UseSubmitBehavior="false"
                                            Text="RePack" class="btn btn-primary btn-block btn-flat" OnClientClick="Confirm()"
                                            OnClick="btnRollback_Click" />
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false"
                                            Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
