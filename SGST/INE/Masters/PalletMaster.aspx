<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PalletMaster.aspx.cs" Inherits="SGST.INE.Masters.PalletMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <h1>Masters       
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Pallet Printing</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6" >
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer</label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box -->
            <asp:HiddenField ID="hidUID" runat="server" />
            <asp:HiddenField ID="hidUpdate" runat="server" />
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">

                                <asp:GridView ID="gv_printingmst" runat="server"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                    Width="100%" class="table table-striped table-bordered table-hover GridPager"
                                    OnPageIndexChanging="gv_printingmst_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="PALLETCODE" DataField="PALLETCODE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
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
