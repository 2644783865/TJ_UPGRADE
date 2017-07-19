<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_HZY_infoDetail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.TM_HZY_infoDetail" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <div class="box-inner"><div class="box_right"><div class="box-title"><table style="width: 100%"><tr><td>
        任务单详细信息</td>
        <td>项目名称：<asp:TextBox ID="tbproject" runat="server"></asp:TextBox></td>
        <td>工程名称：<asp:TextBox ID="tbeng" runat="server"></asp:TextBox></td>
        </tr></table></div></div></div>
  <div class="box-wrapper">      
       <div class="box-outer">
           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
          <asp:Panel ID="UpdatePanel" runat="server">         
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <tr>
                        <td>
                            生产任务编号：</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TextBox3" MinimumPrefixLength="1" ServiceMethod="GetPJName" 
                                ServicePath="~/Ajax.asmx" UseContextKey="True" CompletionInterval="1"/>
                          <%--这里没有ToolkitScriptManager控件是因为母板页中有这个，子页也继承了--%>*<asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TextBox3" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            任务内容：</td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TextBox4" MinimumPrefixLength="1" ServiceMethod="GetENGName" 
                                ServicePath="~/Ajax.asmx" UseContextKey="True" CompletionInterval="1"/>
                          <%--没有根据项目名称来自动智能显示工程名称--%>*<asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="TextBox4" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            数量：</td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TextBox5" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            工程量：</td>
                        <td>
                           <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            交货日期：</td>
                        <td>
                            <input id="txt1" type="text" value="" readonly="readonly" runat="server" onclick="setday(this)"/>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txt1" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            图纸交付时间：</td>
                        <td>
                            <input id="txt2" type="text" value="" readonly="readonly" runat="server" onclick="setday(this)"/>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ControlToValidate="txt2" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            成本：</td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                        <td>
                            状态：</td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                             备注：</td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" ></asp:TextBox>
                                     </td>
                        <td>
                            项目管理部负责人：</td>
                        <td>
                         <asp:TextBox ID="TextBox8" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            经营策划部负责人：</td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                ControlToValidate="TextBox12" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            生产单位负责人：</td>
                        <td>
                         <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="TextBox13" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            收入核算单位：</td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox> 
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                ControlToValidate="TextBox14" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            项目管理部经办人：</td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                ControlToValidate="TextBox15" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            经营策划部经办人：</td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox> 
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ControlToValidate="TextBox14" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            生产单位经办人：</td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                ControlToValidate="TextBox15" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            技术负责人：</td>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox> 
                            *<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                ControlToValidate="TextBox14" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            </td>
                        <td>
                            
                            
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td colspan="4">
                            <asp:Button ID="Submit" runat="server" Text="提 交" onclick="Submit_Click" />&nbsp;&nbsp;
                            <asp:Button ID="Ruturn" runat="server" Text="返回" onclick="Ruturn_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            
             </asp:Panel>
             
             <asp:Panel ID="ViewPanel" runat="server">
               <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                  
                   <tr>
                       <td style="width: 20%">
                           生产任务编号：</td>
                       <td style="width: 30%">
                           <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                       <td style="width: 20%">
                           工程内容：</td>
                       <td style="width: 30%">
                           <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           数量：</td>
                       <td>
                          <asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
                       <td>
                           工程量：</td>
                       <td>
                          <asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           交货日期：</td>
                       <td>
                           <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                       <td>
                           图纸交付时间：</td>
                       <td>
                          <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           成本：</td>
                       <td>
                          <asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
                       <td>
                           状态：</td>
                       <td>
                          <asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           备注：</td>
                       <td>
                          <asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                       <td>
                           项目管理部负责人：</td>
                       <td>
                          <asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           经营策划部负责人：</td>
                       <td>
                            <asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                       <td>
                           生产单位负责人：</td>
                       <td>
                          <asp:Label ID="Label12" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           收入核算单位：</td>
                       <td>
                          <asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                       <td>
                           项目管理部经办人：</td>
                       <td>
                           <asp:Label ID="Label14" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           经营策划部经办人：</td>
                       <td>
                          <asp:Label ID="Label15" runat="server" Text=""></asp:Label></td>
                       <td>
                           生产单位经办人 ：</td>
                       <td>
                           <asp:Label ID="Label16" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr>
                       <td>
                           技术负责人：</td>
                       <td >
                          <asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
                       <td>
                           </td>
                       <td>
                           </td>
                   </tr>
                   
                    <tr style="text-align: right">
                       <td colspan="4">
                          
                           &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="返回" 
                               PostBackUrl ="~/CM_Data/TM_HZY_info.aspx" />
                           &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td>
                   </tr>
               </table>
               
               </asp:Panel>
          
      
   </div> <!--box-wrapper END -->  
</asp:Content>
