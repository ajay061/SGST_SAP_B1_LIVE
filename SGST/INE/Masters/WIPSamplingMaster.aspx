<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPSamplingMaster.aspx.cs" Inherits="SGST.INE.Masters.WIPSamplingMaster" EnableEventValidation="false" %>

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
                    <h3 class="box-title">Sampling Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code :  </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Lot Quantity : </label>
                                <asp:TextBox ID="txtLotQty" placeholder="Enter Lot Quantity"
                                    runat="server" class="form-control" MaxLength="5"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>LT(Life Testing) Hours : </label>
                                <asp:TextBox ID="txtLTHours" placeholder="Enter LT Hours"
                                    runat="server" class="form-control" MaxLength="5"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>PDI Sampling Quantity : </label>
                                <asp:TextBox ID="txtPdiSamplingQty" placeholder="Enter PDI Sampling Quantity"
                                    runat="server" class="form-control" MaxLength="5"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Machine ID :  </label>
                                <asp:DropDownList ID="drpModuleType" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Sampling Quantity : </label>
                                <asp:TextBox ID="txtSamplingQty" placeholder="Enter Sampling Quantity"
                                    runat="server" class="form-control" MaxLength="5"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Xray Sampling Quantity : </label>
                                <asp:TextBox ID="txtXraySamplingQty" placeholder="Enter Xray Sampling Quantity"
                                    runat="server" class="form-control" MaxLength="5"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>


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
                <asp:HiddenField ID="hidUID" runat="server" />
                <asp:HiddenField ID="hidUpdate" runat="server" />
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <b>FG Item Code : 
                        <asp:DropDownList ID="drpFGItemCodeSearch" runat="server" class="form-control select2"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="drpFGItemCodeSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvSamplingMaster" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvSamplingMaster_RowCommand" OnPageIndexChanging="gvSamplingMaster_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="SM_ID" DataField="SM_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Site Code" DataField="SITECODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Module Type" DataField="TYPE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Lot Quantity" DataField="LOT_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Sampling Quantity" DataField="SAMPLING_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="LT hours" DataField="LT_HOURS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Xray Sampling Quantity" DataField="XRAYSAMPLINGQTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PDI Sampling Quantity" DataField="PDISAMPLINGQTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("SM_ID")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
