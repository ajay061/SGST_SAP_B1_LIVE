<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WIPExpiredPCB.aspx.cs" Inherits="SGST.INE.WIP.WIPExpiredPCB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
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
            <h1>ENABLE EXPIRED PCB</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">User Rights</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2" Style="width: 100%;" >
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Work Order No : </label>
                                <asp:TextBox ID="txtWorkOrderNo" runat="server" class="form-control" Style="width: 100%;">
                                </asp:TextBox>
                            </div>                           
                            <div class="col-xs-4">
                                <asp:Button ID="btnGetDetails" runat="server" Text="Show Data"
                                    class="btn btn-primary btn-block btn-flat" OnClick="btnGetDetails_Click" TabIndex="7" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnSave" runat="server" Text="Allowed"
                                    class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" TabIndex="7" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset"
                                    class="btn btn-primary btn-block btn-flat" TabIndex="7"
                                    OnClick="btnReset_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnDownloadData" runat="server" Text="Download" 
                                    class="btn btn-primary btn-block btn-flat" OnClick="btnDownloadData_Click" Visible="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <b>Machine ID : 
                        <asp:DropDownList ID="drpMachineID" runat="server" class="form-control select2"
                             AutoPostBack="true" OnSelectedIndexChanged="drpMachineID_SelectedIndexChanged"></asp:DropDownList>
                    </b>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-header">
                            </div>
                            <div class="box-body">
                                <asp:GridView ID="gvPCBCount" class="table table-striped table-bordered table-hover GridPager"
                                    runat="server"
                                    AutoGenerateColumns="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" AutoPostBack="true" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkViewRights" runat="server" AutoPostBack="false" Checked='<%#Convert.ToBoolean(Eval("VIEW_RIGHTS")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S.NO.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPCBBarcode" runat="server" Text='<%#Eval("PCB_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Work Order No" DataField="WORK_ORDER_NO" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Fg Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Machine" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Stage Code" DataField="STAGE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="PCB ID" DataField="PCB_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Printed On" DataField="PRINTED_ON" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Current Date" DataField="CurrentDate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Avaliable Hours" DataField="PACKING_TIME_HOURS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Hours Taken" DataField="TotalHours" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
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

