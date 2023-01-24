<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="InvoiceLabel.aspx.cs" EnableEventValidation="false"
    Inherits="SGST.INE.FG.InvoiceLabel" %>

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
            <h1>Generate Invoice </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Select Details</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Code</label>
                                <asp:DropDownList ID="drpCustomerCode" runat="server" class="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpCustomerCode_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>OutBond Delivery No</label>
                                <asp:DropDownList ID="drpOutBondDeliveryNo" runat="server" class="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpOutBondDeliveryNo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Invoice No</label>
                                <asp:DropDownList ID="drpInvoiceNo" runat="server" class="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpInvoiceNo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>             
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Flight No</label>
                                <asp:TextBox ID="txtFlightNo" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Flight No" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Port Of Loading</label>
                                <asp:TextBox ID="txtPortOfLoading" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Port Of loading" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Place Of Receipt</label>
                                <asp:TextBox ID="txtPlaceOfReceipt" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Place Of Receipt" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Pre Carraged By</label>
                                <asp:TextBox ID="txtPreCarragedBy" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Pre Carraged By" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Port Of Discharged</label>
                                <asp:TextBox ID="txtPortOfDischarged" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Port Of Discharged" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Final Destination</label>
                                <asp:TextBox ID="txtFinalDestination" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Final Destination" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Gross Weight</label>
                                <asp:TextBox ID="txtGrossWeight" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Gross Weight" autocomplete="off" MaxLength="100" onkeypress="javascript:return isNumber(event)" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Net Weight</label>
                                <asp:TextBox ID="txtNetWeight" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Net Weight" autocomplete="off" onkeypress="javascript:return isNumber(event)" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Dimension Of Cargo</label>
                                <asp:TextBox ID="txtDimensionOfcargo" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Enter Dimension Of Cargo" autocomplete="off" MaxLength="100" onkeydown="return (event.keyCode!=13)"> </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-4">
                                <asp:Button ID="btnSave" runat="server" Text="Save"
                                    class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
