<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Childlabelprinting.aspx.cs" Inherits="SGST.INE.RM.Childlabelprinting" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <!-- Control Sidebar -->
    <!-- Content Wrapper. Contains page content -->
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
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Label Printing       
            </h1>
         
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Kitting/Child Label Printing</h3>
                  
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Part Code : </label>
                                <asp:DropDownList ID="drpItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Quantity :  </label>
                                <asp:TextBox ID="txtQty" runat="server" class="form-control" readonly="true" 
                                    Style="width: 100%;"></asp:TextBox>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name</label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>
                            <!-- /.form-group -->
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Part Barcode : </label>
                                <asp:DropDownList ID="drpReelID" runat="server" class="form-control select2" AutoPostBack="true"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpReelID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <!-- /.form-group -->
                            <div class="form-group">
                                <label>Enter Quantity : </label>
                                <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="Enter Quantity" Style="width: 100%;" MaxLength="5" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false" Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                    <br />
                    <br />
                    <asp:HiddenField ID="hidRNO" runat="server" />
                    <asp:HiddenField ID="hidqty" runat="server" />
                    <asp:HiddenField ID="hidEXPDATE" runat="server" />
                    <!-- /.row -->
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
