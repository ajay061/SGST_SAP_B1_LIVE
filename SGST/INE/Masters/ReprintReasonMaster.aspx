<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReprintReasonMaster.aspx.cs" Inherits="SGST.INE.Masters.ReprintReasonMaster" %>
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
            <h1>MASTER</h1>
        </section>
        <section class="content">
         
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Reason Reprint Master</h3>
                </div>
                 <div class="box-body">
                    <div class="row ">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Reason of Reprint : </label>
                                <asp:TextBox ID="Reason_of_Reprint" runat="server" class="form-control"
                                    placeholder="Enter Reason of Reprint" autocomplete="off"
                                    MaxLength="100"></asp:TextBox>
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
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-xs-4 col-lg-3">
                                    <asp:Button ID="Button3" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
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
                                <asp:GridView ID="gvReprintReasonMst" runat="server" AutoGenerateColumns="false"
                                    PageSize="10" AllowPaging="true" OnRowCommand="GridView1_RowCommand"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="Reason_of_Reprint">
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Reason of Reprint" DataField="Reason_of_Reprint" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("Reprint_ID")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("Reprint_ID")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                 <asp:HiddenField ID="hidUID" runat="server" />
                                <asp:HiddenField ID="hidUpdate" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </section>
    </div>
</asp:Content>
 