<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PDIQuality.aspx.cs"
    Inherits="SGST.INE.WIP.PDIQuality" %>
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
            if (confirm("Do you want to continue?")) {
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
            <h1>PDI Quality</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">                            
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode"
                                    runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtReelID" runat="server" class="form-control"
                                    placeholder="Scan Barcode" autocomplete="off" OnTextChanged="txtReelID_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Defect : </label>
                                <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2" Style="width: 100%;">
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
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
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
                                <label>Enter Observation : </label>
                                <asp:TextBox ID="txtObservation" runat="server" class="form-control"
                                    placeholder="Scan Barcode" autocomplete="off"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat" Text="OK" OnClick="btnOK_Click"
                                    OnClientClick="Confirm()" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat" Text="Reject" OnClick="btnReject_Click" OnClientClick="Confirm()" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
