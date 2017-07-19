<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.master"  CodeBehind="TM_Tech_Assign_OldAdd.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_Assign_OldAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">旧制号录入
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table width="100%"><tr><td>旧制号录入</td><td align="right"><asp:HyperLink ID="goback" CssClass="hand" runat="server" onclick="history.go(-1);">
                        <asp:Image ID="Imageback" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2" Height="16" Width="16"
                            align="absmiddle" runat="server" />返回上一页</asp:HyperLink> &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/TM_Data/TM_Tech_assign.aspx"><asp:Image ID="Image1" ImageUrl="~/Assets/icons/back.png" border="0" hspace="2" Height="16" Width="16"
                            align="absmiddle" runat="server" />返回技术任务管理列表</asp:LinkButton>&nbsp;&nbsp;</td></tr></table>
            </div>
        </div>
    </div>        
    <div class="box-wrapper">
        <div class="box-outer" style="width:99%; ">
            <table width="100%">
             <tr>
              <td align="right" style="width:10%">生产制号:</td>
              <td align="left"  style="width:40%"><asp:TextBox ID="txtTaskID" runat="server"></asp:TextBox>&nbsp;&nbsp;格式:MB/FER/003(O)或MB/FER/003(DQO)<span class="red">*</span></td>
               <td align="right" style="width:10%">项目代号:</td>
               <td align="left"  style="width:40%"><asp:ComboBox ID="proid" runat="server" AutoCompleteMode="SuggestAppend"
                            AutoPostBack="true" DropDownStyle="DropDownList" Height="15px" 
                            Width="90px">
                        </asp:ComboBox><span class="red">*</span></td>
             </tr>
             <tr>
             <td  align="right"  style="width:10%">工程名称:</td>  
              <td  align="left"  style="width:40%">
               <asp:TextBox ID="txtEngName" runat="server"></asp:TextBox><span class="red">*</span></td> 
              <td align="right"  style="width:10%">工程简称:</td>
               <td align="left"  style="width:40%"><asp:ComboBox ID="engid" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" DropDownStyle="DropDownList" Height="15px"  Width="90px">
                   </asp:ComboBox><span class="red">*</span></td>
             </tr>
             <tr><td colspan="4"><hr /></td></tr>
             <tr>
               <td colspan="4" align="center">
                   <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClientClick="return confirm('确认提交吗？\r\r提示:保存后无法删除，请仔细核对！！！');" OnClick="btnSubmit_OnClick" />
                   </td>
             </tr>
            </table>
        </div>
    </div>        
</asp:Content>
