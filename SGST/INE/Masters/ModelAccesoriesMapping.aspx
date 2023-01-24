<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ModelAccesoriesMapping.aspx.cs" Inherits="SGST.INE.Masters.ModelAccesoriesMapping" %>

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
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
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
                    <h3 class="box-title">Model Accessories Mapping </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="ddlModel_Name" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Model Name :  </label>
                                <asp:Label ID="lblModelName" Enabled="false" runat="server"
                                    class="form-control" Style="width: 100%;" MaxLength="50">                                 
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" style="width: 100%; overflow: scroll">
                                <asp:GridView ID="gvModel" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    OnRowCancelingEdit="gvModel_RowCancelingEdit"
                                    OnRowEditing="gvModel_RowEditing"
                                    OnRowUpdating="gvModel_RowUpdating"
                                    AutoGenerateColumns="false">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" OnCheckedChanged="checkAll_CheckedChanged"
                                                    AutoPostBack="true" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkViewRights" runat="server" AutoPostBack="false"
                                                    Checked='<%#Convert.ToBoolean(Eval("Checked")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNo.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccessName" runat="server" Text='<%#Eval("ACCNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Digit">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtt1" runat="server" Text='<%#Eval("START_DIGIT") %>'
                                                    autocomplete="off" MaxLength="2" TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="StartDigit" runat="server" Text='<%#Eval("START_DIGIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Digit">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtt2" runat="server" Text='<%#Eval("END_DIGIT") %>'
                                                    autocomplete="off" MaxLength="2" TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="EndDigit" runat="server" Text='<%#Eval("END_DIGIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtt3" runat="server" Text='<%#Eval("KEY_VAL1") %>'
                                                    autocomplete="off" MaxLength="100"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Value" runat="server" Text='<%#Eval("KEY_VAL1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="IS DUPLICATE ALLOWED">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="drpDuplcate" runat="server" Text='<%#Eval("ISDUPLICATE") %>'
                                                    autocomplete="off" MaxLength="20">
                                                    <asp:ListItem>True</asp:ListItem>
                                                    <asp:ListItem>False</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblISDUPLICATE" runat="server" Text='<%#Eval("ISDUPLICATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnedit" CommandName="EDIT" runat="server" Text="EDIT" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnupdate" CommandName="Update" runat="server" Text="Update" />
                                                <asp:Button ID="btnCancel" CommandName="Cancel" runat="server" Text="Cancel" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                    class="btn btn-primary btn-block btn-flat" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary btn-block btn-flat" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
