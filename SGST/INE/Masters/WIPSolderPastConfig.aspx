<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WIPSolderPastConfig.aspx.cs" Inherits="SGST.INE.WIP.WIPSolderPastConfig" ClientIDMode="Static" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        $(document).ready(function () {
            $("#txtPROCESS_TIME").attr('maxlength', '5');
            $("#txtPROCESS_TIME_ENABLE").attr('maxlength', '5');
            $("#txtNEXT_PROCESS_TIME").attr('maxlength', '5');
            $("#txtNEXT_PROCESS_TIME_ENABLE").attr('maxlength', '5');
        });

    </script>
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
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function fixedlength(textboxID, keyEvent, maxlength) {
            //validation for digits upto 'maxlength' defined by caller function
            if (textboxID.value.length > maxlength) {
                textboxID.value = textboxID.value.substr(0, maxlength);
            }
            else if (textboxID.value.length < maxlength || textboxID.value.length == maxlength) {
                textboxID.value = textboxID.value.replace(/[^\d]+/g, '');
                return true;
            }
            else
                return false;
        }
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                    <h3 class="box-title"></h3>
                    <h3 class="box-title">Machine Config Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Machine Name<span style="color: red; font-size: medium"></span></label>
                                <asp:DropDownList ID="ddlMACHINENAME" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Process Time(in Minutes)<span style="color: red; font-size: medium"></span></label>
                                <span>
                                    <asp:TextBox ID="txtPROCESS_TIME" runat="server"
                                        class="form-control Minunts" placeholder="PROCESS TIME" action="#"
                                        autocomplete="off" onkeypress="javascript:return isNumber(event)"></asp:TextBox></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Process Time Enable<span style="color: red; font-size: medium"></span></label>
                                <span>
                                    <asp:DropDownList ID="ddlPROCESS_TIME_ENABLE" runat="server" class="form-control" action="#" autocomplete="off">
                                        <asp:ListItem Value="0" Text="--Select Process Time--"></asp:ListItem>
                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    </asp:DropDownList></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Previous Process Time(In Minutes)<span style="color: red; font-size: medium"></span></label>
                                <span>
                                    <asp:TextBox ID="txtNEXT_PROCESS_TIME" runat="server" class="form-control" placeholder="Previous Process Time" action="#" autocomplete="off" onkeypress="javascript:return isNumber(event)"></asp:TextBox></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Previous Process Time Enable<span style="color: red; font-size: medium"></span></label>
                                <span>
                                    <asp:DropDownList ID="ddlNEXT_PROCESS_TIME_ENABLE" runat="server" class="form-control" action="#" autocomplete="off">
                                        <asp:ListItem Value="0" Text="--Select Previous Process Time--"></asp:ListItem>
                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    </asp:DropDownList></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col-xs-6">
                            </div>
                            <div class="col-xs-6">
                                <div class="col-xs-3">
                                    <asp:Button ID="btnMachineConfig" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnMachineConfig_Click" />
                                </div>
                                <div class="col-xs-3">
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" class="btn btn-primary btn-block btn-flat" OnClick="btnupdate_Click" />
                                </div>
                                <div class="col-xs-3">
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
                                <asp:GridView ID="GV_MachineConfig" runat="server"
                                    AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    OnRowCommand="GV_MachineConfig_RowCommand" OnPageIndexChanging="GV_MachineConfig_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4"
                                        FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>                                       
                                        <asp:BoundField HeaderText="Machine ID" DataField="MACHINEID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Machine Name" DataField="MACHINENAME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Process Time(In Minutes)" DataField="PROCESS_TIME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300"></asp:BoundField>
                                        <asp:BoundField HeaderText="Process Time Enable" DataField="PROCESS_TIME_ENABLE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300"></asp:BoundField>
                                        <asp:BoundField HeaderText="Previous Process Time(In Minutes)" DataField="NEXT_PROCESS_TIME" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300"></asp:BoundField>
                                        <asp:BoundField HeaderText="Previous Process Time Enable" DataField="NEXT_PROCESS_TIME_ENABLE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server"
                                                    CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("MACHINEID")%>' Visible="true">
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

