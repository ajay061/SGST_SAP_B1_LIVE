<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShippingMaster.aspx.cs" Inherits="SGST.INE.Masters.ShippingMaster" %>

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
            <h1>MASTERS</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Address Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code :</label>
                                <asp:DropDownList ID="ddlFGItemCode" runat="server" class="form-control select2" AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddlFGItemCode_SelectedIndexChanged"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Model Code :</label>
                                <asp:TextBox ID="txtModel" runat="server" class="form-control" ReadOnly="true"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 1 :</label>
                                <asp:TextBox ID="txtAddress1" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 2 :</label>
                                <asp:TextBox ID="txtAdd2" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 3 :</label>
                                <asp:TextBox ID="txtAddress3" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 4 :</label>
                                <asp:TextBox ID="txtAdd4" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 5 :</label>
                                <asp:TextBox ID="txtAdd5" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 6 :</label>
                                <asp:TextBox ID="txtAdd6" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address 7 :</label>
                                <asp:TextBox ID="txtAdd7" runat="server" class="form-control" MaxLength="100"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="col-md-12">
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
                                <asp:GridView ID="gvShippingAddress" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvShippingAddress_RowCommand" OnPageIndexChanging="gvShippingAddress_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Model Name" DataField="MODEL_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 1" DataField="ADDRESS1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 2" DataField="ADDRESS2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 3" DataField="ADDRESS3" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 4" DataField="ADDRESS4" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 5" DataField="ADDRESS5" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 6" DataField="ADDRESS6" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Address 7" DataField="ADDRESS7" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />

                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("msID")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("msID")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                </asp:GridView>
                                <asp:HiddenField ID="hidUID" runat="server" />
                                <asp:HiddenField ID="hidUpdate" runat="server" />
                                <asp:HiddenField ID="hidPoOrderID" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
