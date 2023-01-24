<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="mobQuality.aspx.cs"
    Inherits="SGST.INE.MOB.mobQuality" EnableEventValidation="false" %>

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
            <h1>Quality</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Quality
                    </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code :  </label>
                                <asp:DropDownList ID="drpFGItemCode" AutoPostBack="true"
                                    OnSelectedIndexChanged="lblFGItemCode_SelectedIndexChanged"
                                    runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Model Name : </label>
                                <asp:Label ID="lblModelName" Enabled="false" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Scan Machine ID/Work Station ID : </label>
                                <asp:TextBox ID="txtScanMachineID" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35"
                                    OnTextChanged="txtScanMachineID_TextChanged" autocomplete="off" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Machine Name/ Work Station Name :</label>
                                <asp:Label ID="lblMachineName" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                                <label>Model Name/ Process Name : </label>
                                <asp:Label ID="lblModelNo" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Select Defect : (in case of rejection)</label>
                                <asp:DropDownList ID="drpDefect" class="form-control select2" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                                <div class="form-group">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary btn-block btn-flat" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="gvMultiDefect" runat="server"
                                                    class="table table-striped table-bordered table-hover GridPager"
                                                    AutoGenerateColumns="false"
                                                    OnRowCommand="gvMultiDefect_RowCommand">
                                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Defect" DataField="DEFECT" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/deleteGrid.png' alt='Delete this record' />" runat="server" CausesValidation="False" ToolTip="Remove this record" CommandName="DeleteRecords"
                                                                    CommandArgument='<%#Eval("DEFECT")%>' Visible="true">
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
                            <div class="form-group">
                                <label>Rework Station ID : </label>
                                <asp:DropDownList ID="drpstation" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Scan Here : </label>
                                <asp:TextBox ID="txtScanHere" runat="server" placeholder="Enter Barcode" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="30" OnTextChanged="txtScanHere_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Last Scanned : </label>
                                <asp:Label ID="lbllastscanned" runat="server" placeholder="Enter Last Scanned" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="col-md-12" style="margin-top: 10%;">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary btn-block btn-flat" />
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
