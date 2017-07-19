<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PM_CPFYJSDOTHER.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CPFYJSDOTHER" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    其他运费明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

   <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
    </script>
    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                                <asp:Button runat="server" ID="btnSubmit" Text="保存" OnClick="btnSubmit_OnClick" Width="40px" OnClientClick="return CalWeightJe()"
                                    Height="30px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" ID="notice0" Text="提示：运输商，任务号，税率和含税金额为必填项" ForeColor="Red"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div>
        <table width="100%">
            <tr>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:Label ID="lbJS_BH" runat="server"></asp:Label>
                </th>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="lbJS_RQ" runat="server"></asp:Label>
                </th>
                <th style="width: 20%;">
                    &nbsp;&nbsp;&nbsp;制单人：&nbsp;&nbsp;&nbsp;<asp:Label ID="lbJS_ZDR" runat="server"></asp:Label>
                    <asp:Label ID="lbzdrid" runat="server" Visible="false"></asp:Label>
                </th>
                <th style="width: 40%;">
                    &nbsp;&nbsp;&nbsp;运输商：<asp:TextBox ID="tbJS_GYS" Width="180px" runat="server" OnTextChanged="TextBoxCompany_TextChanged" AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="Company_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="getotherCompany"
                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="tbJS_GYS"
                        UseContextKey="True">
                    </asp:AutoCompleteExtender>
                </th>
            </tr>
            <tr>
                <th colspan="4">
                    &nbsp;&nbsp;&nbsp;备注：
                    <asp:TextBox ID="txtJS_BZ" runat="server" TextMode="MultiLine" Rows="3" Width="80%"></asp:TextBox>
                </th>
            </tr>
             <tr>
                 <th align="left" colspan="4">
                        &nbsp;&nbsp;&nbsp;行数：
                        <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnadd" runat="server" Text="增加" OnClick="btnadd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="delete" runat="server" Text="删除行" OnClick="delete_Click"/>
                </th>
             </tr>
        </table>
   </div>
   <div>
        <table id="tab" class="nowrap cptable fullwidth" align="center">
            <asp:Repeater runat="server" ID="rptProNumCost">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle" style="background-color: #48D1CC; height: 30px">
                        <th>
                            <strong>序号</strong>
                        </th>
                        <th>
                            <strong>任务号</strong>
                        </th>
                        <th>
                            <strong>发货期</strong>
                        </th>
                        <th>
                            <strong>收货单位</strong>
                        </th>
                        <th>
                            <strong>数量</strong>
                        </th>
                        <th>
                            <strong>税率</strong>
                        </th>
                        <th>
                            <strong>金额（含税）</strong>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                        <td>
                            <asp:CheckBox runat="server" ID="chk"/>
                            <asp:Label runat="server" ID="XUHAO" Text='<%#Container.ItemIndex + 1%>'></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_RWH" Text='<%#Eval("JS_RWH")%>'></asp:TextBox>
                            <asp:AutoCompleteExtender ID="Company_AutoCompleteExtender2" runat="server" CompletionSetCount="10"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="getTaskID"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="JS_RWH"
                                UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_JHQ" Text='<%#Eval("JS_JHQ")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_SHDW" Width="200px" Text='<%#Eval("JS_SHDW")%>'  OnTextChanged="JS_SHDW_TextChanged" AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="Company_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="getotherCompany"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="JS_SHDW"
                                UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_BJSL" Text='<%#Eval("JS_BJSL")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_SHUIL" Text='<%#Eval("JS_SHUIL")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="JS_HSJE" Text='<%#Eval("JS_HSJE")%>'></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
            没有数据！</asp:Panel>
    </div>
</asp:Content>
