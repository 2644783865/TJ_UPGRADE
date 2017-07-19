<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.master"  AutoEventWireup="true" CodeBehind="CM_Claim_Add.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Claim_Add" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../Controls/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    
    <script type="text/javascript" language="javascript">
      function ConfrimKK(){
      var a=confirm("确认扣款吗？");
      return a;
      }
      function ConfirmSubmit()
      {
        return confirm("确认提交吗？");
      }
    </script>
    <table>
    <tr>
    <td>合同索赔信息</td>           
    </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

<div class="RightContent">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                    <td>
                        <asp:Label ID="lblSPLB" runat="server" Text=""></asp:Label></td>
                    <td align="right">请选择索赔类别：</td>
                    <td>
                        <asp:DropDownList ID="dplSPLB" runat="server"  AutoPostBack="true"
                            onselectedindexchanged="dplSPLB_SelectedIndexChanged">
                        <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="业主向重机索赔" Value="1"></asp:ListItem>
                        <asp:ListItem Text="重机向业主索赔" Value="2"></asp:ListItem>
                        <asp:ListItem Text="重机向分包商索赔" Value="3"></asp:ListItem>
                        <asp:ListItem Text="分包商向重机索赔" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="middle">
                        <input id="Button1" type="button" value="关 闭" onclick="javascript:window.close();" Class="button-outer"  />
                         &nbsp;               
                    <%--<asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" ToolTip="返回到 合同索赔界面" NavigateUrl="~/Contract_Data/CM_Claim_Total.aspx">
                        返 回<asp:Image ID="Image3" Height="16" Width="16" runat="server" ImageUrl="~/Assets/icons/back.png" /></asp:HyperLink>&nbsp;
                    --%></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

     
        <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="palYZ" runat="server" Visible="false">
            <table width="100%" style="display:none">
            <tr>
            <td align="right">
                 <asp:Button ID="btnConfirmYZ" runat="server" Text="提 交"  CssClass="button-inner"
                    onclick="btnConfirmYZ_Click"  OnClientClick="javascript:return ConfirmSubmit();" />
            </td>
            </tr>
            </table>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>业主索赔信息</h2></td>
                </tr>
                <tr>
                <td>索赔编号</td>
                <td>
                    <asp:TextBox ID="txtSPBH" runat="server" Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;</td>
                        <td>索赔登记日期</td>
                        <td>
                    <asp:TextBox ID="txtSPDJRQ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>项目名称</td>
                <td>
