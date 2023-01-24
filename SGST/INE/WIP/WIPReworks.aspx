<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" EnableEventValidation="false"
    CodeBehind="WIPReworks.aspx.cs" Inherits="SGST.INE.WIP.WIPReworks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
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
            <h1>LINE REWORK</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Type</label>
                                <asp:DropDownList ID="drpType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">IN</asp:ListItem>
                                    <asp:ListItem Value="2">OUT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Rework Station ID</label>
                                <asp:DropDownList ID="drpstation" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divPCB" runat="server">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan PCB</label>
                                <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan PCB ID" autocomplete="off" MaxLength="100" OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label>Failed PCB</label>
                                <asp:GridView ID="gvFailedPCB" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" PageSize="100">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ID" DataField="SR NO." Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Failed PCB" DataField="PART_BARCODE" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Defect" DataField="DEFECT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div id="divOut" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Select Defect</label>
                                    <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Observations</label>
                                    <asp:TextBox ID="txtobservation" runat="server" class="form-control" AutoPostBack="false" onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation" autocomplete="off" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Remarks</label>
                                    <asp:TextBox ID="txtRemarks" runat="server" class="form-control" AutoPostBack="false" onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation" autocomplete="off" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Action</label>
                                    <asp:DropDownList ID="drpAction" runat="server" class="form-control">
                                        <asp:ListItem Value="0">REPAIRED</asp:ListItem>
                                        <asp:ListItem Value="1">SCRAPED</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id ="dvJIOReportType" runat="server" visible ="false">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Repair Type</label>
                                    <asp:DropDownList ID="drpRepairType" runat="server" class="form-control select2" 
                                        AutoPostBack="true" OnSelectedIndexChanged="drpRepairType_SelectedIndexChanged">
                                        <asp:ListItem>Normal Repair</asp:ListItem>
                                        <asp:ListItem>OQC3 Repair</asp:ListItem>
                                        <asp:ListItem>PDI Repair</asp:ListItem>
                                         <asp:ListItem>PACKING REPAIR</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id ="dvJIOFGList" runat="server" visible ="false">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Moving Station</label>
                                    <asp:DropDownList ID="drpMovingStage" runat="server" class="form-control select2">
                                        <asp:ListItem>APT-1</asp:ListItem>
                                        <asp:ListItem>PACK-IN</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="box box-default">
                            <div class="box-header with-border">
                                <h3 class="box-title">Add multiple component used in pcb</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Component Location : </label>
                                                        <asp:TextBox ID="txtLocation" MaxLength="10" autocomplete="off" runat="server" class="form-control" Style="width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Component Part Code : </label>
                                                        <asp:TextBox ID="txtPartCode" MaxLength="50" autocomplete="off" runat="server" class="form-control" Style="width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Component Part Desc : </label>
                                                        <asp:TextBox ID="txtPartDesc" MaxLength="250" autocomplete="off" runat="server" class="form-control" Style="width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Action Taken : </label>
                                                        <asp:TextBox ID="txtActionTaken" MaxLength="50" autocomplete="off" runat="server" class="form-control" Style="width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label></label>
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary btn-block btn-flat" OnClick="btnAdd_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvRepairData" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                                            AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvRepairData_PageIndexChanging"
                                                            OnRowCommand="gvRepairData_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                                            <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                                            <EditRowStyle BackColor="#999999"></EditRowStyle>
                                                            <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SNo.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Com Location" DataField="LOCATION" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                                <asp:BoundField HeaderText="Com Description" DataField="DESCRIPTION" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                                <asp:BoundField HeaderText="Part Code" DataField="COMPARTCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                                <asp:BoundField HeaderText="ActionTaken" DataField="ACTIONTAKEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 col-xs-4">
                                <asp:Button ID="btnSave" Visible="false" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-lg-3 col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

