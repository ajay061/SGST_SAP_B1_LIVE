<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPLabelReprinting.aspx.cs" Inherits="SGST.INE.WIP.WIPLabelReprinting" EnableEventValidation="false" %>

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
            <h1>LABEL REPRINTING</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">WIP Label Re-Printing</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Type</label>
                                <asp:DropDownList ID="ddltype" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                    <asp:ListItem>--Select Type--</asp:ListItem>
                                    <asp:ListItem>COMPONENT</asp:ListItem>
                                    <asp:ListItem>FG ASSEMBLY</asp:ListItem>
                                    <asp:ListItem>UNIT LABEL</asp:ListItem>
                                    <asp:ListItem>UNIT GB LABEL</asp:ListItem>                                   
                                    <asp:ListItem>PRIMARY BOX</asp:ListItem>
                                    <asp:ListItem>SECONDARY BOX</asp:ListItem>
                                     <asp:ListItem>STAND LABEL</asp:ListItem>
                                    <%--    <asp:ListItem>OTHERS</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Reason of Reprint : </label>
                                <asp:DropDownList ID="ddlReasonofReprint" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                             <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" placeholder="please Enter Remarks" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="100"  AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Part Code : </label>
                                <asp:DropDownList ID="drpItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name</label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
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
                     <br />
                    <b>Barcode : 
                        <asp:DropDownList ID="drpBarcode" runat="server" class="form-control select2"
                            Style="width: 100%;"
                            AutoPostBack="true" OnSelectedIndexChanged="drpBarcode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gv_LabelReprint" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                    DataKeyNames="PART_BARCODE" OnPageIndexChanging="gv_LabelReprint_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Barcode" DataField="PART_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Batch No" DataField="BATCHNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Qty" DataField="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Customer" DataField="CUSTOMER_PART_NUMBER" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Model" DataField="MODEL_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-6">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false"
                                        Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false" Text="Reset"
                                        class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
