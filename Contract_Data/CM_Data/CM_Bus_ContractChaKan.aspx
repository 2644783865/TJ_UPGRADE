<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Bus_ContractChaKan.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Bus_ContractChaKan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

<asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">投标详细信息</div>
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' ><tr><td> 
         投标信息(带<span class="red">*</span>号的为必填项)</td></tr></table></div></div></div>
         <div class="box-wrapper">
            <div class="box-outer">
<table  width='100%'  cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
                 <td style="width: 82px" >投标类型</td><td><asp:Label ID="bp_BIDTYPE" runat="server" ></asp:Label> </td>
                 <td style="width: 82px">项目名称</td><td><asp:Label ID="bp_PRONAME" runat="server"></asp:Label></td>
                 
                <td colspan=2></td>
         </tr>
         <tr>
                <td style="width: 82px">设备名称</td><td><asp:Label ID="bp_DEVICENAME" runat="server"></asp:Label></td>    
                <td style="width: 82px">规格</td><td><asp:Label ID="bp_DVCNORM" runat="server"> </asp:Label></td>   
                <td style="width: 82px">数量</td><td><asp:Label ID="bp_NUM" runat="server"/></td>
         </tr>
         <tr>
               <td style="width: 82px">业主联系人<span class="red">*</span></td><td ><asp:Label ID="bp_CUSTMID" runat="server"/></td>
                <td style="width: 82px">业主联系方式<span class="red">*</span></td><td><asp:Label ID="bp_TCCGCLERK" type="text" runat="server"/></td>
               <td style="width: 82px">商务负责人<span class="red">*</span></td><td><asp:Label ID="bp_BSCGCLERK" type="text"  runat="server"/></td>
              
          </tr>     
 <tr>
    <td style="width: 82px">接受日期</td><td>
    <asp:Label ID="bp_ACPDATE" runat="server" type="text"/>
    </td>
    <td style="width: 82px">年度</td><td><asp:Label ID="bp_YEAR" runat="server" type="text"/>
    </td>
    <td style="width: 82px">投标/报价日期</td><td>
    <asp:Label ID="bp_BIDDATE" runat="server" type="text"/>
    </td>
</tr>
<tr>
    <td style="width: 82px">投标方式</td><td><asp:Label ID="bp_BIDSTYLE" runat="server"/>
    </td>
    <td style="width: 82px">发出人<span class="red">*</span></td><td><asp:Label ID="bp_ISSUER" runat="server"/></td>
    <td style="width: 82px">目前状态</td><td>
    <asp:Label ID="bp_STATUS" runat="server"></asp:Label></td>
</tr>   
<tr>
<td colspan='6'>备注：</td></tr>
<tr>
<td colspan='6'>
    <asp:Label ID="bp_NOTE" runat="server"></asp:Label></td>
</tr>        
            </table>
    <input type="button" id="btn_Back" value="返回" onclick="window.location.href='CM_Bus_Contract.aspx'" />       
    </div></div>
</asp:Content>