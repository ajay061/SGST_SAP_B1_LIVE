<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPmSerialGenerationLogic.aspx.cs" Inherits="SGST.INE.WIP.WIPmSerialGenerationLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            if (iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function Validate(event) {
            var regex = new RegExp("^[0-9-$]");
            var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
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
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">WIP Serial Logic Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Barcode Generated For : </label>
                                <asp:DropDownList ID="ddllBarcodeGenerater" runat="server" Style="width: 100%;"
                                    class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddllBarcodeGenerater_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Site Code : </label>
                                <asp:DropDownList ID="ddlPlant" runat="server" class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpfgitemCode" runat="server"
                                    class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpfgitemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Code : </label>
                                <asp:DropDownList ID="ddlCustomer" runat="server" Style="width: 100%;" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="box box-default">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Part Code : </label>
                                        <asp:TextBox ID="txtPartno" runat="server" Style="width: 100%;"
                                            onkeydown="return (event.keyCode!=13)"
                                            class="form-control" autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Part Description : </label>
                                        <asp:TextBox ID="txtPartDescription" runat="server" Style="width: 100%;"
                                            class="form-control" Height="35"
                                            autocomplete="off" onkeydown="return (event.keyCode!=13)">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <b>
                                            <asp:Label Text="FG Qty Per Box :" ID="lblQtyHeader" runat="server">  </asp:Label></b>
                                        <asp:TextBox ID="txtFGqtyperBox" runat="server" onkeypress="javascript:return isNumber(event)"
                                            onkeydown="return (event.keyCode!=13)"
                                            Style="width: 100%;" class="form-control" MaxLength="5"
                                            autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Revison : </label>
                                        <asp:TextBox ID="txtRevision" runat="server" Style="width: 100%;"
                                            class="form-control"
                                            onkeydown="return (event.keyCode!=13)" MaxLength="10"
                                            autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Start No : </label>
                                        <asp:TextBox ID="txtStartno" runat="server"
                                            class="form-control" onkeypress="javascript:return isNumber(event)"
                                            onkeydown="return (event.keyCode!=13)" MaxLength="7"
                                            Style="width: 100%;"
                                            autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Length : </label>
                                        <asp:TextBox ID="txtLength" runat="server"
                                            class="form-control" onkeypress="javascript:return isNumber(event)"
                                            onkeydown="return (event.keyCode!=13)" MaxLength="4"
                                            Style="width: 100%;"
                                            autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Reset Period : </label>
                                        <asp:DropDownList ID="ddlRestPeriod" runat="server" class="form-control select2" Style="width: 100%;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Prefix(If any) : </label>
                                        <asp:TextBox ID="txtPrefix" runat="server"
                                            class="form-control" Style="width: 100%;" AutoPostBack="true"
                                            autocomplete="off" MaxLength="20" OnTextChanged="txtPrefix_TextChanged">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Other Value(If any) : </label>
                                        <asp:TextBox ID="txtOtherValue" runat="server"
                                            class="form-control" Style="width: 100%;"
                                            autocomplete="off" MaxLength="10">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Page Label Count : </label>
                                        <asp:TextBox ID="txtpageLabelCount" runat="server"
                                            class="form-control" Style="width: 100%;"
                                            onkeypress="javascript:return isNumber(event)"
                                            onkeydown="return (event.keyCode!=13)" Text="1"
                                            autocomplete="off" MaxLength="2">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box box-header with-border">
                    <h3 class="box-title">Add Format Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Suffix : </label>
                                    <asp:DropDownList ID="ddlSuffix" runat="server" class="form-control select2" Style="width: 100%;"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSuffix_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Format : </label>
                                    <asp:DropDownList ID="ddlFormat" runat="server" class="form-control select2" Style="width: 100%;"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label></label>
                                <div class="form-group">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-primary btn-block btn-flat" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:GridView ID="gvFormatData" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                                    AutoGenerateColumns="false" AllowPaging="true"
                                                    OnRowCommand="gvFormatData_RowCommand">
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
                                                        <asp:BoundField HeaderText="ID" DataField="P_ID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                        <asp:BoundField HeaderText="FORMAT NO" DataField="FORMAT_NO" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                        <asp:BoundField HeaderText="FORMAT NAME" DataField="FORMAT_NAME" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                        <asp:BoundField HeaderText="FORMAT VALUE" DataField="FORMAT_VALUE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/deleteGrid.png' alt='Delete this record' />" runat="server" CausesValidation="False" ToolTip="Remove this record" CommandName="DeleteRecords"
                                                                    CommandArgument='<%#Eval("FORMAT_NO")%>' Visible="true">
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
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Designer Format(Please enter value in (1$2) Format) : </label>
                            <asp:TextBox ID="txtDesignerFormat" runat="server" onkeypress="return Validate(event);"
                                class="form-control" Style="width: 100%;"
                                autocomplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Prn File : </label>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-primary btn-block btn-flat" />
                                <br />
                                <asp:Button ID="btnReadPRN" runat="server" Text="Read PRN Files"
                                    class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnReadPRN_Click" />
                                <asp:TextBox ID="txtPRN" class="form-control" runat="server" Style="width: 100%;"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <asp:CheckBox ID="chkActive" runat="server" Checked="false" Text="  Active" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnReset" runat="server"
                                Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                        </div>
                        <div class="col-md-3">                          
                            <asp:Button ID="btnGetRunningSN" runat="server"
                                Text="Get Running SN" class="btn btn-primary btn-block btn-flat" OnClick="btnGetRunningSN_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <b>FG Item Code : 
                        <asp:DropDownList ID="drpFilterFGItemCode" runat="server" class="form-control select2"
                            Style="width: 100%;"
                            AutoPostBack="true" OnSelectedIndexChanged="drpFilterFGItemCode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </b>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvDetails" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="P_ID" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="BARCODE GENERATE" DataField="BARCODE_GENERATE_FOR" Visible="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="SITE CODE" DataField="SITE_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="CUSTOMER" DataField="CUSTOMER" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PART_NO" DataField="PART_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PART_DESC" DataField="PART_DESC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG_ITEM_CODE" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="REVISION" DataField="REVISION" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="FG_QTY_PER_BOX" DataField="FG_QTY_PER_BOX" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="START_NO" DataField="START_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="LENGTH" DataField="LENGTH" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="RESET_PERIOD" DataField="RESET_PERIOD" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PREFIX" DataField="PREFIX" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Other Value" DataField="OTHER_VALUE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="PRN_FILE" DataField="PRN_FILE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="DESIGNER_FORMAT" DataField="DESIGNER_FORMAT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Page Label Count" DataField="PAGE_LABEL_COUNT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" ItemStyle-Width="300" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("P_ID")%>' Visible="true">
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

            <asp:HiddenField ID="hidPID" runat="server" />
        </section>
    </div>
</asp:Content>
