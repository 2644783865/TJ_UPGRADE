<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="PM_Manut_Managent_DL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Manut_Managent_DL" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务分工表</asp:Content>
        
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
 <script src="../JS/PDMN.js" type="text/javascript"  charset="GB2312"></script>
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
<div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr>
                    <td align="left"> 带<span style="color:Red">*</span>为必填选项</td>
                    </tr>
                 </table>
             </div>
         </div>
     </div>
    <div class="box-wrapper">
        <div class="box-outer">
           <table width="100%" align="center" cellpadding="2" cellspacing="1" class="toptable grid" border="1">
                <tr>
                <td class="tdleft1">设备名称:</td>          
                <td class="tdright1">
                    <asp:Label ID="lblengname" runat="server" ></asp:Label>
                   
                    <%--<asp:TextBox ID="txtengname" runat="server" Enabled="false"></asp:TextBox>--%>
                </td>
                 <td class="tdleft1">任务号:</td>          
                    <td class="tdright1">
                        <asp:Label ID="lblID" runat="server" ></asp:Label>
                        <%--<asp:TextBox ID="txtmid" runat="server" Enabled="false"></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                <td class="tdleft1">设备类型:</td>          
                    <td class="tdright1">
                        <asp:Label ID="lbltype" runat="server" ></asp:Label>
                        <%--<asp:TextBox ID="txtmid" runat="server" Enabled="false"></asp:TextBox>--%>
                    </td>
                <td class="tdleft1">技术负责人:</td>     
                <td class="tdright1">
                    <asp:Label ID="lblfuzeren" runat="server"  ></asp:Label>
                    <%--<asp:TextBox ID="txttuhao" runat="server"></asp:TextBox>--%>
                </td>
               </tr>

                <tr>
                <td class="tdleft1">制作班组:</td>          
                    <td class="tdright1">
                        <asp:TextBox ID="txtbanzu" runat="server" ></asp:TextBox>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlbanzu" runat="server" onchange="setperson('ctl00_PrimaryContent_txtbanzu',this)" ></asp:DropDownList>
                       <font color="#ff0000">*</font>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请选择制作班组！"
                                    ControlToValidate="txtbanzu" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
               <td class="tdleft1">调度员:</td>          
                <td class="tdright1">
                   <asp:TextBox ID="txtdiaoduyuan" runat="server" ></asp:TextBox>&nbsp;&nbsp;
                   <asp:DropDownList ID="ddlduy" runat="server" onchange="setperson('ctl00_PrimaryContent_txtdiaoduyuan',this)" ></asp:DropDownList>
                 <font color="#ff0000">*</font>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请选择调度员！"
                                    ControlToValidate="txtdiaoduyuan" Display="Dynamic"></asp:RequiredFieldValidator>
                </td> 
                </tr>
                
                 <tr>
                    <td class="tdleft1">预设计划开始:</td>          
                    <td class="tdright1">
                        <input id="ttpstime" type="text" runat="server" class="easyui-datebox" editable="false" />
                        <font color="#ff0000">*</font>

                    </td>
                    <td class="tdleft1">预设计划完成:</td>          
                    <td class="tdright1">
                         <input id="ttpftime" type="text" runat="server" class="easyui-datebox" editable="false" />
                    <font color="#ff0000">*</font>   
                    </td>
                 </tr>
                 
                 <tr>   
                    <td class="tdleft1">防腐负责人:</td>     
                    <td class="tdright1">
                        <asp:TextBox ID="FFMan" runat="server" ></asp:TextBox>&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlffman" runat="server" onchange="setffperson('ctl00_PrimaryContent_FFMan',this)" ></asp:DropDownList>
                    </td>
                 </tr>
                 <tr>
                    <td class="tdleft1">备注:</td>     
                    <td class="tdright1" colspan="3">
                        <asp:TextBox ID="txtbeizhu" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                 </tr>
                <tr>   
                    <td colspan="4" align="center" >
                    <asp:Button ID="btnConfirm" runat="server" Text="保 存" onclick="btnConfirm_Click" 
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="  取 消  " CausesValidation="false" 
                            onclick="btnCancel_Click" />                                     
                    </td>
                 </tr>
                 
            </table>
         </div>
       </div>
       <asp:Label ID="ControlFinder" runat="server" Visible="false" ></asp:Label>
       </asp:Content> 