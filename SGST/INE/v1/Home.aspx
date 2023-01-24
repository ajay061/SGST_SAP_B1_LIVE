<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="SGST.INE.v1.Home" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Components/css/Home.css" rel="stylesheet" />
    <div class="content-wrapper" style="">
        <section class="content-header">
            <h1><small></small>
            </h1>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <div class="box-body">
                            <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                                <div class="carousel-inner">
                                    <div class="item active">
                                        <img src="../../images/Sliders/Slide1.jpg" alt="First slide">
                                        <div class="carousel-caption">
                                        </div>
                                    </div>
                                    <div class="item">
                                        <img src="../../images/Sliders/Slide2.jpg" alt="Second slide">
                                        <div class="carousel-caption">
                                            <%--          Second Slide--%>
                                        </div>
                                    </div>
                                    <div class="item">
                                        <img src="../../images/Sliders/Slide3.jpg" alt="Third slide">

                                        <div class="carousel-caption">
                                            <%-- Third Slide--%>
                                        </div>
                                    </div>
                                </div>
                                <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                                    <span class="fa fa-angle-left"></span>
                                </a>
                                <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                                    <span class="fa fa-angle-right"></span>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="lockscreen-wrapper">
                <div class="lockscreen-logo">
                    <div class="mobile_view">
                        <asp:Image ID="img" runat="server" ImageUrl="~/Images/LoginRotator.png" CssClass="img-responsive" />
                    </div>
                    <div class="control-sidebar-light">
                        <b>
                            <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                            <asp:Label ID="lblCompanyName1" runat="server"></asp:Label></b>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
