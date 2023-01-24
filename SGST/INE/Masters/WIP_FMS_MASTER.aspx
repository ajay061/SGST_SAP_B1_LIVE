<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WIP_FMS_MASTER.aspx.cs" Inherits="SGST.INE.WIP.WIP_FMS_MASTER" EnableEventValidation="false" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script>       
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= 65) && (keyEntry <= 90)) || ((keyEntry >= 97) && (keyEntry <= 122)) || (keyEntry == 46) || (keyEntry == 32) || keyEntry == 45)
                return true;
            else {
                //alert("Please Enter Only Character values.");
                return false;
            }
        }
        function onlyNumbersWithColon(e) {
            var charCode;
            if (e.keyCode > 0) {
                charCode = e.which || e.keyCode;
            }
            else if (typeof (e.charCode) != "undefined") {
                charCode = e.which || e.keyCode;
            }
            if (charCode == 58)
                return true
            if (charCode > 31 && (charCode < 48 || charCode > 57))
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
            <h1>Masters
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">FMS Master</h3>
                </div>
                <div class="box-body">
                    <div class="row ">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Line ID<span style="color: red; font-size: medium"></span></label>
                                <asp:DropDownList ID="ddlLineId" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlLineId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Machine Name<span style="color: red; font-size: medium"></span></label>
                                <asp:DropDownList ID="ddlMachineid" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>FMS IP<span style="color: red; font-size: medium"></span></label><span id="ipaddress" style="color: red;"></span>
                                <asp:TextBox ID="txtFMS_TOP_IP" runat="server" class="form-control" placeholder="FMS TOP IP" action="#" autocomplete="off" onkeypress="return isNumberKey(evt);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>FMS Port<span style="color: red; font-size: medium"></span></label>
                                <asp:TextBox ID="txt_FMSTOPPORT" runat="server" class="form-control" placeholder="FMS TOP PORT" action="#" autocomplete="off" onkeypress="return isNumberKey(event)" MaxLength="6"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>FMS Ip Enable<span style="color: red; font-size: medium"></span></label>
                                <asp:DropDownList ID="ddlFMSTOPIPENABLE" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true">
                                    <asp:ListItem Text="--Select ip enable--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Text="No" Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>FMS Location</label>
                                <span>
                                    <asp:DropDownList ID="ddlLocation" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true">
                                        <asp:ListItem Text="--Select location--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Top" Value="1">Top</asp:ListItem>
                                        <asp:ListItem Text="Bottom" Value="2">Bottom</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <div class="col-xs-8">
                            </div>
                            <div class="col-xs-12 col-lg-6">
                                <div class="col-xs-4 col-lg-3">
                                    <asp:Button ID="btnFMMASTER" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnFMMASTER_Click" />
                                </div>
                                <div class="col-xs-4 col-lg-3">
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" class="btn btn-primary btn-block btn-flat" OnClick="btnupdate_Click" />
                                </div>
                                <div class="col-xs-4 col-lg-3">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
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
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="GV_FMSMATER" runat="server" AutoGenerateColumns="false"
                                    PageSize="10" AllowPaging="true" OnRowCommand="GV_FMSMATER_RowCommand"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    OnPageIndexChanging="GV_FMSMATER_PageIndexChanging" DataKeyNames="FMS_IP">
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Line ID" DataField="LINEID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FMS IP" DataField="FMS_IP" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FMS Port" DataField="FMS_PORT" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FMS IP Enable" DataField="ENABLE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FMS Location" DataField="FMS_LOC" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Machine Name" DataField="MACHINENAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument="<%# Container.DataItemIndex %>" Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument="<%# Container.DataItemIndex %>" OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
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
