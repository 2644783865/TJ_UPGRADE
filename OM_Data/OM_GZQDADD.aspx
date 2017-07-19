<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_GZQDADD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZQDADD" Title="修改工资清单" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    修改工资清单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      
    </script>
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper1">
        <div class="box-outer" style="text-align: center">
            <table runat="server" width="80%">
                <tr>
                    <td style="width: 20%">
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                    </td>
                    <td style="width:20%">
                        <asp:DropDownList ID="ddlMoth" runat="server">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                    </td>
                    <td style="width: 20%">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_OnClick" />
                    </td>
                </tr>
            </table>
            <table border="1" cellspacing="0" cellpadding="0" runat="server">
                <tr style="width:100%">
                    <td style="width:100px">
                        工号
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="txtQD_Worknumber" BorderStyle="None" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                    <td style="width:100px">
                        姓名
                    </td>
                    <td style="width:200px">
                        <asp:Label ID="lbQD_ID" runat="server" BorderStyle="None" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtQD_Name" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td style="width:100px">
                        区分标识
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="txtQD_QuFen" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td style="width:100px">
                        部门
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="txtQD_BuMen" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        岗位
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_GangWei" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        基础工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_JCGZ" runat="server" BorderStyle="None" onchange="amountcheck(this)"></asp:TextBox>
                    </td>
                    <td>
                        工资工龄
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_GZGL" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        固定工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_GDGZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        绩效工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_JXGZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        奖励
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_JiangLi" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        病假工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_BingJiaGZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        加班工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_JiaBanGZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        中夜班费
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_ZYBF" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        年假工资
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_NianJiaGZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        应扣岗位
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_YKGW" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        调整/补发
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_TZBF" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        调整/补扣
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_TZBK" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        交通补贴
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_JTBT" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        防暑降温费
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_FSJW" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        采暖补贴
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_CLBT" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    
                    
                </tr>
                <tr>
                    <td>
                        其他
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_QTFY" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        应付合计
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_YFHJ" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        养老保险
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_YLBX" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        失业保险
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_SYBX" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                     
                </tr>
                
                
                <tr>
                    <td>
                        医疗保险
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_YiLiaoBX" runat="server" BorderStyle="None"></asp:TextBox>
                    </td> 
                    <td>
                        大额救助
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_DEJZ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        补保险
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_BuBX" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        公积金
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_GJJ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    
                    
                </tr>
                
                
                <tr>
                    <td>
                        补公积金
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_BGJJ" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        房租/水电费
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_ShuiDian" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        加班补发
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_BFJB" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        中夜班补发
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_BFZYB" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        扣项
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_KOUXIANG" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        个税
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_GeShui" runat="server" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td>
                        代扣小计
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_DaiKouXJ" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        实发金额
                    </td>
                    <td>
                        <asp:TextBox ID="txtQD_ShiFaJE" runat="server" BorderStyle="None" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">备注</td>
                    <td colspan="6" align="left">
                        <asp:TextBox ID="tbnote" runat="server" Height="42px" TextMode="MultiLine"
                                                    Width="500px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
