<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="CM_Bus_ContractDetail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Bus_ContractDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" charset="GB2312">

         function SelbuPersons()
    {
       var i=window.showModalDialog('CM_Bus_Contract_buperson.aspx','',"dialogHeight:400px;dialogWidth:660px;status:no;scroll:no;center:yes;toolbar=no;menubar=no"); 
       document.getElementById('<%=buperson.ClientID%>').value=i;
    }
    </script>
<asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">修改或新建投标信息</div>
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' ><tr><td> 
         投标信息(带<span class="red">*</span>号的为必填项)</td></tr></table></div></div></div>
         <div class="box-wrapper">
            <div class="box-outer">
            <table  width='100%'  cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
                 <td >投标类型</td><td>
                     <asp:DropDownList ID="ddl_lx" runat="server">
                     <asp:ListItem>公司</asp:ListItem>
                     <asp:ListItem>自揽</asp:ListItem>
                     <asp:ListItem>公司备件</asp:ListItem>
                     <asp:ListItem>自揽备件</asp:ListItem>
                     </asp:DropDownList></td>
                
                <td>项目名称</td><td><asp:TextBox ID="bp_PRONAME" runat="server"></asp:TextBox></td>
                 
                <td colspan=2></td>
         </tr>
         <tr>
                <td>设备名称</td><td><asp:TextBox ID="bp_DEVICENAME" runat="server"/></td>    
                <td>规格</td><td><asp:TextBox ID="bp_DVCNORM" runat="server"/> </td>   
                <td>数量（请填写整数值）</td><td><asp:TextBox ID="bp_NUM" runat="server"/><asp:CompareValidator ID="CompareValidator2" runat="server" 
        ControlToValidate="bp_NUM" ErrorMessage="*" Operator="DataTypeCheck"   Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                </td>
         </tr>
         <tr>
               <td>业主联系人<span class="red">*</span></td><td style="width: 93px"><asp:TextBox ID="bp_CUSTMID" runat="server"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"     ControlToValidate="bp_CUSTMID" ErrorMessage="*"></asp:RequiredFieldValidator>
               </td>
               <td>业主联系方式<span class="red">*</span></td><td><asp:TextBox ID="bp_CUSTMTeL" runat="server"/> </td>
              <%-- <td><input id="teperson" type="text" value="" readonly="readonly" runat="server" style="border-style:none"/>
               <asp:HyperLink ID="teSelect" runat="server"  CssClass="hand"  onClick="SeltePersons()">
               <asp:Image ID="AddImage" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                选择</asp:HyperLink>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="teperson" ErrorMessage="*"></asp:RequiredFieldValidator>
               </td>--%>
               <td>商务负责人<span class="red">*</span></td><td><input id="buperson" type="text" value="" readonly="readonly" runat="server" style="border-style:none"/>
               <asp:HyperLink ID="buSelect" runat="server" CssClass="hand"  onClick="SelbuPersons()">
               <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                选择</asp:HyperLink>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"   ControlToValidate="buperson" ErrorMessage="*"></asp:RequiredFieldValidator>
               </td>
               
          </tr>     
 <tr>
    <td>接受日期</td><td>
    <input id="bp_ACPDATE" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" />
    </td>
    <td>年度</td><td><input id="bp_YEAR" runat="server" type="text"/>
    </td>
    <td>投标/报价日期</td><td>
    <input id="bp_BIDDATE" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" />
    </td>
</tr>
<tr>
    <td>投标方式</td><td><asp:TextBox ID="bp_BIDSTYLE" runat="server"/>
    </td>
    <td>发出人<span class="red">*</span></td><td><asp:TextBox ID="bp_ISSUER" runat="server"/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="bp_ISSUER" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    <td>目前状态</td><td>
    <asp:DropDownList ID="bp_STATUS" runat="server" Width="100px">
    <asp:ListItem Value="0">正在跟踪</asp:ListItem>
    <asp:ListItem Value="1">中标</asp:ListItem>
    <asp:ListItem Value="2">项目未定</asp:ListItem>
    <asp:ListItem Value="3">未中标</asp:ListItem>
    <asp:ListItem Value="4">未参加投标</asp:ListItem>
    <asp:ListItem Value="5">正在投标</asp:ListItem>
    <asp:ListItem Value="6">转公司</asp:ListItem>
    <asp:ListItem Value="7">取消报价</asp:ListItem>
    </asp:DropDownList>
    </td>
</tr>   
<tr>
<td colspan='6'>备注</td></tr>
<tr>
<td colspan='6'>
    <asp:TextBox
        ID="bp_NOTE" runat="server" TextMode="MultiLine" Columns="80" Rows="8" ></asp:TextBox></td>
</tr>        
            </table>
            <asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label> <br />
                <asp:Button ID="btn_Submit" runat="server" Text="提交" 
                    onclick="btn_Submit_Click" /> &nbsp;
                <input type="button" id="btn_Back" value="返回" onclick="window.location.href='CM_Bus_Contract.aspx'" />
</div></div>
</asp:Content>