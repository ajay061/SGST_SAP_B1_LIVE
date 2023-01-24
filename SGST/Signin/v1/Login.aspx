<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SGST.Signin.v1.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <style>
        @media screen and (max-width: 767px) {

            .login-page .page-header > .container {
                padding-top: 0px !important;
                padding-bottom: 0px !important;
            }

            footer {
                display: none !important;
            }

            .login-page .form img {
                margin-left: 0 !important;
                width: 45% !important;
            }

            .card {
                margin-bottom: 7px !important;
                margin-top: 0px !important;
            }

            .card-login .form {
                min-height: auto !important;
                text-align: center;
            }

            .card-login .input-group {
                padding-bottom: 8px !important;
                margin: 16px 0 0 0 !important;
            }

            .card-login .card-body {
                margin-top: 27px !important;
            }

            .page-header {
                display: block !important;
                padding-top: 10px !important;
            }
        }
    </style>

    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png">
    <link rel="icon" type="image/png" href="../assets/img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title id="CaptionHere" runat="server"></title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <!--     Fonts and icons     -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link href="../../Components/css/material-kit.css" rel="stylesheet" />
    <link href="../../Components/css/GoogleFont.css" rel="stylesheet" />
</head>
<body class="login-page sidebar-collapse">
    <div class="page-header header-filter" style="background-image: url('../../Images/1.jpg'); background-size: cover; background-position: top center;">
        <div class="container">
            <div class="row">
                <div class="col-lg-4 col-md-6 ml-auto mr-auto">
                    <div class="card card-login">
                        <form class="form" runat="server">
                            <div class="card-header card-header-info text-center">
                                <h5 class="card-title">Sign in</h5>
                            </div>
                            <img src="../../Images/logo.png" style="margin-left: 2%" />
                            <div class="card-body">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="material-icons">face</i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtUID" runat="server" class="form-control" placeholder="User Name" autocomplete="off"></asp:TextBox>

                                </div>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="material-icons">lock_outline</i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtpassword" runat="server" class="form-control" placeholder="Password" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="material-icons">store</i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtSiteCode" runat="server" class="form-control" placeholder="Plant Code" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="material-icons">store</i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtLineCode" runat="server" class="form-control" placeholder="Line Code" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="footer text-center">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-info" OnClick="btnLogin_Click" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <footer class="footer">
            <div class="container">
                <div class="copyright float-right">
                    &copy;         
                        <script>
                            document.write(new Date().getFullYear())
                        </script>
                    , 
          <a href="#" target="_blank">BCI</a>, Ver :<asp:Label ID="lblVersion" runat="server"></asp:Label>
                </div>
            </div>
        </footer>
    </div>
</body>
</html>

