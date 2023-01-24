<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LocationMaster.aspx.cs" Inherits="SGST.INE.Masters.LocationMaster" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
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
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Location Code : </label>
                                <asp:TextBox ID="txtLocationCode" runat="server" AutoPostBack="true"
                                    class="form-control" placeholder="Enter Location Code"
                                    autocomplete="off" OnTextChanged="txtLocationCode_TextChanged"
                                    MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Location Type : </label>
                                <asp:DropDownList ID="drplocatioType" runat="server" class="form-control select2">
                                    <asp:ListItem>--Select Location Type--</asp:ListItem>
                                    <asp:ListItem>UNDERQUALITY</asp:ListItem>
                                    <asp:ListItem>OK</asp:ListItem>
                                    <asp:ListItem>REJECTED</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer : </label>
                                <asp:DropDownList ID="ddlPrinter" runat="server" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>WH Code : </label>
                                <asp:TextBox ID="txtWHLocation" runat="server" class="form-control"
                                    placeholder="Enter WH Location" autocomplete="off"
                                    MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Location Area : </label>
                                <asp:DropDownList ID="drpArea" runat="server" Enabled="false" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Part Code : </label>
                                <asp:DropDownList ID="drpPartCode" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Description : </label>
                                <asp:TextBox ID="txtDescription" runat="server" class="form-control"
                                    placeholder="Enter Description" autocomplete="off"
                                    Rows="2" MaxLength="40"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                </div>

                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Select File</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-primary btn-block btn-flat" />
                                    <br />
                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload Details" class="btn btn-primary btn-block btn-flat" />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box -->
            <asp:HiddenField ID="hidUID" runat="server" />
            <asp:HiddenField ID="hidUpdate" runat="server" />
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                    <br />
                    <br />
                    <div class="col-md-4">
                        <b>From :  
                            <asp:DropDownList ID="drpLocationColumnType" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpLocationColumnType_SelectedIndexChanged"></asp:DropDownList>
                        </b>
                        <b>Value : 
                        <asp:DropDownList ID="drpLocationFilter" runat="server" class="form-control select2"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="drpLocationFilter_SelectedIndexChanged">
                        </asp:DropDownList>
                        </b>
                        <b>Location Code =:
                        <asp:TextBox ID="txtLocationFilter" name="Search" runat="server" class="form-control"
                            OnTextChanged="txtLocationFilter_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </b>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvLocationMst" runat="server" 
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="50"
                                    OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Location Code" DataField="LOCATIONCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Location Area" DataField="LOCATIONAREA" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Location Type" DataField="LOCATIONTYPE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Description" DataField="DESCRIPTION" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="WH Location" DataField="WH_LOC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("LOCATIONCODE")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("LOCATIONCODE")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
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
            </div>
        </section>
    </div>
</asp:Content>
