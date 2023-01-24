<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GBWTPrinting.aspx.cs" Inherits="SGST.INE.MOB.GBWTPrinting" %>

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
            <h1>CHECKING GB WEIGHT
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">CHECKING GB WEIGHT</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="ddlModel_Name" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>                            
                            <div class="form-group">
                                <label>Weight : </label>
                                <asp:Label ID="lblWT" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Tolrence(+) : </label>
                                <asp:Label ID="lblTP" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Tolrence(-)  : </label>
                                <asp:Label ID="lblTM" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Model Name :  </label>
                                <asp:Label ID="lblModelName" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Scan Here : </label>
                                <asp:TextBox ID="txtScanHere" runat="server" placeholder="Enter Barcode" autocomplete="off"
                                     class="form-control"
                                    Style="width: 100%;" MaxLength="100" OnTextChanged="txtScanHere_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" id="dvMessage" visible ="false">
                                <label>Please put weight on weighing machine and Press validate button : </label>
                                <br />
                                <br />
                                   <div class="col-xs-4">
                                        <asp:Button ID="btnValidate" OnClick="btnValidate_Click" runat="server" Text="Validate"
                                             class="btn btn-primary btn-block btn-flat" />
                                    </div>
                            </div>
                            <div class="form-group">
                                <label>Capture Weight : </label>
                                <asp:TextBox ID="txtCapWT" runat="server" Enabled="false" autocomplete="off" class="form-control"
                                    Style="width: 100%;">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Last Scanned : </label>
                                <asp:Label ID="lbllastscanned" runat="server" placeholder="Enter Last Scanned" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>


                            <div class="col-md-12" style="margin-top: 10%;">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
