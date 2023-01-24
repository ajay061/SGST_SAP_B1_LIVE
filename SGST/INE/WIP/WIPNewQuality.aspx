<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WIPNewQuality.aspx.cs"
    Inherits="SGST.INE.WIP.WIPNewQuality" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>


    <!-- Content Wrapper. Contains page content -->
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
            <h1>FG Quality</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">FG Quality</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode"
                                    runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Box Barcode : </label>
                                <asp:TextBox ID="txtSecondaryBoxID" runat="server" class="form-control" placeholder="Scan Box Barcode"
                                    autocomplete="off" AutoPostBack="true" OnTextChanged="txtSecondaryBoxID_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvBoxData" class="table table-bordered table-striped" runat="server"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="BOX ID" DataField="PRIMARY_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <asp:Button ID="btnResetbarcode" runat="server" class="btn btn-primary btn-block btn-flat"
                            Text="Reset Barcode" OnClick="btnReset_Click" Width="100%" />
                    </div>
                    <div class="col-xs-3">
                        <asp:Button ID="btnCreateLot" runat="server" class="btn btn-primary btn-block btn-flat"
                            Text="Create Lot" OnClick="btnCreateLot_Click" Width="100%" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <hr />
                                <b>Do you want to scan child barcode ?</b>
                                <br />
                                <asp:RadioButton ID="rdPrimaryYes" GroupName="Group1"
                                    Value="Yes" runat="server" OnCheckedChanged="Group1_CheckedChanged"
                                    AutoPostBack="true" />
                                <b>Yes</b>
                                <br />
                                <asp:RadioButton ID="rdPrimaryNo" GroupName="Group1"
                                    Value="No" runat="server"  Checked="true" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                                <b>No</b>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="dvPrimaryScan" visible="false" runat="server">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Scan PCB Barcode : </label>
                                    <asp:TextBox ID="txtPcb" runat="server" class="form-control" placeholder="Scan PCB Barcode" autocomplete="off" AutoPostBack="true" OnTextChanged="txtPcb_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Defects (If any) : </label>
                                <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control" autocomplete="off"
                                    AutoPostBack="false" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Status : </label>
                                <asp:DropDownList ID="drpStatus"
                                    runat="server" class="form-control select2" AutoPostBack="true" Style="width: 100%;" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>OK</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>


                     <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <hr />
                                <b>Do you want to scan AccessoriesBarcode barcode ?</b>
                                <br />
                                <asp:RadioButton ID="rdAccBarcodeYes" GroupName="grpAccessories"
                                    Value="Yes" runat="server"  OnCheckedChanged="rdAccBarcode_CheckedChanged"
                                    AutoPostBack="true" />
                                <b>Yes</b>
                                <br />
                                <asp:RadioButton ID="rdAccBarcodeNo" GroupName="grpAccessories"
                                    Value="No" runat="server" Checked="true" OnCheckedChanged="rdAccBarcode_CheckedChanged" AutoPostBack="true" />
                                <b>No</b>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="dvAccessoriesBarcode" visible="false" runat="server">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Scan Accessories Barcode : </label>
                                    <asp:TextBox ID="txtAccessoriesbarcode" runat="server" class="form-control"  autocomplete="off" AutoPostBack="true"
                                        OnTextChanged="txtAccessoriesbarcode_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Defects (If any) : </label>
                                <asp:DropDownList ID="drpDefectAcc" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtAccRemarks" runat="server" class="form-control" autocomplete="off"
                                    AutoPostBack="false" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Status : </label>
                                <asp:DropDownList ID="drpAccResult"
                                    runat="server" class="form-control select2" AutoPostBack="true" Style="width: 100%;" OnSelectedIndexChanged="drpAccResult_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>OK</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvAccessoreisData" class="table table-bordered table-striped" runat="server"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="BOX ID" DataField="PRIMARY_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="Accessories Barcode" DataField="ACC_BARCODE" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="REMARKS" DataField="REMARKS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="DEFECT" DataField="DEFECT" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="STATUS" DataField="STATUS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>


                    </div>


                </div>
            </div>
            <asp:HiddenField ID="hidCode" runat="server" />
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scanned Data : </h3>
                    <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="col-xs-12">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvQualityData" class="table table-bordered table-striped" runat="server"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="PRIMARY BOX ID" DataField="PRIMARY_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="PCB ID" DataField="PCB_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="REMARKS" DataField="REMARKS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="DEFECT" DataField="DEFECT" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="STATUS" DataField="STATUS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="gvQualityParameterData" runat="server"
                                                class="table table-striped table-bordered table-hover GridPager"
                                                AutoGenerateColumns="false"
                                                OnRowCancelingEdit="gvMultiDefect_RowCancelingEdit"
                                                OnRowEditing="gvMultiDefect_RowEditing"
                                                OnRowUpdating="gvMultiDefect_RowUpdating">
                                                <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                                <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ITEMDESC" runat="server" Text='<%#Eval("ITEM_DESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Zone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ZONE" runat="server" Text='<%#Eval("ZONE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SPECIFICATION" runat="server" Text='<%#Eval("SPECIFICATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="METHODS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="METHODS" runat="server" Text='<%#Eval("METHOD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T1">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtt1" runat="server" Text='<%#Eval("T1") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("T1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T2">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtt2" runat="server" Text='<%#Bind("T2") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("T2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T3">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtT3" runat="server" Text='<%#Bind("T3") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("T3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T4">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtt4" runat="server" Text='<%#Bind("T4") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("T4") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="T5">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtT5" runat="server" Text='<%#Bind("T5") %>' autocomplete="off" MaxLength="100"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("T5") %>'></asp:Label>
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
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="form-group">
                            <label>Enter Final Remarks : </label>
                            <asp:TextBox ID="txtFinalRemarks" runat="server" class="form-control"
                                placeholder="Scan remarks" autocomplete="off" MaxLength="50">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="OK" OnClick="btnOk_Click" Width="100%" OnClientClick="Confirm()" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reject" OnClick="btnReject_Click" Width="100%" OnClientClick="Confirm()" />
                            </div>
                        </div>
                        <div class="form-group" runat="server">
                            <div class="col-xs-3">
                                <asp:Button ID="btnParialOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Partial Reject" OnClick="btnParialOK_Click" Width="100%" OnClientClick="Confirm()" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnRework" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Rework" OnClick="btnRework_Click" Width="100%" OnClientClick="Confirm()" />
                            </div>
                        </div>

                        <div class="col-xs-3">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnResetData_Click" Width="100%" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
