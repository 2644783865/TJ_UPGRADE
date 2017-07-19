<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_History_price_analysis .aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_History_price_analysis"
    Title="历史价格分析" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    历史价格分析
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="samples.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
     <style type="text/css"> 
     .autocomplete_completionListElement 
     {  
     	margin : 0px; 
     	background-color : #1C86EE; 
     	color : windowtext; 
     	cursor : 'default'; 
     	text-align : left; 
     	list-style:none; 
     	padding:0px;
        border: solid 1px gray; 
        width:400px!important;   
     }
     .autocomplete_listItem 
     {   
     	border-style : solid; 
     	border :#FFEFDB; 
     	border-width : 1px;  
     	background-color : #EEDC82; 
     	color : windowtext;  
     } 
     .autocomplete_highlightedListItem 
     { 
     	background-color: #1C86EE; 
     	color: black; 
     	padding: 1px; 
     } 
  </style> 
    <%--<asp:Label ID="ControlFinder" runat="server"  Visible="false"></asp:Label>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        价格数据：
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer"> 
                                <table width="100%" align="center" class="nowrap cptable fullwidth"  border="1">
                                    <tr align="center" style="background-color:#B9D3EE">
                                        <td>
                                            <strong>材料信息</strong>
                                        </td>
                                        <td>
                                            <strong>材料ID</strong>
                                        </td>
                                        <td>
                                            <strong>名称</strong>
                                        </td>
                                        <td>
                                            <strong>规格</strong>
                                        </td>
                                        <td>
                                            <strong>材质</strong>
                                        </td>                                       
                                        <td>
                                            <strong>国标</strong>
                                        </td>
                                        <td>
                                            <strong>数量单位</strong>
                                        </td>
                                    </tr>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <asp:TextBox ID="tb_marinfo" runat="server" Text="" ondblclick="javascript:this.select();"  OnTextChanged="tb_marinfo_textchange" AutoPostBack="true"></asp:TextBox>
                                            <%--<asp:LinkButton ID="lkb_confirm" runat="server" OnClick="lkb_confirm_Click" PostBackUrl="">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/icon-fuction/139.gif" AlternateText="确定" /></asp:LinkButton>--%>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_marinfo"
                                                ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                                                ServiceMethod="GetCompletemar" CompletionInterval="10" FirstRowSelected="true">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="MARID" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MARNAME" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MARNORM" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MARTERIAL" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="GUOBIAO" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUNIT" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center">                                    
                                    <tr align="center">
                                        <td>
                                            &nbsp;&nbsp;时间区间:&nbsp;<asp:TextBox ID="Tb_time1" runat="server" Text=""></asp:TextBox>&nbsp;至&nbsp;
                                            <asp:TextBox ID="Tb_time2" runat="server" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Tb_time1"
                                                Format="yyyy-MM-dd">
                                            </cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Tb_time2"
                                                Format="yyyy-MM-dd">
                                            </cc1:CalendarExtender>
                                            <asp:LinkButton ID="LKB_shenhe2" runat="server" OnClick="Lkb_Click" PostBackUrl="">
                                                <asp:Image ID="Image_shenhe2" runat="server" ImageUrl="~/Assets/icon-fuction/02.gif"
                                                    AlternateText="搜索" /></asp:LinkButton>
                                            <%--<asp:Button ID="btn_search" runat="server" Text="搜索" OnClick="Lkb_Click"/>--%>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drop_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_type_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="折线图"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="波动图"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="柱状图"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>                                                                       
                            <table width="100%" align="center">
                                <tr align="center">
                                    <td  align="center">
                                        <asp:Chart ID="Chart1" runat="server" Height="296px" Width="768px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                                            Palette="BrightPastel" ImageType="Png" BorderDashStyle="Solid" BackSecondaryColor="White"
                                            BackGradientStyle="TopBottom" BorderWidth="2" BackColor="#D3DFF0" BorderColor="26, 59, 105">
                                            <Titles>
                                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Text="价格波动"
                                                    ForeColor="26, 59, 105">
                                                </asp:Title>
                                            </Titles>
                                            <Legends>
                                                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                    <CustomItems>
                                                        <asp:LegendItem Name="最小值" Color="252, 180, 65" MarkerStyle="Cross" ImageStyle="Marker"
                                                            MarkerSize="10">
                                                        </asp:LegendItem>
                                                        <asp:LegendItem Name="最大值" Color="224, 64, 10" MarkerStyle="Triangle" ImageStyle="Marker"
                                                            MarkerSize="10">
                                                        </asp:LegendItem>
                                                    </CustomItems>
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                                <asp:Series Name="价格数据" BorderColor="180, 26, 59, 105" ChartType="Line">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                    BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                                                    BackGradientStyle="TopBottom">
                                                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                                        WallWidth="0" IsClustered="False"></Area3DStyle>
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64" ArrowStyle="SharpTriangle">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </td>
                                    
                                </tr>
                            </table>                        
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
