<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="FGCustomerMapping.aspx.cs" Inherits="SGST.INE.Masters.FGCustomerMapping" %>

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
                    <h3 class="box-title">FG Customer Mapping</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2"
                                    autocomplete="off"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Code : </label>
                                <asp:DropDownList ID="drpCustomerCode" runat="server" class="form-control select2"
                                    autocomplete="off"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Packing Completion Time(in Hours) : </label>
                                <asp:TextBox ID="txtPackignTimeHours" runat="server" Text="0"
                                    onkeypress="javascript:return isNumber(event)"
                                    onkeydown="return (event.keyCode!=13)"
                                    Style="width: 100%;" class="form-control" MaxLength="5"
                                    autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Packing Scanning Status: </label>
                                <asp:DropDownList ID="drpApproved" runat="server"
                                    Style="width: 100%;" class="form-control"
                                    autocomplete="off">
                                    <asp:ListItem>Allowed</asp:ListItem>
                                    <asp:ListItem>Not Allowed</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>No Of SFG mapped : </label>
                                <asp:TextBox ID="textNOPMapped" runat="server" Text="1"
                                    onkeypress="javascript:return isNumber(event)"
                                    onkeydown="return (event.keyCode!=13)" TextMode="Number"
                                    Style="width: 100%;" class="form-control"
                                    autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>OQC Sampling Qty : </label>
                                <asp:TextBox ID="txtPackingSamplingQty" runat="server" Text="0"
                                    onkeypress="javascript:return isNumber(event)"
                                    onkeydown="return (event.keyCode!=13)"
                                    Style="width: 100%;" class="form-control"
                                    autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Aging Time Period : </label>
                                <asp:TextBox ID="txtAgeingTimePeriod" runat="server" Text="0"
                                    onkeypress="javascript:return isNumber(event)"
                                    onkeydown="return (event.keyCode!=13)"
                                    Style="width: 100%;" class="form-control"
                                    autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                          <div class="col-md-6">
                               <div class="form-group">
                                   <label>Is 100% Aging  : </label>
                                    <asp:CheckBox ID="chkAgingfullAging" runat="server" Text=" Is 100% Aging"  AutoPostBack="true"
                                class="form-control"></asp:CheckBox>
                              </div>
                           </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
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
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="box-header with-border">
                        <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                        </b>
                        <br />
                        <b>FG Item Code : 
                        <asp:DropDownList ID="drpFGItemCodeSearch" runat="server" class="form-control select2" AutoPostBack="true"
                            OnSelectedIndexChanged="drpFGItemCodeSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                        </b>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvMappingData" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvMappingData_RowCommand"
                                    OnPageIndexChanging="gvMappingData_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="FG ID" DataField="FG_ID" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Custome Code" DataField="CUSTOMER_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Packing Completion Time" DataField="PACKING_TIME_HOURS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Packing Scanning Status" DataField="STATUS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="No Of SFG Mapped" DataField="NOP_SFG_MAPPING" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="OQC Sampling Qty" DataField="PACKING_SAMPLING_SIZE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Aging Time Period" DataField="AGEING_TIME_PERIOD" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("FG_ID")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
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
