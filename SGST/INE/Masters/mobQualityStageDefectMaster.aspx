<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="mobQualityStageDefectMaster.aspx.cs" Inherits="SGST.INE.Masters.mobQualityStageDefectMaster" %>

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
            <h1>Masters
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Defect Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">                           
                            <div class="form-group">
                                <label>Defect Code : </label>
                                <asp:TextBox ID="txtDefectCode" runat="server" class="form-control"
                                    placeholder="Enter Defect Code" MaxLength="50"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)" Style="width: 100%;">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Defect Desc : </label>
                                <asp:TextBox ID="txtDefect" runat="server" class="form-control"
                                    placeholder="Enter Defect" MaxLength="30"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)" Style="width: 100%;">                                  
                                </asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>Area : </label>
                                <asp:DropDownList ID="drpArea" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                    <asp:ListItem>WIP</asp:ListItem>
                                    <asp:ListItem>OQC</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Active : </label>
                                <asp:CheckBox ID="chkActive" runat="server" class="form-control" Style="width: 100%;"></asp:CheckBox>
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
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                </div>
                <div class="box-header with-border">
                    <b>Defect Code : 
                        <asp:DropDownList ID="drpMachineFilter" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpMachineFilter_SelectedIndexChanged"></asp:DropDownList>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvDefectMaster" runat="server" CssClass="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvDefectMaster_RowCommand"
                                    OnPageIndexChanging="gvDefectMaster_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="DM_NO" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
<%--                                        <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Machine Name" DataField="MACHINENAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />--%>
                                        <asp:BoundField HeaderText="Defect Code" DataField="DEFECT_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Defect Desc" DataField="FAILURE_TYPE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Status" DataField="ACTIVE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Created By" DataField="CREATED_BY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Created On" DataField="CREATED_ON" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("DM_NO")%>' Visible="true">
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
                <asp:HiddenField ID="hidUID" runat="server" />
            </div>
        </section>
    </div>
</asp:Content>
