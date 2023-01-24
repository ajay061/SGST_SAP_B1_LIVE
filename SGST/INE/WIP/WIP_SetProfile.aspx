<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WIP_SetProfile.aspx.cs"
    Inherits="SGST.INE.WIP.WIP_SetProfile" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <h1 id="head1" runat="server">CONFIGURE SMT MC SCANNERS
            </h1>
        </section>
        <asp:HiddenField ID="hidMPID" Value="" runat="server" />
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Set MC Program</h3>
                </div>
                <div class="box-body">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="LineID">Line ID</label>
                            <div>
                                <asp:DropDownList ID="ddlLineId" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlLineId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="ItemCode">FG Item Code</label>
                            <div>
                                <asp:DropDownList ID="ddlFgItemCode" runat="server" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="ItemCode">Machine Type</label>                        <div>
                            <asp:DropDownList ID="drpMachineType" runat="server" class="form-control select2" Style="width: 100%;">
                            </asp:DropDownList>
                        </div>
                    </div>                   
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <div class="col-xs-6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <div class="col-xs-6">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                    <hr />
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
                                <asp:GridView ID="GV_SetProfile" runat="server"
                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" AllowPaging="true"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    OnRowCommand="GV_SetProfile_RowCommand" DataKeyNames="FG_ITEM_CODE" PageSize="10"
                                    OnPageIndexChanging="GV_SetProfile_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="MP ID" DataField="MP_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Line ID" DataField="LINEID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Machine Type" DataField="MACHINE_TYPE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecord"
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
