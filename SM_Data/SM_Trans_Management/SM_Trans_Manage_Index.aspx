<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="SM_Trans_Manage_Index.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Trans_Management.SM_Trans_Manage_Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
发运管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <link href="../../Contract_Data/Button_Style/style.css" rel="stylesheet" type="text/css" />
 <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
       </div>
    </div>
    </div>
    <div class="box-wrapper">
    <div class="box-outer">
        <table style="width: 100%; border:solid 1px #d1e2f2;">
           <tr  style="height:60px">
           <td colspan="4" style="font-size:large" align="center">请选择发运类型</td>
           </tr>
            <tr valign="middle" >
            <td width="20%">&nbsp;</td>
                <td width="30%" style="height:60px"  >
                   <asp:HyperLink ID="Hpl_CNJG" runat="server" 
                        NavigateUrl="SM_Trans_CNJG.aspx" Target="_blank" 
                         Font-Underline="True" Font-Bold="True" Font-Size="Small">厂内集港</asp:HyperLink>
                    &nbsp;</td>                    
                <td width="30%" valign="middle" align="left">
                    <asp:HyperLink ID="Hpl_GNFY" runat="server" 
                        NavigateUrl="SM_Trans_GNFY.aspx" Target="_blank" 
                         Font-Underline="True" Font-Bold="True"  Font-Size="Small">国内发运</asp:HyperLink>
                    &nbsp;</td>
                    <td width="20%">&nbsp;</td>
            </tr>
            <tr>
            <td >&nbsp;</td>
                <td style="height:60px" >
                    <asp:HyperLink ID="Hpl_KHZT" runat="server" 
                        NavigateUrl="SM_Trans_KHZT.aspx" Target="_blank" 
                         Font-Underline="True" Font-Bold="True"  Font-Size="Small">客户自提</asp:HyperLink>
                    &nbsp;</td>
                     
                <td align="left">
                    <asp:HyperLink ID="Hpl_KY" runat="server"
                     NavigateUrl="SM_Trans_KY.aspx"  Target="_blank" 
                     Font-Underline="True" Font-Bold="True" Font-Size="Small">空运</asp:HyperLink></td>
                     <td >&nbsp;</td>
            </tr>
            <tr>
            <td >&nbsp;</td>
                <td style="height:60px">
                    <asp:HyperLink ID="Hpl_LDHY" runat="server" 
                        NavigateUrl="SM_Trans_LDHY.aspx" Target="_blank" 
                         Font-Underline="True" Font-Bold="True"  Font-Size="Small">零担发运</asp:HyperLink>
                    &nbsp;</td>
                   
                <td align="left">
                    <asp:HyperLink ID="Hpl_JZXFY" runat="server"                    
                     NavigateUrl="SM_Trans_JZXFY.aspx" Target="_blank" 
                     Font-Underline="True" Font-Bold="True" Font-Size="Small">集装箱发运</asp:HyperLink></td>
                     <td >&nbsp;</td>
            </tr>
             <tr>
             <td >&nbsp;</td>
                <td style="height:60px" >
                     <asp:HyperLink ID="Hpl_JZXMX" runat="server" 
                        NavigateUrl="SM_Trans_JZXFYMX.aspx" Target="_blank" 
                         Font-Underline="True" Font-Bold="True"  Font-Size="Small">集装箱发运明细</asp:HyperLink>
                    &nbsp;</td>
                   
                <td align="left">
                    
                   </td> 
                   <td >&nbsp;</td>
            </tr>
            
        </table>
    
    </div>
    </div>


</asp:Content>