<%--                    <asp:ComboBox ID="dplXMMC_YZ" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" DropDownStyle="DropDownList">
                    </asp:ComboBox>--%>
                    <asp:DropDownList ID="dplXMMC_YZ" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplXMMC_YZ_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>
                <td>主合同编号</td>
                <td>
                    <asp:DropDownList ID="dplZHTMC" runat="server"  AutoPostBack="true"
                        onselectedindexchanged="dplZHTMC_SelectedIndexChanged">
                    </asp:DropDownList>
                <asp:Label ID="txtZHTH" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td>接受部门</td>
                <td>
                    <asp:DropDownList ID="dplSLBM" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="商务组" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>制作方</td>
                <td>
                    <asp:DropDownList ID="dplZZF" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="机加组" Value="机加组"></asp:ListItem>
                    <asp:ListItem Text="嘉靖达宏基" Value="嘉靖达宏基"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td>技术负责人</td>
                <td>
                    <asp:TextBox ID="txtJSFZR" runat="server"></asp:TextBox></td>
                <td>质量负责人</td>
                <td>
                    <asp:TextBox ID="txtZZFZR" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>业主索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJE" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                        ID="DropDownList1" runat="server" Enabled="false">
                        <asp:ListItem Text="元:RMB" Value="0" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td>
                <td>
                    <asp:RadioButtonList ID="rblSFCL" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                    <asp:ListItem Text="待处理" Value="0"></asp:ListItem>
                    <asp:ListItem Text="不处理" Value="1"></asp:ListItem>
                    <asp:ListItem Text="业主责任" Value="2"></asp:ListItem>
                    <asp:ListItem Text="重机责任" Value="3"></asp:ListItem>
                    <asp:ListItem Text="共同承担" Value="4"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMS" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHF" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>反馈人</td>
                <td>
                    <asp:TextBox ID="txtFKR" runat="server"></asp:TextBox></td>
                <td>反馈日期</td>
                <td>
                    <asp:TextBox ID="txtFKRQ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>业主索赔金额</td>
                <td>
                    <asp:TextBox ID="txtYZSPJE1" runat="server" Enabled="false"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJE" runat="server" onblur="Arabia_to_Chinese(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="50%">
                <tr>
                <td><asp:RadioButtonList ID="rblSSKK" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKK" runat="server" OnClientClick="return ConfrimKK()" Text="扣 款" 
                        onclick="btnKK_Click" /></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司处理信息</h2></td>
                
                </tr>
                <tr>
                <td>内部处理意见</td>
                <td>
                    <asp:TextBox ID="txtNBCLYJ" runat="server"></asp:TextBox></td>
                <td>负责部门</td>
                <td>
                    <asp:CheckBoxList ID="cblFZBM" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    <asp:ListItem Text="技术部" Value="0"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="1"></asp:ListItem>
                    <asp:ListItem Text="质量部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="3"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                </tr>
                <tr>
                <td>原因描述</td>
                <td colspan="3">
                    <asp:CheckBoxList ID="cblWTMS" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%" >
                    </asp:CheckBoxList>
                </td>
                </tr>
                <tr>
                <td>备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtBZ" runat="server" TextMode="MultiLine" Height="40px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
           </asp:Panel>
           <uc1:UploadAttachments ID="at_YZ" runat="server" />
            <asp:Panel ID="palZJYZ" runat="server">
            <table width="100%" style="display:none">
            <tr>
            <td align="right">
                <asp:Button ID="btnConfirmZJYZ" runat="server" Text="提 交"  OnClientClick="javascript:return ConfirmSubmit();"
                    onclick="btnConfirmZJYZ_Click" />
            </td>
            </tr>
            </table>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>索赔信息</h2></td>
                </tr>
                <tr>
                <td>索赔编号</td>
                <td>
                    <asp:TextBox ID="txtSPDHZJYZ" runat="server" Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;</td>
                        <td>索赔登记日期</td>
                        <td>
                    <asp:TextBox ID="txtSPDJRQZJYZ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>项目名称</td>
                <td>
                    <asp:DropDownList ID="dplXMMCZJYZ" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplXMMCZJYZ_SelectedIndexChanged">
                    </asp:DropDownList> 
                </td>
                <td>主合同编号</td>
                <td>
                    <asp:DropDownList ID="dplZHTMCZJYZ" runat="server"  AutoPostBack="true"
                        onselectedindexchanged="dplZHTMCZJYZ_SelectedIndexChanged">
                    </asp:DropDownList>
                <asp:Label ID="lblZHTBHZJYZ" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                <td>提出部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMZJYZ" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="商务组" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                    <asp:ListItem Text="待处理" Value="0"></asp:ListItem>
                    <asp:ListItem Text="业主责任" Value="1"></asp:ListItem>
                    <asp:ListItem Text="重机责任" Value="2"></asp:ListItem>
                    <asp:ListItem Text="共同承担" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJYZ" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                        ID="DropDownList7" runat="server" Enabled="false">
                        <asp:ListItem Text="元:RMB" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSZJYZ" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>业主反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFZJYZ" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJZJYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>反馈人</td>
                <td>
                    <asp:TextBox ID="txtFKRZJYZ" runat="server"></asp:TextBox></td>
                <td>反馈日期</td>
                <td>
                    <asp:TextBox ID="txtFKRQZJYZ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJYZ1" runat="server" Enabled="false"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEZJYZ" runat="server" onblur="Arabia_to_Chinese(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="50%">
                <tr>
                <td><asp:RadioButtonList ID="rblSFKKZJYZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKKZJYZ" runat="server" Text="扣 款" OnClientClick="return ConfrimKK()" 
                        onclick="btnKKZJYZ_Click" /></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQZJYZ" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZZJYZ" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
               
            </asp:Panel> 
            <uc1:UploadAttachments ID="AT_ZJYZ" runat="server" />
            <asp:Panel ID="palZJFBS" runat="server">
            <table width="100%" style="display:none">
            <tr>
            <td align="right">
                <asp:Button ID="btnConfirmZJFBS" runat="server" Text="提 交" OnClick="btnConfirmZJFBS_Click" OnClientClick="javascript:return ConfirmSubmit();" /> 
            </td>
            </tr>
            </table>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>索赔信息</h2></td>
                </tr>
                <tr>
                <td>项目工程</td>
                <td>
                    <asp:DropDownList ID="dplXMMCZJFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplXMMCZJFBS_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dplGCMCZJFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplGCMCZJFBS_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>分包合同</td>
                <td>
                    <asp:DropDownList ID="dplZJFBHTMC" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplZJFBHTMC_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblZJFBHTH" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                <td>索赔单号</td>
                <td>
                    <asp:DropDownList ID="dplZHTMCZJFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplZHTMCZJFBS_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:TextBox ID="txtSPDHZJFBS" runat="server" Enabled="false"></asp:TextBox></td>
                <td>索赔登记日期</td>
                <td>
                    <asp:TextBox ID="txtSPDJRQZJFBS" runat="server" onClick="setday(this)" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                <td>提出部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMZJFBS" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="商务组" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td>
                <td>
                    <asp:RadioButtonList ID="rblCLJGZJFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                    <asp:ListItem Text="待处理" Value="0"></asp:ListItem>
                    <asp:ListItem Text="业主责任" Value="1"></asp:ListItem>
                    <asp:ListItem Text="重机责任" Value="2"></asp:ListItem>
                    <asp:ListItem Text="共同承担" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>              
                </tr>
                <tr>
                                
                <td>索赔金额</td>

                <td>
                    <asp:TextBox ID="txtSPJEZJFBS" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList10" runat="server" Enabled="false" >
                        <asp:ListItem Text="元:RMB" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>  
                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSZJFBS" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>分包商反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFZJFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJZJFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>登记人</td>
                <td>
                    <asp:TextBox ID="txtDJRZJFBS" runat="server"></asp:TextBox></td>
                <td>登记日期</td>
                <td>
                    <asp:TextBox ID="txtDJRQZJFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>索赔金额</td>
                <td>
                    <asp:TextBox ID="txtSPJEZJFBS1" runat="server"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEZJFBS" runat="server" onblur="Arabia_to_Chinese(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="50%">
                <tr>
                <td><asp:RadioButtonList ID="rblSFKKZJFBS" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKKZJFBS" runat="server" Text="扣 款" OnClientClick="return ConfrimKK()" 
                        onclick="btnKKZJFBS_Click" /></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQZJFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZZJFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
            
            </asp:Panel>
               <uc1:UploadAttachments ID="AT_ZJFBS" runat="server" />
            <asp:Panel ID="palFBS" runat="server">
            <table width="100%" style="display:none">
            <tr>
            <td align="right">
                <asp:Button ID="btnConfirmFBS" runat="server" Text="提 交" OnClientClick="javascript:return ConfirmSubmit();" OnClick="btnConfirmFBS_Click" />
            </td>
            </tr>
            </table>
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>分包商索赔信息</h2></td>
                </tr>
                <tr>
                <td>项目工程</td>
                <td>
                    <asp:DropDownList ID="dplXMMCFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplXMMCFBS_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="dplGCMCFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplGCMCFBS_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>分包合同</td>
                <td>
                    <asp:DropDownList ID="dplFBHTMC" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplFBHTMC_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="txtFBHTH" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                <td>索赔单号</td>
                <td>
                    <asp:DropDownList ID="dplZHTMCFBS" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="dplZHTMCFBS_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:TextBox ID="txtSPDHFBS" runat="server" Enabled="false"></asp:TextBox></td>
                <td>索赔登记日期</td>
                        <td>
                    <asp:TextBox ID="txtSPDJRQFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>接受部门</td>
                <td>
                    <asp:DropDownList ID="dplJSBMFBS" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="商务组" Value="1"></asp:ListItem>
                    <asp:ListItem Text="技术部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="3"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>制作方/责任班组</td>
                <td>
                    <asp:DropDownList ID="dplZZFFBS" runat="server">
                    <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="机加组" Value="机加组"></asp:ListItem>
                    <asp:ListItem Text="嘉靖达宏基" Value="嘉靖达宏基"></asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:DropDownList ID="dplZZBZFBS" runat="server">
                    <asp:ListItem Text="责任班组" Value="责任班组"></asp:ListItem>
                    <asp:ListItem Text="责任班组1" Value="责任班组1"></asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
                </tr>
                <tr>
                <td>技术负责人</td>
                <td>
                    <asp:TextBox ID="txtJSFZRFBS" runat="server"></asp:TextBox></td>
                <td>质量负责人</td>
                <td>
                    <asp:TextBox ID="txtZLFZRFBS" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>分包商索赔金额</td>
                <td>
                    <asp:TextBox ID="txtFBSSPJEFBS" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:DropDownList
                        ID="DropDownList5" runat="server" Enabled="false">
                        <asp:ListItem Text="元:RMB" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>处理结果</td>
                <td>
                    <asp:RadioButtonList ID="rblCLJGFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
                    <asp:ListItem Text="待处理" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="分包商责任" Value="1"></asp:ListItem>
                    <asp:ListItem Text="重机责任" Value="2"></asp:ListItem>
                    <asp:ListItem Text="共同承担" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                </tr>
                <tr>
                <td>索赔问题描述</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSPWTMSFBS" runat="server" TextMode="MultiLine" Width="60%" Height="70px"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司反馈信息</h2></td>
                </tr>
                <tr>
                <td>是否回复</td>
                <td>
                    <asp:RadioButtonList ID="rblSFHFFBS" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>回复意见</td>
                <td>
                    <asp:TextBox ID="txtHFYJFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td>反馈人</td>
                <td>
                    <asp:TextBox ID="txtFKRFBS" runat="server"></asp:TextBox></td>
                <td>反馈日期</td>
                <td>
                    <asp:TextBox ID="txtFKRQFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>最终扣款信息</h2></td>
                </tr>
                <tr>
                <td>分包商索赔金额</td>
                <td>
                    <asp:TextBox ID="txtFBSSPJEFBS1" runat="server"></asp:TextBox></td>
                <td>最终索赔金额</td>
                <td>
                    <asp:TextBox ID="txtZZSPJEFBS" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>是否扣款</td>
                <td>
                <table width="50%">
                <tr>
                <td><asp:RadioButtonList ID="rblSFKKFBS" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="0"></asp:ListItem>
                    <asp:ListItem Text="否" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                <td> <asp:Button ID="btnKKFBS" runat="server" Text="扣 款" onclick="btnKKFBS_Click" OnClientClick="return ConfrimKK()"  /></td>
                </tr>
                </table>
                </td>
                <td>扣款日期</td>
                <td>
                    <asp:TextBox ID="txtKKRQFBS" runat="server" onClick="setday(this)"></asp:TextBox></td>
                </tr>
                <tr>
                <td>扣款备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtKKBZFBS" runat="server" TextMode="MultiLine" Height="30px" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                <td colspan="4" align="center" valign="middle"><h2>公司处理信息</h2></td>
                
                </tr>
                <tr>
                <td>内部处理意见</td>
                <td>
                    <asp:TextBox ID="txtNBCLYJFBS" runat="server"></asp:TextBox></td>
                <td>负责部门</td>
                <td>
                    <asp:CheckBoxList ID="cblFZBMFBS" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                    <asp:ListItem Text="技术部" Value="0"></asp:ListItem>
                    <asp:ListItem Text="生产部" Value="1"></asp:ListItem>
                    <asp:ListItem Text="质量部" Value="2"></asp:ListItem>
                    <asp:ListItem Text="采购部" Value="3"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                </tr>
                <tr>
                <td>原因描述</td>
                <td colspan="3">
                    <asp:CheckBoxList ID="cblYYMSFBS" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%" >
                    </asp:CheckBoxList>
                </td>
                </tr>
                <tr>
                <td>备注</td>
                <td colspan="3">
                    <asp:TextBox ID="txtBZFBS" runat="server" TextMode="MultiLine" Height="40px" Width="60%"></asp:TextBox></td>
                </tr>
                </table>
                <br />
              
            </asp:Panel> 
            <uc1:UploadAttachments ID="AT_FBS" runat="server" />
        </div>
        </div>
           




</div>           
</asp:Content>
